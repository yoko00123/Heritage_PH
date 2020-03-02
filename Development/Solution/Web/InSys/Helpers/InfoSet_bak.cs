using InSys.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using z.Data;
using z.SQL;
using System.Runtime.Caching;

namespace InSys.Helpers
{
    /// <summary>
    /// LJ 20160505
    /// InfoSet, Coming from DataTable
    /// </summary>
    /// 
    public class InfoSet_bak : IDisposable
    {
        protected DataSet ds;
        protected Menu menu;
        protected SqlConnection conn;
        protected HttpContext httpcontext;
        protected Dictionary<string, List<int?>> rowDeleted;
        protected DataRow row;
        internal ObjectCache cache { get; set; }
        internal CacheItemPolicy policy { get; set; }

        public Pair Session { private get; set; }

        public InfoSet_bak(int ID_Menu, IQueryArgs sql, HttpContext httpcontext)
        {
            var strg = new Storage.Storage();
            var contnr = strg.Container("MenuSet");

            this.conn = new SqlConnection((sql as Query.QueryArgs).GetConnectionString());
            this.httpcontext = httpcontext;
            this.menu = strg.DownloadString(contnr, $"{ID_Menu }.InSysModule").CompressFromBase64().ToObject<Menu>();
        }

        public void Saveinfo(string data, string RowDeleted)
        {
            SqlTransaction tran = default(SqlTransaction);
            try
            {
                //set relationships
                Status("Preparing Tables");
                this.PrepareTables();

                Status("Deserializing Data");
                this.DeserializedTables(data);
                this.DeserializedRowDeleted(RowDeleted);

                //prepare
                this.conn.Open();
                tran = this.conn.BeginTransaction();

                Status("Saving Header");
                //save master
                var master = ds.Tables[menu.tMenu.TableName] as ZDataTable;
                master.SelectCommand.Transaction = tran;
                master.Update();

                Status("Updating Relations");
                this.SetRelations();

                Status("Saving Details");
                //save details
                int i = 1;

                foreach (ZDataTable dt in ds.Tables)
                {
                    if (dt.TableName != menu.tMenu.TableName)
                    {
                        Status($"Saving Details { i } of { ds.Tables.Count - 1 }");

                        if (this.rowDeleted.ContainsKey(dt.TableName)) //sometimes the collection doesn't register empty IDS
                            dt.DeletedID = this.rowDeleted[dt.TableName];
                        dt.SelectCommand.Transaction = tran;
                        dt.Update();
                    }
                    i++;
                }

                Status("Revalidating Data");
                this.ValidateRequiredIf(master, tran);

                Status("Comitting Save Infos");
                tran.Commit();

                Status("Done");
                this.row = master.Rows[0];
            }
            catch (Exception ex)
            {
                tran?.Rollback();
                string msg = ex.Message;
                try //throwable condition, must enclosed with try catch
                {
                    msg = new Query(conn).ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                }
                catch { }
                throw new Exception(msg, ex);
            }
            finally
            {
                tran?.Dispose();
                conn?.Close();
            }
        }

