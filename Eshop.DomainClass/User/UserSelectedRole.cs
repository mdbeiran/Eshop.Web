using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainClass.User
{
    public class UserSelectedRole
    {
        #region Constructor

        public UserSelectedRole()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #endregion

        #region Navigation Properties

        public DomainClass.User.User User { get; set; }
        public UserRole Role { get; set; }

        #endregion

    }
}
