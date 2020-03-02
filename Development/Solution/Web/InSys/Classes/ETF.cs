using InSys.Office;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using z.Data;

namespace InSys.Classes
{
    public class ETF
    {
        private List<string> SelectedSheets { get; set; }
        private MemoryStream ms { get; set; }
        private string xlsFilename { get; set; }
        public Excel xls { get; set; }
        public bool hasError { get; private set; }
        public object sheetData { get; private set; }

        public ETF(List<string> SelectedSheets, MemoryStream ms, string xlsFilename)
        {
            this.SelectedSheets = SelectedSheets;
            this.ms = ms;
            this.xlsFilename = xlsFilename;
        }

        private void setValidationStyle()
        {
            foreach (DataTable tbl in xls.Read().Tables.Cast<DataTable>())
            {
                if (SelectedSheets.IndexOf(tbl.TableName) > -1)
                {
                    var sheetRow = xls.GetRow(tbl.TableName, 0);
                    foreach (var hssfcell in sheetRow.Cells)
                    {
                        if (hssfcell.StringCellValue != "" && hssfcell.StringCellValue != null)
                        {
                            string cellStyleNameRequired = hssfcell.StringCellValue.ToString() + "_Required_" + hssfcell.ColumnIndex.ToString();
                            string cellStyleNameNormal = hssfcell.StringCellValue.ToString() + "_Normal_" + hssfcell.ColumnIndex.ToString();
                            xls.AddCellStyle(cellStyleNameRequired, BackgroundColor: IndexedColors.Red, DataFormat: hssfcell.CellStyle.DataFormat);
                            xls.AddCellStyle(cellStyleNameNormal, BackgroundColor: IndexedColors.Automatic, DataFormat: hssfcell.CellStyle.DataFormat, fp: FillPattern.NoFill);
                        }
                    }
                }
            }
        }

        public void ValidateExcel()
        {
            using (xls = new Excel(ms, Excel.IsFileInNewFormat(xlsFilename)))
            {
                this.setValidationStyle();
                hasError = this.ValidateRows();
                if (!hasError)
                {
                    var b = xls.Read().Tables.Cast<DataTable>().Where(x => SelectedSheets.IndexOf(x.TableName) > -1).Select(x => new
                    {
                        TableName = x.TableName,
                        Rows = x.Rows.JsonModel().Where(ds => ds["EmployeeCode"].IsNull("").ToString() != "").Select((ds, i) =>
                        {
                            ds["XXX_ROWID"] = i;
                            ds["ID"] = 0;
                            return ds;
                        })
                    }).ToArray();
                    sheetData = b;
                }
            }
        }

        private bool ValidateRows()
        {
            var err = false;
            foreach (DataTable tbl in xls.Read().Tables.Cast<DataTable>())
            {
                if (SelectedSheets.IndexOf(tbl.TableName) > -1)
                {
                    var sheetRow = xls.GetRow(tbl.TableName, 0);
                    for (int cidx = 0; cidx < (sheetRow.Cells.Count()); cidx++)
                    {
                        if (Excel.IsFileInNewFormat(xlsFilename))
                        {
                            if (err == true) this.ValidateNullXLSX(sheetRow, tbl, cidx);
                            else err = this.ValidateNullXLSX(sheetRow, tbl, cidx);
                        }
                        else
                        {
                            if (err == true) this.ValidateNullXLS(sheetRow, tbl, cidx);
                            else err = this.ValidateNullXLS(sheetRow, tbl, cidx);
                        }
                    }
                }
            }
            return err;
        }

        private bool ValidateNullXLSX(IRow sheetRow, DataTable tbl, int cidx)
        {
            bool err = false;
            var cellcolor = sheetRow.Cells[cidx].CellStyle.FillBackgroundColor;

            if (cellcolor == (short)29)
            {
                for (int ridx = 0; ridx < tbl.Rows.Count; ridx++)
                {
                    if (sheetRow.Cells[cidx].StringCellValue != "" && sheetRow.Cells[cidx].StringCellValue != null)
                    {
                        //validate if null
                        if (tbl.Rows[ridx][sheetRow.Cells[cidx].StringCellValue].IsNull("").ToString() == "")
                        {
                            err = true;
                            var xlsrow = xls.GetRow(tbl.TableName, ridx + 1);
                            string cellStyleName = sheetRow.Cells[cidx].StringCellValue.ToString() + "_Required_" + cidx.ToString();
                            xlsrow.CreateCell(cidx);
                            xls.SetCellStyle(xlsrow, cidx, cellStyleName);
                        }
                    }
                }
            }
            else
            {
                for (int ridx = 0; ridx < tbl.Rows.Count; ridx++)
                {
                    if (sheetRow.Cells[cidx].StringCellValue != "" && sheetRow.Cells[cidx].StringCellValue != null)
                    {
                        if (tbl.Rows[ridx][sheetRow.Cells[cidx].StringCellValue].IsNull("").ToString() == "")
                        {
                            var xlsrow = xls.GetRow(tbl.TableName, ridx + 1);
                            string cellStyleName = sheetRow.Cells[cidx].StringCellValue.ToString() + "_Normal_" + cidx.ToString();
                            xlsrow.CreateCell(cidx);
                            xls.SetCellStyle(xlsrow, cidx, cellStyleName);
                        }
                    }
                }
            }

            return err;
        }

        private bool ValidateNullXLS(IRow sheetRow, DataTable tbl, int cidx)
        {
            bool err = false;
            var cellcolor = sheetRow.Cells[cidx].CellStyle.FillForegroundColor;

            if (cellcolor == HSSFColor.Rose.Index)
            {
                for (int ridx = 0; ridx < tbl.Rows.Count; ridx++)
                {
                    if (sheetRow.Cells[cidx].StringCellValue != "" && sheetRow.Cells[cidx].StringCellValue != null)
                    {
                        //validate if null
                        if (tbl.Rows[ridx][sheetRow.Cells[cidx].StringCellValue].IsNull("").ToString() == "")
                        {
                            err = true;
                            var xlsrow = xls.GetRow(tbl.TableName, ridx + 1);
                            string cellStyleName = sheetRow.Cells[cidx].StringCellValue.ToString() + "_Required_" + cidx.ToString();
                            xlsrow.CreateCell(cidx);
                            xls.SetCellStyle(xlsrow, cidx, cellStyleName);
                        }
                    }
                }
            }
            else
            {
                for (int ridx = 0; ridx < tbl.Rows.Count; ridx++)
                {
                    if (sheetRow.Cells[cidx].StringCellValue != "" && sheetRow.Cells[cidx].StringCellValue != null)
                    {
                        if (tbl.Rows[ridx][sheetRow.Cells[cidx].StringCellValue].IsNull("").ToString() == "")
                        {
                            var xlsrow = xls.GetRow(tbl.TableName, ridx + 1);
                            string cellStyleName = sheetRow.Cells[cidx].StringCellValue.ToString() + "_Normal_" + cidx.ToString();
                            xlsrow.CreateCell(cidx);
                            xls.SetCellStyle(xlsrow, cidx, cellStyleName);
                        }
                    }
                }
            }

            return err;
        }
    }
}