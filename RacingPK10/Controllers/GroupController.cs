using JobLibrary.DataAccess;
using JobLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacingPK10.Controllers
{
    public class GroupController : Controller
    {
        public RacingDBContext DbContext = new RacingDBContext();
        // GET: Group
        public ActionResult Index()
        {
            List<NumeralGroup> numeralgroup = DbContext.NumeralGroups.OrderBy(n => n.Ranking).ToList();
            return View(numeralgroup);
        }
    }
}