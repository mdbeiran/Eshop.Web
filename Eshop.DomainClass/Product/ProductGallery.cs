using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.DomainClass.Product
{
    public class ProductGallery
    {
        #region ctor

        public ProductGallery()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int GalleryId { get; set; }


        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductId { get; set; }


        [Display(Name = "نام تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string ImageName { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        #endregion
    }
}
