using System;
using System.Collections.Generic;
using Eshop.DomainClass.User;
using Eshop.ViewModel.Public;
using Eshop.ViewModel.User;

namespace Eshop.Services.Repositories
{
    public interface IUserRepository : IDisposable
    {
        #region Admin Index

        AdminIndexViewModel GetAdminIndexInfo();

        #endregion

        #region Roles
        List<UserRole> GetAllRoles();
        int GetDefaultRoleId();
        UserRole GetDefaultRole();
        void CreateRole(UserRole role);
        void UpdateRole(UserRole edited);
        void SetDefaultRole(int roleId);
        void DeleteRole(int roleId);
        UserRole GetRoleById(int id);
        UserRole GetRoleByName(string name);
        bool IsExistRoleByName(string name);

        #endregion Roles

        #region Permission
        List<Permission> GetAllPermissions();
        List<Permission> GetMainPermissions();
        List<Permission> GetSubPermissions(int permissionId);
        #endregion Permission

        #region RolePermission
        List<RolePersmissions> GetRolePermissionsByRoleId(int roleid);
        void InsertRangeRolePermissions(int roleid, List<int> Permissionid);
        void DeleteExceptRangeRolePermissions(int roleId);

        #endregion Permission

        #region user permissions

        int GetPermissionIdByName(string name);
        bool IsUserInPermission(int userId, int permissionId);

        #endregion

        #region UserRoles
        void AddRoleToUser(int userId, int roleId);
        void DeleteUserRoles(int userId);
        List<UserSelectedRole> GetUserRoles(int userId);

        #endregion

        #region Users

        FilterUserViewModel GetUsers(int pageId, string userState, DateTime? fromDate, DateTime? toDate,
            string filterUserName = null, string filterEmail = "", string filterMobile = "");
        User GetUserByMobileActiveCode(int activeCode);
        string GetUserName(int userId);
        DateTime GetRegisterDateUser(int userId);
        void DeleteUser(int userId);
        void InsertUser(User user);
        void UpdateUser(User user);
        bool IsExistUserWithUserName(string userName);
        void ReturnUser(int userId);
        User GetUserByUserId(int UserId);
        bool IsExistUserWithMobile(string mobile);
        int GetNewRandomMobileCode();
        User GetUserWithOutCheckById(int userId);
        EditUserViewModel GetUserForEdit(int userId);
        UserInformationViewModel GetUserInformationForAdminUserPanel(int userId);
        User GetUserByUserName(string userName);
        int GetAllUsersCount();
        bool IsUserActive(int userId);
        IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector);
        User GetUserByMobile(string mobile);


        #endregion

        #region User Panel

        UserPanelIndexViewModel GetUserPanelIndexData(int userId);

        #endregion

    }
}
