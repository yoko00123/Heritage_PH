using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static z.SQL.Data.SchemaTable;

namespace InSys.Models
{
    public class InfoSchema
    {
        public string TableName { get; set; }
        public List<Dictionary<string, object>> Schema { get; set; }
        public ObjectTypeEnum Type { get; set; }
    }
}