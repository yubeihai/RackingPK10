using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobLibrary.Models
{
    public class NumeralGroup
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public int Ranking { get; set; }
        public DateTime PublishTime { get; set; }
    }
}