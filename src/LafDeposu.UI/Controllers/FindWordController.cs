﻿using LafDeposu.Helper;
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
        [OutputCache(Duration = 300, VaryByParam = "startsWith;contains;endsWith;resultCharCount")]
        public JsonResult Get(string chars)
        {
            string startsWith = Request.QueryString["startsWith"];
            string contains = Request.QueryString["contains"];
            string endsWith = Request.QueryString["endsWith"];
            string resultCharCountStr = Request.QueryString["resultCharCount"];
            int? resultCharCount = null;
            if (!string.IsNullOrEmpty(resultCharCountStr))
            {
                int tmpResultCharCount;
                if (int.TryParse(resultCharCountStr, out tmpResultCharCount))
                {
                    resultCharCount = tmpResultCharCount;
                }
            }

            FindWordHelper help = new FindWordHelper(ConfigurationManager.ConnectionStrings["MySql"].ConnectionString, DataAccessType.MySql);
            WordList wl = help.CreateResult(chars, startsWith, contains, endsWith, resultCharCount);

            JsonResult jr = new JsonResult();
            jr.ContentEncoding = System.Text.Encoding.GetEncoding(1254);
            jr.Data = wl;
            jr.MaxJsonLength = int.MaxValue;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //Logger.Info(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", chars, startsWith, contains, endsWith, resultCharCount, sw.Elapsed));
            return jr;
        }
    }
}
