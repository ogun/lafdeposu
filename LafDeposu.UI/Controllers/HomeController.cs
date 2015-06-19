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
        public ActionResult Index(string word)
        {
            SearchShare ss = new SearchShare();

            ViewBag.Title = "Laf Deposu";
            ViewBag.Description = "Girdiğiniz harflerle oluşabilecek Türkçe kelimeleri üreten bir internet sitesi.";
            ViewBag.Keywords = "laf deposu, türkçe kelimeler, kelimelik oyunu hile, scrabble hile";

            string keyword = Request.QueryString["keyword"];
            if (string.IsNullOrWhiteSpace(keyword))
            {
                if (word != null) {
                    word = word.Replace('-', ' ');
                }
                
                keyword = word;

                if (!string.IsNullOrWhiteSpace(word))
                {
                    ViewBag.Title = string.Format("{0} | Laf Deposu", word);
                    ViewBag.Description = string.Format("{0} kelimesinin anlamı ve {0} kelimesinin harfleriyle oluşturulabilecek Türkçe kelimeler.", word);
                    ViewBag.Keywords = string.Format("{0} anlamı, {0} ne demek, {0} nedir, {0} hakkında bilgi, {0} kelimesinin harfleriyle oluşturulabilecek kelimeler", word);
                }
            }

            if (keyword != null) {
                keyword = keyword.Replace('-', ' ');
            }
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
    }
}
