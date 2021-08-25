using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Eshop.Business.ViewEngins;
using Eshop.Ioc;
using Eshop.DataLayer.Contexts;

namespace Eshop.Web
{
    using System.Web.Optimization;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<EshopDbContext>(null);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());

            // Config Ninject
            ConfigNinjectIoC();
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404)
            {
                Response.Redirect("404.html");
            }
        }


        private void ConfigNinjectIoC()
        {
            ControllerBuilder.Current.SetControllerFactory(new NinjectController());
        }
    }
}
