using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshop.Business.Filter;
using Eshop.Business.StaticTools;
using Eshop.Business.Users;
using Eshop.DomainClass.Articles;
using Eshop.Services.Context;
using Eshop.Utilitiy.Convertor;
using Eshop.ViewModel.Article;

namespace Eshop.Web.Areas.Admin.Controllers
{

    [EshopPermission("ManageArticles")]
    public class ManageArticlesController : Controller
    {

        #region Ctor

        private EshopUOW db;
        public ManageArticlesController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region Specific Methods

        private void AddImageToServer(HttpPostedFileBase image, string imageName, string orginalPath, int? width, int? height, string thumbPath = null, string deleteImageName = null)
        {
            if (image != null)
            {
                // remove image
                if (!string.IsNullOrEmpty(deleteImageName) )
                {
                    if (System.IO.File.Exists(orginalPath + deleteImageName))
                    {
                        System.IO.File.Delete(orginalPath + deleteImageName);
                    }

                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        if (System.IO.File.Exists(thumbPath + deleteImageName))
                        {
                            System.IO.File.Delete(thumbPath + deleteImageName);
                        }
                    }
                }

                // add image
                image.SaveAs(orginalPath + imageName);
                if (!string.IsNullOrEmpty(thumbPath))
                {
                    image.SaveAs(thumbPath + imageName);
                    ImageResizer img = new ImageResizer();
                    if (width != null && height != null)
                    {
                        img.ResizeImageFromFile((thumbPath + imageName), height.Value, width.Value, false, false);
                    }
                    else
                    {
                        img.ResizeImageFromFile((thumbPath + imageName), 200, 200, false, false);
                    }

                }
            }
        }

        #endregion

        #region Article Categories

        #region Categories List

        [EshopPermission("ManageArticleCategory")]
        public ActionResult ArticleCategories(int? id)
        {
            if (id != null) ViewBag.parentId = id.Value;
            var mainCategories = db.ArticleRepository.GetArticleCategories(id);
            return View(mainCategories);
        }


        #endregion

        #region Create Categery

