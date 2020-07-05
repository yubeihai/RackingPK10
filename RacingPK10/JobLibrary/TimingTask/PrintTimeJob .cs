using JobLibrary.DataAccess;
using JobLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary.TimingTask
{
    [DisallowConcurrentExecution]
    public class PrintTimeJob : IJob
    {
        private string Interface = ConfigurationManager.AppSettings["Interface"].ToString();
        public RacingDBContext DbContext = new RacingDBContext();
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Interface);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                JObject json = (JObject)JsonConvert.DeserializeObject(retString);
                LotteryData lottery = new LotteryData();
                List<NumeralGroup> numberlgroup = DbContext.NumeralGroups.OrderBy(n => n.Ranking).ToList();
                JObject data = (JObject)JsonConvert.DeserializeObject(json["data"].ToString());
                JObject newest = (JObject)JsonConvert.DeserializeObject(data["newest"].ToString());
                DateTime datetime = Convert.ToDateTime(newest["time"]);
                lottery.IssueId = Convert.ToInt32(newest["issue"]);
                var array = newest["array"].ToArray();
                lottery.Champion = Convert.ToInt32(array[0]);
                lottery.RunnerUp = Convert.ToInt32(array[1]);
                lottery.Third = Convert.ToInt32(array[2]);
                lottery.Fourth = Convert.ToInt32(array[3]);
                lottery.Fifth = Convert.ToInt32(array[4]);
                lottery.Sixth = Convert.ToInt32(array[5]);
                lottery.Seventh = Convert.ToInt32(array[6]);
                lottery.Eighth = Convert.ToInt32(array[7]);
                lottery.Ninth = Convert.ToInt32(array[8]);
                lottery.Tenth = Convert.ToInt32(array[9]);
                lottery.Date = Convert.ToDateTime(datetime.ToString("d"));
                lottery.Time = datetime.ToString("T");
                var lott = DbContext.LotteryDatas.Where(l => l.IssueId == lottery.IssueId).ToList().Count();
                if (lott < 1)
                {
                    foreach (var item in numberlgroup)
                    {
                        string[] intArray = item.Group.Split(',');
                        int Champion = Array.IndexOf(intArray, lottery.Champion.ToString());
                        int RunnerUp = Array.IndexOf(intArray, lottery.RunnerUp.ToString());
                        int Third = Array.IndexOf(intArray, lottery.Third.ToString());
                        int Fourth = Array.IndexOf(intArray, lottery.Fourth.ToString());
                        int Fifth = Array.IndexOf(intArray, lottery.Fifth.ToString());
                        int Sixth = Array.IndexOf(intArray, lottery.Sixth.ToString());
                        int Seventh = Array.IndexOf(intArray, lottery.Seventh.ToString());
                        int Eighth = Array.IndexOf(intArray, lottery.Eighth.ToString());
                        int Ninth = Array.IndexOf(intArray, lottery.Ninth.ToString());
                        int Tenth = Array.IndexOf(intArray, lottery.Tenth.ToString());
                    }
                    DbContext.LotteryDatas.Add(lottery);
                    DbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}