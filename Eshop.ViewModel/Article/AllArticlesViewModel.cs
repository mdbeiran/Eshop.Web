using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eshop.DomainClass.Articles;

namespace Eshop.ViewModel.Article
{
    using Article = Eshop.DomainClass.Articles.Article;

    public class AllArticlesViewModel
    {
        #region Pagging

        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        #region Filter Options

        public string State { get; set; }
        public string Search { get; set; }
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? fromDate { get; set; }


        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? toDate { get; set; }

        public List<int> SelectedCategories { get; set; }

        public string CategoryUrl { get; set; }

        #endregion

        #region Return Data

        public int ArticlesCount { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
        public List<ArticleCategory> SearchCategories { get; set; }
        public List<Article> Articles { get; set; }

        #endregion
    }
}
