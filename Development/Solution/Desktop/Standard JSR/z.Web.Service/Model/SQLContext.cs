using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace z.Web.Service.Model
{
   public class SQLContext
    {
        public string Qry { get; set; }
        public string[] Parameters { get; set; }
        public object[] Values { get; set; }
    }
}
