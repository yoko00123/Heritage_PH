using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using static InSys.Models.UtilityModel;
using InSys.Helpers;
using System.Data.SqlClient;
using z.SQL;
using System.Data;
using Newtonsoft.Json;
using z.Data;
using System.IO;
using z.Data.JsonClient;
using z.SQL.Data;
using InSys.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Caching;
using System.Threading.Tasks;
using z.Controller;
using System.Text.RegularExpressions;

namespace InSys.Controllers.API
{
    [InvariantCulture]
    public class BaseController : CoreController
    {
        public const string XsrfKey = "XsrfId";
        public Content ContentPath { get; private set; }
        public DataTable getTable(string cmdtxt, params object[] @params)
        {
            DataTable dt = null;
            List<string> p = new List<string>();
            try
            {
                p = getParams(cmdtxt);
                using (SqlConnection ttCon = Sql.Connection())
                {
                    ttCon.Open();
                    using (SqlCommand ttCmd = new SqlCommand(cmdtxt, ttCon))
                    {
                        if (@params.Length > 0)
                        {
                            for (int x = 0; x <= @params.Length - 1; x += 1)
                            {
                                ttCmd.Parameters.AddWithValue(p[x], getObjectValue(@params[x]));
                            }
                        }
                        dt = JsonConvert.DeserializeObject(serialize(ttCmd.ExecuteReader())).ToString().ToObject<DataTable>();
                    }
                    ttCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public void Build()
        {
            var requestId = Q["RequestID"].ToString();
            ObjectCache cache = MemoryCache.Default;

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
            cache.Set(requestId, "Starting", policy);

            Task.Run(() =>
            {
                requestId = Q["RequestID"].ToString();
                cache = MemoryCache.Default;
                policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);

                var strg = new Storage.Storage();
                var cntr = strg.Container("MenuSet");


                int? ID_Menu = Q["ID_Menu"].ToInt32();

                cache.Set(requestId, "Fetching Menu from Database", policy);

                var ds = default(DataSet);
                ds = ID_Menu == 0 ? Sql.ExecQuery("pWebMenuDataSet") : Sql.ExecQuery("pWebMenuDataSet2 @ID_Menu", ID_Menu);

                ds.Tables[0].TableName = "tMenu";
                ds.Tables[1].TableName = "tMenuTab";
                ds.Tables[2].TableName = "tMenuDetailTab";
                ds.Tables[3].TableName = "tMenuTabField";
                ds.Tables[4].TableName = "tMenuDetailTabField";
                ds.Tables[5].TableName = "tMenuButton";
                ds.Tables[6].TableName = "tUserGroupMenu";
                ds.Tables[7].TableName = "tMenuLoadParameters";

                ds.DataSetName = "MenuSet";

                cache.Set(requestId, "Revalidating Tables", policy);

                RevalidateTable(ds);

                int i = 0;
                int max = ds.Tables["tMenu"].Rows.Count;

                foreach (DataRow dr in ds.Tables["tMenu"].Rows)
                {
                    i++;
                    cache.Set(requestId, $"Processing { i } of { max } {  Convert.ToDouble((Convert.ToDouble(i) / Convert.ToDouble(max)) * 100.0).ToInt32()  }%", policy);

                    var p = new Pair();

                    var menutab =
                        ds.Tables["tMenuTab"].Rows.Cast<DataRow>().Where(x => x["ID_Menu"].ToInt32() == dr["ID"].ToInt32());
                    var menudetailtab =
                        ds.Tables["tMenuDetailTab"].Rows.Cast<DataRow>()
                            .Where(x => x["ID_Menu"].ToInt32() == dr["ID"].ToInt32());

                    p.Add("tMenu", dr.JsonModel());
                    p.Add("tMenuTab", menutab.Select(x => new jDataRow(x)));
                    p.Add("tMenuDetailTab", menudetailtab.Select(x => new jDataRow(x)));
                    p.Add("tMenuTabField",
                        ds.Tables["tMenuTabField"].Rows.Cast<DataRow>()
                            .Join(menutab, x => x["ID_MenuTab"].ToInt32(), x => x["ID"].ToInt32(), (x, y) => new jDataRow(x)));
                    p.Add("tMenuDetailTabField",
                        ds.Tables["tMenuDetailTabField"].Rows.Cast<DataRow>()
                            .Join(menudetailtab, x => x["ID_MenuDetailTab"].ToInt32(), x => x["ID"].ToInt32(),
                                (x, y) => new jDataRow(x)));
                    p.Add("tMenuButton",
                        ds.Tables["tMenuButton"].Rows.Cast<DataRow>()
                            .Where(x => x["ID_Menu"].ToInt32() == dr["ID"].ToInt32())
                            .Select(x => new jDataRow(x)));
                    p.Add("tUserGroupMenu",
                        ds.Tables["tUserGroupMenu"].Rows.Cast<DataRow>()
                            .Where(x => x["ID_Menu"].ToInt32() == dr["ID"].ToInt32())
                            .Select(x => new jDataRow(x)));
                    p.Add("tMenuLoadParameters", ds.Tables["tMenuLoadParameters"].Rows.Cast<DataRow>().Where(x => x["ID_Menu"].ToInt32() == dr["ID"].ToInt32())
                        .Select(x => new jDataRow(x)));


                    strg.Upload(cntr, dr["ID"].ToInt32() + ".InSysModule", p.ToJson().CompressToBase64());
                }

                if (ID_Menu == 0)
                    strg.Upload(cntr, "tMenu.InSysModule", ds.Tables[0].Rows.JsonModel().ToJson().CompressToBase64());

                cache.Set(requestId, "Done", policy);

                ds.Dispose();

                cache.Remove(requestId); //remove when done
            });
        }

        public Result BuildStatus(Result r)
        {
            ObjectCache cache = MemoryCache.Default;
            var Status = cache.Get(Q["RequestID"].ToString());

            if (Status != null)
                r.ResultSet = new { Message = Status.ToString(), Status = 0 };
            else
                r.ResultSet = new { Message = "Completed", Status = 1 };

            return r;
        }


        public static void RevalidateTable(DataSet ds)
        {
            //tMenu
            //var cols = new string[] { "WritableAttachmentIf", "EnableSaveIf" };
            var cols = new Dictionary<string, string>();
            cols.Add("EnableSaveIf", "EnableSaveIf");
            cols.Add("WritableAttachmentIf", "WritableAttachmentIf");
            cols.Add("MultiSelectIf", "MultiSelectIf");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (var c in cols.Where(x => ds.Tables[0].Columns.Contains(x.Key)))
                    if (dr[c.Value].IsNull("").ToString() != "")
                        dr[c.Value] = SQLToJS.Convert(dr[c.Key].ToString());
            }

            //details
            var dc = new Dictionary<string, string>();
            dc.Add("VisibleIf", "VisibleIf");
            dc.Add("WritableIf", "WebWritableIf");
            dc.Add("RequiredIf", "WebRequiredIf");
            dc.Add("ReadOnlyIf", "WebReadOnlyIf");
            dc.Add("EnabledIf", "WebEnabledIf");
            dc.Add("DisableButtonsIf", "DisableButtonsIf");
            dc.Add("DisableDetailDeleteIf", "DisableDetailDeleteIf");

            for (var i = 1; i < ds.Tables.Count; i++)
            {
                //Add Columns
                foreach (var c in dc.Where(x => ds.Tables[i].Columns.Contains(x.Key) && !ds.Tables[i].Columns.Contains(x.Value)))
                    ds.Tables[i].Columns.Add(c.Value, typeof(string));

                foreach (DataRow dr in ds.Tables[i].Rows)
                {
                    foreach (var c in dc.Where(x => ds.Tables[i].Columns.Contains(x.Key)))
                        dr[c.Value] = SQLToJS.Convert(dr[c.Key].ToString());
                }
            }
        }
        public void BuildWidget()
        {
            var strg = new Storage.Storage();
            var cntr = strg.Container("Widget");

            var ds = default(DataSet);
            ds = Sql.ExecQuery("pWebWidgetDataSet");

            ds.Tables[0].TableName = "tWebWidgets";
            ds.Tables[1].TableName = "tWebWidgets_Detail";
            ds.Tables[2].TableName = "tWebWidgets_Detail_Column";
            ds.Tables[3].TableName = "tWebWidgetsUserGroup";

            ds.Tables[1].Columns.Add("Columns", typeof(object));

            ds.DataSetName = "WebWidgetSet";

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                dr["Columns"] = ds.Tables[2].Rows.JsonModel().Where(x => x["ID_WebWidgets_Detail"].ToInt32() == dr["ID"].ToInt32()).ToArray();
            }

            foreach (DataRow dr in ds.Tables["tWebWidgets"].Rows)
            {
                var p = new Pair();

                var WebWidgetsDetail =
                    ds.Tables["tWebWidgets_Detail"].Rows.Cast<DataRow>().Where(x => x["ID_WebWidgets"].ToInt32() == dr["ID"].ToInt32());
                var WebWidgetsDetailColumn =
                    ds.Tables["tWebWidgets_Detail_Column"].Rows.Cast<DataRow>()
                        .Where(x => x["ID_WebWidgets_Detail"].ToInt32() == dr["ID"].ToInt32());

                p.Add("tWebWidgets", dr.JsonModel());
                p.Add("tWebWidgets_Detail", WebWidgetsDetail.Select(x => new jDataRow(x)));
                //p.Add("tWebWidgets_Detail_Column", WebWidgetsDetailColumn.Select(x => new jDataRow(x)));
                p.Add("tWebWidgetsUserGroup",
                    ds.Tables["tWebWidgetsUserGroup"].Rows.Cast<DataRow>()
                        .Where(x => x["ID_WebWidgets"].ToInt32() == dr["ID"].ToInt32())
                        .Select(x => new jDataRow(x)));

                strg.Upload(cntr, dr["ID"].ToInt32() + ".InSysModule", p.ToJson().CompressToBase64());
            }

            strg.Upload(cntr, "tWebWidgets.InSysModule", ds.Tables[0].Rows.JsonModel().ToJson().CompressToBase64());

            ds.Dispose();
        }

