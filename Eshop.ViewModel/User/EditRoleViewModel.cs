using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eshop.DomainClass.User;

namespace Eshop.ViewModel.User
{
    public class EditRoleViewModel
    {
        public int RoleId { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string RoleTitle { get; set; }
        [Display(Name = "نقش پیش فرض")]
        public bool IsDefaultRole { get; set; }
        public bool IsDelete { get; set; }
        public List<Permission> Permissions { get; set; }
        public List<int> EditedPermissions { get; set; }
        public List<RolePersmissions> SelectedPermissions { get; set; }
    }
}
