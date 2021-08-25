using System;

namespace Eshop.ViewModel.Article
{
   public class LastArticleViewModel
    {
        public int ArticleID { get; set; }
        public string ArticleTitle { get; set; }
        public string ShortDescription { get; set; }
        public string ArticleImageName { get; set; }
        public int ArticleVisit { get; set; }
        public int ArticleComment { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