        protected DataRowCollection TableSchema(string TableName, string ViewName, string filter = "")
        => Sql.TableQuery("SELECT * FROM dbo.fViewSchema2(@TableName, @ViewName) " + (filter != "" ? $"WHERE {filter}" : "") + " ORDER BY SeqNo ", TableName, ViewName).Rows;

        protected SchemaTable.ObjectTypeEnum ObjectType(string TableName)
            => new SchemaTable(TableName).GetObjectType(Sql.Connection());

        protected DataTable ValidDataSource(string DataSource, string Where)
        {
            var scr = "";
            try
            {

                if (DataSource.ToLower().Contains("order"))
                {
                    string source = DataSource.Substring(0, DataSource.IndexOf("ORDER"));
                    int ob = DataSource.Substring(0, DataSource.IndexOf("ORDER")).Length;
                    string orderBY = DataSource.Substring(ob);
                    DataSource = DataSource.Replace(DataSource.Substring(ob), "");
                    scr = $"SELECT * FROM {DataSource} {Where}  {orderBY}";
                }
                else
                {
                    scr = $"SELECT * FROM {DataSource} {Where}";
                }



                if (Parsers.IsValidSql(scr))
                    return Sql.TableQuery(scr);
                else
                    return Sql.TableQuery($"SELECT * FROM {DataSource} {Where} ");
            }
            catch
            {
                return Sql.ExecQuery($"SELECT * FROM {DataSource} {Where}").Tables[0];
            }
        }

