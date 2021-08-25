using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.DomainClass.Product
{
    public class ProductGroup
    {
        #region Ctor

        public ProductGroup()
        {
            
        }

        #endregion

        #region Properties

        [Key]
        public int GroupId { get; set; }


        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند از {1} کاراکتر بیشتر باشد")]
        public string GroupTitle { get; set; }


        [Display(Name = "زیر گروه ها")]
        public int? ParentId { get; set; }


        [Display(Name = "عنوان در URL")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند از {1} کاراکتر بیشتر باشد")]
        public string NameInUrl { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations


        [ForeignKey("ParentId")]
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public virtual ICollection<Product> MainProducts { get; set; }
        public virtual ICollection<Product> SubProducts { get; set; }

        #endregion

    }
}
