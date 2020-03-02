using InSys.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using z.Controller;
using z.Data;
using z.SQL;
using static InSys.Models.UtilityModel;

namespace InSys.Controllers.API
{
    [AuthorizeMobile]
    public class MobileController : BaseController
    {

        #region Authentication

        public async Task<Result> Login() => await TaskResult(r => {

            string Username = Q["Username"].ToString();
            object Password = Q["Password"];
            String Logcount = this.Sql.ExecScalar("SELECT dbo.fGetSetting('FailedLogInCount')").ToString();

            if (Username.Trim() == "") throw new Exception("Please Specify Username");

            if (Password == null) Password = DBNull.Value;
            else if (Password.ToString() == "") Password = DBNull.Value;
            else Password = Password.ToString().EncryptA(41) + "_BJTGLR";

            var u = this.Query("SELECT * FROM dbo.tUser  WHERE LogInName = @Username", Username).Result;
            if (u.Count == 1 && this.Sql.ExecScalar($"SELECT CASE  WHEN InvalidLogCount >= @Logcount THEN 1 ELSE 0 END AS INVALID FROM dbo.tUser WHERE LogInName = @Username", Logcount, Username).ToBool())
            {
                this.Sql.ExecNonQuery("pFailedLogAttempt  @UserName, @Password, @Comment", Username, Password, "Blocked");
                throw new Exception("User is blocked Please contact System Administrator to unblock account");
            }
            else
            {
                var j = this.Query("SELECT * FROM dbo.fUser(@LogInName, @Password)", Username, Password).Result;

                if (j.Count == 0)
                {
                    this.Sql.ExecNonQuery($"Update u set InValidLogCount = InValidLogCount + 1 from tUSer u where LogInName = @Username AND u.ID <> 1", Username);
                    this.Sql.ExecNonQuery("pFailedLogAttempt  @UserName, @Password, @Comment", Username, Password, "Invalid");
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

                    if (this.Sql.ExecScalar("SELECT dbo.fForceChangePassword_Expired(@Username)", Username).ToBool())
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
                                else if (dt["ID_Company"].IsNull(0).ToInt32() != tCompany.Rows[0]["ID"].ToInt32())
                                {
                                    row.Add("ID_Company", tCompany.Rows[0]["ID"]);
                                    row.Add("Company", tCompany.Rows[0]["Name"]);
                                }
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
                    //Ctx.Response.Cookies.Add(new HttpCookie("X-UID", dt["GUID"].ToString()));

                    r.ResultSet = row;
                }
            }
            
            return r;
        });

        public async Task<Result> Companies() => await TaskResult(r =>
        {
            var j = this.Query("SELECT * FROM dbo.fUserGUID(@GUID)", Q["GUID"]).Result;

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


            tCompany = tCompany.AsEnumerable().OrderBy(x => x["ID"].IsNull(0).ToInt32()).OrderBy(x => x["Name"]).CopyToDataTable();

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
              
            using (var dt = this.Sql.ExecQuery("pAddSession @ID_User, @ID_Company, @ID_Employee", ID, ID_Company, ID_Employee.IsNull(DBNull.Value)).Tables[0])
            {
                dt.Columns.Add("IPAddress", typeof(string));
                dt.Columns.Add("UserMenuIDList", typeof(string));
                dt.Columns.Add("UserWebWidgetIDList", typeof(string));
                dt.Rows[0]["IPAddress"] = GetUserIp();
                
                using (var f = this.Sql.ExecQuery("SELECT * FROM dbo.fWebGetUserInfo(@ID_User)", dt.Rows[0]["ID_User"]).Tables[0])
                {
                    var fr = f.Rows[0];
                    var hj = dt.Rows[0].JsonModel();

                    foreach (DataColumn dc in f.Columns)
                    {
                        if (!hj.Keys.Contains(dc.ColumnName))
                        {
                            hj.Add(dc.ColumnName, fr[dc.ColumnName]);
                           // Ctx.Session.Add(dc.ColumnName, fr[dc.ColumnName]);
                        }
                    }
                    hj.Add("currentVersion", Config.Get("currentVersion"));
                    //hj.Add("XSRF-TOKEN", AuthorizationToken.Auth(Ctx, Q["GUID"].ToString()));
                    //Ctx.Request.Cookies.Remove("X-UID");
                    //Ctx.Response.Cookies.Add(new HttpCookie("XSRF-TOKEN", AuthorizationToken.Auth(Ctx, Q["GUID"].ToString())));

                    r.ResultSet = hj;

                    return r;
                }
            }
        });

