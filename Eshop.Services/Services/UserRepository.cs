using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Eshop.DataLayer.Contexts;
using Eshop.DomainClass.User;
using Eshop.Services.Repositories;
using Eshop.ViewModel.Article;
using Eshop.ViewModel.Public;
using Eshop.ViewModel.User;

namespace Eshop.Services.Services
{
    public class UserRepository : IUserRepository
    {
        #region Context

        private readonly EshopDbContext db;
        
        public UserRepository(EshopDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Admin Index

        public AdminIndexViewModel GetAdminIndexInfo()
        {
            ArticleCommentsViewModel comments = new ArticleCommentsViewModel();
            comments.ArticleComments = db.ArticleComments.Where(c => !c.IsRead && !c.IsDelete).OrderByDescending(o => o.CreateDate).Take(5).AsNoTracking().ToList();
            comments.NewCommentsCount = db.ArticleComments.Count(c => !c.IsRead);

            AdminIndexViewModel data = new AdminIndexViewModel();
            data.ArticleComments = comments;
            data.NewContactCount = db.ContactUs.Count(s => !s.IsRead);
            data.NewOrderdCount = db.Orders.Count(s => s.IsFinally && !s.IsSend);

            return data;
        }

        #endregion

        #region UserRoles

        public void AddRoleToUser(int userId, int roleId)
        {
            UserSelectedRole userSelectedRole = new UserSelectedRole()
            {
                RoleId = roleId,
                UserId = userId
            };
            db.UserSelectedRoles.Add(userSelectedRole);
        }

        public void DeleteUserRoles(int userId)
        {
            var userRoles = this.GetUserRoles(userId);
            foreach (var role in userRoles)
            {
                db.Entry(role).State = EntityState.Deleted;
            }
        }

        public List<UserSelectedRole> GetUserRoles(int userId)
        {
            return db.UserSelectedRoles.Where(ur => ur.UserId == userId).ToList();
        }


        #endregion

        #region Roles

        public UserRole GetDefaultRole()
        {
            return db.UserRoles.SingleOrDefault(r => r.IsDefaultRole && !r.IsDelete);

        }

        public int GetDefaultRoleId()
        {
            var defaultRole = db.UserRoles.FirstOrDefault(r => r.IsDefaultRole);

            if (defaultRole == null)
            {
                throw new Exception("Default Role Not Found");
            }

            return defaultRole.RoleId;
        }

        public List<UserRole> GetAllRoles()
        {
            return db.UserRoles.Where(r => r.IsDelete == false).ToList();
        }

        public UserRole GetRoleByName(string name)
        {
            return db.UserRoles.SingleOrDefault(r => r.RoleTitle == name);
        }

        public bool IsExistRoleByName(string name)
        {
            return db.UserRoles.Where(r => !r.IsDelete).Any(r => r.RoleTitle == name);
        }

        public void UpdateRole(UserRole edited)
        {
            if (edited.IsDefaultRole)
            {
                var def = GetDefaultRole();
                if (def != null && def.RoleId != edited.RoleId)
                {
                    def.IsDefaultRole = false;
                }
            }
            db.Entry(edited).State = EntityState.Modified;

        }

        public void SetDefaultRole(int roleId)
        {
            var def = GetDefaultRole();
            if (def != null)
            {
                def.IsDefaultRole = false;
            }

            var rol = GetRoleById(roleId);
            rol.IsDefaultRole = true;
        }

        public void CreateRole(UserRole role)
        {
            db.UserRoles.Add(role);
            if (role.IsDefaultRole)
            {
                var defaultRole = GetDefaultRole();
                defaultRole.IsDefaultRole = false;
                db.Entry(defaultRole).State = EntityState.Modified;
            }
        }

        public UserRole GetRoleById(int id)
        {
            return db.UserRoles.SingleOrDefault(r => r.RoleId == id);
        }

        public void DeleteRole(int roleId)
        {
            var role = GetRoleById(roleId);
            role.IsDelete = true;
            role.IsDefaultRole = false;
        }

        #endregion

        #region Permissions

        public List<Permission> GetAllPermissions()
        {
            return db.Permissions.OrderBy(p => p.DisplayPriority).ToList();
        }

        public List<Permission> GetMainPermissions()
        {
            return db.Permissions.Where(p => p.ParentId == 1).OrderBy(n => n.DisplayPriority).AsNoTracking().ToList();
        }

        public List<Permission> GetSubPermissions(int permissionId)
        {
            return db.Permissions.Where(p => p.ParentId == permissionId).ToList();
        }

        #endregion

        #region RolePermissions

        public List<RolePersmissions> GetRolePermissionsByRoleId(int roleid)
        {
            return db.RolePersmissions.Where(r => r.RoleId == roleid).ToList();
        }

        public void InsertRangeRolePermissions(int roleid, List<int> Permissionid)
        {
            var rolepermisions = GetRolePermissionsByRoleId(roleid);
            if (Permissionid != null && Permissionid.Any())
            {
                RolePersmissions AdminRolePermission = new RolePersmissions();
                AdminRolePermission.RoleId = roleid;
                AdminRolePermission.PermissionId = 1;
                db.RolePersmissions.Add(AdminRolePermission);
                foreach (var item in Permissionid)
                {
                    if (rolepermisions.All(r => r.PermissionId != item))
                    {
                        RolePersmissions rolePersmission = new RolePersmissions();
                        rolePersmission.RoleId = roleid;
                        rolePersmission.PermissionId = item;
                        db.RolePersmissions.Add(rolePersmission);
                    }
                }
            }

        }

        public void DeleteExceptRangeRolePermissions(int roleId)
        {
            var rolepermisions = GetRolePermissionsByRoleId(roleId);

            foreach (var item in rolepermisions)
            {
                db.RolePersmissions.Remove(item);
            }
        }


        #endregion

        #region user permissions

        public int GetPermissionIdByName(string name)
        {
            return db.Permissions.SingleOrDefault(p => p.Name == name).PermissionId;
        }

        public bool IsUserInPermission(int userId, int permissionId)
        {
            return db.UserSelectedRoles.Any(u => u.UserId == userId && u.Role.RolePersmissionses.Any(p => p.PermissionId == permissionId));
        }

        #endregion

        #region Users

        public bool IsUserActive(int userId)
        {
            return db.Users.Any(u => u.UserId == userId && u.IsActive);
        }

        public IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public User GetUserByMobile(string mobile)
        {
            return db.Users.SingleOrDefault(s => s.Mobile == mobile);
        }

        public FilterUserViewModel GetUsers(int pageId, string userState, DateTime? fromDate, DateTime? toDate,
            string filterUserName = null, string filterEmail = "", string filterMobile = "")
        {
            int take = 12;
            int skip = (pageId - 1) * take;
            IQueryable<User> userList = db.Users;

            #region User State

            switch (userState)
            {
                case "All":
                    {
                        break;
                    }
                case "Deleted":
                    {
                        userList = userList.Where(u => u.IsDelete);
                        break;
                    }
                case "Active":
                    {
                        userList = userList.Where(u => u.IsActive && !u.IsDelete);
                        break;
                    }
                case "NotActive":
                    {
                        userList = userList.Where(u => !u.IsActive && !u.IsDelete);
                        break;
                    }
            }

            #endregion

            FilterUserViewModel returnUsers = new FilterUserViewModel();

            #region implementation filters

            if (!string.IsNullOrEmpty(filterUserName))
            {
                userList = userList.Where(u => u.UserName.Contains(filterUserName));
            }

            if (!string.IsNullOrEmpty(filterMobile))
            {
                userList = userList.Where(u => u.Mobile.Contains(filterMobile));
            }

            if (fromDate != null)
            {
                DateTime fromdate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, new PersianCalendar());
                userList = userList.Where(u => u.RegisterDate >= fromdate);
                returnUsers.fromDate = fromDate.Value;
            }

            if (toDate != null)
            {
                DateTime todate = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, new PersianCalendar());
                userList = userList.Where(u => u.RegisterDate <= todate);
                returnUsers.toDate = toDate.Value;
            }

