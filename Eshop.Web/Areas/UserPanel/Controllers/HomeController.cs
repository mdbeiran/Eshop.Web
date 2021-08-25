using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eshop.Utilitiy.Security.Contracts;

namespace Eshop.Web.Areas.UserPanel.Controllers
{
    using Eshop.Business.Users;
    using Eshop.DomainClass.User;
    using Eshop.Services.Context;
    using Eshop.ViewModel.User;


    [RouteArea("UserPanel", AreaPrefix = "User")]
    [RoutePrefix("")]
    [Authorize]
    public class HomeController : Controller
    {
        #region ctor

        private EshopUOW db;

        private IPasswordHelper passwordHelper;

        public HomeController(EshopUOW db, IPasswordHelper passwordHelper)
        {
            this.db = db;
            this.passwordHelper = passwordHelper;
        }

        #endregion

        #region Index User Panel

        [Route("", Name = "UserIndex")]
        public ActionResult Index()
        {
            var data = db.UserRepository.GetUserPanelIndexData(UserManager.GetCurrentUserId());
            return View(data);
        }

        #endregion

        #region Edit User Information

        [Route("EditInformation", Name = "EditInformationGet")]
        public ActionResult EditUserInformation()
        {
            var user = db.UserRepository.GetUserByUserId(UserManager.GetCurrentUserId());
            ViewBag.UrlReferer = Request.UrlReferrer.ToString();
            return View(user);
        }

        [Route("EditInformation", Name = "EditInformationPost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserInformation(string UserName, string Address,string referer)
        {
            var user = db.UserRepository.GetUserByUserId(UserManager.GetCurrentUserId());
            if (ModelState.IsValid)
            {
                user.Address = Address;
                user.UserName = UserName;
                db.Save();

                if (!string.IsNullOrEmpty(referer))
                {
                    return RedirectPermanent(referer);
                }

                return RedirectToRoute("UserIndex");
            }

            return View(user);
        }

        #endregion

        #region user sidebar

        public ActionResult UserSidebar()
        {
            var user = db.UserRepository.GetUserByUserId(UserManager.GetCurrentUserId());
            return PartialView(user);
        }

        #endregion

        #region Change User Password

        [Route("ChangePassword",Name = "ChangePasswordGet")]
        public ActionResult ChangePassword()
        {
            ViewBag.Error = false;
            return View();
        }


        [Route("ChangePassword", Name = "ChangePasswordPost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel userPass)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserRepository.GetUserByUserId(UserManager.GetCurrentUserId());
                var check = passwordHelper.EncodePasswordMd5(userPass.OldPassword);
                if (check != user.Password)
                {
                    ViewBag.Error = true;
                    return View(userPass);
                }

                user.Password = passwordHelper.EncodePasswordMd5(userPass.Password);
                db.Save();
                return RedirectToRoute("UserIndex");
            }
            return View(userPass);
        }

        #endregion
    }
}