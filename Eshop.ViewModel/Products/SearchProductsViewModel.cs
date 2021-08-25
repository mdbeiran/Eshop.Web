using System;
using System.Collections.Generic;
using Eshop.DomainClass.Product;

namespace Eshop.ViewModel.Products
{
    public class SearchProductsViewModel
    {

        #region Pagging

        public int Take { get; set; } = 15;
        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        #region Filter Option

        public int? SinglePrice { get; set; }
        public int? PublicPrice { get; set; }
        public int? GroupId { get; set; }
        public string Title { get; set; }

        #endregion

        #region Return Data

        public string State { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }

        #endregion

    }
}
