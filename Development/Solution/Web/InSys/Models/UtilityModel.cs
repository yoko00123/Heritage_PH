using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using z.Data;
using z.SQL;

namespace InSys.Models
{
    public class UtilityModel
    {
        static int secretKey = 95242;
        static string bases = "ZYXWVUTSRQPONMLKJIHGFEDCBAzyxwvutsrqponmlkjihgfedcba9876543210";
        public static string replaceValues(string strToReplace, HttpContext Ctx = null, DataRow dr = null, Boolean IsMobile = false, DataRow mobileData = null)
        {
            while (strToReplace.Contains("$"))
            {
                string tmpStr = strToReplace.Substring(strToReplace.IndexOf("$"), (strToReplace.Length - strToReplace.IndexOf("$")));
                if (tmpStr.Contains(" ")) { tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(" ")); }
                if (tmpStr.Contains(",")) { tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(",")); }
                if (tmpStr.Contains(")")) { tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(")")); }
                tmpStr = tmpStr.Replace("$", "");
                if (dr != null)
                {
                    strToReplace = Regex.Replace(strToReplace, @"\$\b" + tmpStr + @"\b", dr[tmpStr].IsNull("0").ToString().Replace("True", "1").Replace("False", "0").ToString());
                }
                else
                {
                    strToReplace = Regex.Replace(strToReplace, @"\$\b" + tmpStr + @"\b", "0");
                }
            }
            while (strToReplace.Contains("@"))
            {
                string tmpStr = strToReplace.Substring(strToReplace.IndexOf("@"), (strToReplace.Length - strToReplace.IndexOf("@")));
                if (tmpStr.Contains(" ")) { tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(" ")); }
                if (tmpStr.Contains(",")) { tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(",")); }
                if (tmpStr.Contains(")")) { tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(")")); }
                if (tmpStr.Contains(";")) { tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(";")); }
                if (IsMobile)
                {
                    strToReplace = Regex.Replace(strToReplace, @"@\b" + tmpStr.Replace("@", "") + @"\b", mobileData[tmpStr.Replace("@", "")].IsNull("NULL").ToString());
                }
                else {
                    strToReplace = Regex.Replace(strToReplace, @"@\b" + tmpStr.Replace("@", "") + @"\b", Ctx.Session[tmpStr.Replace("@", "")].IsNull("NULL").ToString());
                }
                
            }
            return strToReplace;
        }
        public static string addStripSlashes(string s)
        {
            return s.Replace("'", "\'");
        }
        public static string StringReverse(string r)
        {
            char[] arr = r.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string toAnyBase(int num, int baseNum = 62)
        {
            num += secretKey;
            string ret = string.Empty;
            int remainder = 0;
            while (num >= baseNum)
            {
                remainder = num % baseNum;
                ret += bases[remainder].ToString();
                num = Convert.ToInt32(Math.Floor(Convert.ToDouble(num) / Convert.ToDouble(baseNum)));
            }

            ret += bases[num].ToString();
            return StringReverse(ret);
        }
        public static int toBase10(string s, int baseNum = 62)
        {
            int tmp = 0;
            int tmpsamp = 0;
            s = StringReverse(s);
            for (int i = 0; i <= s.Length - 1; i++)
            {
                tmpsamp = bases.IndexOf(s[i]);
                tmp += Convert.ToInt32(Math.Pow(baseNum, i) * bases.IndexOf(s[i]));
            }
            return tmp - secretKey;
        }
        public static List<string> getParams(string query)
        {
            List<string> p = new List<string>();
            while (query.Contains("@"))
            {
                string tmpStr = query.Substring(query.IndexOf("@"), (query.Length) - query.IndexOf("@"));
                if (tmpStr.Contains(" "))
                    tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(" "));
                if (tmpStr.Contains(","))
                    tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(","));
                if (tmpStr.Contains(")"))
                    tmpStr = tmpStr.Substring(0, tmpStr.IndexOf(")"));
                p.Add(tmpStr.Replace("@", ""));
                query = Regex.Replace(query, "@\\b" + tmpStr.Replace("@", "") + "\\b", "");
            }
            return p;
        }
        public static string serialize(SqlDataReader reader)
        {
            StringWriter sw = new StringWriter();

            try
            {
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.WriteStartArray();
                    while (reader.Read())
                    {
                        jsonWriter.WriteStartObject();
                        for (int i = 0; i <= reader.FieldCount - 1; i++)
                        {
                            jsonWriter.WritePropertyName(reader.GetName(i));
                            jsonWriter.WriteValue(reader[i]);
                        }
                        jsonWriter.WritePropertyName("$$rID");
                        jsonWriter.WriteValue(toAnyBase(reader["ID"].ToInt32(), 62));
                        jsonWriter.WriteEndObject();
                    }
                    jsonWriter.WriteEndArray();
                    jsonWriter.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return sw.ToString();
        }
        public static object getObjectValue(object @params)
        {
            string valueType = @params.GetType().Name;
            if (valueType == "String")
            {
                return Convert.ToString(@params);
            }
            else if (valueType == "Boolean")
            {
                return Convert.ToBoolean(@params);
            }
            else if (valueType == "Int32")
            {
                return Convert.ToInt32(@params);
            }
            else if (valueType == "Double")
            {
                return Convert.ToDouble(@params);
            }
            else if (valueType == "Char")
            {
                return Convert.ToChar(@params);
            }
            else
            {
                return @params;
            }
        }
    }
}