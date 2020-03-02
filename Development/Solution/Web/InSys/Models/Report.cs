using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InSys.Models
{

    public class ReportParameters
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public object Model { get; set; }
    }

    public class ReportParameterCollection : List<ReportParameters>
    {
    }

}