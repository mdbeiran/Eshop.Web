using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DomainClass.Product;

namespace Eshop.ViewModel.Products
{
    public class CategoriesInHomeViewModel
    {
        public List<ProductGroup> ProductGroups { get; set; }
        public List<Product> Products { get; set; }
    }
}
