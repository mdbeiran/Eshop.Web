using System.Web;
using System.Configuration;

namespace Eshop.Utilitiy.Security
{
    public class RequestChecker
    {
        public static bool IsRequestFromThisSite()
        {
            return HttpContext.Current.Request.UrlReferrer != null && ConfigurationManager.AppSettings["SiteDomain"]
                       .ToLower().Contains(HttpContext.Current.Request.UrlReferrer.Host.ToLower());
        }

        public static string GetUserIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
    }
}
