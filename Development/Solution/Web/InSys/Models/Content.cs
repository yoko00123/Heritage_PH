using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InSys.Models
{
    public class Content
    {
        public Content(string v)
        {
            Reports = Path.Combine(v, "Reports");
            ExcelTemplates = Path.Combine(v, "ExcelTemplates");
            Files = Path.Combine(v, "Files");
            Photos = Path.Combine(v, "Photos");
        }
         
        public string Reports { get; set; }
        public string ExcelTemplates { get; set; }
        public string Files { get; set; }
        public string Photos { get; set; }
    }
}