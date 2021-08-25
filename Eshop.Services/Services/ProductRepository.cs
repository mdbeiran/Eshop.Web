using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Eshop.DataLayer.Contexts;
using Eshop.DomainClass.Product;
using Eshop.Services.Repositories;
using Eshop.ViewModel.Products;

namespace Eshop.Services.Services
{
    using Eshop.DomainClass.Order;

    public class ProductRepository : IProductRepository
    {
        #region ctor

        private EshopDbContext db;

        public ProductRepository(EshopDbContext db)
        {
            this.db = db;
        }

        #endregion

        #region Product Groups

        public IEnumerable<ProductGroup> GetProductGroups(int? parentId)
        {
            return db.ProductGroups.Where(a => a.ParentId == parentId);
        }

        public void Insertgroup(ProductGroup group)
        {
            db.ProductGroups.Add(group);
        }

        public ProductGroup GetgroupById(int groupId)
        {
            return db.ProductGroups.SingleOrDefault(g => g.GroupId == groupId);
        }

        public void UpdateProductgroup(ProductGroup group)
        {
            db.Entry(group).State = EntityState.Modified;
        }

        public List<ProductGroup> GetMainGroups()
        {
            return db.ProductGroups.Where(s => s.ParentId == null).ToList();
        }

        public List<ProductGroup> GetActiveMainGroups()
        {
            return db.ProductGroups.Where(s => s.ParentId == null && !s.IsDelete).ToList();
        }

        public List<ProductGroup> GetSubGroups(int parentId)
        {
            return db.ProductGroups.Where(s => s.ParentId == parentId).ToList();
        }

        public List<ProductGroup> GetActiveSubGroups(int parentId)
        {
            return db.ProductGroups.Where(s => s.ParentId == parentId && !s.IsDelete).ToList();
        }

        public List<ProductGroup> GetAllActiveGroups()
        {
            return db.ProductGroups.Where(s => !s.IsDelete).ToList();
        }

        public List<ProductGroup> GetProductSubgroup(int mainId)
        {
            return db.ProductGroups.Where(s => s.ParentId == mainId).ToList();
        }

        public bool IsExistUrlTitleInProductGroups(string nameInUrl)
        {
            return db.ProductGroups.Any(g => g.NameInUrl == nameInUrl);
        }

        #endregion

        #region Prodct features

        public List<ProductFeatures> GetProductFeatures()
        {
            return db.ProductFeatureses.ToList();
        }

        public void InsertProductFeature(ProductFeatures feature)
        {
            db.ProductFeatureses.Add(feature);
        }

        public ProductFeatures GetFeatureById(int featureId)
        {
            return db.ProductFeatureses.SingleOrDefault(s => s.FeatureId == featureId);
        }

        public void UpdateProductFeature(ProductFeatures feature)
        {
            db.Entry(feature).State = EntityState.Modified;
        }

        public void DeleteProductSelectedFreatures(int productId)
        {
            var features = db.ProductSelectedFeatures.Where(s => s.ProductId == productId).ToList();

            foreach (var feature in features)
            {
                if (feature.OrderSelectedFeatures.Any())
                {
                    var orderFeatures = feature.OrderSelectedFeatures.ToList();

                    foreach (var order in orderFeatures)
                    {

                    }
                }

                db.Entry(feature).State = EntityState.Deleted;
            }
        }

        #endregion

        #region Products

