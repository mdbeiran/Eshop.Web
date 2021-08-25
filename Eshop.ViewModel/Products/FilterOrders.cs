using System.Collections.Generic;
using Eshop.DomainClass.Order;

namespace Eshop.ViewModel.Products
{
    public class FilterOrders
    {
        public string State { get; set; }

        public List<Order> Orders { get; set; }

        #region Pagging

        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion
    }
}
