using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainClass.Articles
{
    public class ArticleVisit
    {
        public ArticleVisit()
        {

        }

        [Key]
        public int VisitID { get; set; }
        public int ArticleID { get; set; }

        [MaxLength(100)]
        [Required]
        public string IP { get; set; }

        public virtual Article Article { get; set; }
    }
}