        public FilterProductsViewModel GetProductsByFilter(FilterProductsViewModel filter)
        {
            int take = filter.Take;
            int skip = (filter.PageId - 1) * take;
            FilterProductsViewModel data = new FilterProductsViewModel();
            IQueryable<Product> query = db.Products;

            #region State

            switch (filter.State)
            {
                case "All":
                    {
                        break;
                    }
                case "Active":
                    {
                        query = query.Where(p => p.IsActive && !p.IsDelete);
                        break;
                    }
                case "NotActive":
                    {
                        query = query.Where(p => !p.IsActive && !p.IsDelete);
                        break;
                    }
                case "Deleted":
                    {
                        query = query.Where(p => p.IsDelete);
                        break;
                    }
                case "IsExist":
                    {
                        query = query.Where(p => !p.IsDelete && p.IsExist);
                        break;
                    }
                case "NotExist":
                    {
                        query = query.Where(p => !p.IsDelete && !p.IsExist);
                        break;
                    }

            }

            #endregion

            #region Implementing Filters

            if (filter.ProductCode != null && filter.ProductCode != 0)
            {
                query = query.Where(s => s.ProductCode == filter.ProductCode);
                data.ProductCode = filter.ProductCode;
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(s => s.Title.Contains(filter.Title));
                data.Title = filter.Title;
            }


            if (filter.fromDate != null)
            {
                DateTime fromdate = new DateTime(filter.fromDate.Value.Year, filter.fromDate.Value.Month, filter.fromDate.Value.Day, new PersianCalendar());
                query = query.Where(u => u.CreateDate >= fromdate);
                data.fromDate = filter.fromDate.Value;
            }

            if (filter.toDate != null)
            {
                DateTime todate = new DateTime(filter.toDate.Value.Year, filter.toDate.Value.Month, filter.toDate.Value.Day, new PersianCalendar());
                query = query.Where(u => u.CreateDate <= todate);
                data.toDate = filter.toDate;
            }

            #endregion

            #region paging

            int thisPageCount = query.Count();
            if (thisPageCount % take > 0)
            {
                data.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                data.PageCount = thisPageCount / take;
            }

            data.ActivePage = filter.PageId;
            data.StartPage = filter.PageId - 3;
            data.EndPage = data.ActivePage + 3;
            if (data.StartPage <= 0) data.StartPage = 1;
            if (data.EndPage > data.PageCount) data.EndPage = data.PageCount;
            #endregion

            data.State = filter.State;
            data.Products = query.OrderByDescending(p => p.CreateDate).Skip(skip).Take(take).AsNoTracking().ToList();

            return data;
        }

        public void InsertProduct(Product product)
        {
            db.Products.Add(product);
        }

        public void InsertProductTags(string tags, int productId)
        {
            foreach (var tag in tags.Split(','))
            {
                ProductTags newTag = new ProductTags()
                {
                    ProductId = productId,
                    TagTitle = tag
                };
                db.ProductTags.Add(newTag);
            }
        }

        public void InsertProductFeature(ProductSelectedFeatures features)
        {
            db.ProductSelectedFeatures.Add(features);
        }

        public EditProductViewModel GetProductForEdit(int productId)
        {
            var product = db.Products.SingleOrDefault(s => s.ProductId == productId);
            EditProductViewModel data = new EditProductViewModel()
            {
                ProductId = productId,
                ProductCode = product.ProductCode,
                SinglePrice = product.SinglePrice,
                PublicPrice = product.PublicPrice,
                Title = product.Title,
                Text = product.Text,
                SingleProductImageName = product.SingleProductImageName,
                PublicProductImageName = product.PUblicProductImageName,
                Tags = string.Join(",", db.ProductTags.Where(s => s.ProductId == productId).Select(s => s.TagTitle).ToList()),
                MainGroupId = product.MainGroupId,
                SubGroupId = product.SubGroupId,
                IsActive = product.IsActive,
                IsExist = product.IsExist,
                ShortDescription = product.ShortDescription,
                ProductSelectedFeatureses = db.ProductSelectedFeatures.Where(s => s.ProductId == productId).ToList(),
                ProductSubGroups = GetActiveSubGroups(product.MainGroupId),
                ProductGalleries = db.ProductGalleries.Where(s => s.ProductId == productId).ToList()
            };


            return data;

        }

        public Product GetProductById(int productId)
        {
            return db.Products.SingleOrDefault(s => s.ProductId == productId);
        }

        public void DeleteProductTags(int productId)
        {
            var productTags = db.ProductTags.Where(s => s.ProductId == productId);
            foreach (var tag in productTags)
            {
                db.Entry(tag).State = EntityState.Deleted;
            }
        }

        public List<Product> GetMostSellProducts(int count)
        {
            return db.Products.Where(s => !s.IsDelete && s.IsActive).OrderByDescending(s => s.SellCount).Take(count).ToList();
        }

        public List<Product> GetSingleSellProducts(int count)
        {
            return db.Products.Where(s => s.IsActive && !s.IsDelete).OrderBy(o => Guid.NewGuid()).Take(count).ToList();
        }

        public List<Product> GetPublicSellProducts(int count)
        {
            return db.Products.Where(s => s.IsActive && !s.IsDelete).OrderBy(o => Guid.NewGuid()).Take(count).ToList();
        }

