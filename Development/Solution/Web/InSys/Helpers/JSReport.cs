using InSys.Models;
using Newtonsoft.Json;
using NUglify;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using z.Data;
using z.Report.Binary;
using z.Report.Local;
using z.Report.Types;
using z.SQL;

namespace InSys.Helpers
{
    public class JSReport : IDisposable
    {
        public List<SubReportSource> SubReportSource { get; set; } = new List<Helpers.SubReportSource>();
        public string x;
        public IQueryArgs Credentials { private get; set; }
        public DataTable DataSource { private get; set; }
        public DataTable PassParam { private get; set; }
        public string ReportFile { get; set; }
        public ReportParameterCollection ReportParameter { get; set; }

        public bool? IsExcelPasswordProtected { get; set; } = false;

        public string ReportName
        {
            get
            {
                return Path.GetFileName(ReportFile);
            }
        }

        public FilterCollection where;
        public object ID_Menu;

        public string ReportData { private set; get; }
        public string RootPath { private get; set; }
        private string DataFile { get; set; }
        private string OutFile { get; set; }

        public string PdfOutFile
        {
            get
            {
                return Path.GetFileName(OutFile);
            }
            set
            {
                OutFile = Path.Combine(Path.GetTempPath(), value);
            }
        }

        public Pair SessionTable { get; internal set; }
        public string dsource { get; internal set; }

        private string DefaultScript { get; set; }
        private string DefaultStyle { get; set; }
        public object JsonValue { get; private set; }
        public object Windows { get; private set; }

        public JSReport()
        {
            //log4net.ThreadContext.Properties["Class"] = "JSReport";
        }

        public void Init(Func<string, string, DataTable> ValidDataSource, Func<string, FilterCollection, string> PassFilter, Func<FilterCollection, string> BuildFilter)
        {
            Serilog.Log.Information($"Init: { ID_Menu }");
            var ParameterTable = new Pair();
            var SubDataSource = Credentials.ExecQuery("SELECT Name, DataSource FROM dbo.tMenuSubDataSource WHERE ID_Menu = @ID_Menu", ID_Menu).Tables[0].Rows;

            SubDataSource.Cast<DataRow>().Each(x =>
            {
                var s = x["DataSource"].ToString();
                s = s.PassParameter(SessionTable).PassParameter(ParameterTable);
                s = PassFilter(s, where);
                SubReportSource.Add(new SubReportSource
                {
                    Name = x["Name"].ToString(),
                    DataSource = Credentials.ExecQuery($"Select * from {s}").Tables[0]
                });
            });

            DataSource = ValidDataSource(PassFilter(dsource.ToString(), where), BuildFilter(where));
            ParameterFieldDB();
        }

        public void ParameterFieldDB()
        {
            var ParameterField = Credentials.ExecQuery("SELECT Name FROM tReportParameterFields WHERE ID_Menu = @ID_Menu", ID_Menu.ToInt32()).Tables[0];
            PassParam = new DataTable();
            var gg = new string[] { "PreparedBy", "CheckedBy", "ApprovedBy" };

            //if (ParameterField.Rows.Count >= 1)
            //{
            foreach (DataRow dr in ParameterField.Rows)
                PassParam.Columns.Add(dr[0].ToString().Replace(" ", string.Empty), typeof(string));
            foreach (var g in gg)
                if (!PassParam.Columns.Contains(g))
                    PassParam.Columns.Add(g, typeof(string));


            var row = PassParam.NewRow();
            foreach (DataColumn dc in PassParam.Columns)
            {
                if (ParameterField.Rows.Count >= 1)
                {
                    var rf = ReportParameter.Where(y => y.Label.ToString().Replace(" ", string.Empty) == dc.ColumnName.ToString()).FirstOrDefault();
                    row[rf.Name.ToString()] = rf.Model.IsNull("");
                }
                if (row[dc.ColumnName] == null)
                    row[dc.ColumnName] = string.Empty;
            }

            PassParam.Rows.Add(row);
            // }

        }

        public void Load()
        {
            DataTableToJson();
            ExecuteFile();
        }

        public string LoadExcel()
        {
            DataTableToJson();
            return ExecuteExcelFile();
        }

