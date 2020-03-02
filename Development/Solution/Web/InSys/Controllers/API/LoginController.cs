using InSys.Helpers;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using z.Controller; 
using z.Data;
using z.SQL;

namespace InSys.Controllers.API
{
    [AuthorizeRequest]
    public class LoginController : BaseController
    {
        protected override void Initializer()
        {

        }

        [HttpPost]
        public async Task<Result> AuthUser() => await TaskResult(r =>
        {
            var asd = Q["Username"];
            if (asd == null) throw new Exception("Please Specify Username or Password");

            string Username = asd.ToString();
            object Password = Q["Password"];
            String Logcount = this.Sql.ExecScalar("SELECT dbo.fGetSetting('FailedLogInCount')").ToString();

            if (Username.Trim() == "") throw new Exception("Please Specify Username");


            if (Password == null) Password = DBNull.Value;
            else if (Password.ToString() == "") Password = DBNull.Value;
            else Password = Password.ToString().EncryptA(41) + "_BJTGLR";

            var u = this.Query("SELECT * FROM dbo.tUser  WHERE LogInName = @Username", Username).Result;
            var z = this.Query("SELECt * FROM dbo.tUser WHERE LogInName = @Username AND IsActive = 0",Username).Result.Count;
            if (z > 0) throw new Exception("This account has no longer access in the system.");
            if (u.Count == 1 && this.Sql.ExecScalar($"SELECT CASE  WHEN InvalidLogCount >= @Logcount THEN 1 ELSE 0 END AS INVALID FROM dbo.tUser WHERE LogInName = @Username", Logcount, Username).ToBool())
            {
                this.Sql.ExecNonQuery("pFailedLogAttempt  @UserName, @Password, @Comment", Username, Password, "Blocked");
                var ret = unblockUserAutomation(Username);
                throw new Exception(String.Format("User is blocked Please contact System Administrator to unblock your account or try again after {0}", ret));
            }
            else
            {
                var j = this.Query("SELECT * FROM dbo.fUser(@LogInName, @Password)", Username, Password).Result;

                if (j.Count == 0)
                {
                    this.Sql.ExecNonQuery($"Update u set InValidLogCount = InValidLogCount + 1 from tUSer u where LogInName = @Username AND u.ID <> 1", Username);
                    this.Sql.ExecNonQuery("pFailedLogAttempt  @UserName, @Password, @Comment", Username, Password, "Invalid");

                    var bDate = Sql.ExecScalar($"SELECT InvalidLogCount from tuser where LogInName = @username and id <> 1",Username).ToInt32();
                    if(bDate == Logcount.ToInt32())
                        Sql.ExecNonQuery($"UPDATE u set BlockedDate  = GETDATE() from tuser u where LogInName = @username AND u.ID <> 1",Username);
                    throw new Exception("Invalid Username or Password");

                }
                else
                {
                    var dt = j[0];
                    if (this.Sql.ExecScalar("SELECT dbo.fForceChangePassword_FirstLog(@Username)", Username).ToBool())
                    {
                        r.Tag = "Please Setup your Password";
                        r.Status = 2; //ChangePassword
                        r.ResultSet = dt["ID"];
                        return r;
                    }

                    if (this.Sql.ExecScalar("SELECT dbo.fForceChangePassword_Expired(@Username)", Username).ToInt32() == 1)
                    {
                        string exp = this.Sql.ExecScalar("SELECT dbo.fGetSetting('PasswordExpirationDays')").ToString();
                        r.Tag = $"Your password meets {exp} days expiration, Please Change Your Password to log in";
                        r.Status = 3; //ChangePassword
                        r.ResultSet = dt["ID"];
                        return r;
                    }

                    int ID = dt["ID"].ToInt32();
                    int ID_Employee = dt["ID_Employee"].IsNull(0).ToInt32();
                    int ID_CompanyHistory = dt["ID_CompanyHistory"].IsNull(0).ToInt32();
                    this.Sql.ExecNonQuery("pResetInvalidLogCount @ID", ID);
                    Ctx.Session.Add("tmpAuthID", dt["GUID"]);

                    var row = new Pair();
                    row.Add("GUID", dt["GUID"]);

                    using (var tCompany = this.Sql.ExecQuery("pGetCompanyTable @ID", ID).Tables[0])
                    {
                        if (tCompany.Rows.Count == 0)
                        {
                            row.Add("ID_Company", 0);
                            row.Add("Company", "All Companies");
                        }
                        else
                        {
                            if (Q["SelectCompanyAfterValidation"].ToBool())
                                r.Status = 4;
                            else
                            {
                                if (ID_Employee == 0 && tCompany.Rows.Count > 1)
                                {
                                    r.Status = 4;
                                }
                                //else if (dt["ID_Company"].IsNull(0).ToInt32() != 0 && dt["ID_UserGroup"].IsNull(0).ToInt32() == 3)
                                //{
                                //    row.Add("ID_Company", tCompany.Rows[0]["ID"]);
                                //    row.Add("Company", tCompany.Rows[0]["Name"]);
                                //}
                                else if (dt["ID_Company"].IsNull(0).ToInt32() != 0)
                                {
                                    row.Add("ID_Company", dt["ID_Company"]);
                                    row.Add("Company", dt["Company"]);
                                }
                                else
                                    r.Status = 4;
                            }
                        }
                    }

                    row.Add("ID", ID);
                    row.Add("Username", Username);
                    row.Add("ID_Employee", dt["ID_Employee"]);
                    row.Add("ID_CompanyHistory", dt["ID_CompanyHistory"]);
                    row.Add("ID_DefaultCompany", dt["ID_Company"]);
                    row.Add("IPAddress", GetUserIp());
                    row.Add("IsFirstLog", dt["IsFirstLog"]);
                    row.Add("IsSecretQuestionReady", dt["IsSecretQuestionReady"]);
                    PasswordHistory(ID);

                    //hide UID

                    // Ctx.Response.Cookies.Add(new HttpCookie("X-UID", dt["GUID"].ToString()) { Secure = true });

                    r.ResultSet = row;
                }
            }
            return r;
        });

