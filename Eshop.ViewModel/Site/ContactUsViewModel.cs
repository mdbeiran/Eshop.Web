using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eshop.DomainClass.Setting;

namespace Eshop.ViewModel.Site
{
    public class ContactUsViewModel
    {

        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        [Display(Name = "ایمیل")]
        public string FilterEmail { get; set; }

        [Display(Name = "وضعیت")]
        public string ContactState { get; set; }

        [Display(Name = "تلفن همراه")]
        public string FilterMobile { get; set; }

        [Display(Name = "نام شخص")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? fromDate { get; set; }

        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? toDate { get; set; }
        public IEnumerable<ContactUs> Contact { get; set; }
        
    }
}