            #endregion

            #region paging

            int thisPageCount = userList.Count();
            if (thisPageCount % take > 0)
            {
                returnUsers.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                returnUsers.PageCount = thisPageCount / take;
            }

            returnUsers.ActivePage = pageId;
            returnUsers.StartPage = pageId - 3;
            returnUsers.EndPage = returnUsers.ActivePage + 3;
            if (returnUsers.StartPage <= 0) returnUsers.StartPage = 1;
            if (returnUsers.EndPage > returnUsers.PageCount) returnUsers.EndPage = returnUsers.PageCount;
            #endregion 

            #region fill filters fields

            returnUsers.FilterUsername = filterUserName;
            returnUsers.FilterEmail = filterEmail;
            returnUsers.FilterMobile = filterMobile;
            returnUsers.userState = userState;


            #endregion

            returnUsers.Users = userList.OrderByDescending(u => u.UserId).Skip(skip).Take(take).AsNoTracking().ToList();

            return returnUsers;
        }

        public User GetUserByMobileActiveCode(int activeCode)
        {
            return db.Users.SingleOrDefault(s => s.MobileActiveCode == activeCode);
        }

        public UserInformationViewModel GetUserInformationForAdminUserPanel(int userId)
        {
            UserInformationViewModel userInformation = new UserInformationViewModel()
            {
                userId = userId,
                User = GetUserWithOutCheckById(userId),/*
                followers = db.UserFollows.Count(f => f.FollowingId == userId && f.IsAccept),
                following = db.UserFollows.Count(f => f.FollowerId == userId && f.IsAccept),
                UserMessages = db.UserMessages.Count(m => m.SenderMessageId == userId || m.ReciverMessageId == userId),
                UserAccountSettingCategories = db.UserAccountSettingCategories.ToList(),
                UserAccountSettings = db.UserAccountSettings.ToList(),
                UserSelectedAccountSettings = db.UserSelectedAccountSettings.Where(s => s.UserId == userId).ToList()*/
            };

            return userInformation;
        }

