using InSys.Controllers.API;
using InSys.Office;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using z.Data;
using z.SQL;

namespace InSys.Controllers.Infos
{
    public class TKProcessingInfoController : InfoController
    {
        const string HeaderStyle = "Header";
        const string NUllValue = "(Unspecified)";
        const string NormalStyle = "NormalStyle";
        const string SheetTitle = "SheetTitle";

        const string GeneralSheet = "General";


        public async Task<Result> DailyTimeRecord() => await TaskResult(r =>
        {
            var ID = Q["ID"].ToInt32();
            var ID_Employee = Q["ID_Employee", DBNull.Value];
            var ID_Session = Q["ID_Session"].ToInt32();

            var Session = Sql.TableQuery("SELECT * FROM dbo.vSession s WHERE ID = @ID", ID_Session).Rows[0];
            var EDSV = Sql.TableQuery("SELECT * FROM dbo.vEmployeeDailyScheduleView edsv WHERE edsv.ID = @ID", ID).Rows[0];
            //List Employee

            var EDSV_Emp = Sql.ExecQuery("pGenerateEmployeeDailyScheduleForXls @ID_EMployeeDailyScheduleView, @ID_Employee", ID, ID_Employee);

            EDSV_Emp.Tables[0].TableName = GeneralSheet;
            EDSV_Emp.Tables[1].TableName = "Daily Schedule";
            EDSV_Emp.Tables[2].TableName = "Attendance Log";
            EDSV_Emp.Tables[3].TableName = "Attendance";
            EDSV_Emp.Tables[4].TableName = "Leave";
            EDSV_Emp.Tables[5].TableName = "Overtime";
            EDSV_Emp.Tables[6].TableName = "Official Business";
            EDSV_Emp.Tables[7].TableName = "Change of Schedule";

            using (var xls = new ExcelWriter(true))
            {
                //add styling
                xls.AddCellStyle("Title", FontSize: 18);
                xls.AddCellStyle("DateRange", FontSize: 12);
                xls.AddCellStyle(HeaderStyle, BoldWeight: NPOI.SS.UserModel.FontBoldWeight.Bold);
                xls.AddCellStyle(NormalStyle);
                xls.AddCellStyle(SheetTitle, FontSize: 12);

                xls.AddSheet(GeneralSheet);
                //add signature 
                AddRow(xls, GeneralSheet, 0, "Employee Timesheet", "Title");
                AddRow(xls, GeneralSheet, 0, $"{ EDSV["StartDate"].ToDate("yyyy-MM-dd") } - { EDSV["EndDate"].ToDate("yyyy-MM-dd") }", "DateRange");
                AddRowHeader(xls, GeneralSheet, "Company:", EDSV["Company"].IsNull("All Companies").ToString(), "Branch:", EDSV["Branch"].IsNull(NUllValue).ToString());
                AddRowHeader(xls, GeneralSheet, "Payroll Frequency:", EDSV["PayrollFrequency"].IsNull(NUllValue).ToString(), "Department:", EDSV["Department"].IsNull(NUllValue).ToString());
                AddRowHeader(xls, GeneralSheet, "Designation:", EDSV["Designation"].IsNull(NUllValue), "Cost Center:", EDSV["CostCenter"].IsNull(NUllValue));
                AddRowHeader(xls, GeneralSheet, "Date Created:", DateTime.Now.ToString("yyyy-MM-dd"), "Created By:", Session["User"].ToString());
                xls.AddRow(GeneralSheet);

                xls.MergeCells(GeneralSheet, 0, 0, 0, 6);
                xls.MergeCells(GeneralSheet, 1, 1, 0, 6);

                var columnHeaderRow = xls.AddRow(GeneralSheet);
                var eds = EDSV_Emp.Tables[0];
                for (var c = 0; c < eds.Columns.Count; c++)
                    xls.AddCell(columnHeaderRow, c, eds.Columns[c].Caption, HeaderStyle);

                for (var rw = 0; rw < eds.Rows.Count; rw++)
                {
                    var row = xls.AddRow(GeneralSheet);
                    for (var c = 0; c < eds.Columns.Count; c++)
                    {
                        xls.AddCell(row, c, eds.Rows[rw][c], eds.Columns[c].DataType, NormalStyle);
                    }
                }

                PlotDetails(xls, EDSV_Emp);

                var flname = $"InSys_Timesheet_{ EDSV["Company"].IsNull("All Companies").ToString().Replace(" ", "_") }_{ EDSV["StartDate"].ToDate("yyyyMMdd") }_{ EDSV["EndDate"].ToDate("yyyyMMdd") }.xlsx";

                if (ID_Employee != DBNull.Value)
                    flname = $"InSys_Timesheet_{ EDSV["Company"].IsNull("All Companies").ToString().Replace(" ", "_") }_{ EDSV_Emp.Tables[0].Rows[0]["Employee"].ToString().Replace(" ", "_") }_{ EDSV["StartDate"].ToDate("yyyyMMdd") }_{ EDSV["EndDate"].ToDate("yyyyMMdd") }.xlsx";

                using (var ms = new MemoryStream())
                {
                    xls.SaveToStream(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    using (var strg = new Storage.Storage())
                    {
                        var cntr = strg.Container("Files");
                        strg.Upload(cntr, flname, ms);
                    }
                }

                r.ResultSet = flname;
            }

            return r;
        });

        protected void AddRow(ExcelWriter excelWriter, string Sheet, int CellIndex, dynamic Value, string Style = "")
        {
            var row = excelWriter.AddRow(Sheet);
            excelWriter.AddCell(row, CellIndex, Value, Style);
        }

        protected void AddRowHeader(ExcelWriter excelWriter, string Sheet, string lTitle, object lValue, string rTitle, object rValue)
        {
            var row = excelWriter.AddRow(Sheet);
            excelWriter.AddCell(row, 0, lTitle, HeaderStyle);
            excelWriter.AddCell(row, 1, lValue, NormalStyle);
            excelWriter.AddCell(row, 2, string.Empty);
            excelWriter.AddCell(row, 3, string.Empty);
            excelWriter.AddCell(row, 4, string.Empty);
            excelWriter.AddCell(row, 5, rTitle, HeaderStyle);
            excelWriter.AddCell(row, 6, rValue, NormalStyle);
        }


        protected void PlotDetails(ExcelWriter xls, DataSet dataset)
        {
            for (var ix = 1; ix < dataset.Tables.Count; ix++)
            {
                var dt = dataset.Tables[ix];
                xls.AddSheet(dt.TableName);

                AddRow(xls, dt.TableName, 0, $"{ dt.TableName } [{ dt.Rows.Count }]", SheetTitle);
                xls.MergeCells(dt.TableName, 0, 0, 0, 7);

                var columnHeaderRow = xls.AddRow(dt.TableName);
                for (var c = 0; c < dt.Columns.Count; c++)
                    xls.AddCell(columnHeaderRow, c, dt.Columns[c].Caption, HeaderStyle);

                for (var rw = 0; rw < dt.Rows.Count; rw++)
                {
                    var row = xls.AddRow(dt.TableName);
                    for (var c = 0; c < dt.Columns.Count; c++)
                    {
                        xls.AddCell(row, c, dt.Rows[rw][c], dt.Columns[c].DataType, NormalStyle);
                    }
                }
            }
        }


    }
}