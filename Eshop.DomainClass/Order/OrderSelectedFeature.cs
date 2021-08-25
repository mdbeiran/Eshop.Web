using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.DomainClass.Product;

namespace Eshop.DomainClass.Order
{
    public class OrderSelectedFeature
    {
        #region ctor

        public OrderSelectedFeature()
        {

        }

        #endregion

        #region properties

        [Key]
        public int OrderSelectedId { get; set; }

        [Display(Name = "جزییات سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int OrderDetailId { get; set; }

        [Display(Name = "ویژگی ثبت شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PF_Id { get; set; }

        #endregion

        #region relations

        [ForeignKey("OrderDetailId")]
        public virtual OrderDetail OrderDetail { get; set; }

        [ForeignKey("PF_Id")]
        public virtual ProductSelectedFeatures ProductSelectedFeatures { get; set; }
        #endregion
    }
}
