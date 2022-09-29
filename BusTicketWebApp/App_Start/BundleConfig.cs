using System.Web;
using System.Web.Optimization;

namespace BusTicketWebApp
{
    public static class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    //"~/Scripts/jquery-confirm.min.js",
                    "~/Scripts/jquery.dataTables.min.js",
                    "~/Scripts/bootstrap-datepicker.min.js"
                    //"~/Scripts/custalrt.js"                   
                ));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include("~/Scripts/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-datepickr3.min.css",
                "~/Content/jquery.dataTables.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/site.css"
                //"~/Content/custalrt.css",
                //"~/Content/alert-box.css",
                //"~/Content/jquery-confirm.min.css"
            ));
        }
    }
}
