using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.DomainClass.Product
{
    public class ProductTags
    {
        #region Ctor

        public ProductTags()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int TagId { get; set; }


        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductId { get; set; }


        [Display(Name = "عنوان تگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string TagTitle { get; set; }


        #endregion

        #region Relations

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        #endregion
    }
}
