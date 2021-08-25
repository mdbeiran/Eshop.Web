namespace Eshop.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/MainJs").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/jquery.scrollUp.min.js",
                "~/Content/js/price-range.js",
                "~/Content/js/jquery.prettyPhoto.js",
                "~/Content/js/main.js",
                "~/Content/CustomJs/Custom.js",
                "~/Content/SweetAlert/dist/sweetalert.min.js"));
        }
    }
}