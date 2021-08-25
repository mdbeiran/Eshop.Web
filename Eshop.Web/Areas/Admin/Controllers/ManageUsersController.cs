using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshop.Business.Filter;
using Eshop.DomainClass.User;
using Eshop.ViewModel.User;
using Eshop.Services.Context;
using Eshop.Utilitiy.Security.Contracts;

namespace Eshop.Web.Areas.Admin.Controllers
{
    [Authorize]
    [EshopPermission("ManageUsers")]
    
    public class ManageUsersController : Controller
    {
        #region Ctor

        private EshopUOW db;
        private IPasswordHelper passwordHelper;
        public ManageUsersController(EshopUOW db , IPasswordHelper passwordHelper)
        {
            this.db = db;
            this.passwordHelper = passwordHelper;
        }

        #endregion

        #region Users

        #region Users

        [HttpGet]
        [EshopPermission("ManageUsers")]
        public ActionResult Index(string filterUserName, DateTime? fromDate, DateTime? toDate, string filterEmail, string filterMobile, int? pageId = 1, string userState = "Active")
        {
            var users = db.UserRepository.GetUsers(pageId.Value, userState, fromDate, toDate, filterUserName, filterEmail, filterMobile);
            return View(users);
        }

        #endregion

        #region User Information

        #region User Panel

        public ActionResult GoToUserPanel(int id)
        {
            var userPanel = db.UserRepository.GetUserInformationForAdminUserPanel(id);
            return View(userPanel);
        }

        #endregion

        #endregion

        #region CreateUser

        [EshopPermission("CreateUser")]
        public ActionResult CreateUser()
        {
            ViewBag.SelectedRoles = false;
            var roles = db.UserRepository.GetAllRoles();
            CreateUserViewModel newUser = new CreateUserViewModel();
            newUser.Roles = roles;
            newUser.SelectedRoles = new List<int>();
            return View(newUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                if (!db.UserRepository.IsExistUserWithMobile(newUser.Mobile))
                {
                    DomainClass.User.User user = new User()
                    {
                        Mobile = newUser.Mobile,
                        RegisterDate = DateTime.Now,
                        UserName = newUser.UserName,
                        IsActive = newUser.IsActive,
                        IsDelete = false,
                        MobileActiveCode = db.UserRepository.GetNewRandomMobileCode(),
                        Address = newUser.Address,
                        Password = passwordHelper.EncodePasswordMd5(newUser.Password)
                    };
                    db.UserRepository.InsertUser(user);
                    db.Save();

                    #region Add User Roles

                    if (newUser.SelectedRoles != null)
                    {
                        foreach (var role in newUser.SelectedRoles)
                        {
                            db.UserRepository.AddRoleToUser(user.UserId, role);
                        }
                    }
                    else
                    {
                        ViewBag.SelectedRoles = true;
                        newUser.Roles = db.UserRepository.GetAllRoles();
                        return View("CreateUser", newUser);
                    }

                    #endregion

                    db.Save();

                    return RedirectToAction("Index", "ManageUsers");

                }
                else
                {
                    ModelState.AddModelError("Mobile", "این شماره موبایل قبلا استفاده شده است");
                }

            }

            ViewBag.SelectedRoles = false;
            newUser.Roles = db.UserRepository.GetAllRoles();
            return View("CreateUser", newUser);
        }

        #endregion

        #region Edit User

