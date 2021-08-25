using System.Net;
using System.Web.Mvc;
using Eshop.Business.Filter;
using Eshop.Services.Context;

namespace Eshop.Web.Areas.Admin.Controllers
{
    [EshopPermission("AdminPermission")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Ctor

        private EshopUOW db;

        public HomeController(EshopUOW db)
        {
            this.db = db;
        }

        #endregion

        #region Index

        public ActionResult Index()
        {
            var index = db.UserRepository.GetAdminIndexInfo();
            return View(index);
        }

        #endregion

        #region Delete Article Comment By Admin

        public ActionResult DeleteArticleComment(int commentId)
        {
            var comment = db.ArticleRepository.GetArticleCommenByCommentId(commentId);
            if (comment != null)
            {
                comment.IsDelete = true;
                comment.IsRead = true;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Read Article Comment By Admin

        public ActionResult ReadArticleComment(int commentId)
        {
            var comment = db.ArticleRepository.GetArticleCommenByCommentId(commentId);
            if (comment != null)
            {
                comment.IsRead = true;
                db.Save();
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
        }

        #endregion

        #region Get Single Article Comment

        public ActionResult GetSingleArticleComment()
        {
            var article = db.ArticleRepository.GetSingleArticleComment();
            return PartialView(article);
        }

        #endregion
    }
}