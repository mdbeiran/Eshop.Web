using System;
using System.Collections.Generic;

namespace Eshop.ViewModel.Article
{
   public class CommentArticleViewModel
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public string UserAvatar { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public int? ParentID { get; set; }
        public List<CommentArticleViewModel> subComments { get; set; }
        public DateTime CreateDate { get; set; }

        public CommentArticleViewModel()
        {
            subComments=new List<CommentArticleViewModel>();
        }

    }
}
