using Eshop.Services.Context;

namespace Eshop.Business.Users
{
    public class PermissionChecker
    {

        public static bool PermissionCheck(string permissionName)
        {
           EshopUOW db=new EshopUOW();
            
            if (!UserManager.UserIsAuthenticated())
            {
                return false;
            }

            int currentUserId = UserManager.GetCurrentUserId();
            int permissionId = db.UserRepository.GetPermissionIdByName(permissionName);
            return db.UserRepository.IsUserInPermission(currentUserId, permissionId);
        }
    }
}
