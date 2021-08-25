using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainClass.Articles
{
    public class ArticleCategories
    {

        #region Ctor

        public ArticleCategories()
        {
            
        }

        #endregion

        #region Properties

        [Key]
        public int ArticleCategoryID { get; set; }
        public int ArticleID { get; set; }
        public int CategoryID { get; set; }

        #endregion

        #region Relations

        public virtual ArticleCategory ArticleCategory { get; set; }
        public virtual Article Article { get; set; }

        #endregion
    }
}
