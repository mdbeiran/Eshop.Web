using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.DomainClass.Product;

namespace Eshop.ViewModel.Products
{
    public class FilterProductsViewModel
    {
        #region Pagging

        public int Take { get; set; } = 6;
        public int PageId { get; set; } = 1;
        public int PageCount { get; set; }
        public int ActivePage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        #endregion

        #region Filter Search Options

        public string State { get; set; } = "All";


        [Display(Name = "تلفن همراه")]
        public string Title { get; set; }


        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? fromDate { get; set; }


        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime? toDate { get; set; }


        [Display(Name = "کد محصول")]
        public int? ProductCode { get; set; }


        #endregion

        #region Returned Data

        public List<Product> Products { get; set; }

        #endregion
    }
}
