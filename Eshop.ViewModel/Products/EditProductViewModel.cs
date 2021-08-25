using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Eshop.DomainClass.Product;

namespace Eshop.ViewModel.Products
{
   public class EditProductViewModel
    {
        public int ProductId { get; set; }


        [Display(Name = "گروه اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int MainGroupId { get; set; }


        [Display(Name = "زیر گروه")]
        public int? SubGroupId { get; set; }


        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductCode { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(360, ErrorMessage = "{0} نمیتواند از {1} کاراکتر بیشتر باشد")]
        public string Title { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(360, ErrorMessage = "{0} نمیتواند از {1} کاراکتر بیشتر باشد")]
        public string ShortDescription { get; set; }


        [Display(Name = "عکس خرده")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند از {1} کاراکتر بیشتر باشد")]
        public string SingleProductImageName { get; set; }


        [Display(Name = "عکس عمده")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند از {1} کاراکتر بیشتر باشد")]
        public string PublicProductImageName { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        public string Text { get; set; }


        [Display(Name = "قیمت خرده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int SinglePrice { get; set; } = 30000;


        [Display(Name = "قیمت عمده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PublicPrice { get; set; } = 25000;


        [Display(Name = "موجود هست")]
        public bool IsExist { get; set; }


        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }

        public string Tags { get; set; }

        public List<ProductSelectedFeatures> ProductSelectedFeatureses { get; set; }
        public List<ProductGroup> ProductSubGroups { get; set; }

        public List<ProductGallery> ProductGalleries { get; set; }

    }
}
