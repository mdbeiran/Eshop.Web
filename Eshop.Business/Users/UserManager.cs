using System;
using System.Web;
using System.Web.Security;

namespace Eshop.Business.Users
{
    public class UserManager
    {

        public static int GetCurrentUserId()
        {
            return int.Parse(HttpContext.Current.User.Identity.Name);
        }

        public static bool UserIsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static string FixedEmail(string email)
        {
            return email.Trim().ToLower();
        }


        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public static bool IsChangeEmail()
        {
            return HttpContext.Current.Request.QueryString["ChangeEmail"] != null;
        }



        public static string GetUserIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }


        public static string FixetTextForUrl(string text)
        {
            string url = HttpUtility.UrlEncode(text).Trim().Replace("+", "-");
            if (url.EndsWith("-"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            if (url.StartsWith("-"))
            {
                url = url.Substring(1, url.Length - 1);
            }
            return url;
        }

        public static string DecodeUrl(string text)
        {
            string url = HttpUtility.UrlDecode(text).Trim().Replace(" ", "-");
            if (url.EndsWith("-"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            if (url.StartsWith("-"))
            {
                url = url.Substring(1, url.Length - 1);
            }
            return url;
        }
    }
}
