using System.Web.Mvc;

namespace Eshop.Web.Areas.UserPanel
{
    public class UserPanelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UserPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UserPanel_default",
                "UserPanel/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}