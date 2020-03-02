using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using z.Data;
using z.SQL.Data;
using static z.SQL.Extensions;

namespace InSys.Helpers
{
    /// <summary>
    /// @LJ20160919
    /// Extend ZTable - Implement InSys Data Auditing
    /// </summary>
    public class ZDataTable : ZTable
    {
        public bool HasAuditTrail { private get; set; } = false;
        public string HostName { private get; set; }
        public int ID_Session { private get; set; }

        public string Menu { private set; get; }

        public string AuditTable { get; set; } = @"tAuditTrail";

        public ZDataTable(SqlConnection args, string tablename, bool HasAuditTrail, string HostName, int ID_Session, string Menu) : base(args, tablename)
        {
            this.HasAuditTrail = HasAuditTrail;
            this.HostName = HostName;
            this.ID_Session = ID_Session;
            this.Menu = Menu;

            if (HasAuditTrail)
            {
                SQLScript = Properties.Resources.Execute_With_Audit_Trail_VS1;
                var h = new StringBuilder();
                var g = BuildScriptCore();

                Interpolate(ref SQLScript, "Menu", Menu);
                Interpolate(ref SQLScript, "ID_Session", ID_Session.ToString());
                Interpolate(ref SQLScript, "HostName", HostName);
                Interpolate(ref SQLScript, "TableName", TableName);
                Interpolate(ref SQLScript, "SchemaTable", g.SchemaString.Join());
                Interpolate(ref SQLScript, "SchemaUpdate", g.ColString.Select(x => x.Update).Join());
                Interpolate(ref SQLScript, "SchemaInsert", g.ColString.Select(x => x.Insert).Join());
                Interpolate(ref SQLScript, "ColumnInsert", g.ColString.Select(x => x.OutInsert).Join());
                Interpolate(ref SQLScript, "SelectInsert", g.ColString.Select(x => x.TargetInsert).Join());

                //Select
                var c = this.SchemaTable.Rows.Cast<DataRow>().OrderBy(x => x["SeqNo"].ToInt32()).Select(x => $"[{ x["ColumnName"].ToString() }]");
                ScrptSelect = $"Select { c.Join() } From v{ this.TableName.Substring(1) }";
            }
        }
    }
}