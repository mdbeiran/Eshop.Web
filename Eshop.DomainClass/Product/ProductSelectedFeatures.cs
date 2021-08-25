using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eshop.DomainClass.Order;

namespace Eshop.DomainClass.Product
{
    public class ProductSelectedFeatures
    {
        #region Ctor

        public ProductSelectedFeatures()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int PF_Id { get; set; }


        [Required]
        public int ProductId { get; set; }


        [Required]
        public int FeatureId { get; set; }
        

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Value { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        public virtual Product Product { get; set; }
        public virtual ProductFeatures ProductFeatures { get; set; }
        public virtual ICollection<OrderSelectedFeature> OrderSelectedFeatures { get; set; }

        #endregion
    }
}