        public User GetUserByUserName(string userName)
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public int GetAllUsersCount()
        {
            return db.Users.Count();
        }

        public User GetUserByUserId(int userId)
        {
            return db.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public string GetUserName(int userId)
        {
            var result = db.Users.Find(userId)?.UserName;

            return result;
        }

        public DateTime GetRegisterDateUser(int userId)
        {
            return db.Users.Find(userId).RegisterDate;
        }

        public EditUserViewModel GetUserForEdit(int userId)
        {
            var currentUser = GetUserByUserId(userId);
            var roles = GetAllRoles();
            var selectedRoles = GetUserRoles(userId);
            EditUserViewModel returnUser = new EditUserViewModel()
            {
                UserId = currentUser.UserId,
                UserName = currentUser.UserName,
                Mobile = currentUser.Mobile,
                IsActive = currentUser.IsActive,
                Roles = roles,
                SelectedRoles = selectedRoles.Select(u => u.RoleId).ToList(),
                Address = currentUser.Address
            };
            currentUser.ViewByAdmin = true;
            return returnUser;
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserByUserId(userId);
            user.IsDelete = true;
        }

        public void InsertUser(User user)
        {
            db.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var local = db.Set<User>()
              .Local
              .FirstOrDefault(f => f.UserId == user.UserId);
            if (local != null)
            {
                db.Entry(local).State = EntityState.Detached;
            }

            db.Entry(user).State = EntityState.Modified;
        }

        public bool IsExistUserWithUserName(string userName)
        {
            return db.Users.Any(u => u.UserName == userName);
        }

        public void ReturnUser(int userId)
        {
            var user = db.Users.SingleOrDefault(u => u.UserId == userId);
            user.IsDelete = false;
        }

        public bool IsExistUserWithMobile(string mobile)
        {
            return db.Users.Any(u => u.Mobile == mobile);
        }

        public void ActiveTheUser(int userId)
        {
            var user = GetUserByUserId(userId);
            user.IsActive = true;
            UpdateUser(user);
        }

        public int GetNewRandomMobileCode()
        {
            var newCode = new Random().Next(1000, 100000);
            return newCode;
        }

        public User GetUserWithOutCheckById(int userId)
        {
            return db.Users.SingleOrDefault(u => u.UserId == userId);
        }

        #endregion

        #region User Panel

        public UserPanelIndexViewModel GetUserPanelIndexData(int userId)
        {
            UserPanelIndexViewModel data = new UserPanelIndexViewModel();
            data.User = GetUserByUserId(userId);

            return data;
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

    }
}



