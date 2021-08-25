using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Eshop.DataLayer.Contexts;
using Eshop.DomainClass.Articles;
using Eshop.Services.Repositories;
using Eshop.ViewModel.Article;

namespace Eshop.Services.Services
{
    public class ArticleRepository : IArticleRepository
    {
        #region Context

        private readonly EshopDbContext db;

        public ArticleRepository(EshopDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Article Categories

        public IEnumerable<ArticleCategory> GetArticleCategories(int? parentId)
        {
            return db.ArticleCategories.Where(a => a.ParentID == parentId && !a.IsDelete);
        }

        public void InsertCategory(ArticleCategory category)
        {
            db.ArticleCategories.Add(category);
        }

        public ArticleCategory GetCategoryById(int categoryId)
        {
            return db.ArticleCategories.Single(s => s.CategoryID == categoryId);
        }

        public void UpdateArticleCategory(ArticleCategory category)
        {
            db.Entry(category).State = EntityState.Modified;
        }

        public List<ArticleCategory> GetMainCategories()
        {
            return db.ArticleCategories.Where(a => a.ParentID == null && !a.IsDelete).ToList();
        }

        public List<ArticleCategory> GetAllCategories()
        {
            return db.ArticleCategories.Where(s => !s.IsDelete).ToList();
        }

        public List<ArticleCategory> GetArticleSubCategory(int mainId)
        {
            return db.ArticleCategories.Where(a => a.ParentID == mainId && !a.IsDelete).ToList();
        }

        public void InsertArticleCategory(List<int> selectedCategories, int articleId)
        {
            foreach (var categoryId in selectedCategories)
            {
                ArticleCategories articeCategories = new ArticleCategories()
                {
                    CategoryID = categoryId,
                    ArticleID = articleId
                };
                db.ArticleCategory.Add(articeCategories);
            }
        }

        public List<ArticleCategories> GetArticleSelectedCategories(int articleId)
        {
            return db.ArticleCategory.Where(s => s.ArticleID == articleId).ToList();
        }

        public void DeleteArticleCategoriesByArticleId(int articleId)
        {
            var articleCategories = db.ArticleCategory.Where(s => s.ArticleID == articleId);
            foreach (var category in articleCategories)
            {
                if (category != null)
                {
                    db.Entry(category).State = EntityState.Deleted;
                }
            }
        }

        #endregion

        #region Articles


        public AllArticlesViewModel GetAllArticlesByFilter(int pageId, List<int> selectedCategories, DateTime? fromDate, DateTime? toDate,
    string state = "", string title = "", string categoryUrl = "", string search = "")
        {
            int take = 12;
            int skip = (pageId - 1) * take;
            IQueryable<Article> articlesList = db.Articles;
            AllArticlesViewModel articles = new AllArticlesViewModel();

            #region Set State

            switch (state)
            {
                case "All":
                    {
                        break;
                    }
                case "Active":
                    {
                        articlesList = articlesList.Where(a => a.IsActive && !a.IsDelete);
                        break;
                    }

                case "Deleted":
                    {
                        articlesList = articlesList.Where(a => a.IsDelete);
                        break;
                    }
            }

            #endregion

            #region Implementation Filters

            if (!String.IsNullOrEmpty(search))
            {
                articlesList = articlesList.Where(a =>
                    a.ArticleTitle.Contains(search) || a.ArticleText.Contains(search) ||
                    a.ShortDescription.Contains(search) || a.ArticleTags.Any(t => t.Tag.Contains(search)));
            }

            if (fromDate != null)
            {
                DateTime fromdate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, new PersianCalendar());
                articlesList = articlesList.Where(u => u.CreateDate >= fromdate);
                articles.fromDate = fromDate.Value;
            }

            if (toDate != null)
            {
                DateTime todate = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, new PersianCalendar());
                articlesList = articlesList.Where(u => u.CreateDate <= todate);
                articles.toDate = toDate.Value;
            }

            if (selectedCategories != null)
            {
                articlesList = db.ArticleCategory.Where(s => selectedCategories.Contains(s.CategoryID)).Select(a => a.Article);
            }

            if (!string.IsNullOrEmpty(title))
            {
                articlesList = articlesList.Where(a => a.ArticleTitle.Contains(title));
            }

            if (!string.IsNullOrEmpty(categoryUrl))
            {
                articlesList = articlesList.Where(a => a.ArticleCategorieses.Any(s => s.ArticleCategory.NameInUrl == categoryUrl));
                articles.ArticleCategory = db.ArticleCategories.SingleOrDefault(s => s.NameInUrl == categoryUrl);
            }

            #endregion

            #region Fill Return Date

            articles.State = state;
            articles.SelectedCategories = selectedCategories;
            articles.Title = title;
            articles.SearchCategories = GetAllCategories();
            articles.CategoryUrl = categoryUrl;

            #endregion

            #region paging

            int thisPageCount = articlesList.Count();
            if (thisPageCount % take > 0)
            {
                articles.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                articles.PageCount = thisPageCount / take;
            }

            articles.ActivePage = pageId;
            articles.StartPage = pageId - 3;
            articles.EndPage = articles.ActivePage + 3;
            if (articles.StartPage <= 0) articles.StartPage = 1;
            if (articles.EndPage > articles.PageCount) articles.EndPage = articles.PageCount;

