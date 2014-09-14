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

            string keyword = Request.QueryString["keyword"];
            if (!string.IsNullOrEmpty(keyword))
            {
                ss.Keyword = keyword;
                ss.StartsWith = Request.QueryString["startsWith"];
                ss.Contains = Request.QueryString["contains"];
                ss.EndsWith = Request.QueryString["endsWith"];

                string resultCharCountStr = Request.QueryString["resultCharCount"];
                if (!string.IsNullOrEmpty(resultCharCountStr))
                {
                    int tmpResultCharCount;
                    if (int.TryParse(resultCharCountStr, out tmpResultCharCount))
                    {
                        ss.ResultCharCount = tmpResultCharCount;
                    }
                }
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

        [ActionName("Kelime-Ekle")]
        public ActionResult KelimeEkle()
        {
            return View();
        }
    }
}
