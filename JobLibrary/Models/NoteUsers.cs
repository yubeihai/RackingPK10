using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobLibrary.Models
{
    public class NoteUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Cookie { get; set; }
        public string Oid { get; set; }
        public DateTime PublishTime { get; set; }
    }
}