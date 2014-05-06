using LafDeposu.Helper.Logging;
using LafDeposu.Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LafDeposu.UI.Controllers
{
    public class HomeController : Controller
    {
        public ILogger Logger { get; set; }

        public HomeController(ILogger logger)
        {
            Logger = logger;
        }

        // GET: /Home/
        public ActionResult Index()
        {
            SearchShare ss = new SearchShare();

            string keywod = Request.QueryString["keyword"];
            if (!string.IsNullOrEmpty(keywod))
            {
                ss.Keyword = keywod;
                ss.StartsWith = Request.QueryString["startsWith"];
                ss.Contains = Request.QueryString["contains"];
                ss.EndsWith = Request.QueryString["endsWith"];
            }

            return View(ss);
        }

        // GET: /Yardim/
        public ActionResult Yardim()
        {
            return View();
        }

        // GET: /Iletisim/
        public ActionResult Iletisim()
        {
            return View();
        }
    }
}
