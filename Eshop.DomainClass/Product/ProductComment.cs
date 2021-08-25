using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.DomainClass.Product
{
    public class ProductComment
    {
        #region ctor

        public ProductComment()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int CommentId { get; set; }
        public int ProductId { get; set; }

        public int UserID { get; set; }

        [Display(Name = "متن نظر شما")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }

        public bool Isdelete { get; set; }

        public int? ParentId { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ParentId")]
        public virtual ICollection<ProductComment> ProductComments { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("UserID")]
        public virtual User.User User { get; set; }

        #endregion
    }
}
