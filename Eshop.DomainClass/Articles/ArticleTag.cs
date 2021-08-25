using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainClass.Articles
{
    public class ArticleTag
    {

        #region Ctor

        public ArticleTag()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int ArticleTagID { get; set; }


        public int ArticleID { get; set; }


        [Display(Name = "تگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string Tag { get; set; }

        #endregion

        #region Relations

        public virtual Article Article { get; set; }

        #endregion

    }
}