        private void ValidateRequiredIf(DataTable dt, SqlTransaction tran)
        {
            try
            {
                var masterp = RowToPair(dt.Rows[0]);

                menu.tMenuTabField.Where(x => x.RequiredIf.IsNull("").ToString() != "" && x.IsActive == true)
                    .Each(x =>
                    {
                        var h = x.RequiredIf.PassParameter(Session);
                        h = h.PassParameter(masterp, '$');
                        if (dt.Select(h).Length > 0 && dt.Rows[0][x.Name].IsNull("").ToString() == "")
                        {
                            throw new Exception($"{ x.EffectiveLabel } is required");
                        }
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveTrigger(string Command)
        {
            if (Command != "" && Command != null)
            {
                Command = Command.Replace("@ID", row["ID"].ToString());
                new Query(conn).ExecNonQuery(Command);
            }
        }

        protected void DeserializedTables(string tdata)
        {
            var json = (JObject)JsonConvert.DeserializeObject(tdata);
            foreach (var f in json)
            {
                var dt = ds.Tables[f.Key] as ZDataTable;
                if (f.Value.Type == JTokenType.Object)
                    AddRow(dt, f.Value.ToObject<JObject>());
                else if (f.Value.Type == JTokenType.Array)
                {
                    if (dt == null && !f.Key.StartsWith("t")) continue;
                    if (dt == null) dt = TryCreateTable(f.Key); //for custom table injection
                    foreach (JObject s in f.Value)
                        AddRow(dt, s);
                }
            }
        }

        private ZDataTable TryCreateTable(string key)
        {
            var dtl = new ZDataTable(conn, key, false, HostName, Session["ID_Session"].ToInt32(), menu.tMenu.Name);
            ds.Tables.Add(dtl);

            //create fake menudata
            var master = menu.tMenu;
            var FakeID = Math.Abs(new Random().Next(111111111, 999999999));
            menu.tMenuDetailTab.Add(new tMenuDetailTab()
            {
                ID = FakeID,
                Name = key,
                TableName = key,
                ParentColumn = "ID",
                ChildColumn = $"ID_{master.TableName.Substring(1)}",
                ID_Menu = master.ID,
                IsActive = true,
                ID_MenuDetailTabType = MenuDetailTabType.Grid //set by default
            });

            return dtl;
        }

        protected void DeserializedRowDeleted(string RowDeleted)
        {
            this.rowDeleted = new Dictionary<string, List<int?>>();
            var json = (JObject)JsonConvert.DeserializeObject(RowDeleted);
            foreach (var f in json)
            {
                var j = new List<int?>();
                foreach (var s in f.Value)
                    j.Add(s.Value<int?>());
                rowDeleted.Add(f.Key, j);
            }
        }

        protected void PrepareTables()
        {
            ds = new DataSet();
            ds.Tables.Add(new ZDataTable(conn, menu.tMenu.TableName, menu.tMenu.HasAuditTrail.ToBool(), HostName, Session["ID_Session"].ToInt32(), menu.tMenu.Name));
            foreach (tMenuDetailTab tab in menu.tMenuDetailTab.Where(x => (new int[] { 1, 2 }).Where(y => y == x.ID_MenuDetailTabType.ToInt32()).Any() && x.TableName.StartsWith("t"))) //fuck the views
                ds.Tables.Add(new ZDataTable(conn, tab.TableName, tab.HasAuditTrail.ToBool(), HostName, Session["ID_Session"].ToInt32(), menu.tMenu.Name));
        }

        protected void SetRelations()
        {
            var master = ds.Tables[menu.tMenu.TableName];
            menu.tMenuDetailTab.Where(x => (new int[] { 1, 2 }).Where(y => x.ParentTableName.IsNull("").ToString() == "" && y == x.ID_MenuDetailTabType.ToInt32()).Any() && x.TableName.StartsWith("t")).Each(x => // 
            {
                foreach (DataRow dr in ds.Tables[x.TableName].Rows)
                    //dr[x.ChildColumn] = master.Rows[0][x.ParentColumn]; Mod Weng
                    if (x.ChildColumn.ToString().Contains("|") == true && x.ParentColumn.ToString().Contains("|") == true)
                    {
                        string[] drchild = x.ChildColumn.ToString().Split('|');
                        string[] tablechild = x.ParentColumn.ToString().Split('|');
                        for (int i = 0; i < drchild.Length; i++)
                        {
                            if (dr.Table.Columns[drchild[i]].ReadOnly == false)
                                dr[drchild[i]] = master.Rows[0][tablechild[i]];
                        }
                    }
                    else
                    {
                        dr[x.ChildColumn] = master.Rows[0][x.ParentColumn];
                    }

            });
        }

        protected void AddRow(ZDataTable dt, JObject f)
        {
            var Column = default(string);
            try
            {
                var dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    try
                    {
                        Column = dc.ColumnName;
                        var obj = default(object);  //f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase);
                        JToken jj = default(JToken);
                        if (f.TryGetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase, out jj))
                            if (dc.DataType == typeof(string)) obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<string>();
                            else if (dc.DataType == typeof(int))
                            {
                                obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<Single?>();
                                if (obj != null)
                                    obj = Convert.ToInt32(obj);   //obj.ToInt32();
                            }
                            else if (dc.DataType == typeof(decimal)) obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<decimal?>();
                            else if (dc.DataType == typeof(float)) obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<float?>();
                            else if (dc.DataType == typeof(Single)) obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<Single?>();
                            else if (dc.DataType == typeof(double)) obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<double?>();
                            else if (dc.DataType == typeof(bool)) obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<bool?>();
                            else if (dc.DataType == typeof(DateTime)) obj = f.GetValue(dc.ColumnName, StringComparison.OrdinalIgnoreCase).Value<DateTime?>();

                        if (!dc.AllowDBNull && obj == null) obj = dc.DefaultValue;
                        if (dc.ColumnName == "ID" && obj.IsNull(0).ToInt32() == 0)
                        {
                            // case when new row inserted but table doesnt allow duplicate ID
                            // we need to decrement values just to set the statement inserted
                            obj = dt.Columns[dc.ColumnName].AutoIncrementSeed;
                            --dt.Columns[dc.ColumnName].AutoIncrementSeed;
                        }
                        dr[dc.ColumnName] = obj == null ? DBNull.Value : obj;

                        if (dr[dc.ColumnName] == DBNull.Value && dc.DefaultValue != null)
                            dr[dc.ColumnName] = dc.DefaultValue;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                var ff = menu.tMenuDetailTab.Where(x => x.TableName == dt.TableName && x.ParentTableName.IsNull("").ToString() == "");
                if (ff.Any())
                {
                    var fff = ff.Single();
                    //Mod Weng
                    //if (fff.ParentTableName.IsNull("").ToString() == "")
                    //    dr[fff.ChildColumn] = ds.Tables[menu.tMenu.TableName].Rows[0][fff.ParentColumn];
                    if (fff.ChildColumn.ToString().Contains("|") == true && fff.ParentColumn.ToString().Contains("|") == true)
                    {
                        string[] drchild = fff.ChildColumn.ToString().Split('|');
                        string[] tablechild = fff.ParentColumn.ToString().Split('|');
                        for (int i = 0; i < drchild.Length; i++)
                        {
                            dr[drchild[i]] = ds.Tables[menu.tMenu.TableName].Rows[0][tablechild[i]];
                        }
                    }
                    else
                    {
                        dr[fff.ChildColumn] = ds.Tables[menu.tMenu.TableName].Rows[0][fff.ParentColumn];
                    }
                }
                dt.Rows.Add(dr);
                dr.EndEdit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Table: {dt.TableName }, {Column}; { ex.Message }");
            }
        }

        protected int TableIdentity(string TableName)
        {
            return new Query(conn).ExecScalar("SELECT IDENT_CURRENT(@Table)", TableName).ToInt32();
        }

        public int? ID
        {
            get
            {
                return this.row?.Field<int>("ID");
            }
        }

        public string HostName { get; internal set; }
        public string RequestID { get; internal set; }

        protected Pair RowToPair(DataRow dr)
        {
            var p = new Pair(StringComparer.OrdinalIgnoreCase);
            dr.Table.Columns.Cast<DataColumn>().Each(c => p.Add(c.ColumnName, dr[c.ColumnName]));
            return p;
        }

        protected void Status(string Message)
        {
            cache.Set(RequestID, Message, policy);
        }

        public void Dispose()
        {
            ds?.Dispose();
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}