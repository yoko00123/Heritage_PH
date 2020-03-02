using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace z.Web.Service.Arguments
{
    public class ProcessDataArgs : EventArgs
    {
       public long length { private get; set; }
       public int position { private get; set; }
       public long percent { get; set; }
       public double speed { private get; set; }

        /// <summary>
        /// KB
        /// </summary>
       public double CurrentSize
       {
           get
           {
               return Math.Round(Convert.ToDouble(position / 1024), 2);
           }
       }

        /// <summary>
        /// KB
        /// </summary>
       public double TotalSize
       {
           get
           {
               return Math.Round(Convert.ToDouble(length / 1024), 2);
           }
       }

        /// <summary>
        /// KB
        /// </summary>
       public double CurrentSpeed
       {
           get
           {
               return Math.Round(Convert.ToDouble(speed / 1024), 2);
           }
       }

    }
}
