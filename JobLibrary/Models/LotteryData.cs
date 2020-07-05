using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobLibrary.Models
{
    public class LotteryData
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int Champion { get; set; }
        public int RunnerUp { get; set; }
        public int Third { get; set; }
        public int Fourth { get; set; }
        public int Fifth { get; set; }
        public int Sixth { get; set; }
        public int Seventh { get; set; }
        public int Eighth { get; set; }
        public int Ninth { get; set; }
        public int Tenth { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}