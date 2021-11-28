using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Eshop.DomainClass.Setting
{
    public class ContactUs
    {
        #region Constuctor

        public ContactUs()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int ContactId { get; set; }


        [Display(Name = "کاربر")]
        public int? UserId { get; set; }
        

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        public string Name { get; set; }


        [Display(Name = "موضوع پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string Subject { get; set; }


        [Display(Name = "موبایل")]
        [MaxLength(20)]
        [RegularExpression(@"(0|\+98)?([ ]|-|[()]){0,2}9[0|1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}",
            ErrorMessage = "شماره موبایل وارد شده اشتباه می باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Mobile { get; set; }


        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(2000)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }


        [Display(Name = "جواب")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Answer { get; set; }


        [Display(Name = "IP")]
        [MaxLength(20)]
        public string UserIP { get; set; }


        [Display(Name = "خوانده شده")]
        public bool IsRead { get; set; }


        [Display(Name = "تاریخ و ساعت")]
        public DateTime CreateDate { get; set; }

        #endregion

        #region Relations
        
        public virtual User.User User { get; set; }

        #endregion

    }
}
