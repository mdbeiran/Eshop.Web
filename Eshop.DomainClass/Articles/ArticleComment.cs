using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Eshop.DomainClass.Articles
{
    public class ArticleComment
    {
        public ArticleComment()
        {
            
        }

        [Key]
        public int CommantID { get; set; }

        public int ArticleID { get; set; }

        public int UserID { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر داشته باشد")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Name = "متن نظر")]
        public string Comment { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsRead { get; set; }
        public int? ParentID { get; set; }

        [ForeignKey("UserID")]
        public virtual User.User User { get; set; }


        [ForeignKey("ArticleID")]
        public virtual Article Article { get; set; }
        [ForeignKey("ParentID")]
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }


    }
}
