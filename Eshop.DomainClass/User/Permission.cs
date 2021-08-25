using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.DomainClass.User
{
    public class Permission
    {
        #region Constructor

        public Permission()
        {
        }

        #endregion

        #region Properties

        [Key]
        public int PermissionId { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Title { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Name { get; set; }


        public int? ParentId { get; set; }

        public int? DisplayPriority { get; set; }

        #endregion

        #region Navigation Properties

        [ForeignKey("ParentId")]
        public virtual List<Permission> UserPermissions { get; set; }

        public virtual List<RolePersmissions> RolePersmissionses { get; set; }


        #endregion
    }
}
