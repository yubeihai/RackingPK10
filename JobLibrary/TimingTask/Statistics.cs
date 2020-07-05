using JobLibrary.DataAccess;
using JobLibrary.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobLibrary.TimingTask
{
    [DisallowConcurrentExecution]
    public class Statistics : IJob
    {
        public RacingDBContext DbContext = new RacingDBContext();
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                DateTime date = DateTime.Now.AddDays(-9).Date;
                List<LotteryData> lotterylist = DbContext.LotteryDatas.Where(l => l.Date >= date).ToList();
                List<NumeralGroup> numberlgroup = DbContext.NumeralGroups.OrderBy(n => n.Ranking).ToList();
                List<ContinuityNumber> continuitynumber = DbContext.ContinuityNumbers.OrderBy(n => n.Id).ToList();
                foreach (var lotteryitem in lotterylist)
                {
                    for (int i = 1; i <= numberlgroup.Count; i++)
                    {
                        NumeralGroup numberlist = DbContext.NumeralGroups.Where(n => n.Ranking == i).FirstOrDefault<NumeralGroup>();
                        string[] intArray = numberlist.Group.Split(',');
                        ContinuityNumber prize = DbContext.ContinuityNumbers.Where(c => c.RankingId == i).FirstOrDefault<ContinuityNumber>();
                        int contain = Array.IndexOf(intArray, lotteryitem.Champion.ToString());
                        if (contain > 0)
                        {
                            prize.Number++;
                            if (prize.Number >= 5)
                            {
                                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == prize.Number && prize.RankingId == i).FirstOrDefault<StatisticsTen>();
                                if (statis == null)
                                {
                                    StatisticsTen statisnew = new StatisticsTen();
                                    statisnew.RankingId = i;
                                    statisnew.Frequency = prize.Number;
                                    statisnew.Number = 1;
                                    DbContext.StatisticsTens.Add(statisnew);
                                    DbContext.SaveChanges();

                                }
                                else
                                {
                                    statis.Number++;
                                }

                            }
                        }
                        else
                        {
                            prize.Number = 0;
                        }
                        DbContext.SaveChanges();
                    }
                    DbContext.SaveChanges();

                }


                //foreach (var item in numberlgroup)
                //{
                //    string[] intArray = item.Group.Split(',');
                //    foreach (var lotteryitem in lotterylist)
                //    {
                //        ContinuityNumber chacon = DbContext.ContinuityNumbers.Where(c => c.Id == 1).First();
                //        ContinuityNumber runcon = DbContext.ContinuityNumbers.Where(c => c.Id == 2).First();
                //        ContinuityNumber thicon = DbContext.ContinuityNumbers.Where(c => c.Id == 3).First();
                //        ContinuityNumber foucon = DbContext.ContinuityNumbers.Where(c => c.Id == 4).First();
                //        ContinuityNumber fifcon = DbContext.ContinuityNumbers.Where(c => c.Id == 5).First();
                //        ContinuityNumber sixcon = DbContext.ContinuityNumbers.Where(c => c.Id == 6).First();
                //        ContinuityNumber sevcon = DbContext.ContinuityNumbers.Where(c => c.Id == 7).First();
                //        ContinuityNumber eigcon = DbContext.ContinuityNumbers.Where(c => c.Id == 8).First();
                //        ContinuityNumber nincon = DbContext.ContinuityNumbers.Where(c => c.Id == 9).First();
                //        ContinuityNumber tencon = DbContext.ContinuityNumbers.Where(c => c.Id == 10).First();
                //        int Champion = Array.IndexOf(intArray, lotteryitem.Champion.ToString());
                //        int RunnerUp = Array.IndexOf(intArray, lotteryitem.RunnerUp.ToString());
                //        int Third = Array.IndexOf(intArray, lotteryitem.Third.ToString());
                //        int Fourth = Array.IndexOf(intArray, lotteryitem.Fourth.ToString());
                //        int Fifth = Array.IndexOf(intArray, lotteryitem.Fifth.ToString());
                //        int Sixth = Array.IndexOf(intArray, lotteryitem.Sixth.ToString());
                //        int Seventh = Array.IndexOf(intArray, lotteryitem.Seventh.ToString());
                //        int Eighth = Array.IndexOf(intArray, lotteryitem.Eighth.ToString());
                //        int Ninth = Array.IndexOf(intArray, lotteryitem.Ninth.ToString());
                //        int Tenth = Array.IndexOf(intArray, lotteryitem.Tenth.ToString());
                //        if (Champion > 0)
                //        {
                //            chacon.Number++;
                //            if (chacon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == chacon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 1;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            chacon.Number = 0;
                //        }
                //        if (RunnerUp > 0)
                //        {
                //            runcon.Number++;
                //            if (runcon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == runcon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 2;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            runcon.Number = 0;
                //        }
                //        if (Third > 0)
                //        {
                //            thicon.Number++;
                //            if (thicon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == thicon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 3;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }

                //        }
                //        else
                //        {
                //            thicon.Number = 0;
                //        }
                //        if (Fourth > 0)
                //        {
                //            foucon.Number++;
                //            if (foucon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == foucon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 4;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            foucon.Number = 0;
                //        }
                //        if (Fifth > 0)
                //        {
                //            fifcon.Number++;
                //            if (fifcon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == fifcon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 5;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            fifcon.Number = 0;
                //        }
                //        if (Sixth > 0)
                //        {
                //            sixcon.Number++;
                //            if (sixcon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == sixcon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 6;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            sixcon.Number = 0;
                //        }
                //        if (Seventh > 0)
                //        {
                //            sevcon.Number++;
                //            if (sevcon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == sevcon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 7;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            sevcon.Number = 0;
                //        }
                //        if (Eighth > 0)
                //        {
                //            eigcon.Number++;
                //            if (eigcon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == eigcon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 8;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            eigcon.Number = 0;
                //        }
                //        if (Ninth > 0)
                //        {
                //            nincon.Number++;
                //            if (nincon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == nincon.Number).First();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 9;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            nincon.Number = 0;
                //        }
                //        if (Tenth > 0)
                //        {
                //            tencon.Number++;
                //            if (tencon.Number >= 5)
                //            {
                //                StatisticsTen statis = DbContext.StatisticsTens.Where(s => s.Frequency == tencon.Number).FirstOrDefault<StatisticsTen>();
                //                if (statis != null)
                //                {
                //                    statis.Number++;
                //                }
                //                else
                //                {
                //                    StatisticsTen statisnew = new StatisticsTen();
                //                    statisnew.RankingId = 10;
                //                    statisnew.Frequency = tencon.Number;
                //                    statisnew.Number = 1;
                //                    DbContext.StatisticsTens.Add(statisnew);
                //                }
                //                DbContext.SaveChanges();
                //            }
                //        }
                //        else
                //        {
                //            tencon.Number = 0;
                //        }
                //        DbContext.SaveChanges();
                //    }
                //}
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}