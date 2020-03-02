
using Serilog;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using z.Data;
using z.IO;
using z.SQL;

namespace z.Controller
{
    /// <summary>
    /// LJ20161216
    /// </summary>
    public abstract class CoreController : ApiController
    {

        /// <summary>
        /// Request Parameters
        /// </summary>
        public Pair Q { get; set; }
        /// <summary>
        /// SQL Connection Parameters
        /// </summary>
        protected virtual IQueryArgs Sql { get; set; }

        /// <summary>
        /// Error Handler
        /// </summary>
        protected Exception Error { get; set; }

        /// <summary>
        /// Http Context
        /// </summary>
        public HttpContext Ctx { get; private set; }

        /// <summary>
        /// Must Use for Initializing Object
        /// </summary>
        protected abstract void Initializer();

        protected ILogger logger { get; set; }

        /// <summary>
        /// Class
        /// </summary>
        protected CoreController()
        {
            try
            {
                Sql = new Query.QueryArgs(Config.Get("SQLConnection").ToString());

                Initializer();
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }

        /// <summary>
        /// Execute Async Override
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext,
           CancellationToken cancellationToken)
        {
            try
            {
                Q = QueryString(controllerContext);
                Ctx = HttpContext.Current;
                RequestContext = controllerContext.RequestContext;

                logger = Log.Logger
                  .ForContext("Token", Ctx.Session["Token"]?.ToString() ?? "(Unspecified)")
                  .ForContext("Controller", controllerContext.RouteData.Values["controller"].ToString())
                  .ForContext("Action", controllerContext.RouteData.Values["action"].ToString())
                  .ForContext("ID_User", Ctx.Session["ID_User"] ?? DBNull.Value);

                return base.ExecuteAsync(controllerContext, cancellationToken);  //;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, ex.Message);
                //AppInsights.LogEx(ex);
                //throw new Exception("Method Not Found, Add Attribute [httpPost] or [HttpGet] to your Method", ex);
                if(ex.Message == "Padding is invalid and cannot be removed.")
                {
                    return null;
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                // Q?.Dispose();
                GC.Collect();
            }
        }
        
        #region Protected

        /// <summary>
        /// Request Parameter Wrapper
        /// </summary>
        /// <param name="cntx"></param>
        /// <returns></returns>
        protected virtual Pair QueryString(HttpControllerContext cntx)
        {
            var h = new Pair(StringComparer.OrdinalIgnoreCase);

            try
            {

                // var token = EncryptionManager.ValidateRequest(cntx, HttpContext.Current);

                if (cntx.Request.Content.IsMimeMultipartContent() || cntx.Request.Content.Headers.ContentLength > 1e+8)
                {
                    return h;
                }

                string g = default(string);

                if (cntx.Request.Method == HttpMethod.Post)
                    using (var ms = new MemoryStream())
                    {
                        cntx.Request.Content.CopyToAsync(ms).Wait();

                        ms.Seek(0, SeekOrigin.Begin);

                        using (var sr = new StreamReader(ms))
                        {
                            g = sr.ReadToEnd();
                        }
                    }
                else if (cntx.Request.Method == HttpMethod.Get)
                {
                    g = cntx.Request.RequestUri.Query;
                }

                if (g == "") return h;
                string[] exc = { "_", "callback" };

                if (cntx.Request.Method == HttpMethod.Get)
                {
                    foreach (var j in g.Split('&').Select(i => i.Split('=')))
                    {
                        j[0] = j[0].TrimStart('?');
                        if (exc.Contains(j[0]) || j.Length == 1) continue;
                        h.Add(j[0], HttpUtility.UrlDecode(j[1], Encoding.UTF8));
                    }
                }
                else
                {
                    g = CryptoJS.Decrypt(g, Config.Get("EncKey").ToString(), Config.Get("EncSalt").ToString());
                    g.ToObject<Pair>().CopyTo(h);
                }

                return h;
            }
            catch (OutOfMemoryException ex)
            {
                logger?.Error(ex, ex.Message);
                //AppInsights.LogEx(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                logger?.Error(ex, ex.Message);
                //AppInsights.LogEx(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Base Task
        /// </summary>
        /// <param name="action"></param>
        /// <param name="Finally"></param>
        /// <returns></returns>
        protected virtual async Task<Result> TaskResult(Func<Result, Result> action, Action Finally = null)
        {
            StackFrame callingFrame = new StackTrace().GetFrames()[1];
            MethodInfo method = callingFrame.GetMethod() as MethodInfo;

            return await Task.Run(() =>
            {
                var r = new Result();

                r.MethodName = method.Name;


                if (Error != null)
                {
                    r.Status = 1;
                    r.ErrorMsg = Error.Message;
                }
                else
                    try
                    {
                        r.Status = 0;
                        r = action(r);

                        //var token = Ctx.Session["Token"].ToString().CompressFromUriEncoded().ToObject<EncToken>();

                        //var gg = EncryptionManager.EncryptData(token, token.Guid, r.ResultSet.ToJson()); //Ctx.Request.Cookies["APID"].Value

                        r.ResultSet = Convert.ToBase64String(Encoding.UTF8.GetBytes(CryptoJS.Encrypt(r.ResultSet.ToJson(), Config.Get("EncKey").ToString(), Config.Get("EncSalt").ToString())));

                        Ctx.Response.AddHeader("X-Frame-Options", "deny");

                        //  r.ResultSet = gg.Data; //.CompressUriEncoded(); //for justice
                    }
                    catch (Exception ex)
                    {
                        r.Status = 1;
                        r.ErrorMsg = ex.Message;
                        logger?.Error(ex, ex.Message);
                        //AppInsights.LogEx(ex);
                        // var msg = Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                        //r.ErrorMsg = msg != "" ? msg : ex.Message;
                        // throw ex; //angular intercept this error so its okay to throw
                        // iis wont show the true error, so we might need to intercept it 
                    }
                    finally
                    {
                        Finally?.Invoke();
                    }
                return r;
            });
        }

        /// <summary>
        /// Row Collection Wrapper
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected virtual async Task<Result> TaskResult(Func<DataRowCollection> action) =>
            await TaskResult(r =>
            {
                r.ResultSet = action().JsonModel();
                return r;
            });

        /// <summary>
        /// SQL
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="values"></param>
        /// <returns>RowCollection</returns>
        protected virtual async Task<DataRowCollection> Query(string commandText, params object[] values)
        {
            return await Task.Run(() => Sql.ExecQuery(commandText, values).Tables[0].Rows);
        }

        /// <summary>
        /// Host
        /// </summary>
        /// <returns>User Host Address</returns>
        protected string GetUserIp()
        {
            return Ctx.Request.UserHostAddress;
            //Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        ///     Returns None
        /// </summary>
        /// <param name="commanText"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        protected virtual async Task<Result> TaskExec(string commanText, params object[] values)
        {
            return await TaskResult(r =>
            {
                Sql.ExecNonQuery(commanText, values);
                return r;
            });
        }

        /// <summary>
        ///     Returns RowCollection
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        protected virtual async Task<Result> TaskQuery(string commandText, params object[] values)
            => await TaskResult(r =>
            {
                r.ResultSet = Query(commandText, values).Result.JsonModel();
                return r;
            });

        /// <summary>
        ///     Returns Object
        /// </summary>
        /// <param name="CommandText"></param>
        /// <param name="Values"></param>
        /// <returns></returns>
        protected virtual async Task<Result> TaskScalar(string CommandText, params object[] Values)
            => await TaskResult(r =>
            {
                r.ResultSet = Sql.ExecScalar(CommandText, Values);
                return r;
            });

        /// <summary>
        /// SQL
        /// </summary>
        /// <param name="CommandText"></param>
        /// <param name="Values"></param>
        /// <returns>Single Row or First Row</returns>
        protected virtual async Task<Result> TaskRow(string CommandText, params object[] Values)
            => await TaskResult(r =>
            {
                r.ResultSet = Query(CommandText, Values).Result[0].JsonModel();
                return r;
            });

        /// <summary>
        /// Handling Any Object
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected virtual async Task<Result> TaskResultSet(Func<object> action) => await TaskResult(r =>
        {
            r.ResultSet = action();
            return r;
        });

        /// <summary>
        /// SQL
        /// </summary>
        /// <param name="CommandText"></param>
        /// <param name="values"></param>
        /// <returns>All Table Rows</returns>
        protected virtual async Task<Result> TaskDataSet(string CommandText, params object[] values)
            => await TaskResult(r =>
            {
                r.ResultSet =
                    Sql.ExecQuery(CommandText, values)
                        .Tables.Cast<DataTable>()
                        .Select(x => x.Rows.JsonModel())
                        .ToArray();
                return r;
            });

        /// <summary>
        /// SQL
        /// </summary>
        /// <returns>Current Version</returns>
        protected int GetSQLVersion()
        {
            return
                Sql.ExecScalar(
                    "SELECT SUBSTRING(CONVERT(VARCHAR(11), SERVERPROPERTY('productversion')), 0, CHARINDEX('.', CONVERT(VARCHAR(11), SERVERPROPERTY('productversion'))))")
                    .ToInt32();
        }

        /// <summary>
        /// Download Wrapper
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected async Task<HttpResponseMessage> TaskDownloadFile(string FileName, Func<string> action)
        {
            return await Task.Run(() =>
            {
                var tmpfile = action();
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(new FileStream(tmpfile, FileMode.Open))
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(tmpfile.GetContentType(".zip"));
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"{FileName}{Path.GetExtension(tmpfile)}"
                };
                return result;
            });
        }

        /// <summary>
        /// Action
        /// </summary>
        /// <param name="action"></param>
        /// <returns>None</returns>
        protected virtual async Task<Result> TaskVoid(Action action) => await TaskResult(r =>
        {
            action();
            return r;
        });

        /// <summary>
        /// Session
        /// </summary>
        /// <returns>Get Session in Current Context</returns>
        protected Pair GetSession()
        {
            var p = new Pair(StringComparer.OrdinalIgnoreCase);
            foreach (string k in Ctx.Session.Keys)
                p.Add(k, Ctx.Session[k]); //k == "ID" ? "ID_Session" :
            return p;
        }

        #endregion

    }
}
