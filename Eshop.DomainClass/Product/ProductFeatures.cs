using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.DomainClass.Product
{
    public class ProductFeatures
    {
        #region ctor

        public ProductFeatures()
        {
            
        }

        #endregion

        #region Properties

        [Key]
        public int FeatureId { get; set; }


        [Display(Name = "ویژگی")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FeatureTitle { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual ICollection<ProductSelectedFeatures> ProductSelectedFeatures{ get; set; }

        #endregion
    }
}
