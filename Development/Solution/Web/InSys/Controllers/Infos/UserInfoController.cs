using InSys.Controllers.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using z.Data;
using z.SQL;

namespace InSys.Controllers.Infos
{
    public class UserInfoController : InfoController
    { 
        public virtual async Task<Result> ResetPassword() => await TaskResult(r =>
        {
            String SetPassword;
            SetPassword = Sql.ExecScalar("SELECT dbo.fWebResetPassword(@ID_User)", Q["ID_User"]).ToString() != "" ? Sql.ExecScalar("SELECT dbo.fWebResetPassword(@ID_User)", Q["ID_User"]).ToString() : (GeneratePassword().EncryptA(41).ToString() + "_BJTGLR").ToString();
            r.ResultSet = new { Password = SetPassword, IsFirstLog = 1 };
            return r;
        });

        public virtual async Task<Result> UnlockUser() => await TaskResult(r =>
        {
            r.ResultSet = this.Sql.ExecScalar("EXEC pResetInvalidLogCount @ID_User ", Q["ID_User"]);
            return r;
        });

        [HttpPost] 
        public virtual async Task<Result> ShowPassword() => await TaskResult(r =>
        {
            int uID = Q["ID_User"].ToInt32();
            String cpassword = null;
            var password = this.Sql.ExecScalar("Select Password From dbo.tUser where ID = @ID", uID);
            if (password != null)
            {
                cpassword = password.ToString().EncryptA(41).ToString();
            }
            r.ResultSet = new { Spassword = cpassword };
            return r;
        });

    }
}