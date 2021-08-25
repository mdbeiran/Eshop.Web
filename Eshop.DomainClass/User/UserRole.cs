using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainClass.User
{
    public class UserRole
    {

        #region Constructor
        public UserRole()
        {

        }
        #endregion

        #region Properties

        [Key]
        public int RoleId { get; set; }


        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100)]
        public string RoleTitle { get; set; }


        [Display(Name = "نقش پیش فرض")]
        public bool IsDefaultRole { get; set; }

        public bool IsDelete { get; set; }



        #endregion

        #region NavigationProperties

        public virtual ICollection<UserSelectedRole> UserSelectedRoles { get; set; }
        public virtual ICollection<RolePersmissions> RolePersmissionses { get; set; }

        #endregion
      
    }
}
