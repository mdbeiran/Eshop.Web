using System;
using System.Collections.Generic;
using Eshop.DomainClass.Product;
using Eshop.ViewModel.Products;

namespace Eshop.Services.Repositories
{
    using Eshop.DomainClass.Order;

    public interface IProductRepository : IDisposable
    {

        #region Product Groups

        IEnumerable<ProductGroup> GetProductGroups(int? parentId);
        void Insertgroup(ProductGroup group);
        ProductGroup GetgroupById(int groupId);
        void UpdateProductgroup(ProductGroup group);
        List<ProductGroup> GetMainGroups();
        List<ProductGroup> GetActiveMainGroups();
        List<ProductGroup> GetSubGroups(int parentId);
        List<ProductGroup> GetActiveSubGroups(int parentId);
        List<ProductGroup> GetAllActiveGroups();
        List<ProductGroup> GetProductSubgroup(int mainId);
        bool IsExistUrlTitleInProductGroups(string nameInUrl);

        #endregion

        #region Prodct features

        List<ProductFeatures> GetProductFeatures();
        void InsertProductFeature(ProductFeatures feature);
        ProductFeatures GetFeatureById(int featureId);
        void UpdateProductFeature(ProductFeatures feature);
        void DeleteProductSelectedFreatures(int productId);

        #endregion

        #region Products

        FilterProductsViewModel GetProductsByFilter(FilterProductsViewModel filter);
        void InsertProduct(Product product);
        void InsertProductTags(string tags, int productId);
        void InsertProductFeature(ProductSelectedFeatures features);
        EditProductViewModel GetProductForEdit(int productId);
        Product GetProductById(int productId);
        void DeleteProductTags(int productId);
        List<Product> GetMostSellProducts(int count);
        List<Product> GetSingleSellProducts(int count);
        List<Product> GetPublicSellProducts(int count);
        CategoriesInHomeViewModel GetCategoriesInHome();
        SearchProductsViewModel SearchProducts(SearchProductsViewModel filter);
        ProductDetailViewModel GetSingleProductDetail(int productId);
        void InsertProdectComment(ProductComment comment);

        #endregion

        #region Product Gallery

        void InsertGalleryImage(ProductGallery gallery);
        void DeleteGalleryImage(ProductGallery gallery);
        ProductGallery GetGalleryById(int galleryId);

        #endregion

        #region Order

        void InsertProductOrder(Order order);
        void InsertOrderDetailForOrder(OrderDetail detail);
        void InsertOrderSelectedFeatures(int detailid, List<int> selectedFeaturesId);
        int GetMinimumForPublicProducts();
        bool HasUserAnyOpenPrders(int userId);
        Order GetUserLatestOrder(int userId);
        List<Order> GetUserOrders(int userId);
        Order GetUserOpenOrder(int userId);
        OrderDetail GetOrderDetailById(int detailId);
        void RemoveOrderDetailSelectedFeatures(int detailId);
        void RemoveOrderDetail(OrderDetail detail);
        void SetOrderDetailPrices(int orderId);
        Order GetUserOrderById(int orderId);
        FilterOrders GetOrders(FilterOrders filter);
        Order GetOrderById(int id);
        void SendOrderToUser(int orderId);

        bool CanInsertDetail(List<int> selected, int orderId);

        #endregion

        #region Product selected features 

        ProductSelectedFeatures GetSelectedFeatureById(int id);

        #endregion
    }
}