        public CategoriesInHomeViewModel GetCategoriesInHome()
        {
            CategoriesInHomeViewModel data = new CategoriesInHomeViewModel();
            data.ProductGroups = GetMainGroups();

            return data;
        }

        public SearchProductsViewModel SearchProducts(SearchProductsViewModel filter)
        {
            int take = filter.Take;
            int skip = (filter.PageId - 1) * take;
            SearchProductsViewModel data = new SearchProductsViewModel();
            IQueryable<Product> query = db.Products.Where(s => s.IsActive && !s.IsDelete);

            #region State

            #endregion

            #region Filters

            if (filter.GroupId != null && filter.GroupId != 0)
            {
                query = query.Where(s => s.MainGroupId == filter.GroupId || s.SubGroupId == filter.GroupId && s.IsActive && !s.IsDelete);
                data.GroupId = filter.GroupId;
            }

            if (filter.SinglePrice != null && filter.SinglePrice != 0)
            {
                query = query.Where(s => s.SinglePrice <= filter.SinglePrice);
                data.SinglePrice = filter.SinglePrice;
            }

            if (filter.PublicPrice != null && filter.PublicPrice != 0)
            {
                query = query.Where(s => s.PublicPrice <= filter.PublicPrice);
                data.PublicPrice = filter.PublicPrice;
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(s => s.Title.Contains(filter.Title));
                data.Title = filter.Title;
            }

            #endregion

            #region paging

            int thisPageCount = query.Count();
            if (thisPageCount % take > 0)
            {
                data.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                data.PageCount = thisPageCount / take;
            }

            data.ActivePage = filter.PageId;
            data.StartPage = filter.PageId - 3;
            data.EndPage = data.ActivePage + 3;
            if (data.StartPage <= 0) data.StartPage = 1;
            if (data.EndPage > data.PageCount) data.EndPage = data.PageCount;

            #endregion

            data.ProductGroups = db.ProductGroups.Where(g => !g.IsDelete).ToList();
            data.Products = query.OrderByDescending(o => o.CreateDate).Skip(skip).Take(take).AsNoTracking().ToList();

            return data;
        }

        public ProductDetailViewModel GetSingleProductDetail(int productId)
        {
            var product = db.Products.SingleOrDefault(p => p.ProductId == productId && p.IsActive);
            if (product == null)
            {
                return null;
            }

            ProductDetailViewModel data = new ProductDetailViewModel();
            data.Product = product;
            data.ProductGalleries = db.ProductGalleries.Where(g => g.ProductId == productId).OrderByDescending(s => s.GalleryId).AsNoTracking().ToList();
            data.ProductComment = db.ProductComments.Where(s => s.ProductId == productId && !s.Isdelete).OrderByDescending(o => o.CreateDate).AsNoTracking().ToList();
            data.SuggestedProducts = db.Products
                .Where(p => (p.MainGroupId == product.MainGroupId || p.SubGroupId == product.SubGroupId) && p.IsActive && !p.IsDelete)
                .OrderByDescending(s => s.SellCount).Take(9).AsNoTracking().ToList();
            data.ProductGroups = db.ProductGroups.Where(s => !s.IsDelete).AsNoTracking().ToList();
            return data;
        }

        public void InsertProdectComment(ProductComment comment)
        {
            db.ProductComments.Add(comment);
        }

        #endregion

        #region Product Gallery

        public void InsertGalleryImage(ProductGallery gallery)
        {
            db.ProductGalleries.Add(gallery);
        }

        public void DeleteGalleryImage(ProductGallery gallery)
        {
            db.Entry(gallery).State = EntityState.Deleted;
        }

        public ProductGallery GetGalleryById(int galleryId)
        {
            return db.ProductGalleries.SingleOrDefault(g => g.GalleryId == galleryId);
        }

        #endregion

        #region Order

        public void InsertProductOrder(Order order)
        {
            db.Orders.Add(order);
        }

        public void InsertOrderDetailForOrder(OrderDetail detail)
        {
            db.OrderDetails.Add(detail);
        }

        public void InsertOrderSelectedFeatures(int detailid, List<int> selectedFeaturesId)
        {
            foreach (var feature in selectedFeaturesId)
            {
                OrderSelectedFeature selectedFeature = new OrderSelectedFeature()
                {
                    OrderDetailId = detailid,
                    PF_Id = feature
                };

                db.OrderSelectedFeatures.Add(selectedFeature);
            }
        }

