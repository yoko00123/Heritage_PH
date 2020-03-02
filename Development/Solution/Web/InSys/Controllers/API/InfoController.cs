using InSys.Controllers;
using InSys.Helpers;
using InSys.Models;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using z.Data;
using z.Data.JsonClient;
using InSys.Office;
using z.SQL;
using z.SQL.Data;
using Hangfire;
using InSys.Storage;
using System.Net.Mime;
using System.Net.Mail;

namespace InSys.Controllers.API
{
    [AuthorizeRequest]
    public class InfoController : ActionController
    {

        /// <summary>
        /// LJ20170301
        /// Pinag isa ko na ung load ng lahat ng schema , para plot nlng sa client
        /// :For Testing and need to test, mlking data pa din to if marming table
        /// ?ginwa sya para maiwasan ang late binding sa js since dati naka stand alone
        /// </summary>
        /// <returns></returns>
        /// 


        [HttpPost]
        public async Task<Result> GetInfoSchema() => await TaskResult(r =>
        {

            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container("MenuSet");

                var fle = strg.DownloadString(cntr, $"{ Q["ID_Menu"] }.InSysModule").CompressFromBase64().ToObject<Menu>();

                var k = new List<InfoSchema>();

                k.Add(new InfoSchema
                {
                    TableName = fle.tMenu.TableName,
                    Schema = TableSchema(fle.tMenu.TableName, "v" + fle.tMenu.TableName.Substring(1)).JsonModel(),
                    Type = ObjectType(fle.tMenu.TableName)
                });

                foreach (var dt in fle.tMenuDetailTab.Where(x => new List<int> { 1, 2, 5 }.Contains(x.ID_MenuDetailTabType.ToInt32())))
                {
                    k.Add(new InfoSchema
                    {
                        TableName = dt.TableName,
                        Schema = TableSchema(dt.TableName, "v" + dt.TableName.Substring(1)).JsonModel(),
                        Type = ObjectType(dt.TableName)
                    });
                }

                r.ResultSet = new
                {
                    Menu = fle,
                    SchemaTable = k
                };
                return r;
            }

        });

        public async Task<Result> rGetUserGroup() => await TaskResult(r =>
        {
            try
            {
                string str = "select * from vUserGroupMenuSolution where id = @ID_Menu and ID_UserGroup = @ID_UserGroup ";
                var dt = Sql.TableQuery(str, Q["ID_Menu"], Q["ID_UserGroup"]);
                r.ResultSet = new { data = dt };
            }
            catch (Exception ex)
            {
                r.ResultSet = new { error = ex.Message.ToString() };
            }
            return r;
        });

        public async Task<Result> LoadInfo() => await TaskResultSet(() =>
        {
            using (var dt = new ZTable(Sql, Q["TableName"].ToString()))
            {
                dt.Load(Q["ID"].ToInt32());
                var dr = default(DataRow);
                if (dt.Rows.Count == 0)
                    dr = dt.NewRow();
                else
                    dr = dt.Rows[0];

                logger.Information($"ID: { Q["ID"] }, Table: { Q["TableName"] }");

                return new
                {
                    Columns = dt.Columns.Cast<DataColumn>().Select(x => new { ColumnName = x.ColumnName, DataType = x.DataType.Name }).ToArray(),
                    Row = dr.JsonModel()
                };
            }
        });

        public async Task<Result> LoadCombo() => await TaskResult(r =>
        {
            if (Q["DataSource"].IsNull("").ToString() == "") throw new Exception("DataSource not Specified");
            string filter = (Q["filter"] != null) ? $" Where  {Q["filter"]}" : "";
            string Sort = "ID";

            if (Q["Sort"] == null && Q["Name"].ToString() == "ID_Area") Sort = "Name";
            if (Q["Sort"] != null) Sort = Q["Sort"].ToString();

            var Columns = "ID, Name";
            if (Q["Columns"] != null) Columns = Q["Columns"].ToString();


            List<Dictionary<string, object>> dtr = new List<Dictionary<string, object>>();

            var dc = new Dictionary<string, object>();
            dc.Add("ID", DBNull.Value);
            dc.Add("Name", "-");
            dtr.Add(dc);

            var temp = Sql.TableQuery($"Select { Columns } from { Q["DataSource"] } {filter} Order By { Sort }").Rows.JsonModel();
            temp.ForEach(x => dtr.Add(x));

            r.ResultSet = new
            {
                Name = Q["Name"].ToString(),
                DataList = dtr.ToArray()
            };
            return r;
        });