        public void Render()
        {
            using (var ms = File.OpenRead(OutFile))
            {
                byte[] buffer = ms.ToByteArray();
                ms.Close();
                ReportData = String.Format("data:application/pdf;base64,{0}", Convert.ToBase64String(buffer));
            }

            if (File.Exists(OutFile))
                File.Delete(OutFile);
        }

        protected void ExecuteFile()
        {
            try
            {
                LoadAssets();


                Serilog.Log.Information($"Execute File: { ReportFile }");

                OutFile = Path.Combine(Path.GetTempPath(), $"JSR-{Guid.NewGuid().ToString()}.pdf");

                Serilog.Log.Information($"OutFile: { OutFile }");

                var rpt = File.ReadAllText(ReportFile).ToObject<Template>();


                rpt.Helpers = Uglify.Js(this.DefaultScript).Code;
                rpt.Content = Regex.Replace(rpt.Content, @"(\<link)(.*\s.*)(\#asset)(.*\s.*)(\/\>)", $"<style>{ Uglify.Css(this.DefaultStyle).Code }</style>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                rpt.Content = Uglify.Html(rpt.Content).Code;

                var rg = new RenderRequest();

                rg.Template = rpt;

                if (DataSource.Rows.Count == 0)
                    throw new Exception("Report doesn't contain any data");

                rg.Data = DataSource.Rows.Count == 0 ? "{}" : DataTableToJson();

                //RenderReport(rg, rpt);
                Serilog.Log.Information("Rendering");
                var report = JSReportService.RenderAsync(rg).Result;

                using (var fs = File.Create(OutFile))
                {
                    report.Content.CopyTo(fs);
                }

                Serilog.Log.Information("Success");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, ex.Message);
                throw new Exception("Cannot render report.", ex);
            }
        }

