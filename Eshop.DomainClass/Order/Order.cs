using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.DomainClass.Order
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Eshop.DomainClass.User;

    public class Order
    {
        #region Ctor

        public Order()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        public bool IsFinally { get; set; }

        public bool IsSend { get; set; }

        public string StatusCode { get; set; }

        #endregion

        #region Relations

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