        public async Task<Result> CurrentDate() => await TaskScalar("SELECT dbo.fGetDate()");

        #endregion

        #region Official Business

        public async Task<Result> OBList() => await TaskQuery("SELECT * FROM dbo.vOB o WHERE o.ID_Employee = @ID_Employee", Q["ID_Employee"]);

        #endregion

        #region Notifications
        public async Task<Result> mobileInitNotifications() => await TaskResultSet(() =>
        {
            try
            {
                var j = default(DataRowCollection);
                //int ID_ApplicationType = Q["ID_ApplicationType"].ToInt32();
                j = this.Query("SELECT TOP 10 * FROM dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and ID_ApplicationType = 2 and IsExpired = 0 order by DateTimeCreated Desc", Q["ID_User"].ToInt32()).Result;
                int cnt = Sql.ExecScalar("select count(ID) from dbo.vWebNotifications where IsView = 0 and ID_ApplicationType = 2 and ID_User = @id and IsExpired = 0", Q["ID_User"]).ToInt32();
                return new { data = j.JsonModel(), cnt = cnt };
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        public async Task<Result> mobileNewNotifications() => await TaskResultSet(() =>
        {
            try
            {
                List<string> ids = new List<string>();
                ids = Q["ids"].ToString().ToObject<List<string>>();
                var j = default(DataRowCollection);
                //int ID_ApplicationType = Q["ID_ApplicationType"].ToInt32();
                if (ids.Count > 0)
                {
                    j = this.Query("SELECT TOP 10 * FROM  dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and IsView = 0 and ID_ApplicationType = 2 and IsExpired = 0 and ID not in (" + String.Join(",", ids) + ") order by DateTimeCreated Desc", Q["ID_User"]).Result;
                }
                else
                {
                    j = this.Query("SELECT TOP 10 * FROM dbo.vWebNotifications WHERE ID_User = @id and IsSend = 1 and IsView = 0 and ID_ApplicationType = 2 and IsExpired = 0 order by DateTimeCreated Desc", Q["ID_User"]).Result;
                }
                return new { data = j.JsonModel()};
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        });

        public async Task<Result> mobileRemoveNotifications() => await TaskResultSet(() =>
        {
            try
            {
                List<string> ids = new List<string>();
                ids = Q["ids"].ToString().ToObject<List<string>>();
                DataRow drMobile = Sql.TableQuery("Select * from dbo.vSession where ID_Session = @ID", Q["ID_Session"]).Rows[0];
                if (ids.Count > 0)
                {
                    List<int> idToRemove = new List<int>();
                    DataTable dt =  Sql.TableQuery("SELECT * FROM dbo.vWebNotifications WHERE ID_Menu IS NOT NULL AND ID_User = @ID_User AND IsExpired = 0 AND ID IN (" + String.Join(",", ids) + ")", Q["ID_User"]);
                    List<int> idStillExist = dt.Rows.Cast<DataRow>().Select(x => x["ID"].ToInt32()).ToList<int>();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string ds = Sql.ExecScalar("Select DataSource from dbo.tMenu where ID = @ID", dr["ID_Menu"]).ToString();
                            if (ds.IsNull("").ToString() != "")
                            {
                                int cnt = Sql.ExecScalar("Select Count(ID) from " + replaceValues(ds, Ctx, null, true, drMobile) + " where ID = @ID", dr["rID"]).ToInt32();
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

    }
}