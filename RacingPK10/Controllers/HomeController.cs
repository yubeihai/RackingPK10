using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using JobLibrary.DataAccess;
using JobLibrary.Models;

namespace RacingPK10.Controllers
{
    public class HomeController : Controller
    {
        public RacingDBContext DbContext = new RacingDBContext();

        public ActionResult Index()
        {

            List<LotteryData> lottery = DbContext.LotteryDatas.OrderByDescending(l => l.IssueId).Take(15).ToList();
            return View(lottery);
        }
    }
}