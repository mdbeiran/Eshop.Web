using System;
using Eshop.DataLayer.Contexts;
using Eshop.Services.Repositories;
using Eshop.Services.Services;

namespace Eshop.Services.Context
{
    public class EshopUOW : IDisposable
    {
        private EshopDbContext db = new EshopDbContext();

        #region User Repositories

        private IUserRepository _userRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(db);
                }

                return _userRepository;
            }
        }

        #endregion

        #region Slider Repository

        private ISliderRepository _sliderRepository;

        public ISliderRepository SliderRepository
        {
            get
            {
                if (_sliderRepository == null)
                {
                    _sliderRepository = new SliderRepository(db);
                }

                return _sliderRepository;
            }
        }

        #endregion

        #region Articles

        private IArticleRepository _articleRepository;

        public IArticleRepository ArticleRepository
        {
            get
            {
                if (_articleRepository == null)
                {
                    _articleRepository = new ArticleRepository(db);
                }

                return _articleRepository;
            }
        }

        #endregion

        #region Site Repository

        private ISiteRepository _siteRepository;

        public ISiteRepository SiteRepository
        {
            get
            {
                if (_siteRepository == null)
                {
                    _siteRepository = new SiteRepository(db);
                }

                return _siteRepository;
            }
        }

        #endregion

        #region Product Repository

        private IProductRepository _productRepository;

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(db);
                }

                return _productRepository;
            }
        }

        #endregion

        #region Save

        public void Save()
        {
            db.SaveChanges();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            db?.Dispose();
        }

        #endregion

    }
}
