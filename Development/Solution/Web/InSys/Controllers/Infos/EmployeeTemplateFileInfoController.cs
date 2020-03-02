using InSys.Controllers.API;
using InSys.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using InSys.Classes;
using z.Data;
using InSys.Office;
using z.SQL;
using Newtonsoft.Json.Linq;

namespace InSys.Controllers.Infos
{
    public class EmployeeTemplateFileInfoController : InfoController
    {
        [HttpPost]
        public async Task<Result> GenerateEmployeeTemplate() => await TaskResult(r =>
        {
            string tmpfile = Guid.NewGuid().ToString().Replace("-", "") + ".xls";

            var strg = new Storage.Storage();
            var xlcntr = strg.Container("ExcelTemplates");
            var flCntr = strg.Container("Files");

            using (var ms = new MemoryStream())
            {
                strg.DownloadToStream(xlcntr, "Employee Template File.xls", ms);

                var writer = new ExcelWriter(ms, Excel.IsFileInNewFormat(tmpfile));

                using (var msf = new MemoryStream())
                {
                    writer.SaveToStream(msf);
                    strg.Upload(flCntr, tmpfile, msf);
                    r.ResultSet = tmpfile.CompressUriEncoded();
                }
            }
            return r;
        });

        [HttpPost]
        public async Task<Result> ImportEmployeeTemplate() => await TaskResult(r =>
        {
            var streamProvider = new MultipartFormDataStreamProvider(Path.GetTempPath());
            var fg = Request.Content.ReadAsMultipartAsync(streamProvider).Result;
            List<string> selectedSheets = fg.FormData[0].ToString().Split(',').ToList();
            var mFile = fg.FileData[0];
            var strg = new Storage.Storage();
            var flCntr = strg.Container("Files");

            var orgfile = mFile.Headers.ContentDisposition.FileName.Trim(new char[] { '"' });
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(orgfile);

            using (var ms = File.OpenRead(mFile.LocalFileName))
            {
                strg.Upload(flCntr, filename, ms);
            }

            File.Delete(mFile.LocalFileName);

            using (var ms = new MemoryStream())
            {
                strg.DownloadToStream(flCntr, filename, ms);
                List<JObject> sheetData = new List<JObject>();
                var etf = new ETF(selectedSheets, ms, filename);
                etf.ValidateExcel();

                string fn = Guid.NewGuid().ToString() + Path.GetExtension(filename).ToLower();

                if (etf.hasError)
                {
                    string fnPath = Path.Combine(Path.GetTempPath(), fn);

                    etf.xls.mFilename = fnPath;
                    etf.xls.Save();

                    using (var xlsMs = File.OpenRead(fnPath))
                    {
                        strg.Upload(flCntr, fn, xlsMs);
                    }

                    File.Delete(fnPath);

                    r.ResultSet = new
                    {
                        Code = filename,
                        Name = orgfile,
                        Data = sheetData.ToArray(),
                        HasError = etf.hasError,
                        HasErrorFile = fn,
                        FileName = orgfile
                    };
                }
                else
                {
                    r.ResultSet = new
                    {
                        Code = filename,
                        Name = orgfile,
                        Data = etf.sheetData,
                        HasError = false
                    };
                }
            }
            return r;
        });


        [HttpPost]

        public async Task<Result> DownloadExcelWithError() => await TaskResult(r =>
        {
            string tmpfile = Q["FileName"].ToString();
            var strg = new Storage.Storage();
            var flCntr = strg.Container("Files");

            using (var ms = new MemoryStream())
            {
                strg.DownloadToStream(flCntr, tmpfile, ms);
                using (var msf = new MemoryStream())
                {
                    r.ResultSet = tmpfile.CompressUriEncoded();
                }
            }
            return r;
        });

        public async Task<Result> ApplyFile() => await TaskResult(r =>
        {
            int ID = Q["ID"].ToInt32();
            try
            {
                r.ResultSet = this.Sql.ExecQuery("EXEC p_EmployeeTemplateFile @ID, @ID_User", ID, Q["ID_User"]);
                r.Status = 8;
            }
            catch (Exception ex)
            {
                r.Status = 9;
                string msg = this.Sql.ExecScalar("SELECT dbo.fGetMessage(@Msg)", ex.Message).ToString();
                throw new Exception(msg != "" ? msg : ex.Message, ex);
            }
            return r;
        });
    }
}