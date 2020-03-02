using InSys.Controllers.API;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using InSys.Storage;
using z.Data;
using InSys.Office;
using z.Security;
using z.SQL;

namespace InSys.Controllers.Infos
{
    public class EmployeeAttendanceLogFileInfoController : InfoController
    {
        protected Storage.Storage strg;
        protected IStorageContainer cntr;

        [HttpPost, HttpOptions]
        public async Task<Result> ImportLogFile() => await TaskResult(r =>
        {
            strg = new Storage.Storage();
            cntr = strg.Container("Files");
            
            var file = GetUploadedFiles()[0];

            var orgfile = file.Headers.ContentDisposition.FileName.Trim(new char[] { '"' });
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(orgfile);

            using (var fs = File.OpenRead(file.LocalFileName))
                strg.Upload(cntr, filename, fs);

            File.Delete(file.LocalFileName);

            switch (Path.GetExtension(orgfile).ToUpper())
            {
                case ".XLS":
                case ".XLSX":
                    r.ResultSet = TransferExcelData(filename);
                    break;
                case ".LOG":
                    r.ResultSet = TransferLogData(filename);
                    break;
                case ".INSYS":
                    r.ResultSet = TransferInSysData(filename);
                    break;
                case ".DAT":
                    r.ResultSet = TransferDatData(filename);
                    break;
            }
            r.ResultSet = new
            {
                FileName = Path.GetFileName(orgfile),
                OriginalFileName = filename,
                Data = r.ResultSet
            };
            return r;
        });

        public async Task<Result> ValidateLogFileInfo() => await TaskExec("pValidateLogFileImport @ID_EmployeeAttendanceLogFile", Q["ID"]);

        private EmployeeAttendanceCollection TransferExcelData(string FileName)
        {
            var eal = new EmployeeAttendanceCollection();
            using (var ms = new MemoryStream())
            {
                strg.DownloadToStream(cntr, FileName, ms);

                using (var xls = new Excel(ms, Excel.IsFileInNewFormat(FileName)))
                {
                    eal.AddRange(xls.Read(xls.SheetsNames[0]).Rows.Cast<DataRow>().Select(x => new EmployeeAttendanceLogCtx()
                    {
                        AccessNo = x["AccessNo"].ToString(),
                        Source = x["Source"].ToString(),
                        DateTime = $"{ x["Date"].ToDate().ToShortDateString() } { x["Time"].ToDate().ToShortTimeString() }".ToDate(),
                        ID_AttendanceLogType = 1
                    }));

                }
                return eal;
            }
        }

        private EmployeeAttendanceCollection TransferLogData(string FileName)
        { 
            var str = strg.DownloadString(cntr, FileName);

            var eal = new EmployeeAttendanceCollection();
            string s = default(string);
            string[] sa = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            //baka nga encrypted 
            string[] strencryptChar = new string[] { "*", "{", "" };

            if (sa.Length == 1)
                if (strencryptChar.Where(x => sa[0].Contains(x)).Any())
                {
                    var tmp = Path.GetTempFileName();
                    strg.DownloadToFile(cntr, FileName, tmp);
                    if (Encryption.EncryptedA(tmp, Convert.ToInt32('A'), ref s))
                    {
                        Encryption.DecryptLogFile(tmp, s);
                        sa = File.ReadAllLines(tmp);
                    }
                    Thread.Sleep(1000);
                    GC.Collect();
                    File.Delete(tmp);
                }
             
            var AutoDetectAttendanceLogType = GetSetting("AutoDetectAttendanceLogType").ToBool();
            sa.Distinct().Each(x =>
            {
                var a = x.Split(',');
                var g = new EmployeeAttendanceLogCtx()
                {
                    AccessNo = a[0],
                    DateTime = $"{a[1]}, {a[2]}".ToDate(),
                    ID_AttendanceLogType = a[3] == "I" ? 1 : 2
                };

                if (AutoDetectAttendanceLogType)
                {
                    g.ID_AttendanceLogType = 1;
                    if (!eal.Where(y => y.AccessNo == g.AccessNo && y.DateTime == g.DateTime).Any())
                        eal.Add(g);
                }
                else
                    eal.Add(g);

            });
            return eal;
        }

