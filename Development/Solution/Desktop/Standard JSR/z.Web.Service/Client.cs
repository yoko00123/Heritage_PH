using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace z.Web.Service
{
    /// <summary>
    /// Web Service Client Wrapper
    /// Version 1.0, LJ Gomez 20140322
    /// </summary>
    public class Client : IDisposable
    {
        public string Server { private get; set; }
        private HttpWebRequest request;

        /// <summary>
        /// Root Address
        /// </summary>
        /// <param name="mServer"></param>
        public Client(string mServer)
        {
            this.Server = mServer;
        }

        /// <summary>
        /// Throws error when base address not found
        /// </summary>
        public void Connect()
        {
            request = (HttpWebRequest)HttpWebRequest.Create(this.ValidateUrl(this.Server));
            request.Headers.Clear();
            request.AllowAutoRedirect = false;
            request.Method = "HEAD";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);
            }
            catch (WebException wex)
            {
                if (!wex.Message.ToLower().Contains("403")) throw wex; //found but for security its forbidden
            }
        }

        public string PostData(string uri, byte[] fileToSend)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;

                request = (HttpWebRequest)HttpWebRequest.Create(this.Combine(this.Server, uri));
                request.Headers.Clear();
                request.Method = WebRequestMethods.Http.Post; //"POST";
                request.ProtocolVersion = HttpVersion.Version10;
                request.ContentType = "application/json";
                request.Headers.Add("Accept-Language", "en-us\r\n");
                request.Headers.Add("UA-CPU", "x86 \r\n");
                request.Headers.Add("Cache-Control", "no-cache\r\n");
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                request.ContentLength = fileToSend.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    // Send the file as body request.
                    requestStream.Write(fileToSend, 0, fileToSend.Length);
                    requestStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);

                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string responsefromserver = reader.ReadToEnd();

                reader.Close();
                stream.Close();

                return responsefromserver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetData(string uri)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                request = (HttpWebRequest)WebRequest.Create(this.Combine(this.Server, uri));
                request.Headers.Clear();
                request.Method = WebRequestMethods.Http.Get;
                request.ProtocolVersion = HttpVersion.Version10;
                request.ContentType = "application/json";
                request.Headers.Add("Accept-Language", "en-us\r\n");
                request.Headers.Add("UA-CPU", "x86 \r\n");
                request.Headers.Add("Cache-Control", "no-cache\r\n");
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);

                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string responsefromserver = reader.ReadToEnd();

                reader.Close();
                stream.Close();

                return responsefromserver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="destination"></param>
        /// <param name="OnProcessData">length, nread, percent, currentSpeed</param>
        public void GetData(string uri, string destination, Action<MemoryStream> Done, Action<Arguments.ProcessDataArgs> OnProcessData = null)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                request = (HttpWebRequest)WebRequest.Create(this.Combine(this.Server, uri));
                request.Headers.Clear();
                request.Method = WebRequestMethods.Http.Get;
                request.ProtocolVersion = HttpVersion.Version10;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Accept-Language", "en-us\r\n");
                request.Headers.Add("UA-CPU", "x86 \r\n");
                request.Headers.Add("Cache-Control", "no-cache\r\n");
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);

                long length = response.ContentLength;
                var speedtimer = new Stopwatch();
                double currentSpeed = -1;
                int readings = 0;
                int nread = 0;
                bool IsProcessing = true;

                //FileStream fs = new FileStream(destination, FileMode.Create);
                MemoryStream ms = new MemoryStream();

                while (IsProcessing)
                {

                    speedtimer.Start();

                    byte[] readbytes = new byte[4096];
                    int bytesread = response.GetResponseStream().Read(readbytes, 0, 4096);
                    nread += bytesread;
                    short percent = Convert.ToInt16( (nread * 100) / length);

                    if (OnProcessData != null) OnProcessData(new Arguments.ProcessDataArgs() { length = length, position = nread, percent = Math.Abs( percent), speed = currentSpeed });

                    if (bytesread == 0) IsProcessing = false;

                    ms.Write(readbytes, 0, bytesread);
                    //fs.Write(readbytes, 0, bytesread);

                    speedtimer.Stop();

                    readings++;
                    if (readings >= 5)
                    {
                        double lk = (speedtimer.ElapsedMilliseconds / 1000);
                        currentSpeed = (lk == 0) ? 20480 : 20480 / lk;
                        speedtimer.Reset();
                        readings = 0;
                    }
                }

                response.GetResponseStream().Close();
                response.Close();

                ms.Seek(0, SeekOrigin.Begin);
                Done(ms);

                ms.Close();
                ms.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Stream GetDataStream(string uri)
        {
            try
            {
                WebRequest request = WebRequest.Create(this.Combine(this.Server, uri));
                // Get response
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                return response.GetResponseStream();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        public void WriteFile(string filepath, byte[] buffer)
        {

            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    fileStream.WriteByte(buffer[i]);
                }
                fileStream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < fileStream.Length; i++)
                {
                    if (buffer[i] != fileStream.ReadByte())
                    {
                        throw new Exception("Error Writing File");
                    }
                }
                //success
            }
        }

        /// <summary>
        /// ResultName Must be the root JSON Container.
        /// </summary>
        /// <param name="ResponseData"></param>
        /// <param name="ResultName"></param>
        /// <returns></returns>
        public Result GetResult(string ResponseData, string ResultName)
        {
            try
            {
                dynamic fleobj = JsonConvert.DeserializeObject(ResponseData);

                Result rs = new Result();
                rs.Status = Convert.ToInt32(fleobj[ResultName].Status);
                rs.ErrorMsg = Convert.ToString(fleobj[ResultName].ErrorMsg);

                if (rs.Status == 1) throw new Exception(rs.ErrorMsg);

                rs.ResultSet = fleobj[ResultName].ResultSet;

                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Combine(params string[] uri)
        {
            List<string> ss = new List<string>();
            foreach (string s in uri)
            {
                if (s.Contains("/"))
                {
                    string fs = s;
                    if (fs.Substring(0, 1) == "/")
                    {
                        fs = fs.Remove(0, 1);
                    }

                    if (fs.Substring(fs.Length - 1, 1) == "/")
                    {
                        fs = fs.Remove(fs.Length - 1);
                    }
                    ss.Add(fs);
                }
                else
                {
                    ss.Add(s);
                }
            }

            return string.Join("/", ss.ToArray());
        }

        public byte[] ConvertFile(string FileName)
        {
            if (!File.Exists(FileName)) throw new Exception("File could not located.");
            return File.ReadAllBytes(FileName);
        }

        /// <summary>
        /// Get Data Result Set
        /// </summary>
        /// <typeparam name="T">
        ///     T = Represents any object wich your resultset need to produce
        /// </typeparam>
        /// <param name="rs"></param>
        /// <returns></returns>
        public T GetDataSet<T>(Result rs)
        {
            return JsonConvert.DeserializeObject<T>(rs.ResultSet);
        }

        public void Write(Stream from, Stream to)
        {
            for (int a = from.ReadByte(); a != -1; a = from.ReadByte())
                to.WriteByte((byte)a);

            from.Dispose();
        }

        public byte[] GetJSONBytes(string Json)
        {
            return System.Text.ASCIIEncoding.Default.GetBytes(Json);
        }

        public byte[] GetJSONBytes(Dictionary<string, object> Json)
        {
            string j = Newtonsoft.Json.JsonConvert.SerializeObject(Json);
            return System.Text.ASCIIEncoding.Default.GetBytes(j);
        }

        public string ValidateUrl(string url)
        {
            if (url == "") return url;
            if (url.Substring(url.Length - 1, 1) == "/")
            {
                return url;
            }
            else
            {
                return url + "/";
            }
        }

        public void Dispose()
        {
            this.request = null;
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}