        [EshopPermission("ManageArticleCategory")]
        public ActionResult CreateCategory(int? id)
        {
            ViewBag.UrlError = false;
            if (id != null) ViewBag.parentId = id.Value;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(ArticleCategory category)
        {
            if (ModelState.IsValid)
            {
                if (!db.ArticleRepository.IsExistUrlTitleInArticles(category.NameInUrl))
                {
                    db.ArticleRepository.InsertCategory(category);
                    db.Save();
                    return RedirectToAction("ArticleCategories", "ManageArticles", new { id = category.ParentID });
                }
                else
                {
                    ViewBag.UrlError = true;
                }

            }

            return View(category);
        }

        #endregion

        #region Edit Article Categories

        [EshopPermission("ManageArticleCategory")]
        public ActionResult EditCategory(int id)
        {
            ViewBag.UrlError = false;
            var category = db.ArticleRepository.GetCategoryById(id);
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(ArticleCategory category, string oldUrlName)
        {
            if (ModelState.IsValid)
            {
                if (!db.ArticleRepository.IsExistUrlTitleInArticles(category.NameInUrl) || category.NameInUrl == oldUrlName)
                {
                    db.ArticleRepository.UpdateArticleCategory(category);
                    db.Save();
                    return RedirectToAction("ArticleCategories", "ManageArticles", new { id = category.ParentID });
                }
                else
                {
                    ViewBag.UrlError = true;
                }
            }

            return View(category);
        }

        #endregion

        #region Delete Articel Category

        [EshopPermission("ManageArticleCategory")]
        public ActionResult DeleteCategory(int id)
        {
            var category = db.ArticleRepository.GetCategoryById(id);
            if (category != null)
            {
                category.IsDelete = true;
                db.ArticleRepository.UpdateArticleCategory(category);
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Get Article Sub Category

        public JsonResult GetArticleSubCategory(int mainId)
        {
            var subCategoreies = db.ArticleRepository.GetArticleSubCategory(mainId).Select(s => new SelectListItem()
            {
                Value = s.CategoryID.ToString(),
                Text = s.Title
            });

            return Json(subCategoreies, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Articles

        #region Article List

        [EshopPermission("ArticleList")]
        public ActionResult Index(DateTime? fromDate, DateTime? toDate, List<int> SelectedCategories, string State = "All", string Title = "", int PageId = 1)
        {
            var articles = db.ArticleRepository.GetAllArticlesByFilter(PageId, SelectedCategories, fromDate, toDate, State, Title);
            return View(articles);
        }

        #endregion

        #region Create Article

        [EshopPermission("CreateArticle")]
        public ActionResult CreateArticle()
        {
            ViewBag.SelectCategories = false;
            ViewBag.SelectImage = false;
            ViewBag.ArticleText = false;
            CreateArticleViewModel article = new CreateArticleViewModel();
            article.Categories = db.ArticleRepository.GetMainCategories();
            return View(article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(CreateArticleViewModel articleViewModel, HttpPostedFileBase articleImage)
        {
            #region Check Model Errors

            bool hasError = false;
            ViewBag.SelectCategories = false;
            ViewBag.SelectImage = false;
            ViewBag.ArticleText = false;

            if (articleImage == null)
            {
                hasError = true;
                ViewBag.SelectImage = true;
            }

            if (articleViewModel.SelectedCategories == null)
            {
                hasError = true;
                ViewBag.SelectCategories = true;
            }
            if (string.IsNullOrEmpty(articleViewModel.ArticleText))
            {
                hasError = true;
                ViewBag.ArticleText = true;
            }

            if (db.ArticleRepository.IsExistArticleWithTite(articleViewModel.ArticleTitle))
            {
                ModelState.AddModelError(articleViewModel.ArticleTitle, "این عنوان قبلا ثبت شده است");
                hasError = true;
            }

            if (hasError)
            {
                articleViewModel.Categories = db.ArticleRepository.GetMainCategories();
                return View(articleViewModel);
            }

            #endregion

            if (ModelState.IsValid)
            {
                #region Insert Article Image

                string imageName = articleViewModel.ArticleTitle.Replace(" ", "-") + articleImage.FileName;
                AddImageToServer(articleImage, imageName, PathTools.ArticleImageFullPath, 300, 300, PathTools.ArticleImageThumbFullPath);
                db.Save();

                #endregion

                Article insertedArticle = new Article()
                {
                    CreateDate = DateTime.Now,
                    AuthorID = UserManager.GetCurrentUserId(),
                    IsDelete = false,
                    IsActive = articleViewModel.IsActive,
                    CanInsertComment = articleViewModel.CanInsertComment,
                    ArticleText = articleViewModel.ArticleText,
                    ArticleTitle = articleViewModel.ArticleTitle,
                    ShortDescription = FixedText.ConvertNewLineToBr(articleViewModel.ShortDescription),
                    ArticleImageName = imageName
                };

                db.ArticleRepository.InsertArticle(insertedArticle);
                db.Save();

                db.ArticleRepository.InsertArticleCategory(articleViewModel.SelectedCategories, insertedArticle.ArticleID);
                db.ArticleRepository.InsertArticleTags(articleViewModel.ArticleTags, insertedArticle.ArticleID);
                db.Save();

                return RedirectToAction("Index", "ManageArticles", new { area = "Admin" });

            }


            return View(articleViewModel);
        }

        #endregion

        #region Edit Article

        [EshopPermission("EditArticle")]
        public ActionResult EditArticle(int id)
        {
            if (Request.UrlReferrer != null)
            {
                ViewBag.UrlReferrer = Request.UrlReferrer.ToString();
            }

            ViewBag.SelectCategories = false;
            ViewBag.ArticleText = false;
            var editingArticle = db.ArticleRepository.GetArticleForEdit(id);
            if (editingArticle != null)
            {
                editingArticle.ShortDescription = FixedText.ConvertBrToNewLine(editingArticle.ShortDescription);
                return View(editingArticle);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(EditArticleViewModel articleViewModel, HttpPostedFileBase articleImage, string UrlReferrer, string oldTitle)
        {
            #region Check Model Errors

            bool hasError = false;
            ViewBag.SelectCategories = false;
            ViewBag.ArticleText = false;

            if (articleViewModel.SelectedCategories == null)
            {
                hasError = true;
                ViewBag.SelectCategories = true;
            }
            if (string.IsNullOrEmpty(articleViewModel.ArticleText))
            {
                hasError = true;
                ViewBag.ArticleText = true;
            }

            if (articleViewModel.ArticleTitle != oldTitle && db.ArticleRepository.IsExistArticleWithTite(articleViewModel.ArticleTitle))
            {
                ModelState.AddModelError(articleViewModel.ArticleTitle, "این عنوان قبلا ثبت شده است");
                hasError = true;
            }

            if (hasError)
            {
                articleViewModel.ArticleSelectedCategorieses =
                    db.ArticleRepository.GetArticleSelectedCategories(articleViewModel.ArticleID);
                articleViewModel.Categories = db.ArticleRepository.GetAllCategories();
                return View(articleViewModel);
            }

            #endregion

            if (ModelState.IsValid)
            {
                var mainArticle = db.ArticleRepository.GetArticleByArticleId(articleViewModel.ArticleID);
                mainArticle.ArticleTitle = articleViewModel.ArticleTitle;
                mainArticle.ArticleText = articleViewModel.ArticleText;
                mainArticle.ShortDescription = FixedText.ConvertNewLineToBr(articleViewModel.ShortDescription);
                mainArticle.IsActive = articleViewModel.IsActive;
                mainArticle.CanInsertComment = articleViewModel.CanInsertComment;
                if (articleImage != null)
                {
                    string imageName = articleViewModel.ArticleTitle.Replace(" ", "-") + articleImage.FileName;
                    AddImageToServer(articleImage, imageName, PathTools.ArticleImageFullPath, 300, 300,
                        PathTools.ArticleImageThumbFullPath, mainArticle.ArticleImageName);
                    mainArticle.ArticleImageName = imageName;
                }

                db.ArticleRepository.RemoveArticleTags(articleViewModel.ArticleID);
                if (!string.IsNullOrEmpty(articleViewModel.ArticleTags)) db.ArticleRepository.InsertArticleTags(articleViewModel.ArticleTags, articleViewModel.ArticleID);
                db.ArticleRepository.DeleteArticleCategoriesByArticleId(articleViewModel.ArticleID);
                db.ArticleRepository.InsertArticleCategory(articleViewModel.SelectedCategories, articleViewModel.ArticleID);
                db.Save();
                if (!string.IsNullOrEmpty(UrlReferrer))
                {
                    return RedirectPermanent(UrlReferrer);
                }

                return RedirectToAction("Index", "ManageArticles");
            }

            return View(articleViewModel);
        }


        #endregion

        #region Delete Article

        [EshopPermission("DeleteArticle")]
        public ActionResult DeleteArticle(int id)
        {
            var article = db.ArticleRepository.GetArticleByArticleId(id);
            if (article != null)
            {
                article.IsDelete = true;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Return Article

        [EshopPermission("ReturnArticle")]
        public ActionResult ReturnArticle(int id)
        {
            var article = db.ArticleRepository.GetArticleByArticleId(id);
            if (article != null)
            {
                article.IsDelete = false;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #endregion

        #region Manage Article Comments

        #region Article comment list

        [EshopPermission("ArticleComments")]
        public ActionResult VisitArticleComments(int id)
        {
            ViewBag.ArticleId = id;
            var articleComments = db.ArticleRepository.GetArticleComments(id, 1);
            return View(articleComments);
        }

        #endregion

        #region edit article comment

        [EshopPermission("ArticleComments")]
        public ActionResult EditComment(int id)
        {
            var comment = db.ArticleRepository.GetArticleCommenByCommentId(id);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(ArticleComment articleComment)
        {
            if (ModelState.IsValid)
            {
                db.ArticleRepository.UpdateArticleComment(articleComment);
                db.Save();

                return RedirectToAction("VisitArticleComments", new {id = articleComment.ArticleID});
            }

            return View(articleComment);
        }

        #endregion

        #region Get More Comments

        [EshopPermission("ArticleComments")]
        public ActionResult GetMoreComments(int articleId, int pageId)
        {
            var comments = db.ArticleRepository.GetArticleComments(articleId, pageId);
            Tuple<int, IEnumerable<ArticleComment>> data = new Tuple<int, IEnumerable<ArticleComment>>(pageId, comments);
            return PartialView(data);
        }

        #endregion

        #endregion

    }
}