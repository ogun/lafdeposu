using System.Web;
using System.Web.Optimization;

namespace LafDeposu.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                        "~/scripts/index/bottom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/scripts/angular.js",
                        "~/scripts/angular-resource.js",
                        "~/scripts/angular-cookies.js",
                        "~/scripts/index/indexController.js"));

            bundles.Add(new ScriptBundle("~/bundles/analytics").Include(
                        "~/scripts/_layout/analytics.js"));

            bundles.Add(new ScriptBundle("~/bundles/userreport").Include(
                        "~/scripts/_layout/userreport.js"));


            bundles.Add(new StyleBundle("~/sbundles/bootstrap")
                        .Include("~/content/bootstrap.css", new CssRewriteUrlTransform())
                        .Include("~/content/css/_layout/style.css"));

            bundles.Add(new StyleBundle("~/sbundles/index").Include(
                        "~/content/css/index/style.css"));
        }
    }
}