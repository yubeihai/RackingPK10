using JobLibrary.DataAccess;
using JobLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RacingPK10.DiscClass;

namespace RacingPK10.Controllers
{
    public class StatisticController : Controller
    {
        public RacingDBContext DbContext = new RacingDBContext();
        // GET: Statistic
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult TenIndex()
        //{
        //    List<StatisticsTen> list = DbContext.StatisticsTens.OrderBy(t => t.RankingId).ToList();
        //    return View(list);
        //}
        //public ActionResult MonthIndex()
        //{
        //    List<StatisticsMonth> list = DbContext.StatisticsMonths.OrderBy(t => t.RankingId).ToList();
        //    return View(list);
        //}
        //public ActionResult ThreeMonthIndex()
        //{
        //    List<StatisticsThreeMonth> list = DbContext.StatisticsThreeMonths.OrderBy(t => t.RankingId).ToList();
        //    return View(list);
        //}
        //public ActionResult HalfYearIndex()
        //{
        //    List<StatisticsHalfYear> list = DbContext.StatisticsHalfYears.OrderBy(t => t.RankingId).ToList();
        //    return View(list);
        //}
        //public ActionResult YearIndex()
        //{
        //    List<StatisticsYear> list = DbContext.StatisticsYears.OrderBy(t => t.RankingId).ToList();
        //    return View(list);
        //}
        public JsonResult SeeDisc()
        {
            List<RankGroupData> rankgroupdata = new List<RankGroupData>();
            for (int i = 1; i <= 10; i++)
            {
                RankGroupData ranknew = new RankGroupData();
                List<NumeralGroup> numberlist = DbContext.NumeralGroups.Where(n => n.Ranking == i).OrderBy(o => o.Id).ToList();
                List<NumeralGroup> numlist = numberlist.Where(n => n.Ranking == i).ToList();
                List<LotteryData> hunddata = DbContext.LotteryDatas.OrderByDescending(l => l.IssueId).Take(100).ToList();
                List<LotteryData> lotterydata = DbContext.LotteryDatas.OrderByDescending(l => l.IssueId).Take(4).ToList();
                foreach (var item in lotterydata)
                {
                    foreach (var itemnumber in numlist)
                    {
                        switch (i)
                        {
                            case 1:
                                if (itemnumber.Group.IndexOf(item.Champion.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 2:
                                if (itemnumber.Group.IndexOf(item.RunnerUp.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 3:
                                if (itemnumber.Group.IndexOf(item.Third.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 4:
                                if (itemnumber.Group.IndexOf(item.Fourth.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 5:
                                if (itemnumber.Group.IndexOf(item.Fifth.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 6:
                                if (itemnumber.Group.IndexOf(item.Sixth.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 7:
                                if (itemnumber.Group.IndexOf(item.Seventh.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 8:
                                if (itemnumber.Group.IndexOf(item.Eighth.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 9:
                                if (itemnumber.Group.IndexOf(item.Ninth.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                            case 10:
                                if (itemnumber.Group.IndexOf(item.Tenth.ToString()) > 0)
                                {
                                    numberlist.Remove(itemnumber);
                                }
                                break;
                        }
                    }

                }
                List<GroupData> groupdatalist = new List<GroupData>();
                foreach (var item in numberlist)
                {
                    var gunNumber = DbContext.ContinuityNumbers.Where(c => c.NGId == item.Id).FirstOrDefault<ContinuityNumber>().TenNumber;
                    if (gunNumber > 3)
                    {
                        List<StatisticsTen> staten = DbContext.StatisticsTens.Where(t => t.NGId == item.Id).OrderBy(o => o.Id).ToList();
                        List<StatisticsMonth> stamon = DbContext.StatisticsMonths.Where(t => t.NGId == item.Id).OrderBy(o => o.Id).ToList();
                        List<StatisticsThreeMonth> stathrmon = DbContext.StatisticsThreeMonths.Where(t => t.NGId == item.Id).OrderBy(o => o.Id).ToList();
                        List<StatisticsHalfYear> stahaye = DbContext.StatisticsHalfYears.Where(t => t.NGId == item.Id).OrderBy(o => o.Id).ToList();
                        List<StatisticsYear> stayear = DbContext.StatisticsYears.Where(t => t.NGId == item.Id).OrderBy(o => o.Id).ToList();
                        CalculationData cal = new CalculationData();
                        foreach (var tenitem in staten)
                        {
                            cal.tenDay += tenitem.Frequency + "-" + tenitem.Number + ",";
                        }
                        if (cal.tenDay != null)
                        {
                            cal.tenDay = cal.tenDay.Substring(0, cal.tenDay.Length - 1);
                        }

                        foreach (var onemonitem in stamon)
                        {
                            cal.oneMonth += onemonitem.Frequency + "-" + onemonitem.Number + ",";
                        }
                        if (cal.oneMonth != null)
                        {
                            cal.oneMonth = cal.oneMonth.Substring(0, cal.oneMonth.Length - 1);
                        }

                        foreach (var thrmonitem in stathrmon)
                        {
                            cal.threeMonth += thrmonitem.Frequency + "-" + thrmonitem.Number + ",";
                        }
                        if (cal.threeMonth != null)
                        {
                            cal.threeMonth = cal.threeMonth.Substring(0, cal.threeMonth.Length - 1);
                        }

                        foreach (var hayeitem in stahaye)
                        {
                            cal.halfYear += hayeitem.Frequency + "-" + hayeitem.Number + ",";
                        }
                        if (cal.halfYear != null)
                        {
                            cal.halfYear = cal.halfYear.Substring(0, cal.halfYear.Length - 1);
                        }

                        foreach (var yearitem in stayear)
                        {
                            cal.oneYear += yearitem.Frequency + "-" + yearitem.Number + ",";
                        }
                        if (cal.oneYear != null)
                        {
                            cal.oneYear = cal.oneYear.Substring(0, cal.oneYear.Length - 1);
                        }

                        GroupData groupdata = new GroupData();

                        groupdata.group = item.Group;
                        groupdata.gunNumber = gunNumber;
                        groupdata.calculation = cal;
                        groupdatalist.Add(groupdata);
                    }

                }
                HeatDegree fiftydata = new HeatDegree();
                foreach (var item in hunddata.Take(50))
                {
                    switch (i)
                    {
                        case 1:
                            switch (item.Champion)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 2:
                            switch (item.RunnerUp)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 3:
                            switch (item.Third)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 4:
                            switch (item.Fourth)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 5:
                            switch (item.Fifth)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 6:
                            switch (item.Sixth)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 7:
                            switch (item.Seventh)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 8:
                            switch (item.Eighth)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 9:
                            switch (item.Ninth)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                        case 10:
                            switch (item.Tenth)
                            {
                                case 1:
                                    fiftydata.one++;
                                    break;
                                case 2:
                                    fiftydata.two++;
                                    break;
                                case 3:
                                    fiftydata.three++;
                                    break;
                                case 4:
                                    fiftydata.four++;
                                    break;
                                case 5:
                                    fiftydata.five++;
                                    break;
                                case 6:
                                    fiftydata.six++;
                                    break;
                                case 7:
                                    fiftydata.seven++;
                                    break;
                                case 8:
                                    fiftydata.eight++;
                                    break;
                                case 9:
                                    fiftydata.nine++;
                                    break;
                                case 10:
                                    fiftydata.ten++;
                                    break;
                            }
                            break;
                    }
                }
                HeatDegree hundreddata = new HeatDegree();
                foreach (var item in hunddata)
                {
                    switch (i)
                    {
                        case 1:
                            switch (item.Champion)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 2:
                            switch (item.RunnerUp)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 3:
                            switch (item.Third)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 4:
                            switch (item.Fourth)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 5:
                            switch (item.Fifth)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 6:
                            switch (item.Sixth)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 7:
                            switch (item.Seventh)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 8:
                            switch (item.Eighth)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 9:
                            switch (item.Ninth)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                        case 10:
                            switch (item.Tenth)
                            {
                                case 1:
                                    hundreddata.one++;
                                    break;
                                case 2:
                                    hundreddata.two++;
                                    break;
                                case 3:
                                    hundreddata.three++;
                                    break;
                                case 4:
                                    hundreddata.four++;
                                    break;
                                case 5:
                                    hundreddata.five++;
                                    break;
                                case 6:
                                    hundreddata.six++;
                                    break;
                                case 7:
                                    hundreddata.seven++;
                                    break;
                                case 8:
                                    hundreddata.eight++;
                                    break;
                                case 9:
                                    hundreddata.nine++;
                                    break;
                                case 10:
                                    hundreddata.ten++;
                                    break;
                            }
                            break;
                    }
                }
                ranknew.ranking = i;
                ranknew.fiftyheat = fiftydata;
                ranknew.hundredheat = hundreddata;
                ranknew.omission = DbContext.Omissions.Where(o => o.Ranking == i).First();
                ranknew.groupDatas = groupdatalist;
                rankgroupdata.Add(ranknew);
            }
            return Json(rankgroupdata, JsonRequestBehavior.AllowGet);
        }
    }
}