        public async Task<Result> Companies() => await TaskResult(r =>
        {
            var j = this.Query("SELECT * FROM dbo.fUserGUID(@GUID)", Ctx.Session["tmpAuthID"]).Result;

            if (j.Count == 0)
            {
                throw new Exception("Invalid ID");
            }

            var tCompany = new DataTable();
            var dt = j[0];
            int ID = dt["ID"].ToInt32();
            int ID_Employee = dt["ID_Employee"].IsNull(0).ToInt32();

            DataRow dr;
            tCompany.Columns.Add("ID", typeof(Int32));
            tCompany.Columns.Add("Name", typeof(string));
            tCompany = this.Sql.ExecQuery("pGetCompanyTable @ID", ID).Tables[0];
            if (ID_Employee == 0 || tCompany.Rows.Count > 1)
            {
                dr = tCompany.NewRow();
                dr["ID"] = 0;
                dr["Name"] = "All Companies";
                tCompany.Rows.Add(dr);
            }
            else if (dt["ID_Company"].ToInt32() != tCompany.Rows[0]["ID"].ToInt32())
            {
                dr = tCompany.NewRow();
                dr["ID"] = dt["ID_Company"];
                dr["Name"] = dt["Company"];
                tCompany.Rows.Add(dr);
            }

            if (ID_Employee != 0 && dt["ID_UserGroup"].IsNull(0).ToInt32() == 3)
            {
                tCompany = tCompany.AsEnumerable().Where(x => x["Name"].ToString() == dt["Company"].ToString()).OrderBy(x => x["ID"].IsNull(0).ToInt32()).OrderBy(x => x["Name"]).CopyToDataTable();
            }
            else
            {
                tCompany = tCompany.AsEnumerable().OrderBy(x => x["ID"].IsNull(0).ToInt32()).OrderBy(x => x["Name"]).CopyToDataTable();
            }
            

            var row = new Pair();
            row.Add("GUID", dt["GUID"]);

            r.Status = 4;
            row.Add("ID", ID);
            row.Add("Username", dt["Name"]);
            row.Add("ID_Employee", dt["ID_Employee"]);
            row.Add("tCompany", tCompany.Rows.JsonModel());
            row.Add("ID_CompanyHistory", dt["ID_CompanyHistory"]);
            row.Add("ID_DefaultCompany", dt["ID_Company"]);

            row.Add("IPAddress", GetUserIp());

            r.ResultSet = row;

            tCompany?.Dispose();

            return r;
        });

        public async Task<Result> StartSession() => await TaskResult(r =>
        {

            int? ID_Employee = Q["ID_Employee"]?.ToInt32();
            int ID = Q["ID"].ToInt32();
            var ID_Company = Q["ID_Company", DBNull.Value];

            string skinName = "";
            if (ID_Company == DBNull.Value)
            {
                int id_selected = 0;
                skinName = LoadTheme(id_selected);
            }
            else
            {
                skinName = LoadTheme(ID_Company.ToInt32());
            }
            if (skinName != "")
            {
                var strge = new Storage.Storage();
                var container = strge.Container("Themes");
                var blob = strge.ListBlob(container).Where(x => x.Name == strge.ToURLSlug(skinName)); //.SingleOrDefault();
                if (blob.Any())
                {
                    var bb = blob.Single();
                    var sasToken = bb.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        Permissions = SharedAccessBlobPermissions.Read,
                        SharedAccessExpiryTime = DateTime.UtcNow.AddDays(7),

                    }, new SharedAccessBlobHeaders { ContentType = "text/css" });

                    var blobUrl = bb.AbsoluteUri + sasToken;//This will ensure that user will be able to access the blob for one hour.
                    Ctx.Session.Add("Skin", blobUrl.ToString());
                }
                else
                {
                    Ctx.Session.Add("Skin", "");
                }

            }
            else
            {
                Ctx.Session.Add("Skin", "");
            }


