using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Eshop.DomainClass.Product
{
    using Eshop.DomainClass.Order;

    public class Product
    {
        #region Ctor

        public Product()
        {

        }

        #endregion

        #region Properties

        [Key]
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
        public string PUblicProductImageName { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        public string Text { get; set; }


        [Display(Name = "قیمت خرده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int SinglePrice { get; set; }


        [Display(Name = "قیمت عمده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PublicPrice { get; set; }


        [Display(Name = "تاریخ انتشار")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }


        [Display(Name = "تعداد فروش")]
        public long SellCount { get; set; }


        [Display(Name = "موجود هست")]
        public bool IsExist { get; set; }


        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }


        #endregion

        #region Relations

        [ForeignKey("MainGroupId")]
        public virtual ProductGroup Group { get; set; }

        [ForeignKey("SubGroupId")]
        public virtual ProductGroup SubGroup { get; set; }
        public virtual ICollection<ProductTags> ProductTags { get; set; }
        public virtual ICollection<ProductSelectedFeatures> ProductSelectedFeatures { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductGallery> ProductGalleries { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
