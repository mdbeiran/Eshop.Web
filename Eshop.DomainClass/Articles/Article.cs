namespace Eshop.DomainClass.Articles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;
    using Eshop.DomainClass.User;

    public class Article
    {
        #region Ctor

        public Article()
        {

        }

        #endregion

        #region Properties

        [Key]
        public int ArticleID { get; set; }


        [Display(Name = "نویسنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int AuthorID { get; set; }


        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400)]
        public string ArticleTitle { get; set; }


        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }


        [Display(Name = "مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ArticleText { get; set; }


        [Display(Name = "تصویر")]
        [MaxLength(200)]
        public string ArticleImageName { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }


        [Display(Name = "کاربران ویژه")]
        public bool IsVip { get; set; }


        [Display(Name = "درج نظر")]
        public bool CanInsertComment { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }


        public bool IsDelete { get; set; }


        #endregion

        #region Relations

        [ForeignKey("AuthorID")]
        public virtual User User { get; set; }
        public virtual ICollection<ArticleCategories> ArticleCategorieses { get; set; }
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        public virtual ICollection<ArticleVisit> ArticleVisits { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }

        #endregion
    }
}
