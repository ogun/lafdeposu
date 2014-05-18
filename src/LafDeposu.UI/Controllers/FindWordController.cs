using LafDeposu.Helper;
using LafDeposu.Helper.Data;
using LafDeposu.Helper.Logging;
using LafDeposu.Helper.Models;
using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;

namespace LafDeposu.UI.Controllers
{
    public class FindWordController : Controller
    {
        public ILogger Logger { get; set; }

        public FindWordController(ILogger logger)
        {
            Logger = logger;
        }

        [HttpGet]
        [OutputCache(Duration = 300, VaryByParam = "startsWith;contains;endsWith;showTwoChars")]
        public JsonResult Get(string chars)
        {
            Stopwatch sw = Stopwatch.StartNew();

            string startsWith = Request.QueryString["startsWith"];
            string contains = Request.QueryString["contains"];
            string endsWith = Request.QueryString["endsWith"];
            string showTwoCharsStr = Request.QueryString["showTwoChars"];
            bool showTwoChars;
            bool.TryParse(showTwoCharsStr, out showTwoChars);

            FindWordHelper help = new FindWordHelper(ConfigurationManager.ConnectionStrings["MySql"].ConnectionString, DataAccessType.MySql);
            WordList wl = help.CreateResult(chars, startsWith, contains, endsWith, showTwoChars);

            JsonResult jr = new JsonResult();
            jr.ContentEncoding = System.Text.Encoding.GetEncoding(1254);
            jr.Data = wl;
            jr.MaxJsonLength = int.MaxValue;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            sw.Stop();
            Logger.Info(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", chars, startsWith, contains, endsWith, showTwoCharsStr, sw.Elapsed));
            return jr;
        }
    }
}
