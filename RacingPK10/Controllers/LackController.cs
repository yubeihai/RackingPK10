using JobLibrary.DataAccess;
using JobLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace RacingPK10.Controllers
{
    public class LackController : Controller
    {
        public RacingDBContext DbContext = new RacingDBContext();
        // GET: Lack
        public ActionResult Index()
        {
            List<int> intlist = new List<int>();
            try
            {
                int Lack = Convert.ToInt32(ConfigurationManager.AppSettings["Lack"]);
                var firstLottery = DbContext.LotteryDatas.OrderByDescending(o => o.IssueId).First();

                for (int i = 0; i < Lack; i++)
                {
                    var include = DbContext.LotteryDatas.Where(l => l.IssueId == firstLottery.IssueId - i).FirstOrDefault();
                    if (include == null)
                    {
                        intlist.Add(firstLottery.IssueId - i);
                    }
                }
                return View(intlist);
            }
            catch (Exception e)
            {
                return View(intlist);
            }
        }
        public bool AddLottery(LotteryData lottery)
        {
            var lotteryfirst = DbContext.LotteryDatas.Where(o => o.IssueId == lottery.IssueId).FirstOrDefault();
            try
            {
                if (lotteryfirst == null)
                {
                    DbContext.LotteryDatas.Add(lottery);
                    Omission();
                    GunsUpdate();
                    DbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void Omission()
        {
            try
            {
                int Lack = Convert.ToInt32(ConfigurationManager.AppSettings["Lack"]);
                var lottery = DbContext.LotteryDatas.OrderByDescending(o => o.IssueId).Take(Lack).ToList();
                lottery = lottery.OrderBy(o => o.IssueId).ToList();
                for (int i = 1; i <= 10; i++)
                {
                    var omiss = DbContext.Omissions.Where(r => r.Ranking == i).FirstOrDefault();
                    if (omiss != null)
                    {
                        foreach (var item in lottery)
                        {
                            switch (item.Champion)
                            {
                                case 1:
                                    omiss.One = 0;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 2:
                                    omiss.One++;
                                    omiss.Two = 0;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 3:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three = 0;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 4:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four = 0;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 5:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five = 0;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 6:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six = 0;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 7:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven = 0;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 8:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight = 0;
                                    omiss.Nine++;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 9:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine = 0;
                                    omiss.Ten++;
                                    DbContext.SaveChanges();
                                    break;
                                case 10:
                                    omiss.One++;
                                    omiss.Two++;
                                    omiss.Three++;
                                    omiss.Four++;
                                    omiss.Five++;
                                    omiss.Six++;
                                    omiss.Seven++;
                                    omiss.Eight++;
                                    omiss.Nine++;
                                    omiss.Ten = 0;
                                    DbContext.SaveChanges();
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        public void GunsUpdate()
        {
            try
            {
                var connumdata = DbContext.ContinuityNumbers.OrderBy(a => a.Id).ToList();
                foreach (var item in connumdata)
                {
                    item.TenNumber = 0;
                    DbContext.SaveChanges();
                }
                int Lack = Convert.ToInt32(ConfigurationManager.AppSettings["Lack"]);
                var lottery = DbContext.LotteryDatas.OrderByDescending(o => o.IssueId).Take(Lack).ToList();
                lottery = lottery.OrderBy(o => o.IssueId).ToList();
                for (int i = 1; i <= 10; i++)
                {
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
                        foreach (var lotteryitem in lottery.Take(50))
                        {
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
                                }
                            }
                            else
                            {
                                prize.TenNumber = 0;
                            }
                            DbContext.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}