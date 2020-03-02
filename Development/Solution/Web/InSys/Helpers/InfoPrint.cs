using InSys.Office;
using InSys.Storage;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using z.Data;
using z.SQL;

namespace InSys.Helpers
{
    public class InfoPrint : InfoSet
    {
        public string FileName { get; set; }
        // protected string nmpth;

        protected Storage.Storage strg;
        protected IStorageContainer cntr;

        public InfoPrint(int ID_Menu, IQueryArgs sql, HttpContext httpcontext) : base(ID_Menu, sql, httpcontext)
        {
            strg = new Storage.Storage();
            cntr = strg.Container("Files");
        }

        public void Print(string data, int opt)
        {
            try
            {
                this.PrepareTables();
                this.DeserializedTables(data);

                row = ds.Tables[menu.tMenu.TableName].Rows[0];

                switch (opt)
                {
                    case 1: PDF(); break;
                    case 2: Excel(); break;
                    case 3: CSV(); break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Excel()
        {
            using (var xls = new ExcelWriter(true))
            {
                xls.AddSheet(menu.tMenu.Name);

                foreach (var item in menu.tMenuTab)
                {
                    foreach (var fld in menu.tMenuTabField.Where(x => x.ID_MenuTab == item.ID && x.IsActive == true && x.ShowInInfo == true).OrderBy(x => x.SeqNo))
                    {
                        var hd = xls.AddRow(menu.tMenu.Name);

                        xls.AddCell(hd, 0, fld.EffectiveLabel);
                        xls.AddCell(hd, 1, row[fld.Name].ToString());
                    }
                }

                //detail 
                foreach (var dtl in menu.tMenuDetailTab.Where(x => x.IsActive == true))
                {
                    xls.AddSheet(dtl.Name);
                    var cols = menu.tMenuDetailTabField.Where(x => x.ID_MenuDetailTab == dtl.ID && x.IsActive == true && x.ShowInInfo == true).ToArray();
                    var row = xls.AddRow(dtl.Name);
                    for (int i = 0; i < cols.Count(); i++)
                        xls.AddCell(row, i, cols[i].EffectiveLabel);

                    foreach (DataRow dr in ds.Tables[dtl.TableName].Rows)
                    {
                        row = xls.AddRow(dtl.Name);
                        for (int i = 0; i < cols.Count(); i++)
                            xls.AddCell(row, i, dr[cols[i].Name].ToString());
                    }
                }

                FileName = menu.tMenu.Name + " - " + DateTime.Now.ToString("yyyy, MM dd") + ".xlsx";

                using (var msd = new MemoryStream())
                {
                    xls.SaveToStream(msd);
                    strg.Upload(cntr, FileName, msd);
                }

            }
        }

        private void PDF()
        {
            FileName = menu.tMenu.Name + " - " + DateTime.Now.ToString("yyyy, MM dd") + ".pdf";

            var tmfFile = Path.GetTempFileName();

            var pdfDoc = new PdfDocument(new PdfWriter(tmfFile));

            var pagesize = iText.Kernel.Geom.PageSize.LETTER.Rotate();

            var doc = new Document(pdfDoc, pagesize);

            Paragraph p = new Paragraph();
            p.Add(menu.tMenu.Name);
            doc.Add(p);

            var txt = new Text(DateTime.Now.ToString("yyyy, MM dd"));
            txt.SetFontSize(9);
            txt.SetItalic();
            var dte = new Paragraph(txt);
            doc.Add(dte);

            var regular = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            var bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            foreach (var item in menu.tMenuTab)
            {
                var pnls = menu.tMenuTabField.Where(x => x.ID_MenuTab == item.ID && x.IsActive == true && x.ShowInInfo == true).Max(x => x.Panel);

#pragma warning disable CS0618
                var tble = new Table(Convert.ToInt32(pnls), true);
                tble.SetWidth(iText.Layout.Properties.UnitValue.CreatePercentValue(40)); //.SetWidthPercent(40);
                for (var pnl = 1; pnl <= pnls; pnl++)
                {
                    foreach (var fld in menu.tMenuTabField.Where(x => x.ID_MenuTab == item.ID && x.IsActive == true && x.ShowInInfo == true && x.Panel == pnl).OrderBy(x => x.SeqNo))
                    {
                        var pp = new Paragraph();
                        pp.Add(new Text(fld.EffectiveLabel).SetFont(bold).SetFontSize(9));
                        pp.Add(new Text($": { row[fld.Name].ToString() }").SetFont(regular).SetFontSize(9));
                        tble.AddCell(new Cell().SetBorder(Border.NO_BORDER).Add(pp));
                    }
                }

                doc.Add(tble);
            }

            //detail 
            foreach (var dtl in menu.tMenuDetailTab.Where(x => x.IsActive == true))
            {
                var cols = menu.tMenuDetailTabField.Where(x => x.ID_MenuDetailTab == dtl.ID && x.IsActive == true && x.ShowInInfo == true).ToArray();

#pragma warning disable CS0618
                var tble = new Table(cols.Length, true);

                for (int i = 0; i < cols.Count(); i++)
                    tble.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1)).SetFont(bold).SetFontSize(8).Add(new Paragraph(cols[i].EffectiveLabel)));

                foreach (DataRow dr in ds.Tables[dtl.TableName].Rows)
                {
                    for (int i = 0; i < cols.Count(); i++)
                        tble.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFont(regular).SetFontSize(8).Add(new Paragraph(dr[cols[i].Name].ToString())));
                }
                doc.Add(new Paragraph(dtl.Name).SetFont(bold).SetFontSize(9));
                doc.Add(tble);
            }

