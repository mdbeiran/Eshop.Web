using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.ViewModel.User
{
    using System.ComponentModel.DataAnnotations;

    public class VerifyPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد تایید هویت شما")]
        public int MobileActiveCode { get; set; }
    }
}