        protected string ExecuteExcelFile()
        {
            try
            {
                LoadAssets();

                Serilog.Log.Information($"Execute File: { ReportFile }");

                var todate = DateTime.Now.ToString("MMddyyyHHmmssmmm");
                //OutFile = Path.Combine(Path.GetTempPath(),  x + $" JSR -{Guid.NewGuid().ToString()}.xlsx");
                OutFile = Path.Combine(Path.GetTempPath(), x + " JSR-" + todate + ".xlsx");

                Serilog.Log.Information($"OutFile: { OutFile }");

                var rpt = File.ReadAllText(ReportFile).ToObject<Template>();

                rpt.Recipe = Recipe.HtmlToXlsx;
                rpt.Helpers = Uglify.Js(this.DefaultScript).Code;
                rpt.Content = Regex.Replace(rpt.Content, @"(\<link)(.*\s.*)(\#asset)(.*\s.*)(\/\>)", $"<style>{ Uglify.Css(this.DefaultStyle).Code }</style>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                rpt.Content = Uglify.Html(rpt.Content).Code;

                var rg = new RenderRequest();

                rg.Template = rpt;

                if (DataSource.Rows.Count == 0)
                    throw new Exception("Report doesn't contain any data");

                rg.Data = DataSource.Rows.Count == 0 ? "{}" : DataTableToJson();

                Serilog.Log.Information("Rendering");


                var report = JSReportService.RenderAsync(rg).Result;


                using (var fs = File.Create(OutFile))
                {
                    report.Content.CopyTo(fs);
                }

                Serilog.Log.Information("Success");

                try
                {
                    using (var strg = new Storage.Storage())
                    {
                        if (IsExcelPasswordProtected == true)
                        {
                            var nPass = Credentials.ExecScalar("SELECT Token FROM dbo.tExcelReportPasswordToken WHERE CONVERT(DATE, Date, 101) = CONVERT(DATE, dbo.fGetDate(), 101) AND ID_Company = @ID_Company", SessionTable["ID_Company", DBNull.Value]);

                            if (nPass != null)
                            {
                                using (var xls = new Office.Excel(OutFile, false))
                                {
                                    foreach (var i in xls.SheetsNames)
                                    {
                                        xls.GetSheet(i).ProtectSheet(nPass.ToString());
                                    }

                                    xls.Save();
                                }
                            }
                        }

                        using (var stream = File.OpenRead(OutFile))
                        {
                            stream.Seek(0, SeekOrigin.Begin);
                            var container = strg.Container("Files");
                            strg.Upload(container, Path.GetFileName(OutFile), stream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Excel file cannot render at this time.", ex);
                }

                return Path.GetFileName(OutFile);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, ex.Message);
                throw ex;
            }
        }

        protected string DataTableToJson()
        {
            Serilog.Log.Information("Deserialized table");
            var jj = new Dictionary<string, object>();
            jj.Add("DataSource", DataSource.Rows.JsonModel());

            foreach (var sub in SubReportSource)
                jj.Add(sub.Name, sub.DataSource.Rows.JsonModel());

            if (PassParam.Rows.Count >= 1)
                jj.Add("ParameterFields", PassParam.Rows.JsonModel());

            return jj.ToJson();
        }

        protected void RenderReport(RenderRequest rg, Template rpt)
        {
            var tmpData = Path.Combine(Path.GetTempPath(), $"D-{ Guid.NewGuid().ToString() }.json");
            var rptFile = Path.Combine(Path.GetTempPath(), $"R-{Guid.NewGuid().ToString()}.json");

            try
            {

                if (!File.Exists(ReportFile))
                    throw new Exception($"Template file does not exists { rpt.Name }");

                File.WriteAllText(tmpData, rg.Data.ToString());

                rpt.Shortid = null;
                rpt._id = null;
                var tmp = JsonConvert.SerializeObject(rpt, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                File.WriteAllText(rptFile, tmp);

                var gg = new Pair();
                gg.Add("--template", rptFile);
                //gg.Add("--template.recipe", CamelToHtmlCase(rpt.Recipe.ToString()));
                gg.Add("--data", tmpData);
                gg.Add("--out", OutFile);

                var gs = gg.Select(x => $"{ x.Key }={ x.Value }").Join(" ");

                Serilog.Log.Information($"Render Cmd: render { gs }");
                if (!Debugger.IsAttached)
                    ExecuteJSReportCommand("kill");
                ExecuteJSReportCommand("render " + gs);

                Serilog.Log.Information("Plan B. Finish");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!Debugger.IsAttached && File.Exists(tmpData)) File.Delete(tmpData);
                if (!Debugger.IsAttached && File.Exists(rptFile)) File.Delete(rptFile);
                GC.Collect();
            }
        }

        public static void Initiate()
        {
            JSReportService = new LocalReporting()
               .UseBinary(JsReportBinary.GetBinary())
                .Configure(cfg =>
                {
                    cfg.Tasks.AllowedModules = "*";
                    cfg.AllowLocalFilesAccess().FileSystemStore().BaseUrlAsWorkingDirectory();
                    return cfg;
                })
               .KillRunningJsReportProcesses()
               .AsUtility()
               .Create();
        }

        public string CamelToHtmlCase(string input)
        {
            return Regex.Replace(input, "([A-Z])", m => $"-{ m.Value.ToLower() }", RegexOptions.Compiled).Trim(new char[] { '-' });
        }

        protected void ExecuteJSReportCommand(string Command)
        {
            var p = new Process();
            p.StartInfo.FileName = "insysreport.exe";
            p.StartInfo.WorkingDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~/bin");  // Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            p.StartInfo.Arguments = Command;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
        }

        public static ILocalUtilityReportingService JSReportService;


        protected void LoadAssets()
        {
            
            Serilog.Log.Information($"Preparing Assets: { ReportFile }");

            string text = System.IO.File.ReadAllText(ReportFile);
            var jsonnew = JsonConvert.DeserializeObject<NewJson>(text);

            x = jsonnew.name.ToString();

            // var file = JsonConvert.DeserializeObject("name");
            // return json_read_object


            using (var strg = new Storage.Storage())
            {
                var cntr = strg.Container("Assets");
                this.DefaultScript = strg.DownloadString(cntr, "DefaultHelper.js");
                this.DefaultStyle = strg.DownloadString(cntr, "DefaultStyle.css");
            }

        }

        public void Dispose()
        {
            if (File.Exists(ReportFile))
                File.Delete(ReportFile);

            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }

    public class SubReportSource
    {
        public string Name { get; set; }
        public DataTable DataSource { get; set; }
    }

    public class NewJson
    {
        public string name { get; set; }

    }
}