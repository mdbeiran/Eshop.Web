using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DomainClass.Product;

namespace Eshop.ViewModel.Products
{
    public class ProductDetailViewModel
    {
        #region Pagging

        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        public Product Product { get; set; }
        public List<ProductGallery> ProductGalleries { get; set; }
        public List<Product> SuggestedProducts { get; set; }
        public List<ProductComment> ProductComment { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }
    }
}