            doc.Close();
            pdfDoc.Close();

            using (var str = File.OpenRead(tmfFile))
            {
                strg.Upload(cntr, FileName, str);
            }

            File.Delete(tmfFile);
            GC.Collect();
        }

        private void CSV()
        {
            FileName = menu.tMenu.Name + " - " + DateTime.Now.ToString("yyyy, MM dd") + ".zip";

            var dss = new DataSet();
            var dtMaster = new DataTable(menu.tMenu.Name);

            var obj = new List<object>();
            foreach (var fld in menu.tMenuTabField.Where(x => x.IsActive == true && x.ShowInInfo == true).OrderBy(x => x.SeqNo))
            {
                dtMaster.Columns.Add(fld.EffectiveLabel, typeof(string));
                obj.Add(row[fld.Name].ToString());
            }
            dtMaster.Rows.Add(obj.ToArray());
            dss.Tables.Add(dtMaster);

            foreach (var dtl in menu.tMenuDetailTab.Where(x => x.IsActive == true))
            {
                var cols = menu.tMenuDetailTabField.Where(x => x.ID_MenuDetailTab == dtl.ID && x.IsActive == true && x.ShowInInfo == true).ToArray();

                var tble = new DataTable(dtl.Name);

                for (int i = 0; i < cols.Count(); i++)
                    tble.Columns.Add(cols[i].EffectiveLabel, typeof(string));

                foreach (DataRow dr in ds.Tables[dtl.TableName].Rows)
                {
                    var drd = tble.NewRow();
                    for (int i = 0; i < cols.Count(); i++)
                        drd[cols[i].EffectiveLabel] = dr[cols[i].Name].ToString();
                    tble.Rows.Add(drd);
                }

                dss.Tables.Add(tble);
            }

            using (var ms = new MemoryStream())
            {
                using (Ionic.Zip.ZipFile szp = new Ionic.Zip.ZipFile())
                {
                    foreach (DataTable g in dss.Tables)
                    {
                        szp.AddEntry(g.TableName + ".csv", WriteToCsvFile(g));
                    }
                    szp.Save(ms);
                }

                ms.Seek(0, SeekOrigin.Begin);
                strg.Upload(cntr, FileName, ms);
            }
        }

        public byte[] WriteToCsvFile(DataTable dataTable)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {

                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            return UnicodeEncoding.Default.GetBytes(fileContent.ToString());
        }

    }
}