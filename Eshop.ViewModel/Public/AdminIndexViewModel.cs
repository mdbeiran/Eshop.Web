
using Eshop.ViewModel.Article;

namespace Eshop.ViewModel.Public
{
    public class AdminIndexViewModel
    {
        public int NewContactCount { get; set; }
        public ArticleCommentsViewModel ArticleComments { get; set; }

        public int NewOrderdCount { get; set; }
    }
}
