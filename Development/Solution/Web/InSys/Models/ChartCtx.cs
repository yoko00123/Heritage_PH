using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using z.Data;

namespace InSys.Models
{
    public class ChartCtx
    {
        public int GroupID;
        public string GroupName;
        public List<string> SeriesName = new List<string>();
        public List<int> Value = new List<int>();
        public List<decimal> Percentage = new List<decimal>();

        public List<ChartCtx> GenerateChartObject(DataTable dt, int widgetType) {
            List<ChartCtx> ch = new List<ChartCtx>();
            if (widgetType == 3 || widgetType == 5)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ChartCtx chrt = new ChartCtx();
                    if (ch.Where(x => x.GroupName == dr["Label"].ToString()).Count() > 0)
                    {
                        chrt.GroupName = dr["Label"].ToString() + "_dupe";
                    }
                    else
                    {
                        chrt.GroupName = dr["Label"].ToString();
                    }
                    if (dt.Columns.Contains("ID")) chrt.GroupID = dr["ID"].ToInt32();
                    chrt.Value.Add(dr["Value"].ToInt32());
                    chrt.Percentage.Add(Convert.ToDecimal(dr["Percentage"]));
                    ch.Add(chrt);
                }
            }
            else {
                foreach (DataRow dr in dt.Rows) {
                    ChartCtx chrt = new ChartCtx();
                    if (ch.Where(x => x.GroupID == dr["ID"].ToInt32()).Count() > 0)
                    {
                        chrt = ch.Where(x => x.GroupID == dr["ID"].ToInt32()).FirstOrDefault();
                        if (dt.Columns.Contains("Series")) chrt.SeriesName.Add(dr["Series"].ToString());
                        chrt.Value.Add(dr["Value"].ToInt32());
                    }
                    else {
                        chrt.GroupID = dr["ID"].ToInt32();
                        if (ch.Where(x => x.GroupName == dr["Group"].ToString()).Count() > 0)
                        {
                            chrt.GroupName = dr["Group"].ToString() + "_dupe";
                        }
                        else
                        {
                            chrt.GroupName = dr["Group"].ToString();
                        }
                        if (dt.Columns.Contains("Series")) chrt.SeriesName.Add(dr["Series"].ToString());
                        if(dt.Columns.Contains("Percentage")) chrt.Percentage.Add(Convert.ToDecimal(dr["Percentage"]));
                        chrt.Value.Add(dr["Value"].ToInt32());
                        ch.Add(chrt);
                    }
                }
            }
            return ch;
        }
    }
}