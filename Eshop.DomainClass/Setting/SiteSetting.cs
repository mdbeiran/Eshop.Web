using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Eshop.DomainClass.Setting
{
    public class SiteSetting
    {
        public SiteSetting()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SettingID { get; set; }


        [Display(Name = "نام سایت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string SiteName { get; set; }

        [Display(Name = "شرح سایت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string SiteDescription { get; set; }

        [Display(Name = "ایمیل سایت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string SiteEmail { get; set; }

        [Display(Name = "تلفن سایت")]
        [MaxLength(250)]
        public string SiteTell { get; set; }

        [Display(Name = "موبایل سایت")]
        [MaxLength(200)]
        public string SiteMobile { get; set; }

        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Address { get; set; }


        [Display(Name = "شماره فکس")]
        [MaxLength(200)]
        public string SiteFax { get; set; }

        [Display(Name = "تلفن پشتیبانی")]
        [MaxLength(200)]
        public string SupportTell { get; set; }

        [Display(Name = "ایمیل پشتیبانی")]
        [MaxLength(250)]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string SupportEmail { get; set; }

        [Display(Name = "متن کپی رایت")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string CopyRight { get; set; }


        [Display(Name = "قوانین سایت")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string SiteRules { get; set; }

        [Display(Name = "تعداد کالای عمده برای خرید عمده")]
        public int? PublicProductCount { get; set; }
    }
}