        #region Save
        public async Task<Result> SaveInfo() => await TaskResult(r =>
        {
            try
            {
                var requestId = Q["RequestID"].ToString();
                ObjectCache cache = MemoryCache.Default;

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
                cache.Set(requestId, "Saving Info", policy);
                cache.Set($"{ requestId }_Status", 0, policy);
                using (var info = new InfoSet(Q["ID_Menu"].ToInt32(), this.Sql, this.Ctx))
                {
                    info.Session = GetSession();
                    info.HostName = GetUserIp();
                    info.RequestID = requestId;
                    info.cache = cache;
                    info.policy = policy;
                    info.logger = logger;

                    Task.Run(() =>
                    {
                        try
                        {

                            info.Saveinfo(Q["Data"].ToString(), Q["RowDeleted"].ToString());
                            info.SaveTrigger(Q["SaveTrigger"]?.ToString());

                            cache.Set($"{ requestId }_InfoID", info.ID, policy);
                            cache.Set($"{ requestId }_Status", 1, policy);



                        }
                        catch (Exception ex)
                        {
                            ex = (ex.InnerException != null) ? ex.InnerException : ex;
                            string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();

                            cache.Set($"{ requestId }_Status", 2, policy);
                            cache.Set($"{ requestId }_ErrorMsg", msg != "" ? msg : ex.Message, policy);

                            logger.Error(ex, ex.Message);
                        }
                        finally
                        {
                            cache.Remove(requestId); //remove when done
                            GC.Collect();
                        }
                    });
                }
                return r;
            }
            catch (Exception ex)
            {
                ex = (ex.InnerException != null) ? ex.InnerException : ex;
                string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                throw new Exception(msg != "" ? msg : ex.Message, ex);
            }
        });

        public async Task<Result> SavingStatus() => await TaskResult(r =>
        {
            ObjectCache cache = MemoryCache.Default;
            var Status = cache.Get($"{ Q["RequestID"].ToString() }_Status").IsNull(1).ToInt32(); //cache.Get(Q["RequestID"].ToString());

            switch (Status)
            {
                case 0: //ongoing
                    var msg = cache.Get(Q["RequestID"].ToString());
                    r.ResultSet = new { Message = msg.ToString(), Status = 0 };
                    break;
                case 1: //done
                    var InfoID = cache.Get($"{ Q["RequestID"].ToString() }_InfoID");

                    cache.Remove($"{ Q["RequestID"].ToString() }_InfoID");
                    cache.Remove($"{ Q["RequestID"].ToString() }_Status");

                    r.ResultSet = new { Message = "Completed", Status = 1, ID = InfoID };
                    break;
                case 2:
                    var err = cache.Get($"{ Q["RequestID"].ToString()  }_ErrorMsg");

                    cache.Remove($"{ Q["RequestID"].ToString() }_ErrorMsg");
                    cache.Remove($"{ Q["RequestID"].ToString() }_Status");

                    throw new Exception(err.ToString());
            }

            //if (Status != null)
            //    r.ResultSet = new { Message = Status.ToString(), Status = 0 };
            //else
            //{
            //    var InfoID = cache.Get($"{ Q["RequestID"].ToString() }_InfoID");
            //    cache.Remove($"{ Q["RequestID"].ToString() }_InfoID"); //remove when done
            //    r.ResultSet = new { Message = "Completed", Status = 1, ID = InfoID };
            //}
            return r;
        });

        public async Task<Result> SaveHeader() => await TaskResult(r =>
        {
            try
            {
                using (var info = new InfoSet(Q["ID_Menu"].ToInt32(), this.Sql, this.Ctx))
                {
                    info.Session = GetSession();
                    info.HostName = GetUserIp();
                    info.SaveHeader(Q["Data"].ToString());

                    r.ResultSet = info.Row;

                    return r;
                }
            }
            catch (Exception ex)
            {
                ex = (ex.InnerException != null) ? ex.InnerException : ex;
                string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                throw new Exception(msg != "" ? msg : ex.Message, ex);
            }
        });

