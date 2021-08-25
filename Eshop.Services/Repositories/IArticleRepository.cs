using System;
using System.Collections.Generic;
using Eshop.DomainClass.Articles;
using Eshop.ViewModel.Article;

namespace Eshop.Services.Repositories
{
    public interface IArticleRepository : IDisposable
    {
        #region Article Categories

        IEnumerable<ArticleCategory> GetArticleCategories(int? parentId);
        void InsertCategory(ArticleCategory category);
        ArticleCategory GetCategoryById(int categoryId);
        void UpdateArticleCategory(ArticleCategory category);
        List<ArticleCategory> GetMainCategories();
        List<ArticleCategory> GetAllCategories();
        List<ArticleCategory> GetArticleSubCategory(int mainId);
        void InsertArticleCategory(List<int> selectedCategories, int articleId);
        List<ArticleCategories> GetArticleSelectedCategories(int articleId);
        void DeleteArticleCategoriesByArticleId(int articleId);

        #endregion

        #region Articles

        void InsertArticle(Article article);
        EditArticleViewModel GetArticleForEdit(int articleId);
        Article GetArticleByArticleId(int acticleId);
        bool IsExistUrlTitleInArticles(string urlTitle);
        SingleArticleViewModel GetSingleArticle(string articleTitle,int pageId);
        IEnumerable<Article> GetLastArticles(int count);
        bool IsExistArticleWithTite(string title);
        void InsertVisitForArticle(int articleId, string userIp);
        AllArticlesViewModel GetAllArticlesByFilter(int pageId, List<int> selectedCategories, DateTime? fromDate,
            DateTime? toDate, string state = "", string title = "", string categoryUrl = "", string search = "");

        #endregion

        #region Article Tags

        void InsertArticleTags(string tags, int articleId);
        void RemoveArticleTags(int articleId);

        #endregion

        #region Article Comments

        void InsertCommentForArticle(ArticleComment commnet);
        IEnumerable<ArticleComment> GetArticleComments(int articleId, int pageId);
        ArticleComment GetArticleCommenByCommentId(int commentId);
        ArticleComment GetSingleArticleComment();
        void UpdateArticleComment(ArticleComment comment);

        #endregion
    }
}