        public int GetMinimumForPublicProducts()
        {
            return db.SiteSettings.SingleOrDefault().PublicProductCount.Value;
        }

        public bool HasUserAnyOpenPrders(int userId)
        {
            return db.Orders.Any(s => s.UserId == userId && !s.IsFinally);
        }

        public Order GetUserLatestOrder(int userId)
        {
            return db.Orders.SingleOrDefault(s => s.UserId == userId && !s.IsFinally);
        }

        public List<Order> GetUserOrders(int userId)
        {
            return db.Orders.Where(s => s.UserId == userId).ToList();
        }

        public Order GetUserOpenOrder(int userId)
        {
            return db.Orders.SingleOrDefault(s => s.UserId == userId && !s.IsFinally);
        }

        public Order GetUserOrderById(int orderId)
        {
            return db.Orders.SingleOrDefault(s => s.OrderId == orderId);
        }

        public FilterOrders GetOrders(FilterOrders filter)
        {
            int take = 15;
            int skip = (filter.PageId - 1) * take;
            IQueryable<Order> ordersQuery = db.Orders.Where(s => s.IsFinally);

            #region User State

            switch (filter.State)
            {
                case "All":
                    {
                        break;
                    }
                case "NotSent":
                    {
                        ordersQuery = ordersQuery.Where(u => !u.IsSend);
                        break;
                    }
            }

            #endregion



            #region paging

            int thisPageCount = ordersQuery.Count();

            if (thisPageCount % take > 0)
            {
                filter.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                filter.PageCount = thisPageCount / take;
            }

            #endregion

            filter.Orders = ordersQuery.OrderByDescending(s => s.CreateDate).ToList();
            filter.ActivePage = filter.PageId;

            return filter;
        }

        public Order GetOrderById(int id)
        {
            var order = db.Orders.SingleOrDefault(s => s.OrderId == id);

            if (order != null)
                return order;

            return null;
        }

        public void SendOrderToUser(int orderId)
        {
            var order = db.Orders.SingleOrDefault(s => s.OrderId == orderId);

            if (order != null)
                order.IsSend = true;
        }

        public bool CanInsertDetail(List<int> selected, int orderId)
        {
            var orderfeatures = db.OrderSelectedFeatures.Where(s => s.OrderDetail.OrderId == orderId).Select(s => s.OrderDetail).ToList();

            var a = false;

            foreach (var features in orderfeatures.Select(s => s.OrderSelectedFeatures.Select(f=>f.PF_Id).ToList()).ToList())
            {
                a = features.All(selected.Contains) && features.Count == selected.Count;
            }

            if (a)
            {
                return false;
            }

            return true;
        }

        public OrderDetail GetOrderDetailById(int detailId)
        {
            return db.OrderDetails.SingleOrDefault(s => s.DetailId == detailId);
        }

        public void RemoveOrderDetailSelectedFeatures(int detailId)
        {
            var detail = db.OrderDetails.SingleOrDefault(s => s.DetailId == detailId);
            var features = detail.OrderSelectedFeatures.ToList();
            foreach (var feature in features)
            {
                if (feature != null)
                {
                    db.Entry(feature).State = EntityState.Deleted;
                }
            }
        }

        public void RemoveOrderDetail(OrderDetail detail)
        {
            db.Entry(detail).State = EntityState.Deleted;
        }

        public void SetOrderDetailPrices(int orderId)
        {
            var details = db.OrderDetails.Where(s => s.OrderId == orderId).ToList();

            var siteCount = db.SiteSettings.FirstOrDefault().PublicProductCount;

            var count = details.Sum(s => s.Count);

            if (count > siteCount)
            {
                foreach (var detail in details)
                {
                    detail.Price = detail.Product.PublicPrice;

                    db.SaveChanges();
                }
            }
            else
            {
                foreach (var detail in details)
                {
                    detail.Price = detail.Product.SinglePrice;

                    db.SaveChanges();
                }
            }
        }

        #endregion

        #region Product selected features

        public ProductSelectedFeatures GetSelectedFeatureById(int id)
        {
            return db.ProductSelectedFeatures.SingleOrDefault(s => s.PF_Id == id);
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