        protected string PassFilter(string source, FilterCollection fltr, Pair Session)
        {
            var p = new Pair();
            var g = source.ParseParameter();
            if (g.Where(y => y.ToLower() == "@startdate" || y.ToLower() == "@enddate").Any())
            {
                var j = fltr?.Where(x => x.Name.ToLower() == "date");

                if (j?.Any() == true)
                {
                    var jj = j.SingleOrDefault().Value;
                    p.Add("StartDate", jj[0]);
                    p.Add("EndDate", jj[1]);
                }
                else
                {
                    p.Add("StartDate", DateTime.Now.ToString("yyyy/MM/dd"));
                    p.Add("EndDate", DateTime.Now.ToString("yyyy/MM/dd"));
                }
            }

            source = source.PassParameter(p);
            source = source.PassParameter(Session); //
            if (fltr != null)
                source = source.PassParameter(fltr.Select(x => new KeyValuePair<string, object>(x.Name, x.Value[0])).ToPair());
            else
            {
                var jk = source.ParseParameter().Select(x => new KeyValuePair<string, object>(x.Replace("@", ""), DBNull.Value)).ToPair();
                source = source.PassParameter(jk);
            }

            return source;
        }

        protected string BuildFilter(FilterCollection fltr)
        {
            if (fltr == null) return string.Empty;

            var ftr = new List<string>();

            foreach (var h in fltr)
            {
                ValidateInjector(h.Value);
                switch (h.Type)
                {
                    case FilterType.Like:
                        ftr.Add($"{ h.Name } Like '{ h.Value[0].StripSlashes() }%'");
                        break;
                    case FilterType.Equal:
                        ftr.Add($"{ h.Name } = { h.Value[0].SQLFormat() }");
                        break;
                    case FilterType.Between:
                        ftr.Add($"({ h.Name } Between { h.Value[0].SQLFormat() } And { h.Value[1].SQLFormat() })");
                        break;
                    case FilterType.In:
                        ftr.Add($"{ h.Name } IN ( { h.Value[0].ToString().Replace("[", "").Replace("]", "") } )");
                        break;
                    case FilterType.Greater:
                        ftr.Add($"{ h.Name } >= { h.Value[0].SQLFormat() }");
                        break;
                    case FilterType.Lesser:
                        ftr.Add($"{ h.Name } <= { h.Value[0].SQLFormat() }");
                        break;
                }
            }

            return (ftr.Count > 0) ? $" Where { ftr.Join(" AND ") }" : string.Empty;
        }