        public async Task<Result> SaveDetail() => await TaskResult(r =>
        {
            try
            {
                using (var info = new InfoSet(Q["ID_Menu"].ToInt32(), this.Sql, this.Ctx))
                {
                    info.Session = GetSession();
                    info.HostName = GetUserIp();
                    info.SaveDetail(Q["TableName"].ToString(), Q["Data"].ToString(), Q["RowDeleted"].ToString(), Q["RowHeader"].ToString());
                    return r;
                }
            }
            catch (Exception ex)
            {
                ex = (ex.InnerException != null) ? ex.InnerException : ex;
                string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                throw new Exception(msg != "" ? msg : ex.Message, ex);
            }
        });

        public async Task<Result> SaveTrigger() => await TaskResult(r =>
        {
            try
            {
                var Command = Q["CommandText"].ToString();
                if (Command != "" && Command != null)
                {
                    Command = Command.Replace("@ID", Q["ID"].ToString());
                    Sql.ExecNonQuery(Command);
                }
                return r;
            }
            catch (Exception ex)
            {
                ex = (ex.InnerException != null) ? ex.InnerException : ex;
                string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                throw new Exception(msg != "" ? msg : ex.Message, ex);
            }
        });

        #endregion

        public async Task<Result> DetailInfo() => await TaskResult(r =>
        {
            var pColumn = Q["ParentColumn"].ToString().Split(',');
            var cColumn = Q["ChildColumn"].ToString().Split(',');
            var row = Q["Row"].ToString().ToObject<Pair>();
            var Order = Q["Order", "ID Desc"].ToString();
            var filter = Q["Filter"]?.ToString();

            var k = new List<string>();
            var j = new List<object>();
            for (var i = 0; i < cColumn.Length; i++)
            {
                k.Add($"{ cColumn[i] } = @{ cColumn[i] }");
                j.Add(row[pColumn[i], DBNull.Value]);
            }
            if (filter != null) {
                k.Add(filter);
            }
            var query = $"Select *, ROW_NUMBER() OVER (ORDER BY (SELECT 1)) XXX_ROWID from dbo.v{ Q["TableName"].ToString().Substring(1) } With(NOLOCK) where {  k.Join(" AND ") } ORDER BY { Order }";
            r.ResultSet = Sql.TableQuery(query, j.ToArray()).Rows.JsonModel();

            return r;
        });

        /// <summary>
        /// Custom Parameters Starts with Colon (:)
        /// </summary>
        /// <returns></returns>
        public async Task<Result> LoadCustomParameters() => await TaskResult(r =>
        {
            var g = Q["Param"].ToString().ToObject<Pair>();
            var j = new Pair();
            foreach (var h in g)
                j.Add(h.Key, Sql.ExecScalar(h.Value.ToString()));
            r.ResultSet = j;
            return r;
        });

        public async Task<Result> PrintInfo() => await TaskResult(r =>
        {
            try
            {
                using (var info = new InfoPrint(Q["ID_Menu"].ToInt32(), this.Sql, this.Ctx))
                {
                    info.Session = GetSession();
                    info.HostName = GetUserIp();
                    info.Print(Q["Data"].ToString(), Q["opt"].ToInt32());
                    r.ResultSet = info.FileName.CompressUriEncoded();
                    return r;
                }
            }
            catch (Exception ex)
            {
                ex = (ex.InnerException != null) ? ex.InnerException : ex;
                string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                throw new Exception(msg != "" ? msg : ex.Message, ex);
            }
        });

        public virtual async Task<Result> ButtonValidateCommand() => await TaskExec(Q["ValidateCommandText"].ToString());

        public virtual async Task<Result> ButtonCommand() => await TaskResult(r =>
        {
            string CommandText = Q["CommandText"].ToString();

            r.ResultSet = this.Sql.ExecScalar(CommandText);

            return r;
        });

        public virtual async Task<Result> LookUpExtraFields() => await TaskRow($"Select { Q["Columns"] } From { Q["Tablename"] } With(NOLOCK) Where ID = @ID", Q["ID"]);

