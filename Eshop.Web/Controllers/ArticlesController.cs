using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eshop.Business.Users;
using Eshop.DomainClass.Articles;
using Eshop.Services.Context;
using Eshop.Utilitiy.Convertor;

namespace Eshop.Web.Controllers
{

    [RoutePrefix("Articles")]
    public class ArticlesController : Controller
    {
        #region Constructor

        private EshopUOW db;

        public ArticlesController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region All Article List

        [Route(Name = "ArticleIndex")]
        public ActionResult Index(string search = "", int pageId = 1)
        {
            var articles = db.ArticleRepository.GetAllArticlesByFilter(pageId, null, null, null, "Active", null, null, search);
            return View(articles);
        }

        #endregion

        #region Article Group List

        [Route("{categoryName}", Name = "GetArticleCategory")]
        public ActionResult ArticleGroups(string categoryName, int pageId = 1, string search = "")
        {
            var articles = db.ArticleRepository.GetAllArticlesByFilter(pageId, null, null, null, "Active", null, categoryName, search);
            if (articles.ArticleCategory == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        #endregion

        #region Category Section

        public ActionResult CategorySection()
        {
            var categories = db.ArticleRepository.GetAllCategories();
            return PartialView(categories);
        }

        #endregion

        #region Single Article

        [Route("SingleArticle/{articleTitle}", Name = "SingleArticle")]
        public ActionResult SingleArticle(string articleTitle)
        {
            articleTitle = articleTitle.Replace("-", " ");
            var article = db.ArticleRepository.GetSingleArticle(articleTitle, 1);
            if (article == null)
            {
                return HttpNotFound();
            }
            db.ArticleRepository.InsertVisitForArticle(article.Article.ArticleID, UserManager.GetUserIP());
            db.Save();
            return View(article);
        }

        #endregion

        #region Article Comments

        #region Create Comment

        [HttpPost]
        [Route(Name = "CreateCommnet")]
        public ActionResult CreateCommnet(ArticleComment replyComment, string articleTitle)
        {
            if (ModelState.IsValid)
            {
                replyComment.Comment = FixedText.ConvertNewLineToBr(replyComment.Comment);
                replyComment.UserID = UserManager.GetCurrentUserId();
                replyComment.CreateDate = DateTime.Now;
                db.ArticleRepository.InsertCommentForArticle(replyComment);
                db.Save();
            }

            return RedirectToRoute("SingleArticle", new { articleTitle = UserManager.DecodeUrl(articleTitle), pageId = 1 });
        }

        #endregion

        #region Get More Comments

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