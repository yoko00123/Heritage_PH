using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using z.Data;

namespace InSys
{
    public static class Extensions
    {

        public static void AddCookie(this HttpResponseBase Response, string Name, string Value, DateTime? expiry, bool? secure)
        {
            HttpCookie appCookie = new HttpCookie(Name);
            appCookie.Value = Value;
            appCookie.Expires = expiry ?? DateTime.Now.AddDays(1);
            appCookie.Secure = secure ?? false;
            //appCookie.Path = "/";
            Response.Cookies.Add(appCookie);
            Response.SetCookie(appCookie);
             
            Serilog.Log.Information("Cookie " + Name, appCookie);
        }

        [System.Diagnostics.DebuggerHidden]
        public static T TryParse<T>(this string strObj) where T : new()
        {
            try
            {
                return strObj.ToObject<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public static string StripSlashes(this object str)
        {
            return str.ToString().Trim(new char[] { '\'', '\r', '\n', '\t' });
        }

        public static Pair ToPair(this IEnumerable<KeyValuePair<string, object>> keyObjects)
        {
            var p = new Pair();
            foreach (var g in keyObjects)
                p.Add(g.Key, g.Value);
            return p;
        }

        public static string CheckDir(this string dr)
        {
            var g = dr;
            if (Path.HasExtension(g))
            {
                g = Path.GetDirectoryName(dr);
            }
            if (!Directory.Exists(g)) Directory.CreateDirectory(g);
            return dr;
        }

        public static string DeleteFileIfExists(this string file)
        {
            if (File.Exists(file))
                File.Delete(file);

            return file;
        }

        public static string GetHeader(this HttpRequestMessage request, string key)
        {
            IEnumerable<string> keys = null;
            if (!request.Headers.TryGetValues(key, out keys))
                return null;

            return keys.First();
        }

        /// <summary>
        /// Retrieves an individual cookie from the cookies collection
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookie(this HttpRequestMessage request, string cookieName)
        {
            CookieHeaderValue cookie = request.Headers.GetCookies(cookieName).FirstOrDefault();
            if (cookie != null)
                return cookie[cookieName].Value;

            return null;
        }

    }
}