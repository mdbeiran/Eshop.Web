using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eshop.ViewModel.User
{

    public class FilterUserViewModel
    {
        #region Pagging

        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        #region Filter Options

        [Display(Name = "نام کاربری")]
        public string FilterUsername { get; set; }
        [Display(Name = "ایمیل")]
        public string FilterEmail { get; set; }
        [Display(Name = "وضعیت ")]
        public string userState { get; set; }
        [Display(Name = "تلفن همراه")]
        public string FilterMobile { get; set; }

        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? fromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? toDate { get; set; }

        #endregion

        #region Return Values

        public List<DomainClass.User.User> Users { get; set; }

        #endregion

    }
}