            using (var dt = this.Sql.ExecQuery("pAddSession @ID_User, @ID_Company, @ID_Employee, @IPAddress", ID, ID_Company, ID_Employee.IsNull(DBNull.Value), GetUserIp().Replace("::1", "127.0.0.1")).Tables[0])
            {
                //dt.Columns.Add("IPAddress", typeof(string));
                dt.Columns.Add("UserMenuIDList", typeof(string));
                dt.Columns.Add("UserWebWidgetIDList", typeof(string));
                //dt.Rows[0]["IPAddress"] = GetUserIp();

                foreach (DataColumn dc in dt.Columns)
                {
                    Ctx.Session.Add(dc.ColumnName, dt.Rows[0][dc.ColumnName]);
                }

                if (ID > 2)
                {
                    dt.Rows[0]["UserMenuIDList"] = GetUserMenuID(ID).ToJson().CompressToUTF16();
                }
                else
                {
                    dt.Rows[0]["UserMenuIDList"] = null;
                }

                using (var dt2 = this.Sql.ExecQuery("SELECT w.ID FROM dbo.tWebWidgets w INNER JOIN dbo.tWebWidgetsUserGroup w2 ON w.ID = w2.ID_WebWidgets WHERE w2.ID_UserGroup = @ID_UserGroup", dt.Rows[0]["ID_UserGroup"]).Tables[0])
                {
                    dt.Rows[0]["UserWebWidgetIDList"] = dt2.Rows.Cast<DataRow>().Select(x => { return new Tuple<int>(x[0].ToInt32()); }).ToList().ToJson().CompressToUTF16();
                }


                using (var f = this.Sql.ExecQuery("SELECT * FROM dbo.fWebGetUserInfo(@ID_User)", dt.Rows[0]["ID_User"]).Tables[0])
                {
                    var fr = f.Rows[0];
                    var hj = dt.Rows[0].JsonModel();

                    foreach (DataColumn dc in f.Columns)
                    {
                        if (!hj.Keys.Contains(dc.ColumnName))
                        {
                            hj.Add(dc.ColumnName, fr[dc.ColumnName]);
                            Ctx.Session.Add(dc.ColumnName, fr[dc.ColumnName]);
                        }
                    }
                    hj.Add("currentVersion", Config.Get("currentVersion"));
                    // hj.Add("XSRF-TOKEN", AuthorizationToken.Auth(Ctx, Q["GUID"].ToString()));
                    Ctx.Session.Remove("tmpAuthID");
                    //Ctx.Response.Cookies.Add(new HttpCookie("XSRF-TOKEN", AuthorizationToken.Auth(Ctx, Q["GUID"].ToString())) ); //{  Secure = true }

                    //Ctx.Session.Timeout = 30; // set 30 minutes

                    r.ResultSet = hj;

                    Ctx.Session.Add("Session", hj);


                    logger.ForContext("Token", hj["Token"]?.ToString()).ForContext("ID_User", hj["ID_User"]).Information("Session Started");


                    return r;
                }
            }
        });
        private string unblockUserAutomation(string username)
        {
            var displayDate = "";
            try
            {
                TimeSpan execTime;
                var setting = Sql.ExecScalar("SELECT dbo.fGetSetting('UnblockUserTimeOut')").ToString().ToLower();
                if (setting.Contains("day"))
                {
                    setting = setting.Remove(setting.IndexOf('d'));
                    execTime = new TimeSpan(setting.ToInt32(), 0, 0, 0);
                }
                else if (setting.ToLower().Contains("hr"))
                {
                    setting = setting.Remove(setting.IndexOf('h'));
                    execTime = new TimeSpan(0, setting.ToInt32(), 0, 0);
                }
                else
                {
                    execTime = new TimeSpan(0, 0, setting.ToInt32(), 0);
                }

                var blockedDate = Sql.ExecScalar($"SELECT BlockedDate from tuser where LogInName = @username", username).ToDate();
                var currentTime = Sql.ExecScalar("SELECT dbo.fGetDate()").ToDate();
                var udate = blockedDate.Add(execTime) - currentTime;
                if (udate <= TimeSpan.Zero)
                {
                    Sql.ExecNonQuery($"UPDATE tuser set BlockedDate = null, InvalidLogCount = 0 where LogInName = @username and id <> 1", username);
                    throw new Exception("Block duration has passed, Your account will be unblock on your next login.");
                }
                var t = new Timer(x =>
                {
                    Sql.ExecNonQuery($"UPDATE tuser set BlockedDate = null, InvalidLogCount = 0 where LogInName = @username and id <> 1", username);
                }, null, udate, udate);

                if (udate.Days > 0)
                    displayDate = String.Format("{0} Day(s) {1} Hour(s)", udate.Days, udate.Hours);
                else if (udate.Hours > 0)
                    displayDate = String.Format("{0} Hour(s) {1} Min(s)", udate.Hours, udate.Minutes);
                else if (udate.Minutes > 0)
                    displayDate = String.Format("{0} Min(s) {1} Sec(s)", udate.Minutes, udate.Seconds);
                else
                    displayDate = String.Format("{0} Sec(s)", udate.Seconds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return displayDate;
        }
    }
}