        public async Task<Result> LoadTable() => await TaskResult(r =>
        {
            var o = string.Empty;
            var w = string.Empty;
            var Columns = Q["Columns", "*"].ToString();
            var DataSource = Q["DataSource"].ToString();
            var OrderBy = Q["OrderBy", "ID Desc"].ToString();
            var Where = Q["Where"]?.ToString().ToObject<FilterCollection>();

            var dt = new DataTable();

            var np = new Pair();
            var nWhere = new List<string>();

            var nFltr = BuildFilter(Where);

            o = $"Select {Columns}, ROW_NUMBER() OVER (ORDER BY (SELECT 1)) XXX_ROWID From {DataSource} { nFltr } Order By {OrderBy}";
            o = $"Set NoCount On; {o} OPTION (ROBUST PLAN, FAST 30, FORCE ORDER, KEEPFIXED PLAN, MAXRECURSION 0)";

            if (nWhere.Count > 0)
            {
                using (var q = new Query(Sql))
                {
                    dt = q.TableQuery(o, np.Select(x => x.Key).ToArray(), np.Select(x => x.Value).ToArray());
                }
            }
            else
            {
                dt = Sql.TableQuery(o);
            }

            r.ResultSet = dt.Rows.JsonModel();

            dt?.Dispose(); //clean 
            GC.Collect();

            return r;
        });

