using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RacingPK10.DiscClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RacingPK10.Controllers
{
    public class BetsController : Controller
    {
        // GET: Bets
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult StartBets(string url2)
        {
            return Content(url2);
            //url22 = Request["url22"];
            //url22=Request.Form["url22"];

            //var betsurl = ConfigurationManager.AppSettings["BetsUrl"].ToString() + url22;
            //string Interface = ConfigurationManager.AppSettings["Interface"].ToString();
            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Interface);
            //    request.Method = "GET";
            //    request.ContentType = "text/html;charset=UTF-8";
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    Stream myResponseStream = response.GetResponseStream();
            //    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            //    string retString = myStreamReader.ReadToEnd();
            //    JObject json = (JObject)JsonConvert.DeserializeObject(retString);
            //    JObject data = (JObject)JsonConvert.DeserializeObject(json["data"].ToString());
            //    //JObject betsObject= (JObject)JsonConvert.DeserializeObject(betsJson);
            //    //betsObject["periods"] = Convert.ToString(data["current"]);
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}
        }
    }
}