        protected void ValidateInjector(object[] values)
        {
            foreach (var k in values)
                ValidateInjector(k.ToString());
        }

        protected void ValidateInjector(string Value)
        {
            string pattern = @"\{{2}|\}{2}|'|(<(\\?)\w+(\s*?)(\w*?)>)|--";
            if (Regex.IsMatch(Value, pattern))
                throw new Exception("Invalid input");
        }

        protected Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        protected System.Drawing.Imaging.ImageFormat GetFormat(string Image)
        {
            switch (Path.GetExtension(Image).ToLower())
            {
                case ".jpeg":
                case ".jpg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ".png":
                    return System.Drawing.Imaging.ImageFormat.Png;
                case ".gif":
                    return System.Drawing.Imaging.ImageFormat.Gif;
                default:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
            }
        }

        protected int LRatio(int val)
        {
            int ubound = 3, lbound = 1;
            return val * lbound / ubound;
        }

        protected string GeneratePassword()
        {
            RandomKeyGenerator KeyGen = new RandomKeyGenerator();
            KeyGen.KeyLetters = "abcdefghijklmnopqrstuvwxyz";
            KeyGen.KeyNumbers = "0123456789";
            KeyGen.KeyChars = Convert.ToInt32(this.Sql.ExecScalar("SELECT ISNULL(dbo.fGetSetting('PasswordLength'), 0)"));
            return KeyGen.Generate();
        }

        protected override void Initializer()
        {
            ContentPath = new Content((AppDomain.CurrentDomain.BaseDirectory + @"Contents").ToString());
        }

