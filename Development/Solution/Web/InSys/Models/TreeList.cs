using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InSys.Models
{
    public class TreeListCtx
    {
        public string Name { get; set; }
        public string ListColumn { get; set; }
    }

    public class TreeListCollection : List<TreeListCtx> { }
}