        public async Task<Result> GenerateText() => await TaskResult(r =>
        {

            //var jk = $"{Path.GetFileNameWithoutExtension(Q["DefaultFileName"].ToString()) } { DateTime.Now.ToString("yyyy, MM dd") } { Guid.NewGuid().ToString().Substring(0, 5) }";
            var jk = $"{Path.GetFileNameWithoutExtension(Q["DefaultFileName"].ToString()) }";
            try
            {
                jk = Sql.ExecScalar($"Select { jk }").ToString();
            }
            catch { }
            finally
            {
                jk = $"{jk}.txt";
            }

            string tmpfile = Path.GetTempFileName();

            TextWriter writer = File.CreateText(tmpfile);
            var table = Sql.ExecQuery(Q["CommandText"].ToString()).Tables[0];
            String text = " ";
            foreach (DataRow rw in table.Rows)
            {
                foreach (DataColumn cl in table.Columns)
                {
                    writer.Write(rw[cl].ToString());
                }
                writer.WriteLine(text);
            }
            writer.Dispose();

            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container("Files");
                using (var ms = File.OpenRead(tmpfile))
                    strg.Upload(cntr, jk, ms);
            }

            File.Delete(tmpfile);

            r.ResultSet = jk.CompressUriEncoded();

            return r;
        });


        #region Excel

        [HttpPost]
        public virtual async Task<Result> DownloadExcelTemplate() => await TaskResult(r =>
        {
            string tmpfile = $"{Path.GetFileNameWithoutExtension(Q["ImportFile"].ToString()) } { DateTime.Now.ToString("yyyy, MM dd") } { Guid.NewGuid().ToString().Substring(0, 5) }{ Path.GetExtension(Q["ImportFile"].ToString()) }";

            using (var strg = new Storage.Storage()) {
                var cntr = strg.Container("ExcelTemplates");
                var IsDynamic = false;
                var defaultSheetName = string.Empty;

                using (var ms = new MemoryStream())
                {
                    strg.DownloadToStream(cntr, Q["ImportFile"].ToString(), ms);

                    //check excel if no columns specified
                    using (var xlr = new Excel(ms, Excel.IsFileInNewFormat(Q["ImportFile"].ToString())))
                    {
                        defaultSheetName = xlr.SheetsNames[0];
                        try
                        {
                            using (var dtMain = xlr.Read(defaultSheetName))
                                IsDynamic = dtMain.Columns.Count == 0;
                        }
                        catch
                        {
                            IsDynamic = true;
                        }
                    }
                }

                using (var ms = new MemoryStream())
                {
                    strg.DownloadToStream(cntr, Q["ImportFile"].ToString(), ms);

                    using (var xls = new ExcelWriter(ms, Excel.IsFileInNewFormat(Q["ImportFile"].ToString())))
                    {
                        string fontName = "Calibri";

                        string[] k = Q["Sort"].ToString().Split('|');
                        string[] src = Q["DataSource"].ToString().Split('|');

                        if (IsDynamic)
                        {
                            var menujson = strg.DownloadString(strg.Container("MenuSet"), $"{Q["ID_Menu"]}.InSysModule").CompressFromBase64();
                            var menu = menujson.ToObject<Menu>();

                            var row = xls.AddRow(defaultSheetName);
                            var icolIndex = 0;
                            foreach (var TabField in menu.tMenuDetailTabField.Where(x => x.ID == Q["IDTab"].ToInt32() && x.ListColumn != null))
                            {
                                xls.AddCell(row, icolIndex, TabField.Name);
                                icolIndex++;
                            }
                        }

                        if (Q["Sort", ""].ToString() != "")
                            for (int i = 0; i < k.Length; i++)
                            {
                                xls.AddSheet(k[i].Trim());
                                xls.AddCellStyle("Header", fontName, 11, BoldWeight: FontBoldWeight.Bold, BackgroundColor: IndexedColors.Grey25Percent);
                                using (var dt1 = this.Sql.ExecQuery($"Select * from {src[i]}").Tables[0])
                                {
                                    var row = xls.AddRow(k[i].Trim());
                                    foreach (DataColumn dc in dt1.Columns)
                                        xls.AddCell(row, dc.Ordinal, dc.ColumnName, "Header");

                                    foreach (DataRow dr in dt1.Rows)
                                    {
                                        row = xls.AddRow(k[i].Trim());
                                        foreach (DataColumn dc in dt1.Columns)
                                            xls.AddCell(row, dc.Ordinal, dr[dc.ColumnName].ToString());
                                    }
                                }
                            }

                        var flcntr = strg.Container("Files");

                        using (var msd = new MemoryStream())
                        {
                            xls.SaveToStream(msd);
                            strg.Upload(flcntr, tmpfile, msd);
                        }


                        r.ResultSet = tmpfile.CompressUriEncoded();

                        return r;
                    }
                }
            }
        });

        public async Task<Result> UploadExcelTemplate() => await TaskResult(r =>
        {
            var streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var fg = Request.Content.ReadAsMultipartAsync(streamProvider).Result;
            var file = fg.FileData[0];

            var strg = new Storage.Storage();
            var cntr = strg.Container("MenuSet");

            var menujson = strg.DownloadString(cntr, $"{fg.FormData["ID_Menu"]}.InSysModule").CompressFromBase64();

            var menu = menujson.ToObject<Menu>();

            var orgfile = file.Headers.ContentDisposition.FileName.Trim(new char[] { '"' });
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(orgfile);

            var fc = strg.Container("Files");
            using (var ms = File.OpenRead(file.LocalFileName))
            {
                strg.Upload(fc, filename, ms);
            }

            using (var ms = new MemoryStream())
            {
                strg.DownloadToStream(fc, filename, ms);

                using (var xls = new Excel(ms, Excel.IsFileInNewFormat(filename)))
                {
                    using (var ds = new DataSet())
                    {
                        var zt = new ZTable(this.Sql, fg.FormData["TableName"]);
                        var dtMain = xls.Read(xls.SheetsNames[0]);

                        if (!fg.FormData["Sort"].Equals("null"))
                        {
                            string[] k = fg.FormData["Sort"].Split('|');
                            string[] src = fg.FormData["DataSource"].Split('|');

                            foreach (DataColumn sc in zt.Columns) sc.ExtendedProperties.Clear();

                            for (int i = 0; i < k.Length; i++)
                            {
                                using (var dt = this.Sql.ExecQuery($"Select * from {src[i]}").Tables[0])
                                {
                                    dt.TableName = k[i].Trim();
                                    ds.Tables.Add(dt.Copy());
                                }
                            }
                        }
                        foreach (DataRow dr in dtMain.Rows)
                        {
                            var ir = zt.NewRow();
                            ir["ID"] = zt.Columns["ID"].AutoIncrementSeed;
                            --zt.Columns["ID"].AutoIncrementSeed;

                            foreach (DataColumn dc in dtMain.Columns)
                            {
                                var TabField = menu.tMenuDetailTabField.Where(x => x.Name == dc.ColumnName && x.ListColumn != null);
                                if (!TabField.Any()) continue;

                                var tf = TabField.SingleOrDefault();
                                var lc = tf.ListColumn.Split(':').Select(x => x.Trim()).ToArray();

                                if (lc.Count() > 1) //if multiple references
                                {
                                    //Plot User Input
                                    ir[tf.Name] = dr[lc[0]].ToString().TrimStart('\'');

                                    var gs = tf.ParentLookUpListColumn.Split('|').Select(x => x.Trim()).ToArray();
                                    var src = gs[0];
                                    var cs = gs[1].Split(':').Select(x => x.Trim()).ToArray();

                                    var ndt = ds.Tables[src].Rows.Cast<DataRow>().Where(x => x[cs[0]].ToString() == dr[lc[0]].ToString());
                                    if (ndt.Any())
                                    {
                                        var irow = ndt.SingleOrDefault();
                                        for (var i = 1; i < cs.Length; i++)
                                        {
                                            ir[lc[i]] = irow[cs[i]];
                                        }
                                    }
                                }
                                else
                                {
                                    if (dtMain.Columns[tf.ListColumn].DataType.Name.ToLower() == "string")
                                        ir[tf.Name] = dr[tf.ListColumn].ToString().TrimStart('\'');
                                    else
                                        ir[tf.Name] = dr[tf.ListColumn];
                                }
                            }

                            ir[menu.tMenuDetailTab.Where(x => x.TableName == zt.TableName).Single().ChildColumn] = fg.FormData["ID"];

                            zt.Rows.Add(ir);
                        }

                        this.Sql.ExecNonQuery("pLogFileAudit @FileName, @ID_Menu, @ID_User", filename, fg.FormData["ID_Menu"], fg.FormData["ID_User"]);

                        r.ResultSet = zt.Rows.JsonModel();
                    }
                }
            }
            File.Delete(file.LocalFileName);

            return r;
        });

        #endregion

        #region TreeView

        public async Task<Result> ListSource() => await TaskResult(r =>
        {

            string value = "", top = "";
            if (Q["value"] != null)
                value = $"Where { Q["childcolumn"] } = { Q["value"] }";
            if (Q["top"] != null)
                top = "TOP " + Q["top"];
            if (Q["DetailTabFilter"] != null && !Q["DetailTabFilter"].Equals(""))
                value = $"Where {Q["DetailTabFilter"]}";

            r.ResultSet = Query($"Select {top} * from {Q["TableName"] } a {value}").Result.JsonModel();

            return r;
        });

        public async Task<Result> LoadTreeListSource() => await TaskResult(r =>
        {
            var n = "v" + Q["TableName"].ToString().Substring(1);
            string s = "";
            if (Q["pListSource"].ToString() == "vSystemApplication")
            {
                s = Q["pListSource"].ToString() == "vCompany" ? $"SELECT * FROM { Q["pListSource"] } a where dbo.fCompanyRights({ Q["ID_Session"]},ID) = 1 AND not exists (SELECT * FROM { n } b where { Q["ChildColumn"] } = { Q["pID", DBNull.Value].SQLFormat() }" : $"SELECT * FROM { Q["pListSource"] } a where not exists (SELECT * FROM { n } b where { Q["ChildColumn"] } = { Q["pID", DBNull.Value].SQLFormat() }";
            }
            else
            {
                s = Q["pListSource"].ToString() == "vCompany" ? $"SELECT * FROM { Q["pListSource"] } a where dbo.fCompanyRights({ Q["ID_Session"]},ID) = 1 AND not exists (SELECT * FROM { n } b where { Q["ChildColumn"] } = { Q["pID", DBNull.Value].SQLFormat() }" : $"SELECT * FROM { Q["pListSource"] } a where dbo.fCompanyRights({ Q["ID_Session"]},ID_Company) = 1 AND not exists (SELECT * FROM { n } b where { Q["ChildColumn"] } = { Q["pID", DBNull.Value].SQLFormat() }";
            }

            var ListKeyRow = Q["ListKeyRow"].ToString().ToObject<TreeListCollection>();
            if (ListKeyRow.Count > 0)
            {
                s += " AND " + ListKeyRow.Select(x =>
                {
                    if (x.ListColumn == "") x.ListColumn = x.Name;
                    return $"b.{ x.Name } = a.{ x.ListColumn }";
                }).Join(" And ");
            }

            s += ")";

            r.ResultSet = Sql.ExecQuery(s).Tables[0].Rows.JsonModel();
            return r;
        });

        #endregion

        #region Extended Infos

        public virtual async Task<Result> NewTableSet() => await TaskResult(r =>
        {
            var dh = this.Sql.ExecScalar("SELECT COUNT(1) FROM sys.views WHERE name = @ViewName", $"v{ Q["TableName"].ToString().Substring(1) }_List").ToInt32();
            var g = dh == 0 ? $"v{ Q["TableName"].ToString().Substring(1) }" : $"v{ Q["TableName"].ToString().Substring(1) }_List";

            var MenuDetailTab = new tMenuDetailTab
            {
                ID = new Random().Next(111111, 99999999),
                Name = Q["TableName"].ToString(),
                TableName = Q["TableName"].ToString(),
                AllowNewRow = true,
                AllowDeleteRow = true,
                ID_MenuDetailTabType = MenuDetailTabType.Grid,
                ParentColumn = "ID",
                ChildColumn = $"ID_{ Q["ParentTableName"].ToString().Substring(1) }",
                IsActive = true

            };

            var MenuDetailTabFieldCollection = new List<tMenuDetailTabField>();

            foreach (DataColumn dc in this.Sql.ExecQuery($"Select top 0 * from { g }").Tables[0].Columns)
            {
                var systype = SystemControlType.TextBox;
                if (dc.DataType == typeof(bool)) systype = SystemControlType.CheckBox;
                else if ((dc.DataType == typeof(int) || dc.DataType == typeof(long)) && dc.ColumnName.StartsWith("ID_"))
                    systype = SystemControlType.ComboBox;
                else if (dc.DataType == typeof(DateTime))
                {
                    if (dc.ColumnName.ToLower().Contains("datetime"))
                        systype = SystemControlType.DataDateTime;
                    else
                        systype = dc.ColumnName.ToLower().Contains("time") ? SystemControlType.DataTime : SystemControlType.DataDate;
                }

                MenuDetailTabFieldCollection.Add(new tMenuDetailTabField
                {
                    ID_MenuDetailTab = MenuDetailTab.ID,
                    Name = dc.ColumnName,
                    ID_SystemControlType = systype,
                    EffectiveLabel = dc.ColumnName,
                    ShowInInfo = true,
                    IsActive = true
                });
            }

            r.ResultSet = new
            {
                MenuDetailTab = MenuDetailTab,
                MenuDetailTabField = MenuDetailTabFieldCollection,
                Schema = new InfoSchema
                {
                    TableName = Q["TableName"].ToString(),
                    Schema = TableSchema(Q["TableName"].ToString(), "v" + Q["TableName"].ToString().Substring(1)).JsonModel(),
                    Type = ObjectType(Q["TableName"].ToString())
                }
            };

            return r;
        });

        #endregion

        #region Background Job

        public virtual async Task<Result> Job_CheckQueue() => await TaskResult(r =>
        {
            r.ResultSet = Sql.ExecScalar("SELECT COUNT(1) FROM dbo.tMenuButtonBackgroundQueue with(nolock) WHERE ID_Menu = @ID_Menu AND ID_Record = @ID_Record AND ID_User = @ID_User AND ID_MenuButtonBackgroundQueueStatus = 1 AND IsActive = 1", Q["ID_Menu"], Q["ID_Record"], Q["ID_User"]);
            return r;
        });

        public virtual async Task<Result> Job_Enqueue() => await TaskResult(r =>
        {

            var ndf = Sql.ExecScalar("pMenuButtonAddBackgroundQueue @ID_Menu, @ID_Record, @ID_User", Q["ID_Menu"], Q["ID_Record"], Q["ID_User"]).ToString();

            var controller = RequestContext.RouteData.Values["controller"].ToString();
            var action = RequestContext.RouteData.Values["action"].ToString();
            var token = Ctx.Session["Token"]?.ToString();

            BackgroundJob.Enqueue<JobQueue>(jb => jb.Enqueue(Q["ID_Record"].ToInt32(), Q["MenuName"].ToString(), ndf, Q["CommandText"].ToString(), token, controller, action, Q["ID_User"].ToInt32(), Q["ID_Menu"].ToInt32(), null));

            return r;
        });

        #endregion

        #region Attachment
        public async Task<Result> UploadFileTab() => await TaskResult(r =>
        {
            var streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var fg = Request.Content.ReadAsMultipartAsync(streamProvider).Result;
            var file = fg.FileData[0];

            var orgfile = file.Headers.ContentDisposition.FileName.Trim(new char[] { '"' });
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(orgfile);

            var Container = "Files";

            var strg = new Storage.Storage();
            var cntr = strg.Container(Container);

            using (var fileStream = System.IO.File.OpenRead(file.LocalFileName))
            {
                strg.Upload(cntr, filename, fileStream);
            }

            File.Delete(file.LocalFileName);

            r.ResultSet = new { OrigFileName = orgfile, GUID = filename };

            return r;
        });
        #endregion
    }
}