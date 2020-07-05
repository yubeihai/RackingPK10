using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLibrary.Models
{
    public class ContinuityNumber
    {
        public int Id { get; set; }
        public int NGId { get; set; }
        public int RankingId { get; set; }
        public int TenNumber { get; set; }
        public int MonthNumber { get; set; }
        public int ThreeMonthNumber { get; set; }
        public int HalfYearNumber { get; set; }
        public int YearNumber { get; set; }
    }
}
