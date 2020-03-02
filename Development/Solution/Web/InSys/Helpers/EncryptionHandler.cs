using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using z.Controller;
using z.Data;

namespace InSys.Helpers
{
    public class EncryptionHandler
    {

        public static string GetUserIP(HttpRequestBase Request)
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
                return ipList.Split(',')[0];

            return Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetUserIP(HttpActionContext actionContext)
        {
            //var myRequest = ((HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).Request;
            //return GetUserIP(myRequest);
            if (actionContext.Request.Properties.ContainsKey("MS_HttpContext"))
            {
                return IPAddress.Parse(((HttpContextBase)actionContext.Request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            }
            if (actionContext.Request.Properties.ContainsKey("MS_OwinContext"))
            {
                return IPAddress.Parse(((OwinContext)actionContext.Request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();
            }
            return null;
        }

        public static string CreateClientToken(HttpRequestBase Request)
        {
            var ip = GetUserIP(Request).Replace("::1", "127.0.0.1");
            var key = Config.Get("EncKey").ToString();
            var salt = Config.Get("EncSalt").ToString();
            var uniqueid = Config.Get("EncUID").ToString();
            var ticks = DateTime.UtcNow.Ticks;

            var enc1 = string.Join(":", new string[] { ip, ticks.ToString() });

            var hashLeft = CryptoJS.Encrypt(enc1, key, salt);
            var hashRight = string.Join(":", new string[] { uniqueid, ticks.ToString(), key.CompressToUTF16(), salt.CompressToUTF16() });

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashRight, hashLeft)));
        }
    }

    public class EncryptJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
        {
            var cValue = value;
            if (cValue != null)
            {
                cValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(CryptoJS.Encrypt(cValue.ToJson(), Config.Get("EncKey").ToString(), Config.Get("EncSalt").ToString())));
            }

            base.WriteToStream(type, cValue, writeStream, effectiveEncoding);
        }
    }

}