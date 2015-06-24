using LafDeposu.Helper;
using LafDeposu.Helper.Data;
using LafDeposu.Helper.Logging;
using LafDeposu.Helper.Models;
using LafDeposu.UI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        [OutputCache(Duration = 300, VaryByParam = "startsWith;contains;endsWith;resultCharCount")]
        public ActionResult Index(string word)
        {
            SearchShare ss = new SearchShare();
            ss.Data = "[]";

            ViewBag.Title = "Laf Deposu";
            ViewBag.Description = "Girdiğiniz harflerle oluşabilecek Türkçe kelimeleri üreten bir internet sitesi.";
            ViewBag.Keywords = "laf deposu, türkçe kelimeler, kelimelik oyunu hile, scrabble hile";

            if (string.IsNullOrWhiteSpace(word)) {
                string keyword = Request.QueryString["keyword"];
                word = keyword;
            }
            

            if (!string.IsNullOrWhiteSpace(word))
            {
                word = word.Replace('-', ' ');

                ViewBag.Title = string.Format("{0} | Laf Deposu", word);
                ViewBag.Description = string.Format("{0} kelimesinin anlamı ve {0} kelimesinin harfleriyle oluşturulabilecek Türkçe kelimeler.", word);
                ViewBag.Keywords = string.Format("{0} anlamı, {0} ne demek, {0} nedir, {0} hakkında bilgi, {0} kelimesinin harfleriyle oluşturulabilecek kelimeler", word);

                ss.Keyword = word;
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

                FindWordHelper help = new FindWordHelper(ConfigurationManager.ConnectionStrings["MySql"].ConnectionString, DataAccessType.MySql);
                WordList wl = help.CreateResult(ss.Keyword, ss.StartsWith, ss.Contains, ss.EndsWith, ss.ResultCharCount);
                ss.Data = JsonConvert.SerializeObject(wl);
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

        // GET: /Kelime-Ekle/
        [ActionName("Kelime-Ekle")]
        public ActionResult KelimeEkle()
        {
            return View();
        }

        // GET: /Kelime-Listele/
        [ActionName("Kelime-Listele")]
        public ActionResult KelimeListele()
        {
            return View();
        }

        // GET: /Rss/
        [ChildActionOnly]
        [OutputCache(Duration=60*60*24)]
        public ActionResult Rss() {
            RssModel model = new RssModel();

            try {
                model.Load();
            } catch (Exception ex) {
                Logger.ErrorException(ex.Message, ex);
            }

            return PartialView(model);
        }
    }
}
