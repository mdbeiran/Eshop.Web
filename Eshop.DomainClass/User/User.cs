using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eshop.DomainClass.Articles;
using Eshop.DomainClass.Product;

namespace Eshop.DomainClass.User
{
    using Eshop.DomainClass.Order;

    public class User
    {
        #region Constructor

        public User()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int UserId { get; set; }


        [Display(Name = "نام نمایشی")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }


        [Display(Name = "تلفن همراه")]
        [MaxLength(20, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        [RegularExpression(@"(0|\+98)?([ ]|-|[()]){0,2}9[0|1|2|3|4|9]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}",
            ErrorMessage = "شماره موبایل وارد شده اشتباه می باشد.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Mobile { get; set; }


        [MaxLength(500, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Address { get; set; }


        [Display(Name = "کلمه عبور")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }


        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد فعال سازی شماره موبایل")]
        public int MobileActiveCode { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime RegisterDate { get; set; }


        [Display(Name = "تایید نهایی")]
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public bool ViewByAdmin { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        #endregion

    }
}
