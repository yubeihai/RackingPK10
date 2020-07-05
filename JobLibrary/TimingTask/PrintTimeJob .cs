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

                var lott = DbContext.LotteryDatas.Where(l => l.IssueId == lottery.IssueId).ToList().Count();
                if (lott < 1)
                {
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
                    DbContext.LotteryDatas.Add(lottery);
                    DbContext.SaveChanges();
                    for (int i = 1; i <= 10; i++)
                    {
                        Omissions omission = DbContext.Omissions.Where(o => o.Ranking == i).First();
                        switch (Convert.ToInt32(array[i - 1]))
                        {
                            case 1:
                                omission.One = 0;
                                omission.Two++;
                                omission.Three++;
                                omission.Four++;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 2:
                                omission.One++;
                                omission.Two = 0;
                                omission.Three++;
                                omission.Four++;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 3:
                                omission.One++;
                                omission.Two++;
                                omission.Three = 0;
                                omission.Four++;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 4:
                                omission.One++;
                                omission.Two++;
                                omission.Three++;
                                omission.Four = 0;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 5:
                                omission.One++;
                                omission.Two++;
                                omission.Three++;
                                omission.Four++;
                                omission.Five = 0;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 6:
                                omission.One++;
                                omission.Two++;
                                omission.Three++;
                                omission.Four++;
                                omission.Five++;
                                omission.Six = 0;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 7:
                                omission.One++;
                                omission.Two++;
                                omission.Three++;
                                omission.Four++;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven = 0;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 8:
                                omission.One++;
                                omission.Two++;
                                omission.Three++;
                                omission.Four++;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight = 0;
                                omission.Nine++;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 9:
                                omission.One++;
                                omission.Two++;
                                omission.Three++;
                                omission.Four++;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine = 0;
                                omission.Ten++;
                                DbContext.SaveChanges();
                                break;
                            case 10:
                                omission.One++;
                                omission.Two++;
                                omission.Three++;
                                omission.Four++;
                                omission.Five++;
                                omission.Six++;
                                omission.Seven++;
                                omission.Eight++;
                                omission.Nine++;
                                omission.Ten = 0;
                                DbContext.SaveChanges();
                                break;
                        }
                        List<NumeralGroup> numberlist = DbContext.NumeralGroups.Where(n => n.Ranking == i).ToList<NumeralGroup>();
                        foreach (var item in numberlist)
                        {
                            string[] intArray = item.Group.Split(',');
                            ContinuityNumber prize = DbContext.ContinuityNumbers.Where(c => c.NGId == item.Id).FirstOrDefault<ContinuityNumber>();
                            if (prize == null)
                            {
                                prize = new ContinuityNumber();
                                prize.NGId = item.Id;
                                prize.RankingId = item.Ranking;
                                prize.TenNumber = 0;
                                prize.MonthNumber = 0;
                                prize.ThreeMonthNumber = 0;
                                prize.HalfYearNumber = 0;
                                prize.YearNumber = 0;
                                DbContext.ContinuityNumbers.Add(prize);
                                DbContext.SaveChanges();
                            }
                            int contain = Array.IndexOf(intArray, array[i - 1].ToString());
                            if (contain < 0)
                            {
                                prize.TenNumber++;
                                if (prize.TenNumber >= 5)
                                {
                                    StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == prize.TenNumber && s.RankingId == i && s.NGId == item.Id).FirstOrDefault<StatisticsTen>();
                                    if (statis == null)
                                    {
                                        StatisticsTen statisnew = new StatisticsTen();
                                        statisnew.RankingId = i;
                                        statisnew.Frequency = prize.TenNumber;
                                        statisnew.Number = 1;
                                        statisnew.NGId = item.Id;
                                        DbContext.StatisticsTens.Add(statisnew);
                                        DbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        statis.Number++;
                                    }
                                    StatisticsMonth statimon = DbContext.StatisticsMonths.Where(s => s.Frequency == prize.TenNumber && s.RankingId == i && s.NGId == item.Id).FirstOrDefault<StatisticsMonth>();
                                    if (statimon == null)
                                    {
                                        StatisticsMonth statisnew = new StatisticsMonth();
                                        statisnew.RankingId = i;
                                        statisnew.Frequency = prize.TenNumber;
                                        statisnew.Number = 1;
                                        statisnew.NGId = item.Id;
                                        DbContext.StatisticsMonths.Add(statisnew);
                                        DbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        statimon.Number++;
                                    }
                                    StatisticsThreeMonth statithrmon = DbContext.StatisticsThreeMonths.Where(s => s.Frequency == prize.TenNumber && s.RankingId == i && s.NGId == item.Id).FirstOrDefault<StatisticsThreeMonth>();
                                    if (statithrmon == null)
                                    {
                                        StatisticsThreeMonth statisnew = new StatisticsThreeMonth();
                                        statisnew.RankingId = i;
                                        statisnew.Frequency = prize.TenNumber;
                                        statisnew.Number = 1;
                                        statisnew.NGId = item.Id;
                                        DbContext.StatisticsThreeMonths.Add(statisnew);
                                        DbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        statithrmon.Number++;
                                    }
                                    StatisticsHalfYear statihaye = DbContext.StatisticsHalfYears.Where(s => s.Frequency == prize.TenNumber && s.RankingId == i && s.NGId == item.Id).FirstOrDefault<StatisticsHalfYear>();
                                    if (statihaye == null)
                                    {
                                        StatisticsHalfYear statisnew = new StatisticsHalfYear();
                                        statisnew.RankingId = i;
                                        statisnew.Frequency = prize.TenNumber;
                                        statisnew.Number = 1;
                                        statisnew.NGId = item.Id;
                                        DbContext.StatisticsHalfYears.Add(statisnew);
                                        DbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        statihaye.Number++;
                                    }
                                    StatisticsYear statiyear = DbContext.StatisticsYears.Where(s => s.Frequency == prize.TenNumber && s.RankingId == i && s.NGId == item.Id).FirstOrDefault<StatisticsYear>();
                                    if (statiyear == null)
                                    {
                                        StatisticsYear statisnew = new StatisticsYear();
                                        statisnew.RankingId = i;
                                        statisnew.Frequency = prize.TenNumber;
                                        statisnew.Number = 1;
                                        statisnew.NGId = item.Id;
                                        DbContext.StatisticsYears.Add(statisnew);
                                        DbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        statiyear.Number++;
                                    }

                                }
                            }
                            else
                            {
                                prize.TenNumber = 0;
                            }
                        }

                    }

                    DbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}