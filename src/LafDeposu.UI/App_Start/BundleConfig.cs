using System.Web;
using System.Web.Optimization;

namespace LafDeposu.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region JavaScript Bundles
            // Jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/scripts/bootstrap.js"));

            // Angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/scripts/angular.js",
                        "~/scripts/angular-resource.js",
                        "~/scripts/angular-cookies.js",
                        "~/scripts/_layout/angModule.js"));

            // Analytics
            bundles.Add(new ScriptBundle("~/bundles/analytics").Include(
                        "~/scripts/_layout/analytics.js"));

            // User Report
            bundles.Add(new ScriptBundle("~/bundles/userreport").Include(
                        "~/scripts/_layout/userreport.js"));

            // Index
            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                        "~/scripts/index/bottom.js",
                        "~/scripts/index/indexController.js"));

            // Kelime-Ekle
            bundles.Add(new ScriptBundle("~/bundles/kelimeEkle").Include(
                        "~/scripts/kelimeEkle/kelimeEkleController.js",
                        "~/scripts/kelimeEkle/bottom.js",
                        "~/scripts/jquery.growl.js"));

            // Kelime-Listele
            bundles.Add(new ScriptBundle("~/bundles/kelimeListele").Include(
                        "~/scripts/kelimeListele/kelimeListeleController.js",
                        "~/scripts/jquery.growl.js"));
            #endregion

            #region CSS Bundles
            // Bootstrap
            bundles.Add(new StyleBundle("~/sbundles/bootstrap")
                        .Include("~/content/bootstrap.css", new CssRewriteUrlTransform())
                        .Include("~/content/css/_layout/style.css"));

            // Index
            bundles.Add(new StyleBundle("~/sbundles/index").Include(
                        "~/content/css/index/style.css"));

            // Kelime-Ekle
            bundles.Add(new StyleBundle("~/sbundles/kelimeEkle").Include(
                        "~/content/jquery.growl.css"));

            // Kelime-Listele
            bundles.Add(new StyleBundle("~/sbundles/kelimeListele").Include(
                        "~/content/jquery.growl.css",
                        "~/content/css/kelimeListele/style.css"));
            #endregion
        }
    }
}