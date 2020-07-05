using JobLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacingPK10.DiscClass
{
    public class CalculationData
    {
        public string tenDay { get; set; }
        public string oneMonth { get; set; }
        public string threeMonth { get; set; }
        public string halfYear { get; set; }
        public string oneYear { get; set; }
    }
    public class GroupData
    {
        public string group { get; set; }
        public int gunNumber { get; set; }
        public CalculationData calculation { get; set; }
    }
    public class HeatDegree
    {
        public int one { get; set; }
        public int two { get; set; }
        public int three { get; set; }
        public int four { get; set; }
        public int five { get; set; }
        public int six { get; set; }
        public int seven { get; set; }
        public int eight { get; set; }
        public int nine { get; set; }
        public int ten { get; set; }
    }
    public class RankGroupData
    {
        public int ranking { get; set; }
        public Omissions omission { get; set; }
        public HeatDegree fiftyheat { get; set; }
        public HeatDegree hundredheat { get; set; }
        public List<GroupData> groupDatas { get; set; }

    }
}