        private EmployeeAttendanceCollection TransferInSysData(string FileName)
        {
            var eal = new EmployeeAttendanceCollection();
            var logs = strg.DownloadString(cntr, FileName).Split(new string[] { "\r\n" }, StringSplitOptions.None); // File.ReadAllLines(FileName);
            var AutoDetectAttendanceLogType = GetSetting("AutoDetectAttendanceLogType").ToBool();

            logs.Distinct().Each(x =>
            {
                var y = x.Trim().Split('\t');

                var g = new EmployeeAttendanceLogCtx()
                {
                    AccessNo = y[1],
                    DateTime = y[2].ToDate(),
                    ID_AttendanceLogType = y[3].ToInt32()
                };

                if (AutoDetectAttendanceLogType)
                {
                    g.ID_AttendanceLogType = 1;
                    if (!eal.Where(z => z.AccessNo == g.AccessNo && z.DateTime == g.DateTime).Any())
                        eal.Add(g);
                }
                else
                    eal.Add(g);
            });
            return eal;
        }

        private EmployeeAttendanceCollection TransferDatData(string FileName)
        {
            var eal = new EmployeeAttendanceCollection();
            var logs = strg.DownloadString(cntr, FileName).Split(new string[] { "\r\n" }, StringSplitOptions.None); //File.ReadAllLines(FileName);
            var AutoDetectAttendanceLogType = GetSetting("AutoDetectAttendanceLogType").ToBool();

            logs.Distinct().Each(x =>
            {
                if (x == "") return;

                var y = x.Trim().Split('\t');

                var g = new EmployeeAttendanceLogCtx()
                {
                    AccessNo = y[0],
                    DateTime = y[1].ToDate(),
                    ID_AttendanceLogType = y[3].ToInt32() == 0 ? 1 : 2
                };

                if (AutoDetectAttendanceLogType)
                {
                    g.ID_AttendanceLogType = 1;
                    if (!eal.Where(z => z.AccessNo == g.AccessNo && z.DateTime == g.DateTime).Any())
                        eal.Add(g);
                }
                else
                    eal.Add(g);
            });
            return eal;
        }

        public class EmployeeAttendanceLogCtx
        {
            public string AccessNo { get; set; }
            public DateTime DateTime { get; set; }
            public int ID_AttendanceLogType { get; set; }

            public string Source { get; set; }
        }

        [HttpPost, HttpGet]
        public async Task<Result> GenerateEmployeeTemplate() => await TaskResult (_ =>
        { 
            string tmpfile = Guid.NewGuid().ToString().Replace("-", "") + ".xls";

            var strg = new Storage.Storage();
            var xlcntr = strg.Container("ExcelTemplates");
            var flCntr = strg.Container("Files");
              
            using(var ms = new MemoryStream())
            {
                strg.DownloadToStream(xlcntr, "Employee Attendance Log File.xls", ms);

                var writer = new ExcelWriter(ms, Excel.IsFileInNewFormat(tmpfile));
                String sheetname = "Access No";
                DataTable dt = this.Sql.ExecQuery("SELECT '''' + e.AccessNo,e.Name FROM vEmployee e where dbo.fEmployeeRights( @ID_Header ,e.ID) = 1 AND e.IsActive = 1", Q["ID_Header"].ToString()).Tables[0];
                writer.AddSheet(sheetname);
                IRow row;
                Object[,] DataArray = new object[dt.Rows.Count, dt.Columns.Count];
                int r = 0;
                int c = 0;
                r = 0;
                row = writer.AddRow(sheetname);
                writer.AddCell(row, 0, "AccessNo", "Header");
                writer.AddCell(row, 1, "Employee Name", "Header");
                row = writer.AddRow(sheetname);
                foreach (DataRow drw in dt.Rows)
                {
                    c = 0;
                    foreach (DataColumn col in dt.Columns)
                    {
                        DataArray[r, c] = drw[c];
                        c++;
                        writer.AddCell(row, c - 1, drw[String.Format("{0}", col.ColumnName.ToString())].ToString(), "Data");
                    }
                    row = writer.AddRow(sheetname);
                    r += 1;
                }
                  
                using(var j = new MemoryStream())
                {
                    writer.SaveToStream(j);
                    strg.Upload(flCntr, tmpfile, j);
                }
            }
             
            _.ResultSet = tmpfile.CompressUriEncoded();

            return _;
        });

        public class EmployeeAttendanceCollection : List<EmployeeAttendanceLogCtx> { }

    }
}