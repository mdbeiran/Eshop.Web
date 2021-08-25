using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Eshop.DomainClass.Articles;

namespace Eshop.ViewModel.Article
{
    public class CreateArticleViewModel
    {
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

        [Display(Name = "درج نظر")]
        public bool CanInsertComment { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }

        public string ArticleTags { get; set; }

        public List<ArticleCategory> Categories { get; set; }
        public List<int> SelectedCategories { get; set; }
    }
}
