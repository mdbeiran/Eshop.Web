using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.DomainClass.Articles
{
    public class ArticleCategory
    {
        #region Ctor

        public ArticleCategory()
        {
            
        }

        #endregion

        #region Properties

        [Key]
        public int CategoryID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(260)]
        public string Title { get; set; }


        [Display(Name = "عنوان در Url")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string NameInUrl { get; set; }


        public int? ParentID { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }


        public bool IsDelete { get; set; }

        #endregion

        #region Relations

        [ForeignKey("ParentID")]
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }

        #endregion
    }
}
