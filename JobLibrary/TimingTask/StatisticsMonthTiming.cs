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
    public class StatisticsMonthTiming : IJob
    {
        public RacingDBContext DbContext = new RacingDBContext();
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var data = DbContext.StatisticsMonths.OrderBy(a => a.Id).ToList();
                foreach (var item in data)
                {
                    item.Number = 0;
                    DbContext.SaveChanges();
                }
                var connumdata = DbContext.ContinuityNumbers.OrderBy(a => a.Id).ToList();
                foreach (var item in connumdata)
                {
                    item.MonthNumber = 0;
                    DbContext.SaveChanges();
                }
                DateTime date = DateTime.Now.AddMonths(-1).Date;
                List<LotteryData> lotterylist = DbContext.LotteryDatas.Where(l => l.Date >= date).OrderBy(o => o.IssueId).ToList();
                foreach (var lotteryitem in lotterylist)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        List<NumeralGroup> numberlist = DbContext.NumeralGroups.Where(n => n.Ranking == i).OrderBy(c => c.Id).ToList<NumeralGroup>();
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
                            int contain = 0;
                            switch (i)
                            {
                                case 1:
                                    contain = Array.IndexOf(intArray, lotteryitem.Champion.ToString());
                                    break;
                                case 2:
                                    contain = Array.IndexOf(intArray, lotteryitem.RunnerUp.ToString());
                                    break;
                                case 3:
                                    contain = Array.IndexOf(intArray, lotteryitem.Third.ToString());
                                    break;
                                case 4:
                                    contain = Array.IndexOf(intArray, lotteryitem.Fourth.ToString());
                                    break;
                                case 5:
                                    contain = Array.IndexOf(intArray, lotteryitem.Fifth.ToString());
                                    break;
                                case 6:
                                    contain = Array.IndexOf(intArray, lotteryitem.Sixth.ToString());
                                    break;
                                case 7:
                                    contain = Array.IndexOf(intArray, lotteryitem.Seventh.ToString());
                                    break;
                                case 8:
                                    contain = Array.IndexOf(intArray, lotteryitem.Eighth.ToString());
                                    break;
                                case 9:
                                    contain = Array.IndexOf(intArray, lotteryitem.Ninth.ToString());
                                    break;
                                case 10:
                                    contain = Array.IndexOf(intArray, lotteryitem.Tenth.ToString());
                                    break;
                            }

                            if (contain < 0)
                            {
                                prize.MonthNumber++;
                                if (prize.MonthNumber >= 5)
                                {
                                    StatisticsMonth statis = DbContext.StatisticsMonths.Where(s => s.Frequency == prize.MonthNumber && s.RankingId == i && s.NGId == item.Id).FirstOrDefault<StatisticsMonth>();
                                    if (statis == null)
                                    {
                                        StatisticsMonth statisnew = new StatisticsMonth();
                                        statisnew.RankingId = i;
                                        statisnew.Frequency = prize.MonthNumber;
                                        statisnew.Number = 1;
                                        statisnew.NGId = item.Id;
                                        DbContext.StatisticsMonths.Add(statisnew);
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
                                prize.MonthNumber = 0;
                            }
                            DbContext.SaveChanges();
                        }
                    }
                    DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}