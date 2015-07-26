using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LafDeposu.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "KelimeIslemleri",
                url: "Kelime/{action}/{word}",
                defaults: new { controller = "Kelime", action = "Getir", word = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Kelime-Ekle",
                url: "Kelime-Ekle",
                defaults: new { controller = "Home", action = "Kelime-Ekle" }
            );

            routes.MapRoute(
                name: "Kelime-Listele",
                url: "Kelime-Listele",
                defaults: new { controller = "Home", action = "Kelime-Listele" }
            );

            routes.MapRoute(
                name: "Kelimelik-Hile",
                url: "Kelimelik-Hile",
                defaults: new { controller = "Home", action = "Kelimelik-Hile" }
            );

            routes.MapRoute(
                name: "Iletisim",
                url: "Iletisim",
                defaults: new { controller = "Home", action = "Iletisim" }
            );

            routes.MapRoute(
                name: "Yardim",
                url: "Yardim",
                defaults: new { controller = "Home", action = "Yardim" }
            );

            routes.MapRoute(
                name: "Rss",
                url: "Rss",
                defaults: new { controller = "Home", action = "Rss" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{word}",
                defaults: new { controller = "Home", action = "Index", word = UrlParameter.Optional }
            );
        }
    }
}