            #endregion

            articles.Title = title;
            articles.CategoryUrl = categoryUrl;
            articles.ArticlesCount = thisPageCount;
            articles.Search = search;
            articles.Articles = articlesList.OrderByDescending(s => s.CreateDate).Skip(skip).Take(take).Distinct().AsNoTracking().ToList();
            return articles;
        }

        public void InsertArticle(Article article)
        {
            db.Articles.Add(article);
        }

        public EditArticleViewModel GetArticleForEdit(int articleId)
        {
            var article = db.Articles.Single(s => s.ArticleID == articleId);
            EditArticleViewModel editArticle = new EditArticleViewModel()
            {
                ArticleID = article.ArticleID,
                ArticleImageName = article.ArticleImageName,
                ArticleTags = string.Join(",", db.ArticleTags.Where(s => s.ArticleID == articleId).Select(s => s.Tag).ToList()),
                ArticleText = article.ArticleText,
                ShortDescription = article.ShortDescription,
                ArticleTitle = article.ArticleTitle,
                IsActive = article.IsActive,
                CanInsertComment = article.CanInsertComment,
                ArticleSelectedCategorieses = db.ArticleCategory.Where(s => s.ArticleID == articleId).ToList(),
                Categories = db.ArticleCategories.ToList()
            };
            return editArticle;
        }

        public Article GetArticleByArticleId(int acticleId)
        {
            return db.Articles.SingleOrDefault(s => s.ArticleID == acticleId);
        }

        public bool IsExistUrlTitleInArticles(string urlTitle)
        {
            return db.ArticleCategories.Any(a => a.NameInUrl == urlTitle);
        }

        public SingleArticleViewModel GetSingleArticle(string articleTitle, int pageId)
        {
            int take = 12;
            int skip = (pageId - 1) * take;

            SingleArticleViewModel article = new SingleArticleViewModel();
            var singleArticle = db.Articles.SingleOrDefault(a => a.ArticleTitle == articleTitle);
            if (singleArticle == null)
            {
                return null;
            }

            IQueryable<ArticleComment> comments = db.ArticleComments.Where(c => c.ArticleID == singleArticle.ArticleID && !c.IsDelete);

            #region Pagging

            int thisPageCount = comments.Count();
            if (thisPageCount % take > 0)
            {
                article.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                article.PageCount = thisPageCount / take;
            }

            article.ActivePage = pageId;
            article.StartPage = pageId - 3;
            article.EndPage = article.ActivePage + 3;
            if (article.StartPage <= 0) article.StartPage = 1;
            if (article.EndPage > article.PageCount) article.EndPage = article.PageCount;

            #endregion

            article.Article = singleArticle;
            article.CommentsCount = thisPageCount;
            article.ArticleComments = comments.OrderByDescending(o => o.CreateDate).Skip(skip).Take(take).ToList();
            return article;
        }

        public IEnumerable<ArticleComment> GetArticleComments(int articleId, int pageId)
        {
            int take = 12;
            int skip = (pageId - 1) * take;

            IQueryable<ArticleComment> comments = db.ArticleComments.Where(c => c.ArticleID == articleId && !c.IsDelete);

            return comments.OrderByDescending(o => o.CreateDate).Skip(skip).Take(take).ToList();
        }

        public ArticleComment GetArticleCommenByCommentId(int commentId)
        {
            return db.ArticleComments.SingleOrDefault(c => c.CommantID == commentId);
        }

        public ArticleComment GetSingleArticleComment()
        {
            var article = db.ArticleComments.Where(c => !c.IsRead && !c.IsDelete).OrderByDescending(o => o.CreateDate).Skip(4).Take(1).AsNoTracking().SingleOrDefault();
            return article;
        }

        public void UpdateArticleComment(ArticleComment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
        }

        public IEnumerable<Article> GetLastArticles(int count)
        {
            return db.Articles.OrderByDescending(o => o.CreateDate).Skip(0).Take(count).ToList();
        }

        public bool IsExistArticleWithTite(string title)
        {
            return db.Articles.Any(a => a.ArticleTitle == title);
        }

        public void InsertVisitForArticle(int articleId, string userIp)
        {
            if (!db.ArticleVisits.Any(a => a.ArticleID == articleId && a.IP == userIp))
            {
                ArticleVisit visit = new ArticleVisit()
                {
                    ArticleID = articleId,
                    IP = userIp
                };

                db.ArticleVisits.Add(visit);
            }
        }

        #endregion

        #region Article Tags

        public void InsertArticleTags(string tags, int articleId)
        {
            foreach (var tag in tags.Split(','))
            {
                ArticleTag articleTag = new ArticleTag()
                {
                    ArticleID = articleId,
                    Tag = tag
                };
                db.ArticleTags.Add(articleTag);
            }
        }

        public void RemoveArticleTags(int articleId)
        {
            var tags = db.ArticleTags.Where(s => s.ArticleID == articleId);
            foreach (var tag in tags)
            {
                db.Entry(tag).State = EntityState.Deleted;
            }
        }

        public void InsertCommentForArticle(ArticleComment commnet)
        {
            db.ArticleComments.Add(commnet);
        }


        #endregion

        #region Dispose
        public void Dispose()
        {
            db.Dispose();
        }


        #endregion
    }
}
