using System.Collections.Generic;
using Eshop.DomainClass.Articles;

namespace Eshop.ViewModel.Article
{
    using Article = Eshop.DomainClass.Articles.Article;

    public class SingleArticleViewModel
    {
        #region Pagging

        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        public Article Article { get; set; }
        public List<ArticleComment> ArticleComments { get; set; }
        public int CommentsCount { get; set; }
    }
}
