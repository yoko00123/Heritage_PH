using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using z.Controller;
using z.Data;
using z.SQL;

namespace InSys.Helpers
{

    public class AuthorizeRequest : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext context)
        { 
            try
            {
                IEnumerable<string> tokenHeaders;
                if (context.Request.Headers.TryGetValues("Api-Key", out tokenHeaders))
                {
                    string tokens = tokenHeaders.First();
                    string key1 = Encoding.UTF8.GetString(Convert.FromBase64String(tokens));
                    var key = Config.Get("EncKey").ToString();
                    var salt = Config.Get("EncSalt").ToString();
                    var uniqueid = Config.Get("EncUID").ToString();
                    var ip = EncryptionHandler.GetUserIP(context).Replace("::1", "127.0.0.1");

                    var val1 = CryptoJS.Decrypt(key1, key, salt);

                    var k = val1.Split(new char[] { ':' });

                    var dUID = k[0];
                    var dIP = k[1];
                    var dTcks = Convert.ToInt64(k[2]);

                    if (dUID != uniqueid)
                        throw new Exception("Invalid Authentication ID");

                    if (ip != dIP)
                        throw new Exception("Invalid client request");

                    //var currentDate = new DateTime(dTcks);
                    var currentDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                    currentDate = currentDate.AddSeconds(dTcks).ToLocalTime();

                    if (currentDate == null)
                        throw new InvalidOperationException("Invalid Request 302 (Date)");


                    var timeExpired = Math.Abs((DateTime.Now - currentDate).TotalMinutes) > 10;

                    if (timeExpired)
                        throw new Exception("Request date has expired");

                    return true;
                }
                else
                    throw new Exception("API-Key doesnt exists in request header");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, ex.Message);
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new System.Net.Http.HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Content = new StringContent("Invalid Request")
            };
        }
    }

    public class AuthorizationToken
    {
        const string ConstantSalt = "I%$s4$*17@zR3";

        public static string GenerateAuthToken(string AuthToken)
        {
            return GenerateCookieFriendlyHash(AuthToken);
        }

        public static bool DoesCsrfTokenMatchAuthToken(string csrfToken, string authToken)
        {
            return csrfToken == GenerateCookieFriendlyHash(authToken);
        }

        private static string GenerateCookieFriendlyHash(string authToken)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(authToken + ConstantSalt));
                var cookieFriendlyHash = HttpServerUtility.UrlTokenEncode(computedHash);
                return cookieFriendlyHash;
            }
        }

        public static bool IsAuthenticated(HttpContextBase Ctx)
        {
            if (Ctx.Session["AuthID"].IsNull("").ToString() == "")
                return false;
            string ID_User = Ctx.Session["AuthID"].ToString();
            if (string.IsNullOrEmpty(ID_User))
                return false;

            string authToken = Convert.ToString(Ctx.Session["AuthID"]) + "_" + Convert.ToString(Ctx.Session["CSRF-TOKEN"]);
            string csrfToken = Ctx.Request.Headers.GetValues("X-CSRF-Token").FirstOrDefault();
            if ((string.IsNullOrEmpty(csrfToken)))
                return false;

            return AuthorizationToken.DoesCsrfTokenMatchAuthToken(csrfToken, authToken);
        }

        public static string Auth(HttpContext Ctx, string AuthID)
        {
            string csrf = Guid.NewGuid().ToString();
            Ctx.Session.Add("CSRF-TOKEN", csrf);
            Ctx.Session.Add("AuthID", AuthID);

            var token = AuthorizationToken.GenerateAuthToken(AuthID + "_" + csrf);
            var csrfCookie = new HttpCookie("XSRF-TOKEN", token);
            csrfCookie.HttpOnly = false;
            Ctx.Response.Cookies.Add(csrfCookie);

            return token;
        }

        public static void Clear(HttpContextBase Ctx)
        {
            Ctx.Session.Clear();
            Ctx.Response.Cookies.Clear();
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeMobile : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext context)
        {
            var token = context.Request.Headers.Contains("Auth-Key");
            if (!token) return false;

            string csrfToken = context.Request.Headers.GetValues("Auth-Key").FirstOrDefault();
            string mob = Config.Get("MobileKey").ToString();

            return mob == csrfToken;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class InvariantCulture : Attribute
    {
        public InvariantCulture() : base()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        }
    }
}