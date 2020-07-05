using JobLibrary.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RacingPK10.DiscClass;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace RacingPK10.Controllers
{
    public class BetController : Controller
    {
        public RacingDBContext DbContext = new RacingDBContext();
        // GET: Bets
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string StartBets(Bets betsdata)
        {
            var dataString = "";
            var user = DbContext.NoteUsers.OrderBy(n => n.Id).ToList();
            var betsurl = ConfigurationManager.AppSettings["BetsUrl"].ToString() + betsdata.url;
            string Interface = ConfigurationManager.AppSettings["Interface"].ToString();
            //GetUser();
            //GetUserCookie();
            try
            {
                #region 获取下期id
                HttpWebRequest current = (HttpWebRequest)WebRequest.Create(Interface);
                current.Method = "GET";
                current.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse currentresponse = (HttpWebResponse)current.GetResponse();
                Stream currentResponseStream = currentresponse.GetResponseStream();
                StreamReader currentStreamReader = new StreamReader(currentResponseStream, Encoding.UTF8);
                string retcurrentString = currentStreamReader.ReadToEnd();
                JObject json = (JObject)JsonConvert.DeserializeObject(retcurrentString);
                JObject data = (JObject)JsonConvert.DeserializeObject(json["data"].ToString());
                betsdata.betsJson.periods = data["current"].ToString();
                #endregion
                //foreach (var item in user)
                //{
                //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(betsurl);
                //    request.Method = "POST";
                //    request.ContentType = "application/json;charset=UTF-8";
                //    request.Headers["Cookie"] = item.Cookie;
                //    //request.ContentLength = Encoding.UTF8.GetByteCount(JsonConvert.SerializeObject(betsdata.betsJson));
                //    Stream myRequestStream = request.GetRequestStream();
                //    StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                //    myStreamWriter.Write(JsonConvert.SerializeObject(betsdata.betsJson));
                //    myStreamWriter.Close();

                //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //    Stream myResponseStream = response.GetResponseStream();
                //    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                //    var retString = myStreamReader.ReadToEnd();
                //    JObject jsonString = (JObject)JsonConvert.DeserializeObject(retString);
                //    dataString += item.UserName + jsonString["Message"].ToString() + ";";
                //    myStreamReader.Close();
                //    myResponseStream.Close();

                //}
                return dataString;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //public static string Post(string url)
        //{
        //    string result = "";
        //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //    req.Method = "POST";
        //    req.ContentType = "application/x-www-form-urlencoded";
        //    #region 添加Post 参数
        //    StringBuilder builder = new StringBuilder();
        //    byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
        //    req.ContentLength = data.Length;
        //    using (Stream reqStream = req.GetRequestStream())
        //    {
        //        reqStream.Write(data, 0, data.Length);
        //        reqStream.Close();
        //    }
        //    #endregion
        //    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        //    Stream stream = resp.GetResponseStream();
        //    //获取响应内容
        //    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        //    {
        //        result = reader.ReadToEnd();
        //    }
        //    return result;

        //}
        private void GetUser()
        {
            var loginurl = ConfigurationManager.AppSettings["Login"].ToString();
            try
            {
                string retString = "";
                var user = DbContext.NoteUsers.OrderBy(n => n.Id).ToList();
                foreach (var item in user)
                {
                    string postString = "username=" + item.UserName + "&password=" + item.PassWord;
                    HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(loginurl);
                    wbRequest.Method = "POST";
                    wbRequest.ContentType = "application/x-www-form-urlencoded";
                    wbRequest.ContentLength = Encoding.UTF8.GetByteCount(postString);
                    using (Stream requestStream = wbRequest.GetRequestStream())
                    {
                        using (StreamWriter swrite = new StreamWriter(requestStream))
                        {
                            swrite.Write(postString);
                        }
                    }
                    HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                    using (Stream responseStream = wbResponse.GetResponseStream())
                    {
                        using (StreamReader sread = new StreamReader(responseStream))
                        {
                            retString = sread.ReadToEnd();
                        }
                    }
                    JObject data = (JObject)JsonConvert.DeserializeObject(retString);
                    JObject Oiddata = (JObject)JsonConvert.DeserializeObject(data["data"].ToString());
                    item.Oid = Oiddata["oid"].ToString();
                    DbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }
        private void GetUserCookie()
        {
            try
            {
                var user = DbContext.NoteUsers.OrderBy(n => n.Id).ToList();
                foreach (var item in user)
                {
                    var dataurl = "";
                    var url = @"http://www.zx5519.com/api/loginapi?oid=" + item.Oid + "&action=lottery&gameid=&domain=https://p.sdcp99.com&client=1&s=1.3497294104869528&_=1552913750769";
                    HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                    wbRequest.Method = "GET";
                    HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                    using (Stream responseStream = wbResponse.GetResponseStream())
                    {
                        using (StreamReader sReader = new StreamReader(responseStream))
                        {
                            var result = sReader.ReadToEnd();
                            JObject date = (JObject)JsonConvert.DeserializeObject(result);
                            dataurl = date["data"].ToString();
                            item.Cookie = GetUserCookie(dataurl);
                            DbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private string GetUserCookie(string url)
        {
            try
            {

                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                var cookies = wbResponse.Headers.GetValues("Set-Cookie");
                var cfduid = cookies[0];
                string[] sArray = cfduid.Split(';');
                cfduid = sArray[0];
                var lott_home_login = cookies[1];
                string[] lArray = lott_home_login.Split(';');
                lott_home_login = lArray[0];
                return cfduid + ";" + lott_home_login + ";";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}