        public void generateControlQueryBuilder(StringBuilder strQueryBuilder, List<string> tmp, DataRow dr)
        {
            if (dr != null)
            {
                string DataSource = "";
                if (dr["DataSource"].IsNull("").ToString() == "")
                {
                    DataSource = "v" + dr["Name"].ToString().Substring(3);
                }
                else
                {
                    DataSource = replaceValues(dr["DataSource"].ToString(), Ctx, dr);
                }
                string FixedFilter = "";
                if (dr["FixedFilter"].IsNull("").ToString() != "")
                {
                    FixedFilter = replaceValues(dr["FixedFilter"].ToString(), Ctx, dr);
                }

                DataSource = (FixedFilter != "" ? DataSource + " WHERE " + FixedFilter : DataSource);

                if (dr["ID_SystemControlType"].ToString() == "2")
                {
                    strQueryBuilder.Append("SELECT " + dr["DisplayID"].IsNull("ID").ToString() + " AS ID," + dr["DisplayMember"].IsNull("Name").ToString() + " AS Name" + " FROM " + DataSource + ";");
                }

                tmp.Add(dr["ID_MenuTabField"].ToString());
            }
        }
        public DataTable GetTable(string str, string columns = "*", string filter = "")
        {
            return Sql.ExecQuery("Select " + columns.IsNull("*").ToString() + " from " + replaceValues(str, Ctx, null) + (filter != "" ? " WHERE " + replaceValues(filter, Ctx) : "")).Tables[0] as DataTable;
        }

        public void Writer(string path, string body)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter scriptWriter = File.CreateText(path))
            {
                try
                {
                    scriptWriter.Write(body);
                    scriptWriter.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scriptWriter.Close();
                }
            }
        }

        protected object GetSetting(string Name, object alternativevalue = null)
        {
            return Sql.ExecScalar("SELECT Value FROM dbo.tSetting WHERE Name = @Name", Name).IsNull(alternativevalue);
        }

        protected Collection<MultipartFileData> GetUploadedFiles()
        {
            var streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var fg = Request.Content.ReadAsMultipartAsync(streamProvider).Result;
            return fg.FileData;
        }

        public string EscapeSQLString(string s)
        {
            return s.Replace("'", "''");
        }

        public void PasswordHistory(int ID_User)
        {
            string sql = string.Format("SELECT ID FROM dbo.tPasswordHistory WHERE ID_User = {0} AND Password = '{1}'", ID_User, Q["Password"]);
            var dt = new DataTable();
            dt = Sql.ExecQuery(sql).Tables[0] as DataTable;

            if (dt.Rows.Count == 0)
            {
                Sql.ExecNonQuery(string.Format("INSERT INTO dbo.tPasswordHistory(Password, ID_User) VALUES('{0}', {1})", Q["Password"], ID_User));
            }
        }

        public List<Tuple<int, bool>> GetUserMenuID(int ID_User)
        {
            using (var dt = this.Sql.ExecQuery("pWebGetUserMenuID @ID_User", ID_User).Tables[0])
                return dt.Rows.Cast<DataRow>().Select(x => { return new Tuple<int, bool>(x[0].ToInt32(), x[1].ToBool()); }).ToList();
        }

        public string LoadTheme(int id_company)
        {
            var strge = new Storage.Storage();
            string skin = "";
            skin = Sql.ExecScalar("select ISNULL(dbo.fGetTheme(@ID), '')", id_company).ToString();
            if (skin != "")
            {
                skin = skin + ".css";
            }
            return skin;
        }

        //public void PasswordHistory(int ID_User)
        //{
        //    string sql = string.Format("SELECT ID FROM dbo.tPasswordHistory WHERE ID_User = {0} AND Password = '{1}'", ID_User, Q["Password"]);
        //    var dt = new DataTable();
        //    dt = Sql.ExecQuery(sql).Tables[0] as DataTable;

        //    if (dt.Rows.Count == 0)
        //    {
        //        Sql.ExecNonQuery(string.Format("INSERT INTO dbo.tPasswordHistory(Password, ID_User) VALUES('{0}', {1})", Q["Password"], ID_User));
        //    }
        //}

        #region Captcha

        [HttpPost]
        public virtual async Task<Result> Captcha() => await TaskResult(r =>
        {
            using (var dd = new Util.Captcha(200, 80))
            {
                var orn = new Random();
                int iNum = orn.Next(100000, 999999);

                r.ResultSet = new
                {
                    Value = iNum,
                    Image = $"data:image/png;base64,{Convert.ToBase64String(dd.GenerateToBytes(iNum.ToString()))}"
                };

                return r;
            }
        });

        #endregion

        public virtual async Task<Result> Session() => await TaskResultSet(() => Ctx.Session["Session"]);
    }
}
