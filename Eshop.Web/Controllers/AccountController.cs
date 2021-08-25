using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Eshop.Business.Users;
using Eshop.DomainClass.User;
using Eshop.Services.Context;
using Eshop.Utilitiy.Security.Contracts;
using Eshop.ViewModel.User;
using Eshop.Utilitiy.Sms;

namespace Eshop.Web.Controllers
{
    public class AccountController : Controller
    {
        #region ctor

        private EshopUOW db;
        private IPasswordHelper passwordHelper;

        public AccountController(EshopUOW db, IPasswordHelper passwordHelper)
        {
            this.db = db;
            this.passwordHelper = passwordHelper;
        }

        #endregion

        #region Register

        [Route("Register", Name = "RegisterGet")]
        public ActionResult Register()
        {
            return View();
        }


        [Route("Register", Name = "RegisterPost")]
        [HttpPost]
        public ActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ActiveMobile", user);
            }

            return View(user);
        }


        [HttpGet]
        [Route("ActiveMobile", Name = "ActiveMobileGet")]
        public ActionResult ActiveMobile(RegisterViewModel user, int? id)
        {
            ViewBag.HasMobile = false;

            var activeCode = db.UserRepository.GetNewRandomMobileCode();
            TempData["FailRegister"] = null;
            Session["ActiveCode"] = activeCode;
            TempData["ActiveCode"] = activeCode;
            user.MobileActiveCode = activeCode;
            if (db.UserRepository.IsExistUserWithMobile(user.Mobile))
            {
                ViewBag.HasMobile = true;
            }
            else
            {
                KavehNegarSms sms = new KavehNegarSms();
                sms.SendSms(user.Mobile, "کار زیبا \n کد فعالسازی : " + activeCode);
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ActiveMobile", Name = "ActiveMobilePost")]
        public ActionResult ActiveMobile(RegisterViewModel user)
        {
            var activeCode = (int)Session["ActiveCode"];
            if (user.MobileActiveCode == activeCode)
            {
                if (!db.UserRepository.IsExistUserWithMobile(user.Mobile))
                {
                    TempData["SuccessRegister"] = true;
                    Session.Remove("ActiveCode");

                    User newUser = new User()
                    {
                        UserName = user.UserName,
                        RegisterDate = DateTime.Now,
                        MobileActiveCode = db.UserRepository.GetNewRandomMobileCode(),
                        Address = null,
                        Mobile = user.Mobile,
                        IsDelete = false,
                        IsActive = true,
                        ViewByAdmin = false,
                        Password = passwordHelper.EncodePasswordMd5(user.Password)
                    };
                    db.UserRepository.InsertUser(newUser);
                    db.Save();
                    int defaultRoleId = db.UserRepository.GetDefaultRoleId();
                    db.UserRepository.AddRoleToUser(newUser.UserId, defaultRoleId);
                    db.Save();

                    FormsAuthentication.SetAuthCookie(newUser.UserId.ToString(), true);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.HasMobile = true;
                }
            }
            TempData["ActiveCode"] = activeCode;
            TempData["FailRegister"] = activeCode;
            return View(user);
        }

        #endregion

        #region Login

        [Route("Login", Name = "LoginGet")]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login", Name = "LoginPost")]
        public ActionResult Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            string hashPassword = passwordHelper.EncodePasswordMd5(user.Password);
            var thisUser = db.UserRepository.GetUserByMobile(user.Mobile);
            if (thisUser != null)
            {
                if (thisUser.Password == hashPassword && thisUser.IsActive && !thisUser.IsDelete)
                {
                    FormsAuthentication.SetAuthCookie(thisUser.UserId.ToString(), true);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Mobile", "حساب کاربری شما مسدود شده است");
                }
            }
            else
            {
                ModelState.AddModelError("Mobile", "کاربری با اطلاعات وارد شده یافت نشد");
            }

            return View(user);
        }

        #endregion

        #region Sign Out

        [Route("SignOut")]
        public ActionResult SignOut()
        {
            UserManager.SignOut();
            return Redirect("/");
        }

        #endregion

        #region reset password

        [Route("ResetPassword", Name = "ResetPasswordGet")]
        public ActionResult ResetPassword()
        {
            return View();
        }


        [Route("ResetPassword", Name = "ResetPasswordpost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel phone)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserRepository.GetUserByMobile(phone.Mobile);
                if (user != null)
                {
                    user.MobileActiveCode = db.UserRepository.GetNewRandomMobileCode();
                    db.Save();
                    KavehNegarSms sms = new KavehNegarSms();
                    sms.SendSms(user.Mobile, "کار زیبا \n کد تایید : " + user.MobileActiveCode);
                    return RedirectToRoute("VerifyCodeGet");
                }
            }

            return View(phone);
        }


        [Route("VerifyCode", Name = "VerifyCodeGet")]
        public ActionResult VerifyCode()
        {
            return View();
        }


        [Route("VerifyCode", Name = "VerifyCodePost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode(VerifyPasswordViewModel verify)
        {
            var user = db.UserRepository.GetUserByMobileActiveCode(verify.MobileActiveCode);
            if (user != null && user.MobileActiveCode == verify.MobileActiveCode)
            {
                string newPass = db.UserRepository.GetNewRandomMobileCode().ToString();
                user.Password = passwordHelper.EncodePasswordMd5(newPass);
                db.Save();
                KavehNegarSms sms = new KavehNegarSms();
                sms.SendSms(user.Mobile, "کار زیبا \n رمز عبور جدید شما : " + newPass + "\nلطفا در اولین فرصت رمز عبور خود را تغییر دهید");
                return RedirectToRoute("LoginGet");
            }
            return RedirectToRoute("ResetPasswordGet");
        }

        #endregion

    }
}