        [EshopPermission("EditUser")]
        public ActionResult EditUser(int id)
        {
            ViewBag.SelectedRoles = false;
            var editUser = db.UserRepository.GetUserForEdit(id);
            db.Save();
            ViewBag.UrlReferrer = Request.UrlReferrer.ToString();
            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUserViewModel editUser, HttpPostedFileBase userAvatar, HttpPostedFileBase melliCard, string UrlReferrer)
        {
            var user = db.UserRepository.GetUserByUserId(editUser.UserId);
            if (ModelState.IsValid)
            {
                if (!db.UserRepository.IsExistUserWithMobile(editUser.Mobile) || editUser.Mobile == user.Mobile)
                {
                    #region Check User Roles

                    // check user selected roles
                    if (editUser.SelectedRoles != null)
                    {
                        db.UserRepository.DeleteUserRoles(user.UserId);
                        foreach (var role in editUser.SelectedRoles)
                        {
                            db.UserRepository.AddRoleToUser(user.UserId, role);
                        }
                    }
                    else
                    {
                        ViewBag.SelectedRoles = true;
                        editUser.Roles = db.UserRepository.GetAllRoles();
                        return View("EditUser", editUser);
                    }

                    #endregion

                    #region Edit User Information

                    user.UserName = editUser.UserName;
                    user.Mobile = editUser.Mobile;
                    user.IsActive = editUser.IsActive;
                    user.Address = editUser.Address;

                    #endregion

                    db.UserRepository.UpdateUser(user);
                    db.Save();
                    if (!string.IsNullOrEmpty(UrlReferrer))
                    {
                        return RedirectPermanent(UrlReferrer);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("UserName", "نام کاربری قبلا استفاده شده است");
                }
            }

            ViewBag.SelectedRoles = false;
            ViewBag.UrlReferrer = UrlReferrer;
            editUser.Roles = db.UserRepository.GetAllRoles();
            return View(editUser);
        }


        #endregion

        #region Delete User

        [HttpGet]
        [EshopPermission("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            db.UserRepository.DeleteUser(id);
            db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        #endregion

        #region Return User

        [HttpGet]
        [EshopPermission("ReturnUser")]
        public ActionResult ReturnUser(int id)
        {
            db.UserRepository.ReturnUser(id);
            db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #endregion

        #region Role controller

        #region index Roles

        [HttpGet]
        [EshopPermission("ManageRoles")]
        public ActionResult Roles()
        {
            ManageRolesViewModel roleManage = new ManageRolesViewModel();
            roleManage.Roles = db.UserRepository.GetAllRoles();
            roleManage.Permissions = db.UserRepository.GetMainPermissions();
            ViewBag.Success = "no";
            return View(roleManage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Roles(ManageRolesViewModel newRole)
        {
            if (ModelState.IsValid)
            {
                if (!db.UserRepository.IsExistRoleByName(newRole.RoleTitle))
                {
                    newRole.IsDefaultRole = false;

                    UserRole creatingRole = new UserRole()
                    {
                        RoleTitle = newRole.RoleTitle,
                        IsDefaultRole = newRole.IsDefaultRole,
                        IsDelete = false
                    };

                    db.UserRepository.CreateRole(creatingRole);
                    db.Save();
                    var role = db.UserRepository.GetRoleByName(newRole.RoleTitle);
                    db.UserRepository.InsertRangeRolePermissions(role.RoleId, newRole.SelectedPermissions);
                    db.Save();
                    ViewBag.Success = "yes";
                    ManageRolesViewModel ManageRole = new ManageRolesViewModel();
                    ManageRole.Roles = db.UserRepository.GetAllRoles();
                    ManageRole.Permissions = db.UserRepository.GetMainPermissions();
                    return View(ManageRole);

                }
                else
                {
                    ModelState.AddModelError("RoleTitle", "این نقش از قبل وجود دارد");
                }
            }

            ManageRolesViewModel roleManage = new ManageRolesViewModel();
            roleManage.Roles = db.UserRepository.GetAllRoles();
            roleManage.Permissions = db.UserRepository.GetMainPermissions();

            return View(roleManage);
        }
        #endregion index Roles

        #region EditRole

        [EshopPermission("EditRole")]
        public ActionResult EditRole(int id)
        {
            var editingRole = db.UserRepository.GetRoleById(id);
            EditRoleViewModel roleManage = new EditRoleViewModel();
            roleManage.RoleId = editingRole.RoleId;
            roleManage.RoleTitle = editingRole.RoleTitle;
            roleManage.IsDelete = editingRole.IsDelete;
            roleManage.IsDefaultRole = editingRole.IsDefaultRole;
            roleManage.Permissions = db.UserRepository.GetAllPermissions();
            roleManage.SelectedPermissions = db.UserRepository.GetRolePermissionsByRoleId(id);
            ViewBag.RoleId = id;
            ViewBag.Success = "no";
            return View(roleManage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(EditRoleViewModel editedRole)
        {
            if (ModelState.IsValid)
            {
                var role = db.UserRepository.GetRoleById(editedRole.RoleId);
                if (!db.UserRepository.IsExistRoleByName(editedRole.RoleTitle) || editedRole.RoleTitle == role.RoleTitle)
                {
                    var editingRole = db.UserRepository.GetRoleById(editedRole.RoleId);
                    editingRole.RoleTitle = editedRole.RoleTitle;
                    editingRole.IsDefaultRole = editedRole.IsDefaultRole;
                    db.UserRepository.UpdateRole(editingRole);
                    db.UserRepository.DeleteExceptRangeRolePermissions(role.RoleId);
                    db.Save();
                    db.UserRepository.InsertRangeRolePermissions(role.RoleId, editedRole.EditedPermissions);
                    db.Save();

                    return RedirectToAction("Roles");
                }
                else
                {
                    ModelState.AddModelError("RoleTitle", "این نقش از قبل وجود دارد");
                }
            }

            ViewBag.RoleId = editedRole.RoleId;
            editedRole.Permissions = db.UserRepository.GetAllPermissions();
            editedRole.SelectedPermissions = db.UserRepository.GetRolePermissionsByRoleId(editedRole.RoleId);
            return View(editedRole);
        }

        #endregion

        #region DeleteRole

        [EshopPermission("DeleteRole")]
        public ActionResult DeleteRole(int RoleId)
        {
            db.UserRepository.DeleteRole(RoleId);
            db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion DeleteRole

        #region Get Permissions With Ajax

        public JsonResult GetSubPermissions(int permissionId)
        {
            var permissions = db.UserRepository.GetSubPermissions(permissionId).Select(c => new SelectListItem()
            {
                Text = c.Title,
                Value = c.PermissionId.ToString()
            });
            return Json(permissions, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region default role


        public ActionResult SetDefaultRole(int RoleId)
        {
            db.UserRepository.SetDefaultRole(RoleId);
            db.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion

        #endregion

    }
}