using System.Collections.Generic;
using Eshop.DomainClass.Articles;

namespace Eshop.ViewModel.Article
{
    public class ArticleCommentsViewModel
    {
        public int NewCommentsCount { get; set; }
        public List<ArticleComment> ArticleComments { get; set; }
    }
}
