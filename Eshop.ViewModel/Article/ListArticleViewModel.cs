using System;
using System.Collections.Generic;
using Eshop.DomainClass.Articles;

namespace Eshop.ViewModel.Article
{
   public class ListArticleViewModel
    {
        public ListArticleViewModel()
        {
            ArticleCategories=new List<ArticleCategory>();
        }

        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string CategoryTitle { get; set; }
        public string AuthorName { get; set; }
        public int VisitCount { get; set; }
        public int CommentCount { get; set; }
        public int Rate { get; set; }
        public int RelatedArticleCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsCommentActive { get; set; }
        public bool IsVip { get; set; }
        public DateTime CreateDate { get; set; }
        public List<ArticleCategory> ArticleCategories { get; set; }

    }
}
