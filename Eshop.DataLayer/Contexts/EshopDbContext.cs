using System.Data.Entity;
using Eshop.DomainClass.Articles;
using Eshop.DomainClass.Product;
using Eshop.DomainClass.Public;
using Eshop.DomainClass.Setting;
using Eshop.DomainClass.User;

namespace Eshop.DataLayer.Contexts
{
    using Eshop.DomainClass.Order;

    public class EshopDbContext : DbContext
    {
        #region Ctor

        public EshopDbContext() : base()
        {

        }

        #endregion

        #region Users

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSelectedRole> UserSelectedRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePersmissions> RolePersmissions { get; set; }

        #endregion

        #region Public

        public DbSet<Slider> Sliders { get; set; }

        #endregion

        #region Articles

        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategories> ArticleCategory { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<ArticleVisit> ArticleVisits { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }

        #endregion

        #region Setting

        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }

        #endregion

        #region Product

        public DbSet<ProductTags> ProductTags { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductFeatures> ProductFeatureses { get; set; }
        public DbSet<ProductSelectedFeatures> ProductSelectedFeatures { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }

        #endregion

        #region Orders

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderSelectedFeature> OrderSelectedFeatures { get; set; }

        #endregion

        #region Fluent Api

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(x => x.ArticleComments)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserID)
                .WillCascadeOnDelete(false);

            builder.Entity<ProductGroup>()
                .HasMany(s => s.MainProducts)
                .WithRequired(s => s.Group)
                .HasForeignKey(s => s.MainGroupId)
                .WillCascadeOnDelete(false);

            builder.Entity<ProductGroup>()
                .HasMany(s => s.SubProducts)
                .WithRequired(s => s.SubGroup)
                .HasForeignKey(s => s.SubGroupId)
                .WillCascadeOnDelete(false);

            builder.Entity<User>()
                .HasMany(s => s.ProductComments)
                .WithRequired(s => s.User)
                .HasForeignKey(s => s.UserID)
                .WillCascadeOnDelete(false);

            builder.Entity<ProductSelectedFeatures>()
                .HasMany(s => s.OrderSelectedFeatures)
                .WithRequired(s => s.ProductSelectedFeatures)
                .HasForeignKey(s => s.PF_Id)
                .WillCascadeOnDelete(false);
        }


        #endregion
    }
}

