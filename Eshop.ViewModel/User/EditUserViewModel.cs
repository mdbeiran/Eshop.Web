using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eshop.DomainClass.User;

namespace Eshop.ViewModel.User
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }


        [Display(Name = "نام نمایشی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }


        [Display(Name = "تلفن همراه")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [RegularExpression(@"(0|\+98)?([ ]|-|[()]){0,2}9[0|1|2|3|4|9]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}",
            ErrorMessage = "شماره موبایل وارد شده اشتباه می باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Mobile { get; set; }


        [MaxLength(10, ErrorMessage = "{0} نمیتواند بیشتر از {1} رقم باشد")]
        [Display(Name = "کد ملی")]
        public string MelliCode { get; set; }


        [Display(Name = "آدرس")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }


        [Display(Name = "تایید نهایی")]
        public bool IsActive { get; set; }

        public List<UserRole> Roles { get; set; }
        public List<int> SelectedRoles { get; set; }
    }
}
