using InSys.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using z.Data;
using z.SQL;

namespace InSys.Helpers
{
    public class InfoPrintList : IDisposable
    {
        private HttpContext ctx;
        private IQueryArgs sql;
        private int v;
        private PrintArgument Q;

        public InfoPrintList(int v, IQueryArgs sql, HttpContext ctx, Pair Q)
        {
            this.v = v;
            this.sql = sql;
            this.ctx = ctx;
            this.Q = Q.ToObject<PrintArgument>();
        }

        public string HostName { get; internal set; }
        public Pair Session { get; internal set; }
        public string FileName { get; set; }

        internal void Print()
        {
            switch (Q.Doc.DocType)
            {
                case 1: PDF(); break;
                case 2: Excel(); break;
                case 3: CSV(); break;

            }
        }

        private void CSV()
        {
            throw new NotImplementedException();
        }

        private void Excel()
        {
            throw new NotImplementedException();
        }

        private void PDF()
        {
            throw new NotImplementedException();
        }

        protected DataTable LoadData()
        {
            throw new NotImplementedException();
            //var o = string.Empty;
            //var w = string.Empty;
            //var Columns = Q["Columns", "*"].ToString();
            //var DataSource = Q["DataSource"].ToString();
            //var OrderBy = Q["OrderBy", "ID Desc"].ToString();
            //var Skip = Q["Skip", 1].ToInt32();
            //var Take = Q["Take", 30].ToInt32();
            //var Where = Q["Where"]?.ToString().ToObject<FilterCollection>();    //Q["Where", ""].ToString();
            //var SearchAll = Q["SearchAll", ""].ToString();
            //var FixedFilter = Q["FixedFilter", "1 = 1"].ToString();

            //var dt = new DataTable();
            //var count = 0;
            //var np = new Pair();
            //var nWhere = new List<string>();

            //var nFltr = BuildFilter(Where);
            //if (nFltr != "")
            //    nFltr = $"{ nFltr } AND { FixedFilter }";
            //else
            //    nFltr = $"WHERE { FixedFilter }";

            //if (GetSQLVersion() > 10)
            //{
            //    if (SearchAll != "")
            //    {
            //        var dName = "";
            //        if (DataSource.ToLower().Split(new[] { "from" }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
            //        {
            //            DataSource = DataSource.Replace("\r", " ").Replace("\n", " ");
            //            dName =
            //                DataSource.ToLower().Split(new[] { "from" }, StringSplitOptions.RemoveEmptyEntries)[1]
            //                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
            //            dName = dName.Replace("dbo.", "");
            //        }
            //        o = $"EXEC dbo.pSearchAllColumns '{Columns}', '{EscapeSQLString(SearchAll)}', '{(dName == "" ? DataSource : dName)}'";
            //        var WhereString = nFltr != ""
            //                ? $"WHERE {nFltr}"
            //                : "WHERE " + Sql.ExecScalar(o.Replace("dbo.", ""));
            //        o = $"Select {Columns} From {DataSource} { WhereString } Order By {OrderBy} OFFSET {(Skip - 1) * Take} ROWS FETCH NEXT {Take} ROWS ONLY";
            //        w = $"Select COUNT(1) [RowCount] From {DataSource} { WhereString }";
            //    }
            //    else
            //    {
            //        o = $"Select {Columns} From {DataSource} { nFltr } Order By {OrderBy} OFFSET {(Skip - 1) * Take} ROWS FETCH NEXT {Take} ROWS ONLY";
            //        w = $"Select COUNT(1) [RowCount] From {DataSource} { nFltr }";
            //    }
            //}
            //else
            //{
            //    o = $"Select Top {Take} {Columns} From {DataSource} { nFltr } Order By {OrderBy}";
            //    w = $"Select COUNT(1) [RowCount] From {DataSource} { nFltr }";
            //}

            //o = $"Set NoCount On; {o} OPTION (ROBUST PLAN, FAST 30, FORCE ORDER, KEEPFIXED PLAN, MAXRECURSION 0)";

            //if (nWhere.Count > 0)
            //{
            //    using (var q = new Query(Sql))
            //    {
            //        dt = q.TableQuery(o, np.Select(x => x.Key).ToArray(), np.Select(x => x.Value).ToArray());
            //        count = q.ExecScalar(w, np.Select(x => x.Key).ToArray(), np.Select(x => x.Value).ToArray()).ToInt32();
            //    }
            //}
            //else
            //{
            //    dt = Sql.TableQuery(o);
            //    count = Sql.ExecScalar(w).ToInt32();
            //}

            //r.ResultSet = new
            //{
            //    rows = dt.Rows.JsonModel(),
            //    count = count
            //};

            //dt?.Dispose(); //clean 
            //GC.Collect();

            //return r;
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }

    public class PrintArgument
    {
        public int ID_Menu { get; set; }
        public PrintDataSource Data { get; set; }
        public PrintDoc Doc { get; set; }
    }

    public class PrintDataSource
    {
        public string DataSource { get; set; }
        public int Skip { get; set; }
        public string Where { get; set; }
        public string TableName { get; set; }
        public FilterSchema FilterColumns { get; set; }
        public int Take { get; set; }
        public string OrderBy { get; set; }
    }

    public class PrintDoc
    {
        public int DocType { get; set; }

        public int Type { get; set; }

        public int Layout { get; set; }

        public string Page { get; set; }
    }
}