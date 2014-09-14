using LafDeposu.Helper;
using LafDeposu.Helper.Data;
using LafDeposu.Helper.Logging;
using LafDeposu.Helper.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Text;
using System.Web.Mvc;

namespace LafDeposu.UI.Controllers
{
    public class KelimeController : Controller
    {
        private ILogger Logger { get; set; }
        private readonly string ConnectionString;
        private readonly DataAccessType AccessType;

        public KelimeController(ILogger logger)
        {
            Logger = logger;
            ConnectionString = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
            AccessType = DataAccessType.MySql;
        }

        [HttpGet]
        [OutputCache(Duration = 300, VaryByParam = "startsWith;contains;endsWith;resultCharCount")]
        public JsonResult Getir(string word)
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

            FindWordHelper help = new FindWordHelper(ConnectionString, AccessType);
            WordList wl = help.CreateResult(word, startsWith, contains, endsWith, resultCharCount);

            JsonResult jr = new JsonResult();
            jr.ContentEncoding = Encoding.GetEncoding(1254);
            jr.Data = wl;
            jr.MaxJsonLength = int.MaxValue;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        [HttpGet]
        public JsonResult Ekle(string word)
        {
            WordResponse returnValue = new WordResponse();

            string meaning = Request.QueryString["meaning"];
            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(meaning))
            {
                returnValue.Type = WordResponseType.Error;
                returnValue.Title = "Zorunlu Alanlar Boş";
                returnValue.Message = "Kelime eklerken kelimeyi ve anlamını girmelisiniz. İki alan da boş bırakılamaz.";
            }
            else
            {
                try
                {
                    FindWordHelper help = new FindWordHelper(ConnectionString, AccessType);
                    returnValue = help.AddWord(word, meaning);
                }
                catch (Exception ex)
                {
                    returnValue.Type = WordResponseType.Error;
                    returnValue.Title = "Kayıt Eklerken Hata Oluştu";
                    returnValue.Message = ex.Message;
                }
            }

            Logger.Info(JsonConvert.SerializeObject(returnValue));

            JsonResult jr = new JsonResult();
            jr.ContentEncoding = Encoding.GetEncoding(1254);
            jr.Data = returnValue;
            jr.MaxJsonLength = int.MaxValue;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }
    }
}
