using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainClass.User
{
    public class RolePersmissions
    {

        #region Constructor
        public RolePersmissions()
        {

        }
        #endregion

        #region Properties

        [Key]
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        #endregion

        #region NavigationProperties

        public virtual Permission UserPermission { get; set; }
        public virtual UserRole Role { get; set; }

        #endregion

    }
}
