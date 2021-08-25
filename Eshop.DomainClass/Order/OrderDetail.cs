using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.DomainClass.Order
{
    using Product = Eshop.DomainClass.Product.Product;

    public class OrderDetail
    {
        #region Ctor

        public OrderDetail()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int DetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public virtual ICollection<OrderSelectedFeature> OrderSelectedFeatures { get; set; }

        #endregion
    }
}
