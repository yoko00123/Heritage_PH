using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InSys.Models
{
    public class FilterSchema
    {
        public string Name { get; set; }
        public object[] Value { get; set; }
        public FilterType Type { get; set; }
    }

    public class FilterCollection : List<FilterSchema> { }

    public enum FilterType : int
    {
        Like = 1,
        Equal = 2,
        Between = 3,
        In = 4,
        Greater = 5,
        Lesser = 6
    }
}