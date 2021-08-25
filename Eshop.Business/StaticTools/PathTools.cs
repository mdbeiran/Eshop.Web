using System.Web;

namespace Eshop.Business.StaticTools
{
    public static class PathTools
    {
        public static readonly string NoPhoto = "/Images/DefaultImages/no-photo.png";
        public static readonly string UserAvatar = "/Images/DefaultImages/UserAvatarDefault.jpg";

        #region User

        public static readonly string SiteImagePath = HttpContext.Current.Server.MapPath("/Images/");
        public static readonly string UserAvatarFullPath = HttpContext.Current.Server.MapPath("/Images/User/UserAvatar/Image/");
        public static readonly string UserAvatarThumbFullPath = HttpContext.Current.Server.MapPath("/Images/User/UserAvatar/Thumb/");
        public static readonly string UserAvatarDefaultFullPath = HttpContext.Current.Server.MapPath("/Images/User/UserAvatar/DefaultAvatar/");

        public static readonly string UserAvatarPath = "/Images/User/UserAvatar/Image/";
        public static readonly string UserAvatarThumbPath = "/Images/User/UserAvatar/Thumb/";
        public static readonly string UserAvatarDefaultPath = "/Images/User/DefaultImages/";
        public static readonly string UserAvatarDefaultPathWithImage = "/Images/User/DefaultImages/UserAvatarDefault.jpg";
        public static readonly string UserAvatarDefaultImage = "NoImage.jpg";

        #endregion

        #region Pages

        public static readonly string OriginPageImagePath = "/Images/Pages/Origin/";
        public static readonly string OriginPageImageFullPath = HttpContext.Current.Server.MapPath("/Images/Pages/Origin/");
        public static readonly string ThumbPageImagePath = "/Images/Pages/Thumb/";
        public static readonly string ThumbPageImageFullPath = HttpContext.Current.Server.MapPath("/Images/Pages/Thumb/");
        public static readonly string PagesDefaultPathWithImage = "/Images/User/DefaultImages/no-photo.png";

        #endregion

        #region Articles

        public static readonly string ArticleImagePath = "/Images/Articles/Origin/";
        public static readonly string ArticleImageThumbPath = "/Images/Articles/Thumb/";

        public static readonly string ArticleImageFullPath = HttpContext.Current.Server.MapPath("/Images/Articles/Origin/");
        public static readonly string ArticleImageThumbFullPath = HttpContext.Current.Server.MapPath("/Images/Articles/Thumb/");

        #endregion

        #region Slider

        public static readonly string OriginSliderImagePath = "/Images/Slider/Origin/";
        public static readonly string OriginSliderImageFullPath = HttpContext.Current.Server.MapPath("/Images/Slider/Origin/");
        public static readonly string ThumbSliderImagePath = "/Images/Slider/Thumb/";
        public static readonly string ThumbSliderImageFullPath = HttpContext.Current.Server.MapPath("/Images/Slider/Thumb/");

        #endregion

        #region Products

        public static readonly string ProductImagePath = "/Images/Products/Origin/";
        public static readonly string ProductImageThumbPath = "/Images/Products/Thumb/";
        public static readonly string ProductImageGalleryOriginPath = "/Images/ProductGallery/Origin/";
        public static readonly string ProductImageGalleryThumbPath = "/Images/ProductGallery/Thumb/";

        public static readonly string ProductImageFullPath = HttpContext.Current.Server.MapPath("/Images/Products/Origin/");
        public static readonly string ProductImageThumbFullPath = HttpContext.Current.Server.MapPath("/Images/Products/Thumb/");

        public static readonly string ProductImageGalleryFullPath = HttpContext.Current.Server.MapPath("/Images/ProductGallery/Origin/");
        public static readonly string ProductImageGalleryThumbFullPath = HttpContext.Current.Server.MapPath("/Images/ProductGallery/Thumb/");

        #endregion


    }
}
