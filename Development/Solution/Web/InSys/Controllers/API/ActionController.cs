﻿using InSys.Controllers.API;
using InSys.Helpers;
using InSys.Models;
using InSys.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Yahoo.Yui.Compressor;

using z.Data;
using z.IO;
using z.SQL;
using static InSys.Models.UtilityModel;
using Microsoft.Web.Administration;
using System.Threading;
using System.Data.SqlClient;

namespace InSys.Controllers
{
    [AuthorizeRequest]
    public class ActionController : BaseController
    {

        #region Common

        [HttpPost, HttpGet]
        public async Task<Result> Publish() => await TaskVoid(Build);

        [HttpPost]
        public async Task<Result> pBuildWebsite() => await TaskResult(r =>
        {
            int buildNumber = System.Configuration.ConfigurationManager.AppSettings["BuildNumber"].ToInt32();
            buildNumber += 1;
            if (!Directory.Exists(Ctx.Server.MapPath("~/Build")))
                Directory.CreateDirectory(Ctx.Server.MapPath("~/Build"));
            if (!Directory.Exists(Ctx.Server.MapPath($"~/Build/{buildNumber}")))
                Directory.CreateDirectory(Ctx.Server.MapPath($"~/Build/{buildNumber}"));

            foreach (string dirPath in Directory.GetDirectories(Ctx.Server.MapPath("~/Scripts"), "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(Ctx.Server.MapPath("~/Scripts"), Ctx.Server.MapPath($"~/Build/{buildNumber}/Scripts")));

            foreach (string newPath in Directory.GetFiles(Ctx.Server.MapPath("~/Scripts"), "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(Ctx.Server.MapPath("~/Scripts"), Ctx.Server.MapPath($"~/Build/{buildNumber}/Scripts")), true);

            foreach (string dirPath in Directory.GetDirectories(Ctx.Server.MapPath("~/Styles"), "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(Ctx.Server.MapPath("~/Styles"), Ctx.Server.MapPath($"~/Build/{buildNumber}/Styles")));

            foreach (string newPath in Directory.GetFiles(Ctx.Server.MapPath("~/Styles"), "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(Ctx.Server.MapPath("~/Styles"), Ctx.Server.MapPath($"~/Build/{buildNumber}/Styles")), true);

            foreach (string dirPath in Directory.GetDirectories(Ctx.Server.MapPath("~/Web"), "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(Ctx.Server.MapPath("~/Web"), Ctx.Server.MapPath($"~/Build/{buildNumber}/Web")));

            foreach (string newPath in Directory.GetFiles(Ctx.Server.MapPath("~/Web"), "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(Ctx.Server.MapPath("~/Web"), Ctx.Server.MapPath($"~/Build/{buildNumber}/Web")), true);

            var cfg = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            cfg.AppSettings.Settings["BuildNumber"].Value = buildNumber.ToString();
            cfg.Save();            
            return r;
        });

        [HttpPost, HttpGet]
        public async Task<Result> PublishStatus() => await TaskResult(BuildStatus);

        [HttpPost]
        public async Task<Result> PublishWidget() => await TaskVoid(BuildWidget);
        [HttpPost]
        public async Task<Result> FetchWidgetCount() => await TaskResult(r =>
        {
            try
            {
                var dsSet = Q["ds"].ToString().ToObject<PairCollection>();
                List<string> dsList = new List<string>();
                foreach (Pair ds in dsSet)
                {
                    string str = "SELECT COUNT(1) CNT FROM " + replaceValues(ds["DS"].ToString(), Ctx);
                    dsList.Add(str);
                }
                string allSource = dsList.Join(";");
                DataSet dataCount = Sql.ExecQuery(allSource);

                Pair p = new Pair();
                for (int x = 0; x < dsSet.Count; x++)
                {
                    p.Add(dsSet[x]["ID"].ToString(), dataCount.Tables[x].Rows[0]["CNT"].ToInt32());
                }

                r.ResultSet = new { data = p };
            }
            catch (Exception ex)
            {
                r.ResultSet = new { error = ex.Message.ToString() };
            }


            return r;
        });

        public async Task<Result> DailyAutomation() => await TaskExec("pDailyAutomation");

        [HttpPost]
        public Task<Result> GetMenu() => TaskResult(r =>
        {
            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container("MenuSet");
                var mnu = strg.DownloadString(cntr, $"{ Q["ID_Menu"] }.InSysModule").CompressFromBase64().ToObject<Menu>();
                r.ResultSet = mnu;

            }
            return r;
        });

        public async Task<Result> FetchChild() => await TaskResult(r =>
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

        [HttpPost]
        public async Task<Result> GetWidget() => await TaskResult(r =>
        {
            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container("Widget");
                r.ResultSet = strg.DownloadString(cntr, $"{ Q["ID_Menu"] }.InSysModule").CompressFromBase64().ToObject<dynamic>();
            }
            return r;
        });

        [HttpPost]
        public Task<Result> FetchTableData() => TaskResult(r =>
        {
            try
            {
                int page = Q["page"].ToInt32();
                int countPerPage = Q["count"].ToInt32();
                string ds = Q["ds"].IsNull("").ToString();
                string filter = Q["filter"].IsNull("").ToString();
                string cols = Q["cols"].IsNull("*").ToString();

                string datasource = "SELECT " + cols + " FROM " + ds + (filter.Length > 0 ? " WHERE " + filter : "") +
                                    " ORDER BY ID DESC OFFSET " + ((page - 1) * countPerPage).ToString() + " ROWS FETCH NEXT " + countPerPage.ToString() + " ROWS ONLY";
                string datasource2 = "SELECT Count(1) FROM " + ds + (filter.Length > 0 ? " WHERE " + filter : "");
                int cnt = Sql.ExecScalar(replaceValues(datasource2, Ctx)).ToInt32();
                var totalPage = (cnt / countPerPage);
                var remainder = (cnt % countPerPage);
                if (remainder >= 1)
                {
                    totalPage += 1;
                }

                var dt = Sql.TableQuery(replaceValues(datasource, Ctx));
                r.ResultSet = new { data = dt, pages = totalPage };
            }
            catch (Exception ex)
            {
                r.ResultSet = new { error = ex.Message.ToString() };
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> LoadUserMenu() => await TaskResult(r =>
        {
            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container("MenuSet");

                r.ResultSet = new
                {
                    Menu = strg.DownloadString(cntr, $"tMenu.InSysModule").CompressFromBase64().ToObject<dynamic>(),
                    UserFav = Sql.TableQuery("SELECT ID, SeqNo, ID_Menu, Menu, ID_ApplicationType FROM dbo.vUserFavMenu AS UFM WHERE UFM.ID_User = @ID_User", Q["ID_User"]).Rows.JsonModel()
                };
            }

            return r;
        });

        /// <summary>
        /// Note Memory stream cannot dispose, 
        /// di ko pa naccheck kung mag aaffect sa memory pagtagal na
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage DownloadFile()
        {
            var FileName = Q["f"].ToString().CompressFromUriEncoded();
            var origFileName = Q["origFileName"]?.ToString();

            var Container = Q["c"]?.ToString().CompressFromUriEncoded();
            if (Container == null)
                Container = "Files";

            var strg = new Storage.Storage();

            //for multi sub dir
            var dfg = Container.Split('/');

            var cntr = strg.Container(dfg[0]);

            var dr = default(IStorageDirectory);
            for (var k = 1; k < dfg.Length; k++)
            {
                if (dr == null)
                    dr = cntr.GetDirectoryReference(dfg[k]);
                else
                    dr = dr.GetDirectoryReference(dfg[k]);
            }

            var ms = new MemoryStream();
            {
                if (dr == null)
                    strg.DownloadToStream(cntr, FileName, ms);
                else
                    strg.DownloadToStream(dr, FileName, ms);

                ms.Seek(0, SeekOrigin.Begin);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(ms)
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue(FileName.GetContentType(".zip"));
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"{FileName}{Path.GetExtension(FileName)}"
                };
                result.Headers.Add("x-filename", FileName);

                return result;
            }

        }

        [HttpGet, HttpPost]
        public Task<Result> LoadImage() => TaskResult(r =>
        {

            var strg = new Storage.Storage();

            var cntr = strg.Container(Q["Container", "Images"].ToString());
            var tmpfile = Q["ImgFile", "some.png"].ToString();

            LocalBlob blob;

            if (Q["Path"] != null)
            {
                var dr = cntr.GetDirectoryReference(strg.ToURLSlug(Q["Path"].ToString()));
                blob = strg.LoadBlob(dr, tmpfile);
            }
            else
            {
                blob = strg.LoadBlob(cntr, tmpfile);
            }

            if (blob != null)
                r.ResultSet = blob.GetSharedAccess();


            return r;
        });

        public async Task<Result> UploadImage() => await TaskResult(r =>
        {
            var strg = new Storage.Storage();
            var cntr = strg.Container("Photos");

            var streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var fg = Request.Content.ReadAsMultipartAsync(streamProvider).Result;
            var file = fg.FileData[0];

            var orgfile = file.Headers.ContentDisposition.FileName.Trim(new char[] { '"' });
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(orgfile);

            using (var fp = File.OpenRead(file.LocalFileName))
                strg.Upload(cntr, filename, fp);

            File.Delete(file.LocalFileName);

            r.ResultSet = new { orgfile = filename, FileName = orgfile };

            return r;
        });

        //Added by Yoku 02282019
        public async Task<Result> UploadAttachContainer() => await TaskResult(r =>
        {
            var strg = new Storage.Storage();
            var cntr = strg.Container("Photos");

            var streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var fg = Request.Content.ReadAsMultipartAsync(streamProvider).Result;
            var file = fg.FileData[0];

            var orgfile = file.Headers.ContentDisposition.FileName.Trim(new char[] { '"' });
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(orgfile);

            using (var fp = File.OpenRead(file.LocalFileName))
                strg.Upload(cntr, filename, fp);

            File.Delete(file.LocalFileName);

            r.ResultSet = new { GUID = filename, FileName = orgfile };

            return r;
        });


        [HttpPost]
        public async Task<Result> FetchWidgetChartData() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            try
            {
                JObject dr = (JObject)JsonConvert.DeserializeObject(Q["data"].ToString());

                string s = dr["DataSource"].ToString() + (dr["Filter"].ToString() != "" ? " WHERE " + dr["Filter"].ToString() : "");
                s = replaceValues(s, Ctx);
                DataTable dt = Sql.ExecQuery("SELECT * FROM " + s).Tables[0] as DataTable;

                return new { data = dt };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> SaveColumnSelection() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            try
            {
                var ID_Menu = Q["ID"];
                var cols = (JArray)JsonConvert.DeserializeObject(Q["Cols"].ToString());
                var columns = cols.ToObject<DataTable>();

                foreach (DataRow dr in columns.Select())
                {
                    this.Sql.ExecNonQuery("EXEC dbo.pSaveUserMenuTabField @ID_MenuTabField, @ID_User, @SeqNo, @GroupSeqNo", dr["ID_MenuTabField"], Ctx.Session["ID_User"], dr["SeqNo"], dr["GroupSeqNo"]);
                }

                if (columns.Rows.Count > 0)
                {
                    //this.SQL.ExecNonQuery("DELETE FROM tUserMenuTabField WHERE ID_User = @ID_User AND ID_MenuTabField NOT IN (" + String.Join(",", columns.AsEnumerable().Select(x => x["ID_MenuTabField"])) + ")", Ctx.Session["ID_User"]);
                    this.Sql.ExecNonQuery("DELETE wmuc " +
                                                     "FROM dbo.tUserMenuTabField wmuc " +
                                                     "LEFT JOIN dbo.tMenuTabField wmc ON wmc.ID = wmuc.ID_MenuTabField " +
                                                     "LEFT JOIN dbo.tMenuTab wmt ON wmt.ID = wmc.ID_MenuTab " +
                                                     "WHERE wmuc.ID_User = @ID_User And wmt.ID_Menu = @ID_Menu " +
                                                     "AND wmuc.ID_MenuTabField NOT IN (" + String.Join(",", columns.AsEnumerable().Select(x => x["ID_MenuTabField"])) + ")", Ctx.Session["ID_User"], ID_Menu);
                }
                else
                {
                    this.Sql.ExecNonQuery("DELETE wmuc " +
                                                     "FROM dbo.tUserMenuTabField wmuc " +
                                                     "LEFT JOIN dbo.tMenuTabField wmc ON wmc.ID = wmuc.ID_MenuTabField " +
                                                     "LEFT JOIN dbo.tMenuTab wmt ON wmt.ID = wmc.ID_MenuTab " +
                                                     "WHERE wmuc.ID_User = @ID_User And wmt.ID_Menu = @ID_Menu", Ctx.Session["ID_User"], ID_Menu);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });
        [HttpPost]
        public async Task<Result> RemoveSelectedInfo() => await TaskExec($"Delete from { Q["TableName"] } Where ID in ({ Q["IDS"].ToString().ToObject<int[]>().Join() })");
        [HttpPost]
        public async Task<Result> fetchFilterSource() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            var comboData = (JArray)JsonConvert.DeserializeObject(Q["comboData"].ToString());
            var comboDT = comboData.ToObject<DataTable>();

            try
            {
                StringBuilder strQueryBuilder = new StringBuilder();

                //combo box
                List<string> ddTemp = new List<string>();
                foreach (DataRow dr in comboDT.Rows)
                {
                    generateControlQueryBuilder(strQueryBuilder, ddTemp, dr);
                }

                Dictionary<string, object> dict = new Dictionary<string, object>();
                Dictionary<string, object> source = new Dictionary<string, object>();
                if (strQueryBuilder.ToString().Length > 0)
                {
                    var ds = this.Sql.ExecQuery(strQueryBuilder.ToString());

                    //Combo box
                    for (int x = 0; x <= ddTemp.Count - 1; x++)
                    {
                        dict.Add(ddTemp[x], ds.Tables[x].Rows.JsonModel());
                    }
                    if (dict.Count > 0)
                    {
                        source.Add("dropdown_source", JsonConvert.DeserializeObject(dict.ToJson()));
                        dict.Clear();
                    }
                    else
                    {
                        source.Add("dropdown_source", JsonConvert.DeserializeObject("{}"));
                    }

                    //other controls


                }

                return new { data = source };

            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public virtual async Task<Result> SetPassword() => await TaskResult(r =>
        {
            var uID = Q["ID_User"].ToInt32();
            //bool ph;
            var initlogbool = Q["IsFirstLog", false].ToBool();
            var passexpiredbool = false;
            var changepasswordbool = false;
            var gUser = Q["gUser", 1].ToInt32();
            var password = Q["password"].ToString();
            var question = Q["question", 0].ToInt32();
            var answer = Q["answer", ""].ToString();

            //var c = Sql.ExecScalar("SELECT dbo.fPasswordValidation(@password)", password).ToBool();
            var c = Sql.ExecScalar("Select dbo.fValidatePassword(@Password, @ID_User)", password, uID);

            var plc = Sql.ExecScalar("SELECT dbo.fGetSetting('PasswordLength')").ToInt32();
            var special = Sql.ExecScalar("SELECT dbo.fCheckSpecialCharacter(@password)", password).ToBool();

            if (c.IsNull("").ToString() != "")
            {
                r.Status = 5;
                r.ResultSet = c.ToString();
                return r;
            }


            var encPass = password.EncryptA(41).ToString() + "_BJTGLR";

            Sql.ExecNonQuery("EXEC pSavePassword @ID_User , @Password ", uID, encPass);
            Sql.ExecNonQuery("EXEC pPasswordHistory @ID_User ,@Password ", uID, encPass);
            if (initlogbool)
            {
                Sql.ExecNonQuery("EXEC pUpdateIsFirstLog @ID_User, @ID_SecurityQuestion, @Answer ", uID, question, answer);
                r.Status = 7;
            }
            if (passexpiredbool)
            {
                r.Status = 8;
            }
            if (changepasswordbool)
            {
                r.Status = 9;
            }
            r.Status = 9;
            r.ResultSet = "Password Saved";

            return r;
        });

        [HttpPost]
        public async Task<Result> FetchThemes() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            try
            {
                DataTable dt = Sql.ExecQuery("SELECT * FROM dbo.tThemes").Tables[0] as DataTable;

                return new { data = dt };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> FetchThemesPalette() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            try
            {
                int id = Q["id"].ToInt32();
                DataTable dt = Sql.ExecQuery("SELECT * FROM dbo.tThemes WHERE ID = @ID", id).Tables[0] as DataTable;

                return new { data = dt };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> FetchSkinCompany() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            try
            {
                DataTable dt = Sql.ExecQuery("EXEC dbo.pGetAvailableCompany @ID_Skin", Q["ID_Skin"].ToInt32()).Tables[0] as DataTable;

                return new { data = dt };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> FetchQuestions() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            try
            {
                DataTable dt = GetTable("tSecretQuestion", "*", "");

                return new { data = dt };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        /// <summary>
        /// Container Parameter Required
        /// </summary>
        /// <returns></returns>
        public async Task<Result> UploadFile() => await TaskResult(r =>
        {
            var streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var fg = Request.Content.ReadAsMultipartAsync(streamProvider).Result;
            var file = fg.FileData[0];

            var UseOrigFileName = fg.FormData["UseOriginalName"]?.ToBool();

            var orgfile = file.Headers.ContentDisposition.FileName.Trim(new char[] { '"' });

            string filename = orgfile;
            if (UseOrigFileName == false)
            {
                filename = Guid.NewGuid().ToString() + Path.GetExtension(orgfile);
            }

            var Container = fg.FormData["Container"];

            var strg = new Storage.Storage();
            var cntr = strg.Container(Container);

            using (var fileStream = System.IO.File.OpenRead(file.LocalFileName))
            {
                strg.Upload(cntr, filename, fileStream);
            }

            File.Delete(file.LocalFileName);

            r.ResultSet = filename;

            return r;
        });
		
		public async Task<Result> SetUserOnline() => await TaskResult(r =>
        {
            Sql.ExecNonQuery(Q["GUID"].ToString());
            return r;
        });

        public async Task<Result> UploadFileDetail() => await TaskResult(r =>
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

        public async Task<Result> DownloadFileDetail() => await TaskResult(r =>
        {
            string tmpfile = Q["FileName"].ToString();
            var strg = new Storage.Storage();
            var flCntr = strg.Container("Files");

            using (var ms = new MemoryStream())
            {
                strg.DownloadToStream(flCntr, tmpfile, ms);
                using (var msf = new MemoryStream())
                {
                    r.ResultSet = tmpfile.CompressUriEncoded();
                }
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> SaveTheme() => await TaskResultSet(() =>
        {
            JObject skin = (JObject)JsonConvert.DeserializeObject(Q["data"].ToString());
            List<string> cols = new List<string>();
            string str = "";
            int id = 0;
            try
            {
                if (skin["ID"].ToInt32() != 0)
                {
                    str = "UPDATE dbo.tThemes set ";
                    foreach (var c in skin)
                    {
                        if (c.Key.ToString() != "ID" && c.Key.ToString() != "IsActive")
                        {
                            cols.Add(c.Key.ToString() + "='" + c.Value.ToString() + "'");
                        }
                        else if (c.Key.ToString() == "IsActive")
                        {
                            cols.Add(c.Key.ToString() + "=" + (c.Value.ToBool() ? 1 : 0));
                        }
                    }
                    str = str + string.Join(",", cols) + " WHERE ID = @ID";
                    id = skin["ID"].ToInt32();
                    Sql.ExecNonQuery(str, id);
                }
                else
                {
                    str = "INSERT INTO dbo.tTHemes(";
                    foreach (var c in skin)
                    {
                        if (c.Key.ToString() != "ID")
                        {
                            cols.Add(c.Key.ToString());
                        }
                    }
                    str = str + string.Join(",", cols) + ") VALUES(";
                    cols.Clear();
                    foreach (var c in skin)
                    {
                        if (c.Key.ToString() != "ID" && c.Key.ToString() != "IsActive")
                        {
                            cols.Add((c.Value.ToString() == "" ? "null" : "'" + c.Value.ToString() + "'"));
                        }
                        else if (c.Key.ToString() == "IsActive")
                        {
                            cols.Add((c.Value.ToBool() ? 1 : 0).ToString());
                        }
                    }
                    str = str + string.Join(",", cols) + ");SELECT SCOPE_IDENTITY() AS ID;";
                    id = Sql.ExecScalar(str).ToInt32();
                }

                if (skin["IsActive"].ToBool())
                {
                    str = "UPDATE dbo.tThemes SET IsActive = 0 WHERE ID != @ID AND (ID_Company IS NULL OR ID_Company = '')";
                    Sql.ExecNonQuery(str, id);

                    var strge = new Storage.Storage();
                    var container = strge.Container("Themes");
                    var blob = strge.ListBlob(container).Where(x => x.Name == strge.ToURLSlug("_Themes.scss")).SingleOrDefault();
                    if (blob != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            strge.DownloadToStream(container, strge.ToURLSlug("_Themes.scss"), ms);

                            using (StreamReader sr = new StreamReader(ms))
                            {
                                string line = sr.ReadToEnd();
                                line = Regex.Replace(line, "^\\s*", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
                                line = Regex.Replace(line, "\\r\\n", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
                                line = Regex.Replace(line, "<!--*.*?-->", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
                                line = Regex.Replace(line, "/\\**.*?\\*/", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);

                                foreach (var c in skin)
                                {
                                    if (c.Key != "ID" && c.Key != "ID_Company" && c.Key != "IsActive" && c.Key != "Name")
                                    {
                                        string column = c.Key;
                                        line = Regex.Replace(line, @"\$\b" + column + @"\b", c.Value.ToString(), RegexOptions.Compiled | RegexOptions.Multiline);
                                    }

                                }

                                CssCompressor cssCompressor = new CssCompressor();
                                var tmpFileName = Path.GetTempFileName();
                                Writer(tmpFileName, cssCompressor.Compress(line));

                                using (var fileRead = File.OpenRead(tmpFileName))
                                {
                                    strge.Upload(container, skin["Name"].ToString() + ".css", fileRead);
                                }
                            }
                        }
                        return new { data = "success", ID = id };
                    }
                    else
                    {
                        return new { error = "Theme Template not Found!" };
                    }
                }
                else
                {
                    return new { data = "success", ID = id };
                }

            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> ApplyTheme() => await TaskResultSet(() =>
        {
            int id_skin = Q["ID_Skin"].ToInt32();
            List<string> cols = new List<string>();
            string str = "";
            try
            {
                DataTable dt = Sql.ExecQuery("Select * from dbo.tThemes where ID = @ID", id_skin).Tables[0] as DataTable;
                DataRow dr = dt.Rows[0];
                if (dr["ID_Company"].IsNull("").ToString() != "")
                {
                    str = "UPDATE dbo.tThemes SET IsActive = 1 WHERE ID = @ID;";
                    Sql.ExecNonQuery(str, id_skin);
                }
                else
                {
                    str = "UPDATE dbo.tThemes SET IsActive = 1 WHERE ID = @ID;";
                    str = str + "UPDATE dbo.tThemes SET IsActive = 0 WHERE ID != @ID AND (ID_Company IS NULL OR ID_Company = '');";
                    Sql.ExecNonQuery(str, id_skin);
                }

                var strge = new Storage.Storage();
                var container = strge.Container("Themes");
                var blob = strge.ListBlob(container).Where(x => x.Name == strge.ToURLSlug("_Themes.scss")).SingleOrDefault();
                if (blob != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        strge.DownloadToStream(container, strge.ToURLSlug("_Themes.scss"), ms);

                        using (StreamReader sr = new StreamReader(ms))
                        {
                            string line = sr.ReadToEnd();
                            line = Regex.Replace(line, "^\\s*", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
                            line = Regex.Replace(line, "\\r\\n", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
                            line = Regex.Replace(line, "<!--*.*?-->", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
                            line = Regex.Replace(line, "/\\**.*?\\*/", string.Empty, RegexOptions.Compiled | RegexOptions.Multiline);

                            foreach (DataColumn c in dt.Columns)
                            {
                                if (c.ColumnName != "ID" && c.ColumnName != "ID_Company" && c.ColumnName != "IsActive" && c.ColumnName != "Name")
                                {
                                    string column = c.ColumnName;
                                    line = Regex.Replace(line, @"\$\b" + column + @"\b", dr[c.ColumnName].ToString(), RegexOptions.Compiled | RegexOptions.Multiline);
                                }

                            }

                            CssCompressor cssCompressor = new CssCompressor();
                            var tmpFileName = Path.GetTempFileName();
                            Writer(tmpFileName, cssCompressor.Compress(line));

                            using (var fileRead = File.OpenRead(tmpFileName))
                            {
                                strge.Upload(container, dr["Name"].ToString() + ".css", fileRead);
                            }
                        }
                    }
                    return new { data = "success" };
                }
                else
                {
                    return new { error = "Theme Template not Found!" };
                }

            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> CreateWidget() => await TaskResultSet(() =>
        {
            var menuData = Q["menu"]?.ToString().ToObject<Pair>();
            string selectedColumn = Q["column"]?.ToString();
            string ret = string.Empty;
            string name = Q["name"].ToString();
            string str = "";
            int idx = 0;
            int id_menu = Q["ID_Menu"].ToInt32();
            int id_menuType = Q["ID_MenuType"].ToInt32();
            int ID_User = Ctx.Session["ID_User"].ToInt32();
            int widgetType = Q["Type"].ToInt32();
            int w = 2;
            int h = 4;
            int cnt = 0;
            int x = Q["x"].ToInt32();
            int y = Q["y"].ToInt32();
            int ID_ApplicationType = Q["ID_ApplicationType", 0].ToInt32();
            DataTable tbl = null;
            switch (widgetType)
            {
                case 1:
                    w = 2;
                    h = 4;
                    break;
                case 3:
                    w = 4;
                    h = 8;
                    break;
                case 4:
                    w = 8;
                    h = 8;
                    break;
                case 5:
                    w = 4;
                    h = 8;
                    break;
                case 6:
                    w = 8;
                    h = 8;
                    break;
                case 7:
                    w = 8;
                    h = 8;
                    break;
            }
            try
            {
                if (widgetType == 1)
                {
                    DataRow m = Sql.ExecQuery("select * from dbo.tMenu where ID = @ID", id_menu).Tables[0].Rows[0] as DataRow;
                    if (m["DataSource"].IsNull("").ToString() != "")
                    {
                        str = "INSERT INTO dbo.tUserWidgets (xPos, yPos, Name, ID_User, ID_WidgetType, ID_Menu, wWidth, hHeight, ID_ApplicationType) VALUES(@x,@y,@name,@ID_User,@type,@menu,@w,@h,@ID_ApplicationType);SELECT SCOPE_IDENTITY();";
                        idx = Sql.ExecScalar(str, x, y, name, ID_User, widgetType, id_menu, w, h, ID_ApplicationType).ToInt32();
                        cnt = Sql.ExecScalar("select count(1) cnt from " + replaceValues(m["DataSource"].ToString(), Ctx, null)).ToInt32();
                    }
                    else
                    {
                        if (id_menuType == 6)
                        {
                            str = "INSERT INTO dbo.tUserWidgets (xPos, yPos, Name, ID_User, ID_WidgetType, ID_Menu, wWidth, hHeight, ID_ApplicationType) VALUES(@x,@y,@name,@ID_User,@type,@menu,@w,@h,@ID_ApplicationType);SELECT SCOPE_IDENTITY();";
                            idx = Sql.ExecScalar(str, x, y, name, ID_User, widgetType, id_menu, w, h, ID_ApplicationType).ToInt32();
                            cnt = -1;
                        }
                        else
                        {
                            return new { error = "No datasource found in this menu." };
                        }
                    }
                }
                else if (widgetType == 3)
                {
                    string ds = Sql.ExecScalar("Select DataSource from dbo.tMenu where ID = @ID", id_menu).ToString();
                    if (ds.IsNull("").ToString() != "")
                    {
                        string ds2 = "EXEC dbo.pGetChart '" + (selectedColumn.Contains("ID_") ? selectedColumn.Substring(3) : selectedColumn) + "', '" + addStripSlashes(ds) + "'";
                        str = "INSERT INTO dbo.tUserWidgets (xPos, yPos, Name, ID_User, ID_WidgetType, ID_Menu, wWidth, hHeight, DataSource, SelectedColumn, ID_ApplicationType) VALUES(@x,@y,@name,@ID_User,@type,@menu,@w,@h,@ds,@col,@ID_ApplicationType);SELECT SCOPE_IDENTITY();";
                        idx = Sql.ExecScalar(str, x, y, name, ID_User, widgetType, id_menu, w, h, ds2, (selectedColumn.Contains("ID_") ? selectedColumn.Substring(3) : selectedColumn), ID_ApplicationType).ToInt32();
                        tbl = Sql.ExecQuery(replaceValues(ds2, Ctx, null)).Tables[0] as DataTable;
                    }
                    else
                    {
                        return new { error = "No datasource found in this menu." };
                    }
                }
                else if (widgetType == 4)
                {
                    string ds = Sql.ExecScalar("Select DataSource from dbo.tMenu where ID = @ID", id_menu).ToString();
                    if (ds.IsNull("").ToString() != "")
                    {
                        string ds2 = "EXEC dbo.pGetChart '" + (selectedColumn.Contains("ID_") ? selectedColumn.Substring(3) : selectedColumn) + "', '" + addStripSlashes(ds) + "'";
                        str = "INSERT INTO dbo.tUserWidgets (xPos, yPos, Name, ID_User, ID_WidgetType, ID_Menu, wWidth, hHeight, DataSource, SelectedColumn, ID_ApplicationType) VALUES(@x,@y,@name,@ID_User,@type,@menu,@w,@h,@ds,@col,@ID_ApplicationType);SELECT SCOPE_IDENTITY();";
                        idx = Sql.ExecScalar(str, x, y, name, ID_User, widgetType, id_menu, w, h, ds2, (selectedColumn.Contains("ID_") ? selectedColumn.Substring(3) : selectedColumn), ID_ApplicationType).ToInt32();
                        tbl = Sql.ExecQuery(replaceValues(ds2, Ctx, null)).Tables[0] as DataTable;
                    }
                    else
                    {
                        return new { error = "No datasource found in this menu." };
                    }
                }

                return new { index = idx, cnt = cnt, tbl = tbl };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> CreateNewWidget() => await TaskResultSet(() =>
        {
            var menuData = Q["menu"]?.ToString().ToObject<Pair>();
            string selectedColumn = Q["column"]?.ToString();
            string selectedColumn2 = Q["column2"]?.ToString();
            string ret = string.Empty;
            string name = Q["name"].ToString();
            string str = "";
            int idx = 0;
            int id_menu = Q["ID_Menu"].ToInt32();
            int id_menuType = Q["ID_MenuType"].ToInt32();
            int ID_User = Ctx.Session["ID_User"].ToInt32();
            int widgetType = Q["Type"].ToInt32();
            int w = 2;
            int h = 4;
            int cnt = 0;
            int x = Q["x"].ToInt32();
            int y = Q["y"].ToInt32();
            int ID_ApplicationType = Q["ID_ApplicationType", 0].ToInt32();
            selectedColumn2 = selectedColumn2.IsNull("").ToString();
            List<ChartCtx> ch = null;
            switch (widgetType)
            {
                case 1:
                    w = 2;
                    h = 4;
                    break;
                case 3:
                    w = 4;
                    h = 8;
                    break;
                case 4:
                    w = 8;
                    h = 8;
                    break;
                case 5:
                    w = 4;
                    h = 8;
                    break;
                case 6:
                    w = 8;
                    h = 8;
                    break;
                case 7:
                    w = 8;
                    h = 8;
                    break;
            }
            try
            {
                if (widgetType == 1)
                {
                    DataRow m = Sql.ExecQuery("select * from dbo.tMenu where ID = @ID", id_menu).Tables[0].Rows[0] as DataRow;
                    if (m["DataSource"].IsNull("").ToString() != "")
                    {
                        str = "INSERT INTO dbo.tUserWidgets (xPos, yPos, Name, ID_User, ID_WidgetType, ID_Menu, wWidth, hHeight, ID_ApplicationType) VALUES(@x,@y,@name,@ID_User,@type,@menu,@w,@h,@ID_ApplicationType);SELECT SCOPE_IDENTITY();";
                        idx = Sql.ExecScalar(str, x, y, name, ID_User, widgetType, id_menu, w, h, ID_ApplicationType).ToInt32();
                        cnt = Sql.ExecScalar("select count(1) cnt from " + replaceValues(m["DataSource"].ToString(), Ctx, null)).ToInt32();
                    }
                    else
                    {
                        if (id_menuType == 6)
                        {
                            str = "INSERT INTO dbo.tUserWidgets (xPos, yPos, Name, ID_User, ID_WidgetType, ID_Menu, wWidth, hHeight, ID_ApplicationType) VALUES(@x,@y,@name,@ID_User,@type,@menu,@w,@h,@ID_ApplicationType);SELECT SCOPE_IDENTITY();";
                            idx = Sql.ExecScalar(str, x, y, name, ID_User, widgetType, id_menu, w, h, ID_ApplicationType).ToInt32();
                            cnt = -1;
                        }
                        else
                        {
                            return new { error = "No datasource found in this menu." };
                        }
                    }
                }
                else
                {
                    string ds = Sql.ExecScalar("Select DataSource from dbo.tMenu where ID = @ID", id_menu).ToString();
                    DataTable dt = Sql.ExecQuery("EXEC dbo.pCreateNewChart @c1, @c2, @ds, @wt", selectedColumn, selectedColumn2, replaceValues(ds, Ctx, null), widgetType).Tables[0];
                    if (ds.IsNull("").ToString() != "")
                    {
                        string ds2 = "EXEC dbo.pCreateNewChart '" + selectedColumn + "', '" + selectedColumn2 + "', '" + ds + "', " + widgetType + "";
                        str = "INSERT INTO dbo.tUserWidgets (xPos, yPos, Name, ID_User, ID_WidgetType, ID_Menu, wWidth, hHeight, DataSource, SelectedColumn, SelectedColumn2, ID_ApplicationType) VALUES(@x,@y,@name,@ID_User,@type,@menu,@w,@h,@ds,@col,@col2,@ID_ApplicationType);SELECT SCOPE_IDENTITY();";
                        idx = Sql.ExecScalar(str, x, y, name, ID_User, widgetType, id_menu, w, h, ds2, selectedColumn, selectedColumn2, ID_ApplicationType).ToInt32();
                        ChartCtx chrt = new ChartCtx();
                        ch = new List<ChartCtx>();
                        ch = chrt.GenerateChartObject(dt, widgetType);
                    }
                    else
                    {
                        return new { error = "No datasource found in this menu." };
                    }
                }

                return new { index = idx, cnt = cnt, tbl = ch, type = widgetType };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> FetchNewWidgetChartData() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            PairCollection pd = new PairCollection();
            PairCollection bd = new PairCollection();
            DataTable chartData = null;
            List<ChartCtx> ch = null;
            List<List<ChartCtx>> chList = new List<List<ChartCtx>>();
            int ID_ApplicationType = Q["ID_ApplicationType", 0].ToInt32();
            try
            {
                DataTable dt = null;
                dt = GetTable("vUserWidgets", "cast(0 as bit) IsRemove, xPos as x, yPos as y, wWidth as w, hHeight as h, CAST(ID AS VARCHAR(MAX)) AS i, Name as name, ID_WidgetType as type, 0 as cnt, ID_Menu, DataSource as ds, SelectedColumn as [column], SelectedColumn2 as [column2], MinW, MinH, cast(1 as bit) showType", "ID_User = @ID_User and ID_ApplicationType = " + ID_ApplicationType + "");

                foreach (DataRow dr in dt.Rows)
                {
                    int ID_MenuType = Sql.ExecScalar("Select ID_MenuType from dbo.tMenu where ID = @ID", dr["ID_Menu"]).ToInt32();
                    if (dr["type"].ToInt32() == 1)
                    {
                        DataRow m = Sql.ExecQuery("select * from dbo.tMenu where ID = @ID", dr["ID_Menu"].ToInt32()).Tables[0].Rows[0] as DataRow;
                        if (ID_MenuType == 6)
                        {
                            dr["cnt"] = -1;
                        }
                        else
                        {
                            dr["cnt"] = Sql.ExecScalar("select count(1) cnt from " + replaceValues(m["DataSource"].ToString(), Ctx, null));
                        }

                    }
                    else
                    {
                        chartData = Sql.ExecQuery(replaceValues(dr["ds"].ToString(), Ctx, null)).Tables[0] as DataTable;
                        ChartCtx chrt = new ChartCtx();
                        ch = new List<ChartCtx>();
                        ch = chrt.GenerateChartObject(chartData, dr["type"].ToInt32());
                        chList.Add(ch);
                    }
                }

                return new { data = dt, chartList = chList };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> FetchNewWidgets() => await TaskResultSet(() =>
        {
            string ret = string.Empty;
            PairCollection pd = new PairCollection();
            PairCollection bd = new PairCollection();
            DataTable pieData = null;
            DataTable barData = null;
            int ID_ApplicationType = Q["ID_ApplicationType", 0].ToInt32();
            try
            {
                DataTable dt = null;
                dt = GetTable("vUserWidgets", "cast(0 as bit) IsRemove, xPos as x, yPos as y, wWidth as w, hHeight as h, CAST(ID AS VARCHAR(MAX)) AS i, Name as name, ID_WidgetType as type, 0 as cnt, ID_Menu, DataSource as ds, SelectedColumn as [column], MinW, MinH", "ID_User = @ID_User and ID_ApplicationType = " + ID_ApplicationType + "");

                foreach (DataRow dr in dt.Rows)
                {
                    int ID_MenuType = Sql.ExecScalar("Select ID_MenuType from dbo.tMenu where ID = @ID", dr["ID_Menu"]).ToInt32();
                    if (dr["type"].ToInt32() == 1)
                    {
                        DataRow m = Sql.ExecQuery("select * from dbo.tMenu where ID = @ID", dr["ID_Menu"].ToInt32()).Tables[0].Rows[0] as DataRow;
                        if (ID_MenuType == 6)
                        {
                            dr["cnt"] = -1;
                        }
                        else
                        {
                            dr["cnt"] = Sql.ExecScalar("select count(1) cnt from " + replaceValues(m["DataSource"].ToString(), Ctx, null));
                        }

                    }
                    else if (dr["type"].ToInt32() == 3)
                    {
                        pieData = Sql.ExecQuery(replaceValues(dr["ds"].ToString(), Ctx, null)).Tables[0] as DataTable;
                        Pair p = new Pair();
                        p.Add(dr["i"].ToString(), pieData);
                        pd.Add(p);
                    }
                    else if (dr["type"].ToInt32() == 4)
                    {
                        barData = Sql.ExecQuery(replaceValues(dr["ds"].ToString(), Ctx, null)).Tables[0] as DataTable;
                        Pair p = new Pair();
                        p.Add(dr["i"].ToString(), barData);
                        bd.Add(p);
                    }
                }

                return new { data = dt, pieData = pd, barData = bd };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> SaveLayout() => await TaskResultSet(() =>
        {
            var data = Q["layout"].ToString().ToObject<PairCollection>();
            string ids = Q["idsToRemove"]?.ToString().Replace("[", "").Replace("]", "");
            try
            {
                if (ids != "")
                {
                    string del = "DELETE FROM dbo.tUserWidgets WHERE ID IN (" + ids + ")";
                    Sql.ExecNonQuery(del);
                }
                foreach (var wd in data)
                {
                    List<string> cols = new List<string>();
                    string str = "UPDATE dbo.tUserWidgets set ";
                    cols.Add("xPos = " + wd["x"].ToInt32());
                    cols.Add("yPos = " + wd["y"].ToInt32());
                    cols.Add("wWidth = " + wd["w"].ToInt32());
                    cols.Add("hHeight = " + wd["h"].ToInt32());

                    str = str + string.Join(",", cols) + " WHERE ID = @ID";
                    Sql.ExecNonQuery(str, wd["i"].ToInt32());
                }

                return new { d = "success" };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> PreviewChartData() => await TaskResultSet(() =>
        {
            int widgetType = Q["widgetType"].ToInt32();
            string column1 = Q["column1"].ToString();
            string column2 = Q["column2"].IsNull("").ToString();
            string ds = Q["ds"].IsNull("").ToString();

            try
            {
                if (ds != "" && ds != null)
                {
                    ds = replaceValues(ds, Ctx, null);
                    DataTable dt = Sql.ExecQuery("EXEC dbo.pCreateNewChart @c1, @c2, @ds, @wt", column1, column2, ds, widgetType).Tables[0];
                    ChartCtx chrt = new ChartCtx();
                    List<ChartCtx> ch = new List<ChartCtx>();
                    ch = chrt.GenerateChartObject(dt, widgetType);
                    return new { data = ch };
                }
                else
                {
                    return new { data = "No Datasource found!" };
                }
            }
            catch (Exception ex)
            {
                return new { error = ex.Message.ToString() };
            }
        });

        [HttpPost]
        public async Task<Result> FetchSchemaTable() => await TaskResult(r =>
        {

            var k = new InfoSchema
            {
                TableName = Q["tName"].ToString(),
                Schema = TableSchema(Q["tName"].ToString(), "v" + Q["tName"].ToString().Substring(1)).JsonModel(),
                Type = ObjectType(Q["tName"].ToString())
            };

            r.ResultSet = new
            {
                SchemaTable = k
            };
            return r;

        });

        [HttpPost]
        public async Task<Result> IONSDashboard() => await TaskResult(r =>
        {
            var ds = default(DataSet);
            ds = Sql.ExecQuery("EXEC dbo.pIONSDashboard @ID_Employee", Q["ID_User"]);
            r.ResultSet = new
            {
                Employee = ds.Tables[0],
                LeaveCredits = ds.Tables[1],
                FilingData = ds.Tables[2]
            };
            return r;
        });

        //Added by Yoku 02282019
        [HttpPost]
        public async Task<Result> ValidateUserPayslip() => await TaskResult(r =>
        {

            int ID_User = Ctx.Session["ID_User"].ToInt32();
            var ds2 = default(DataSet);
            ds2 = Sql.ExecQuery("SELECT dbo.fEncrypt(REPLACE(Password,'_BJTGLR',''),41) FROM dbo.vUser WHERE ID = @ID_User ", Ctx.Session["ID_User"].ToInt32());
            r.ResultSet = new
            {
                Password = ds2.Tables[0]
            };
            return r;
        });

        #endregion

        #region Favorites

        [HttpPost]
        public Task<Result> AddUserFav() => TaskExec("Insert Into tUserFavMenu (ID_Menu, SeqNo, ID_User) Values (@ID_Menu, @SeqNo, @ID_User)", Q["ID_Menu"], Q["SeqNo"], Q["ID_User"]);

        [HttpPost]
        public Task<Result> RemoveUserFav() => TaskExec("Delete From tUserFavMenu Where ID_User = @ID_User And ID_Menu = @ID_Menu", Q["ID_User"], Q["ID_Menu"]);

        #endregion

        #region Reports

        //public virtual async Task<Result> LoadReportParameters() => await TaskResult(_ =>
        //{
        //    using (var strg = new Storage.Storage())
        //    {
        //        var rptContainer = strg.Container("Reports");

        //        var fg = Path.GetTempFileName();

        //        strg.DownloadToFile(rptContainer, Q["ReportName"].ToString(), fg);

        //        using (var rpt = new Crystal())
        //        {
        //            rpt.ReportFile = fg;
        //            rpt.Init();

        //            var exc = new string[] { "STARTDATE", "ENDDATE" };

        //            _.ResultSet = rpt.GetPromptKeys().Where(x => exc.Contains(x.Name.ToUpper()) == false).ToArray();
        //        }

        //        File.Delete(fg);
        //        GC.Collect();

        //        return _;
        //    }
        //});

        //public virtual async Task<Result> LoadReport() => await TaskResult(r =>
        //{
        //    using (var strg = new Storage.Storage())
        //    {
        //        var rptContainer = strg.Container("Reports");

        //        var fg = Path.GetTempFileName();

        //        strg.DownloadToFile(rptContainer, Q["ReportName"].ToString(), fg);

        //        using (var rpt = new Crystal())
        //        {
        //            var where = Q["Where"]?.ToString().ToObject<FilterCollection>(); //Q["Where"]?.ToString();
        //            var dsource = Q["DataSource"].ToString();
        //            var ParameterTable = new Pair();

        //            rpt.Credentials = new DBParameters
        //            {
        //                Server = Sql.Server,
        //                User = Sql.UserName,
        //                Password = Sql.Password,
        //                Database = Sql.Database
        //            };

        //            rpt.ReportFile = fg;
        //            rpt.Init();

        //            var SubDataSource =
        //                Sql.ExecQuery("SELECT Name, DataSource FROM dbo.tMenuSubDataSource WHERE ID_Menu = @ID_Menu", Q["ID_Menu", DBNull.Value]).Tables[0].Rows;
        //            var SessionTable = GetSession();

        //            SubDataSource.Cast<DataRow>().Join(rpt.GetSubReportTableNames(), x => x["Name"].ToString(), x => x, (x, y) => x).Each(x =>
        //            {
        //                var s = x["DataSource"].ToString();
        //                s = s.PassParameter(SessionTable).PassParameter(ParameterTable);
        //                s = PassFilter(s, where);
        //                rpt.SubReportSource.Add(new SubReportSourceCtx()
        //                {
        //                    Name = x["Name"].ToString(),
        //                    DataSource = Sql.ExecQuery($"Select * from {s}").Tables[0]
        //                });
        //            });

        //            rpt.DataSource = ValidDataSource(PassFilter(dsource.ToString(), where), BuildFilter(where));

        //            var gf = rpt.GetParameterKeys();
        //            var rp = Q["ReportParameter"]?.ToString().ToObject<ReportParameterCollection>();

        //            gf.Each(x =>
        //            {
        //                var rf = rp?.Where(y => y.Name == x);
        //                if (rf != null && rf.Any())
        //                    rpt.AddParameters(x, rf?.First().Value.IsNull(""));
        //                else
        //                    switch (x.ToLower())
        //                    {
        //                        case "startdate":
        //                            var hhg = where?.Where(y => y.Name.ToLower() == "Date".ToLower()).SingleOrDefault();
        //                            var sval = DateTime.Now;
        //                            if (hhg != null)
        //                                sval = hhg.Value[0] != null ? hhg.Value[0].ToDate() : sval;

        //                            rpt.AddParameters(x, sval);
        //                            break;
        //                        case "enddate":
        //                            var hhg2 = where?.Where(y => y.Name.ToLower() == "Date".ToLower()).SingleOrDefault();
        //                            var sval2 = DateTime.Now;
        //                            if (hhg2 != null)
        //                                sval2 = hhg2.Value[1] != null ? hhg2.Value[1].ToDate() : sval2;

        //                            rpt.AddParameters(x, sval2);
        //                            break;
        //                        case "prepared by":
        //                        case "approved by":
        //                        case "checked by":
        //                            rpt.AddParameters(x, "");
        //                            break;
        //                    }
        //            });

        //            rpt.Load(t => { t.SummaryInfo.ReportAuthor = Q["Author"].ToString(); });

        //            r.ResultSet = new
        //            {
        //                FileName = rpt.ReportName,
        //                FileString = rpt.ReportData
        //            };
        //        }

        //        File.Delete(fg);
        //        GC.Collect();

        //        return r;
        //    }
        //});

        public virtual async Task<Result> LoadReport() => await TaskResult(r =>
        {
            var requestId = Q["RequestID"].ToString();
            ObjectCache cache = MemoryCache.Default;
            var rp = Q["ReportParameter"]?.ToString().ToObject<Models.ReportParameterCollection>(); //REM 2017 11 09
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
            cache.Set(requestId, "Rendering Report", policy);
            cache.Set($"{ requestId }_Status", 0, policy);

            using (var strg = new Storage.Storage())
            {
                var rptContainer = strg.Container("Reports");

                var fg = Path.GetTempFileName().Replace(".tmp", ".json");

                strg.DownloadToFile(rptContainer, Q["ReportName"].ToString().ToUrlSlug(), fg);
                using (var rpt = new JSReport())
                {
                    rpt.SessionTable = GetSession();
                    rpt.RootPath = Ctx.Server.MapPath("~/bin");
                    rpt.where = Q["Where"]?.ToString().ToObject<FilterCollection>();
                    rpt.dsource = Q["DataSource"].ToString();
                    rpt.ID_Menu = Q["ID_Menu", DBNull.Value];
                    rpt.ReportParameter = rp;
                    Task.Run(() =>
                    {
                        try
                        {
                            rpt.Credentials = Sql;
                            rpt.ReportFile = fg;
                            rpt.Init(ValidDataSource, (s, f) => PassFilter(s, f, rpt.SessionTable), BuildFilter);

                            rpt.Load();

                            cache.Set($"{ requestId }_InfoID", rpt.PdfOutFile, policy);
                            cache.Set($"{ requestId }_Status", 1, policy);


                        }
                        catch (Exception ex)
                        {
                            ex = (ex.InnerException != null) ? ex.InnerException : ex;
                            string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();

                            cache.Set($"{ requestId }_Status", 2, policy);
                            cache.Set($"{ requestId }_ErrorMsg", msg != "" ? msg : ex.Message, policy);
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
        });

        [HttpPost, HttpGet]
        public async Task<Result> LoadExcelReport() => await TaskResult(_ =>
        {
            var requestId = Q["RequestID"].ToString();
            ObjectCache cache = MemoryCache.Default;
            var rp = Q["ReportParameter"]?.ToString().ToObject<Models.ReportParameterCollection>(); //REM 2017 11 09
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
            cache.Set(requestId, "Rendering Report", policy);
            cache.Set($"{ requestId }_Status", 0, policy);

            using (var strg = new Storage.Storage())
            {
                var rptContainer = strg.Container("Reports");

                var fg = Path.GetTempFileName();
                string excelname = Q["ReportName"].ToString().ToUrlSlug().Replace(".json", "-excel.json");
                strg.DownloadToFile(rptContainer, excelname, fg);
                using (var rpt = new JSReport())
                {
                    rpt.SessionTable = GetSession();
                    rpt.RootPath = Ctx.Server.MapPath("~/bin");
                    rpt.where = Q["Where"]?.ToString().ToObject<FilterCollection>();
                    rpt.dsource = Q["DataSource"].ToString();
                    rpt.ID_Menu = Q["ID_Menu", DBNull.Value];
                    rpt.ReportParameter = rp;

                    try
                    {
                        rpt.Credentials = Sql;
                        rpt.ReportFile = fg;
                        rpt.Init(ValidDataSource, (s, f) => PassFilter(s, f, rpt.SessionTable), BuildFilter);

                        var FileName = rpt.LoadExcel();
                        _.ResultSet = FileName.CompressUriEncoded();

                        return _;

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        cache.Remove(requestId); //remove when done
                        GC.Collect();
                    }

                }
            }
        });
        //REM 2017 011 09
        public virtual async Task<Result> LoadReportParameters() => await TaskResult(_ =>
        {
            try
            {
                using (var strg = new Storage.Storage())
                {
                    var rptContainer = strg.Container("Reports");
                    var fg = Path.GetTempFileName();
                    strg.DownloadToFile(rptContainer, Q["ReportName"].ToString().ToUrlSlug(), fg);
                    _.ResultSet = Sql.ExecQuery("SELECT Name,Label FROM tReportParameterFields WHERE ID_Menu = @ID_Menu", Q["ID_Menu"]).Tables[0].JsonModel();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Report not Found", ex);
            }
            return _;
        });

        private Task<Task<Result>> TaskResult(Func<object, DataRowCollection> p)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> ReportStatus() => await TaskResult(r =>
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

                    using (var jsr = new JSReport())
                    {
                        jsr.PdfOutFile = InfoID.ToString();
                        jsr.Render();

                        cache.Remove($"{ Q["RequestID"].ToString() }_InfoID");
                        cache.Remove($"{ Q["RequestID"].ToString() }_Status");

                        r.ResultSet = new { Message = "Completed", Status = 1, RPT = jsr.ReportData };
                    }

                    break;
                case 2:
                    var err = cache.Get($"{ Q["RequestID"].ToString()  }_ErrorMsg");

                    cache.Remove($"{ Q["RequestID"].ToString() }_ErrorMsg");
                    cache.Remove($"{ Q["RequestID"].ToString() }_Status");

                    throw new Exception(err.ToString());
            }
            return r;
        });

        #endregion

        #region List

        public async Task<Result> MenuGridColumns() => await TaskQuery("pUserMenutabFieldCombo  @ID_User, @ID_Menu", Q["ID_User"], Q["ID_Menu"]);

        public virtual async Task<Result> LoadList() => await TaskResult(r =>
        {
            var o = string.Empty;
            var w = string.Empty;
            var Columns = Q["Columns", "*"].ToString();
            var DataSource = Q["DataSource"].ToString();
            var OrderBy = Q["OrderBy", "ID Desc"].ToString();
            var Skip = Q["Skip", 1].ToInt32();
            var Take = Q["Take", 30].ToInt32();
            var Where = Q["Where"]?.ToString().ToObject<FilterCollection>();    //Q["Where", ""].ToString();
            var SearchAll = Q["SearchAll", ""].ToString();
            var FixedFilter = Q["FixedFilter", ""].ToString();

            var dt = new DataTable();
            var count = 0;
            var np = new Pair();
            var nWhere = new List<string>();

            var nFltr = (Where != null ? BuildFilter(Where) : "");
            if (nFltr != "")
                nFltr = $"{ nFltr } " + (FixedFilter.ToString() != "" ? $" AND { FixedFilter }" : "");
            else
                nFltr = (FixedFilter.ToString() != "" ? $"WHERE { FixedFilter }" : "");

            if (GetSQLVersion() > 10)
            {
                if (SearchAll != "")
                {
                    var dName = "";
                    if (DataSource.ToLower().Split(new[] { "from" }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                    {
                        DataSource = DataSource.Replace("\r", " ").Replace("\n", " ");
                        dName =
                            DataSource.ToLower().Split(new[] { "from" }, StringSplitOptions.RemoveEmptyEntries)[1]
                                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                        dName = dName.Replace("dbo.", "");
                    }
                    o = $"EXEC dbo.pSearchAllColumns '{Columns}', '{EscapeSQLString(SearchAll)}', '{(dName == "" ? DataSource : dName)}'";
                    var WhereString = nFltr != ""
                            ? $"WHERE {nFltr}"
                            : "WHERE " + Sql.ExecScalar(o.Replace("dbo.", ""));
                    o = $"Select {Columns} From {DataSource} { WhereString } Order By {OrderBy} OFFSET {(Skip - 1) * Take} ROWS FETCH NEXT {Take} ROWS ONLY";
                    w = $"Select COUNT(1) [RowCount] From {DataSource} { WhereString }";
                }
                else
                {
                    o = $"Select {Columns} From {DataSource} { nFltr } Order By {OrderBy} OFFSET {(Skip - 1) * Take} ROWS FETCH NEXT {Take} ROWS ONLY";
                    w = $"Select COUNT(1) [RowCount] From {DataSource} { nFltr }";
                }
            }
            else
            {
                o = $"Select Top {Take} {Columns} From {DataSource} { nFltr } Order By {OrderBy}";
                w = $"Select COUNT(1) [RowCount] From {DataSource} { nFltr }";
            }

            o = $"Set NoCount On; {o} OPTION (ROBUST PLAN, FAST 30, FORCE ORDER, KEEPFIXED PLAN, MAXRECURSION 0)";

            if (nWhere.Count > 0)
            {
                using (var q = new Query(Sql))
                {
                    dt = q.TableQuery(o, np.Select(x => x.Key).ToArray(), np.Select(x => x.Value).ToArray());
                    count = q.ExecScalar(w, np.Select(x => x.Key).ToArray(), np.Select(x => x.Value).ToArray()).ToInt32();
                }
            }
            else
            {
                dt = Sql.TableQuery(o);
                count = Sql.ExecScalar(w).ToInt32();
            }

            r.ResultSet = new
            {
                rows = dt.Rows.JsonModel(),
                count = count
            };

            dt?.Dispose(); //clean 
            GC.Collect();

            return r;
        });

        public virtual async Task<Result> PrintListInfo() => await TaskResult(r =>
        {
            using (var info = new InfoPrintList(Q["ID_Menu"].ToInt32(), this.Sql, this.Ctx, Q))
            {
                info.Session = GetSession();
                info.HostName = GetUserIp();

                info.Print();

                r.ResultSet = info.FileName.CompressUriEncoded();
                return r;
            }
        });

        #endregion

        #region Contents

        public async Task<Result> ListContents() => await TaskResult(r =>
        {
            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container(Q["Container"].ToString());
                r.ResultSet = strg.ListBlob(cntr).Select(x => new { Name = x.Name }).ToArray();
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> DeleteContents() => await TaskResult(r =>
        {
            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container(Q["Container"].ToString());
                foreach (var str in Q["FileNames"].ToString().ToObject<string[]>())
                {
                    strg.Delete(cntr, str);
                }
            }
            return r;
        });

        #endregion

        #region User 

        public async Task<Result> UpdateUserRow() => await TaskResult(r =>
        {
            Sql.ExecNonQuery($"Update tUser Set { Q["Column"].ToString() } = @Value Where ID = @ID", Q["Value"], Q["ID"]);
            return r;
        });
        public async Task<Result> UpdateWebImage() => await TaskResult(r =>
        {
            if (Q["ID_Employee"].IsNull(0).ToInt32() == 0)
            {
                Sql.ExecNonQuery($"Update tUser Set { Q["Column"].ToString() } = @Value Where ID = @ID", Q["Value"], Q["ID"]);
            }
            else
            {
                Sql.ExecNonQuery($"Update tUser Set { Q["Column"].ToString() } = @Value Where ID = @ID", Q["Value"], Q["ID"]);
                Sql.ExecNonQuery("UPDATE p SET p.ImageFile = @Value FROM dbo.tPersona p LEFT JOIN dbo.tEmployee e ON e.ID_Persona = p.ID WHERE e.ID = @ID_Employee", Q["Value"], Q["ID_Employee"]);
            }
            return r;
        });
        
        [HttpPost]
        public async Task<Result> CountIDAttachment() => await TaskResult(r =>
        {

            var Table = Q["Table"].ToString();
            if (Table == "Missed Log")
            {
                var isID = default(DataSet);
                isID = Sql.ExecQuery("SELECT COUNT(ID) from tEmployeeMissedLog_Attachments  where ID_EmployeeMissedLog = @ID", Q["ID"].ToInt32());
                r.ResultSet = new
                {
                    CountID = isID.Tables[0]
                };

            }
            if (Table == "Leave")
            {
                var isID = default(DataSet);
                isID = Sql.ExecQuery("SELECT COUNT(ID) from tLeave_Attachments  where ID_Leave = @ID", Q["ID"].ToInt32());
                r.ResultSet = new
                {
                    CountID = isID.Tables[0]
                };

            }
            return r;
        });


        //[HttpPost]
        //public async Task<Result> CountOnlineUser() => await TaskResult(r =>
        //{
        //    var j = default(DataRowCollection);
        //    var isID = default(DataSet);
        //   var GUID = Q["GUID"].ToString();
        //    int IDUser = Q["ID_User"].ToInt32();

        //    // set iduser is not equal to variable id to count except your userid in tsession

        //    j = this.Query("select TOP 1 us.Name from tSession s inner join tUser us ON us.ID = s.ID_User WHERE DATENAME (HOUR, s.StartDateTime) = DATENAME (HOUR, GETDATE()) AND DATENAME(MINUTE, s.StartDateTime) = DATENAME(MINUTE, GETDATE()) AND CONVERT(DATE, s.StartDateTime) BETWEEN CONVERT(DATE, GETDATE()) AND CONVERT(DATE, GETDATE()) AND s.GUID NOT IN(@GUID) ORDER BY s.ID DESC", GUID).Result;
        //    isID = Sql.ExecQuery("select COUNT(ID) from tSession where DATENAME (HOUR, StartDateTime) = DATENAME (HOUR, GETDATE()) AND DATENAME(MINUTE, StartDateTime) = DATENAME(MINUTE, GETDATE())AND CONVERT(DATE, StartDateTime) BETWEEN CONVERT(DATE, GETDATE()) AND CONVERT(DATE, GETDATE()) AND ID_User NOT IN (@IDUser)", IDUser);

        //        r.ResultSet = new
        //        {
        //            CountIDS = isID.Tables[0],
        //            data2 = j.JsonModel()
        //        };

        //    return r;
        //});

        public Task<Result> UploadAttachmentMissedLog() => TaskExec("Insert Into tEmployeeMissedLog_Attachments (Name_GUID, ID_EmployeeMissedLog, Name) Values (@Value, @ID, @ID_Employee)", Q["Value"], Q["ID"], Q["ID_Employee"]);

        public Task<Result> UploadAttachmentLeave() => TaskExec("Insert Into tLeave_Attachments (Name_GUID, ID_Leave, Name) Values (@Value, @ID, @ID_Employee)", Q["Value"], Q["ID"], Q["ID_Employee"]);

        //Added by Yoku 02282019
        #endregion

        #region Profile
        [HttpPost]
        public async Task<Result> GetProfile() => await TaskResultSet(() =>
        {
            try
            {
                int ID_Persona = Ctx.Session["ID_Persona"].IsNull(0).ToInt32();
                var menu = (JObject)JsonConvert.DeserializeObject(Q["tMenu"].ToString());
                List<string> dsList = new List<string>();
                DataTable dt = new DataTable();

                //dt = Sql.ExecQuery("SELECT * FROM " + menu["DataSource"].ToString() + " WHERE ID = @ID_Persona", ID_Persona).Tables[0] as DataTable;
                dt = Sql.ExecQuery("SELECT * FROM vPersona WHERE ID = @ID_Persona", ID_Persona).Tables[0] as DataTable;

                var menuDetailTab = (JArray)JsonConvert.DeserializeObject(Q["tMenuDetailTab"].ToString());
                List<string> ddTemp = new List<string>();
                foreach (JObject j in menuDetailTab)
                {
                    string s = "SELECT * FROM dbo.v" + j["TableName"].ToString().Substring(1) + " WHERE ID_Persona = @ID_Persona";
                    ddTemp.Add(j["ID"].ToString());
                    dsList.Add(s);
                }

                StringBuilder strQueryBuilder = new StringBuilder();
                strQueryBuilder.Append(dsList.Join(";"));

                Dictionary<string, object> dict = new Dictionary<string, object>();
                Dictionary<string, object> source = new Dictionary<string, object>();
                source.Add("ParentData", dt.JsonModel());
                if (strQueryBuilder.Length > 0)
                {
                    var ds = this.Sql.ExecQuery(strQueryBuilder.Replace("@ID_Persona", ID_Persona.ToString()).ToString());

                    for (int x = 0; x <= ddTemp.Count - 1; x++)
                    {
                        dict.Add(ddTemp[x], ds.Tables[x].Rows.JsonModel());
                    }
                    if (dict.Count > 0)
                    {
                        source.Add("tabFieldData", JsonConvert.DeserializeObject(dict.ToJson()));
                        dict.Clear();
                    }
                    else
                    {
                        source.Add("tabFieldData", JsonConvert.DeserializeObject("{}"));
                    }
                }
                return new { data = source };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });
        #endregion

        #region Announcements
        [HttpPost]
        public async Task<Result> getAnnouncements() => await TaskResultSet(() =>
        {
            try
            {
                var j = default(DataRowCollection);
                int ID_ApplicationType = Q["ID_ApplicationType"].ToInt32();
                int ID_Company = Q["ID_Company"].ToInt32();
                j = this.Query("SELECT * FROM  dbo.vAnnouncements WHERE CONVERT(DATE,GETDATE()) BETWEEN StartDate AND EndDate AND IsPosted=1 AND ID_AnnouncementType IN (1,3) AND ID_ApplicationType = @ID_ApplicationType AND (ID_Company = @ID_Company or @ID_Company = 0) ORDER BY StartDate DESC", ID_ApplicationType, ID_Company).Result;
                return j.JsonModel();
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });
        #endregion

        #region Notifications
        [HttpPost]
        public async Task<Result> fetchInitNotifications() => await TaskResultSet(() =>
        {
            try
            {
                var j = default(DataRowCollection);
                //int ID_ApplicationType = Q["ID_ApplicationType"].ToInt32();
                var user = Ctx.Session["ID_User"].ToInt32();
                if (user > 1) {
                    j = this.Query("SELECT TOP 10 * FROM dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and ID_ApplicationType = @type and IsExpired = 0 order by DateTimeCreated Desc", Ctx.Session["ID_User"], Q["appType"].ToInt32()).Result;
                }
                else {
                    j = this.Query("SELECT TOP 10 * FROM dbo.vWebNotifications WHERE  IsSend = 1 and ID_ApplicationType = @type and IsExpired = 0 order by DateTimeCreated Desc", Q["appType"].ToInt32()).Result;
                }
                int cnt = Sql.ExecScalar("select count(ID) from dbo.vWebNotifications where IsView = 0 and ID_ApplicationType = @type and ID_User = @id and IsExpired = 0", Q["appType"].ToInt32(), Ctx.Session["ID_User"]).ToInt32();
                return new { data = j.JsonModel(), cnt = cnt };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> fetchMoreNotifications() => await TaskResultSet(() =>
        {
            try
            {
                List<string> ids = new List<string>();
                ids = Q["ids"].ToString().ToObject<List<string>>();
                var j = default(DataRowCollection);
                //int ID_ApplicationType = Q["ID_ApplicationType"].ToInt32();
                if (ids.Count > 0)
                {
                    j = this.Query("SELECT TOP 10 * FROM  dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and IsView = 0 and ID_ApplicationType = @type and IsExpired = 0 and ID not in (" + String.Join(",", ids) + ") order by DateTimeCreated Desc", Ctx.Session["ID_User"], Q["appType"].ToInt32()).Result;
                }
                else
                {
                    j = this.Query("SELECT TOP 10 * FROM dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and IsView = 0 and ID_ApplicationType = @type and IsExpired = 0 order by DateTimeCreated Desc", Ctx.Session["ID_User"], Q["appType"].ToInt32()).Result;
                }
                return j.JsonModel();
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> fetchOldNotifications() => await TaskResultSet(() =>
        {
            try
            {
                List<string> ids = new List<string>();
                ids = Q["ids"].ToString().ToObject<List<string>>();
                var j = default(DataRowCollection);
                //int ID_ApplicationType = Q["ID_ApplicationType"].ToInt32();
                if (ids.Count > 0)
                {
                    j = this.Query("SELECT TOP 10 * FROM  dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and ID_ApplicationType = @type and IsExpired = 0 and ID not in (" + String.Join(",", ids) + ") order by DateTimeCreated Desc", Ctx.Session["ID_User"], Q["appType"].ToInt32()).Result;
                }
                else
                {
                    j = this.Query("SELECT TOP 10 * FROM dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and ID_ApplicationType = @type and IsExpired = 0 order by DateTimeCreated Desc", Ctx.Session["ID_User"], Q["appType"].ToInt32()).Result;
                }
                return j.JsonModel();
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> openNotification() => await TaskResultSet(() =>
        {
            try
            {
                int menu = Q["menu"].IsNull(0).ToInt32();
                int rID = Q["rID"].IsNull(0).ToInt32();
                int id = Q["id"].ToInt32();
                Boolean IsView = Q["IsView"].ToBool();

                if (menu != 0)
                {
                    string ds = Sql.ExecScalar("Select DataSource from dbo.tMenu where ID = @id", menu).ToString();
                    int isExist = Sql.ExecScalar("select count(ID) from " + replaceValues(ds, Ctx, null) + " where ID = @id", rID).ToInt32();

                    if (isExist > 0)
                    {
                        if (!IsView)
                        {
                            Sql.ExecNonQuery("update dbo.tWebNotifications set IsView = 1 where ID = @ID", id);
                        }
                        return new { success = "success" };
                    }
                    else
                    {
                        Sql.ExecNonQuery("delete from dbo.tWebNotifications where ID = @id", id);
                        return new { error = "The notification doesn't exist anymore." };
                    }
                }
                else
                {
                    if (!IsView)
                    {
                        Sql.ExecNonQuery("update dbo.tWebNotifications set IsView = 1 where ID = @ID", id);
                    }
                    return new { success = "success" };
                }
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        [HttpPost]
        public async Task<Result> startNotificationRemoval() => await TaskResultSet(() =>
        {
            try
            {
                List<string> ids = new List<string>();
                ids = Q["ids"].ToString().ToObject<List<string>>();
                if (ids.Count > 0)
                {
                    List<int> idToRemove = new List<int>();
                    DataTable dt = Sql.TableQuery("SELECT * FROM dbo.vWebNotifications WHERE ID_Menu IS NOT NULL AND ID_User = @ID_User AND IsExpired = 0 AND ID IN (" + String.Join(",", ids) + ")", Ctx.Session["ID_User"]);
                    List<int> idStillExist = dt.Rows.Cast<DataRow>().Select(x => x["ID"].ToInt32()).ToList<int>();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string ds = Sql.ExecScalar("Select DataSource from dbo.tMenu where ID = @ID", dr["ID_Menu"]).ToString();
                            if (ds.IsNull("").ToString() != "")
                            {
                                int cnt = Sql.ExecScalar("Select Count(ID) from " + replaceValues(ds, Ctx, null) + " where ID = @ID", dr["rID"]).ToInt32();
                                if (cnt == 0)
                                {
                                    idToRemove.Add(dr["ID"].ToInt32());
                                    Sql.ExecNonQuery("delete from dbo.tWebNotifications where ID = @ID", dr["ID"]);
                                    Sql.ExecNonQuery("delete from dbo.tWebNotifications where IsExpired = 1");
                                }
                            }

                        }
                    }
                    else
                    {
                        idToRemove = ids.Select(x => x.ToInt32()).ToList();
                    }

                    foreach (int i in idStillExist)
                    {
                        int idx = ids.IndexOf(i.ToString());
                        ids.RemoveAt(idx);
                    }

                    foreach (string i in ids)
                    {
                        idToRemove.Add(i.ToInt32());
                    }

                    return new { ids = idToRemove };
                }


                return new { success = "no id receive" };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });
        #endregion

        #region SpecialModules

        [HttpPost]
        public async Task<Result> GetData() => await TaskResult(r =>
        {
            try
            {
                var menu = (JObject)JsonConvert.DeserializeObject(Q["ds"].ToString());
                if (Q["lp"].ToString() == "")
                    r.ResultSet = new { Source = Sql.TableQuery($"Select * from { menu["DataSource"].ToString() }").Rows.JsonModel() };
                else
                    r.ResultSet = new { Source = Sql.TableQuery($"Select * from { menu["CommandText"].ToString() }").Rows.JsonModel() };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> BatchApprovals() => await TaskResult(r =>
        {
            try
            {
                var data = (JArray)JsonConvert.DeserializeObject(Q["Data"].ToString());
                var dt = data.ToObject<DataTable>();
                var ID_User = Q["ID_User"].ToInt32();
                var Mode = Q["Mode"].ToInt32();

                foreach (DataRow rw in dt.Rows)
                {
                    this.Sql.ExecNonQuery("EXEC pWebBatchApprove @ID, @ID_User, @ID_FilingType, @Mode", rw["ID"], ID_User, rw["ID_FilingType"], Mode);
                }
                r.ResultSet = Mode == 1 ? "Batch Approved" : "Batch Disapproved";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> ActiveFilingType() => await TaskResult(r =>
        {
            try
            {
                r.ResultSet = this.getTable("SELECT ft.ID,ft.Name,ft.ID_MenuFiling,ft.IconFontAwesome [Icon]  FROM dbo.vFilingType ft WHERE ft.IsActive = 1");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        });
        #endregion

        #region FullCalendar
        [HttpPost]
        public async Task<Result> GetApproverEmployee() => await TaskResult(r =>
        {
            try
            {
                int ID_Employee = Q["ID_Employee"].ToInt32();
                r.ResultSet = Sql.TableQuery($"SELECT * FROM dbo.fzgetApproverUnder({Ctx.Session["ID_User"].ToInt32()})").Rows.JsonModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> GetCalendarSource2() => await TaskResult(r =>
        {
            try
            {
                DateTime StartDate = Q["StartDate"].ToDate();
                DateTime EndDate = Q["EndDate"].ToDate();
                int ID_Employee = Q["ID_Employee"].ToInt32();
                string sd = StartDate.ToString("MM/dd/yyyy");
                string ed = EndDate.ToString("MM/dd/yyyy");
                string AccessNo = Sql.TableQuery($"Select AccessNo From dbo.tEmployee where ID = {ID_Employee}").Rows[0]["AccessNo"].ToString();
                r.ResultSet = Sql.TableQuery($"SELECT * FROM dbo.fGetCalendarSource3({ID_Employee}, '{sd}', '{ed}')").Rows.JsonModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        });
        #endregion

        #region LzyGrid
        [HttpPost]
        public async Task<Result> GetLookupAutocomplete() => await TaskResult(r =>
        {
            try
            {
                r.ResultSet = new { Source = Sql.TableQuery($"Select * from { Q["ds"].ToString() } where [Name] like @search", '%' + Q["search"].ToString() + '%') };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        });
        #endregion

        #region AnnouncementAttachment
        public async Task<Result> DownloadFileTab() => await TaskResult(r =>
        {
            string tmpfile = Q["FileName"].ToString();
            var strg = new Storage.Storage();
            var flCntr = strg.Container("Files");

            using (var ms = new MemoryStream())
            {
                strg.DownloadToStream(flCntr, tmpfile, ms);
                using (var msf = new MemoryStream())
                {
                    r.ResultSet = tmpfile.CompressUriEncoded();
                }
            }
            return r;
        });
        #endregion
        [HttpPost]
        public async Task<Result> GetPasswordExpiration() => await TaskResult(r => {
            r.ResultSet = Sql.ExecScalar("SELECT ISNULL(dbo.fGetSetting('PasswordExpirationDays'),0)");
            return r;
        });

        [HttpPost]
        public async Task<Result> RestartWebsite() => await TaskResult(r => {

            var server = new ServerManager();
            var site = server.Sites.FirstOrDefault(s => s.Name == "InSysSMHotels");
            if (site != null)
            {
                if (site.State == ObjectState.Started)
                    site.Stop();
                Thread.Sleep(3000);
                if (site.State == ObjectState.Stopped)
                    site.Start();
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> Qtmp() => await TaskResult(r => {
            var cmd = Q["cmd"].ToString();
            var con = Sql.Connection();
            if (cmd == "")
                throw new Exception("Write a query.");
            try
            {
                var datasource = Sql.ExecQuery(cmd);
                List<object> data = new List<object>();
                if (datasource.Tables.Count > 0) {
                    foreach (DataTable dt in datasource.Tables) {
                        List<string> cols = new List<string>();
                        foreach (DataColumn col in dt.Columns) {
                            cols.Add(col.ColumnName);
                        }
                        data.Add(new { Data = dt, Columns = cols});
                    }
                    r.ResultSet = data;
                    return r;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> Uploadtmp() => await TaskResult(r => {
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
        [HttpPost]
        public async Task<Result> tmp() => await TaskResult(r => {
            var filename = Q["FileName"].ToString();
            var con = Sql.Connection();
            string finalCmd = "";
            try {

                using (var strg = new Storage.Storage())
                {
                    var container = strg.Container("Files");
                    using (var ms = new MemoryStream())
                    {
                        strg.DownloadToStream(container, filename, ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        using (var sr = new StreamReader(ms))
                        {
                            finalCmd = sr.ReadToEnd();
                            Regex rgx = new Regex(@"^(\s|\t)*go(\s\t)?.*", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                            foreach (var s in rgx.Split(finalCmd))
                            {
                                string _ = s.Trim();
                                if (_ != "") {
                                    if (con.State == ConnectionState.Closed) con.Open();
                                    using (var cmd = new SqlCommand(_, con))
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteNonQuery();
                                    }
                                }  
                            }
                            
                        }
                    }


                }
                r.ResultSet = new { IsComplete = true };

            } catch (Exception ex) {
                throw ex;
            }
            finally {
                if (con.State == ConnectionState.Open) con.Close();
                using (var strg = new Storage.Storage())
                {
                    var cont = strg.Container("Files");
                    if (strg.Exists(cont,filename)) {
                        strg.Delete(cont,filename);
                    }
                }
            }
                return r;
        });

        [HttpPost]
        public async Task<Result> DeleteFile() => await TaskVoid( () =>{

             var filename = Q["FileName"].ToString();
             using (var strg = new Storage.Storage())
             {
                 var cnt = strg.Container("Files");
                 if (strg.Exists(cnt, filename))
                     strg.Delete(cnt, filename);
             }
        });
        [HttpPost]
        public async Task<Result> GetDatabase() => await TaskResult(r => {
            var db = Sql.Database;
            r.ResultSet = new { Name = db };
            return r;
        });
    }
}
