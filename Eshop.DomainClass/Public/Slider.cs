using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Eshop.DomainClass.Public
{
    public class Slider
    {
        public Slider()
        {

        }

        [Key]
        public int ID { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }


        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Url]
        public string Url { get; set; }


        [Display(Name = "تصویر")]
        public string ImageName { get; set; }


        [Display(Name = "بازدید")]
        public int Visit { get; set; }


        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }


        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [AllowHtml]
        public string Text { get; set; }
    }
}
