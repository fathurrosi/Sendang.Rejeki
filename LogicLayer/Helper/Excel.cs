using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using DataObject;
using System.Diagnostics;

namespace LogicLayer.Helper
{
    public class Excel
    {

        public static string WriteExcel(String extension, DataTable dataTable, DateTime date)
        {
            // dll refered NPOI.dll and NPOI.OOXML  
            string path = string.Empty;
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            XSSFCellStyle decimalStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            decimalStyle.WrapText = true;
            decimalStyle.Alignment = HorizontalAlignment.Right;
            decimalStyle.VerticalAlignment = VerticalAlignment.Top;
            decimalStyle.BorderBottom = BorderStyle.Thin;
            decimalStyle.BorderTop = BorderStyle.Thin;
            decimalStyle.BorderLeft = BorderStyle.Thin;
            decimalStyle.BorderRight = BorderStyle.Thin;

            XSSFCellStyle headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.WrapText = true;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Top;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.FillForegroundColor = IndexedColors.LightCornflowerBlue.Index;

            XSSFCellStyle footerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            footerStyle.WrapText = true;
            footerStyle.Alignment = HorizontalAlignment.Center;
            footerStyle.VerticalAlignment = VerticalAlignment.Top;
            footerStyle.BorderBottom = BorderStyle.Thin;
            footerStyle.BorderTop = BorderStyle.Thin;
            footerStyle.BorderLeft = BorderStyle.Thin;
            footerStyle.BorderRight = BorderStyle.Thin;
            footerStyle.FillPattern = FillPattern.SolidForeground;
            footerStyle.FillForegroundColor = IndexedColors.LightGreen.Index;

            //make a header row  
            IRow row1 = sheet1.CreateRow(0);
            int colIndex = 0;
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                DataColumn column = dataTable.Columns[j];
                ICell cell = row1.CreateCell(colIndex);
                sheet1.SetColumnWidth(colIndex, 5000);
                String columnName = column.ColumnName.Replace("col", string.Format("{0:MMM}-", date));
                if (columnName.ToLower() == "CatalogID".ToLower()) continue;
                cell.SetCellValue(columnName);
                cell.CellStyle = headerStyle;
                colIndex++;
            }


            int rowIndex = 0;
            IRow row;
            foreach (DataRow dr in dataTable.Rows)
            {
                rowIndex++;
                row = sheet1.CreateRow(rowIndex);

                colIndex = 0;
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    DataColumn column = dataTable.Columns[j];
                    ICell cell = row.CreateCell(colIndex);
                    cell.CellStyle = decimalStyle;
                    String columnName = column.ColumnName;
                    if (columnName.ToLower() == "CatalogID".ToLower()) continue;
                    //if (columnName == "Quantity") cell.SetCellValue(string.Format("{0:N2}{1}", item.Quantity, item.Unit));
                    //else if (columnName == "HPP") cell.SetCellValue(string.Format("Rp. {0:N2}", item.HPP));
                    //else 
                    //if (columnName == "Purchase")
                    //{
                    //    cell.SetCellValue(string.Format("Rp. {0:N2}", item.Purchase));
                    //    totalPurchase += item.Purchase;
                    //    totalSales += item.Sale;
                    //}
                    //else if (columnName == "Tanggal")
                    //{
                    //    cell.SetCellValue(string.Format("{0:dd MMM yyyy}", item.TransDate));
                    //}
                    //else if (columnName == "Item")
                    //{
                    cell.SetCellValue(dr[column].ToString());
                    //}
                    colIndex++;
                }
            }

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = string.Format(@"{0}\{1}.xlsx", dir, DateTime.Now.ToString("ddMMMyyyyHHmmss"));
            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(stream);
            }

            //string path = Path.GetTempFileName();
            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                path = fileName;
                workbook.Write(fs);
            }

            return path;

        }

        public static string WriteExcel(String extension, DataTable dataTable)
        {
            // dll refered NPOI.dll and NPOI.OOXML  
            string path = string.Empty;
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            XSSFCellStyle decimalStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            decimalStyle.WrapText = true;
            decimalStyle.Alignment = HorizontalAlignment.Right;
            decimalStyle.VerticalAlignment = VerticalAlignment.Top;
            decimalStyle.BorderBottom = BorderStyle.Thin;
            decimalStyle.BorderTop = BorderStyle.Thin;
            decimalStyle.BorderLeft = BorderStyle.Thin;
            decimalStyle.BorderRight = BorderStyle.Thin;

            XSSFCellStyle headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.WrapText = true;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Top;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.FillForegroundColor = IndexedColors.LightCornflowerBlue.Index;

            XSSFCellStyle footerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            footerStyle.WrapText = true;
            footerStyle.Alignment = HorizontalAlignment.Center;
            footerStyle.VerticalAlignment = VerticalAlignment.Top;
            footerStyle.BorderBottom = BorderStyle.Thin;
            footerStyle.BorderTop = BorderStyle.Thin;
            footerStyle.BorderLeft = BorderStyle.Thin;
            footerStyle.BorderRight = BorderStyle.Thin;
            footerStyle.FillPattern = FillPattern.SolidForeground;
            footerStyle.FillForegroundColor = IndexedColors.LightGreen.Index;

            //make a header row  
            IRow row1 = sheet1.CreateRow(0);
            int colIndex = 0;
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                DataColumn column = dataTable.Columns[j];
                ICell cell = row1.CreateCell(colIndex);
                sheet1.SetColumnWidth(colIndex, 5000);
                String columnName = column.ColumnName;
                cell.SetCellValue(columnName);
                cell.CellStyle = headerStyle;
                colIndex++;
            }


            int rowIndex = 0;
            IRow row;
            foreach (DataRow dr in dataTable.Rows)
            {
                rowIndex++;
                row = sheet1.CreateRow(rowIndex);

                colIndex = 0;
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    DataColumn column = dataTable.Columns[j];
                    ICell cell = row.CreateCell(colIndex);
                    cell.CellStyle = decimalStyle;
                    String columnName = column.ColumnName;
                    //if (columnName == "Quantity") cell.SetCellValue(string.Format("{0:N2}{1}", item.Quantity, item.Unit));
                    //else if (columnName == "HPP") cell.SetCellValue(string.Format("Rp. {0:N2}", item.HPP));
                    //else if (columnName == "Purchase")
                    //{
                    //    cell.SetCellValue(string.Format("Rp. {0:N2}", item.Purchase));
                    //    totalPurchase += item.Purchase;
                    //    totalSales += item.Sale;
                    //}
                    //else if (columnName == "Tanggal")
                    //{
                    //    cell.SetCellValue(string.Format("{0:dd MMM yyyy}", item.TransDate));
                    //}
                    //else if (columnName == "Item")
                    //{
                    cell.SetCellValue(dr[column].ToString());
                    //}
                    colIndex++;
                }
            }

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = string.Format(@"{0}\{1}.xlsx", dir, DateTime.Now.ToString("ddMMMyyyyHHmmss"));
            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(stream);
            }

            //string path = Path.GetTempFileName();
            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                path = fileName;
                workbook.Write(fs);
            }

            return path;

        }

        public static void WriteExcel(String extension, List<DailyGrossProfit> dataList, List<TotalSale> totalSale, out string path)
        {
            // dll refered NPOI.dll and NPOI.OOXML  
            path = string.Empty;
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            XSSFCellStyle decimalStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            decimalStyle.WrapText = true;
            decimalStyle.Alignment = HorizontalAlignment.Right;
            decimalStyle.VerticalAlignment = VerticalAlignment.Top;
            decimalStyle.BorderBottom = BorderStyle.Thin;
            decimalStyle.BorderTop = BorderStyle.Thin;
            decimalStyle.BorderLeft = BorderStyle.Thin;
            decimalStyle.BorderRight = BorderStyle.Thin;

            XSSFCellStyle headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.WrapText = true;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Top;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.FillForegroundColor = IndexedColors.LightCornflowerBlue.Index;

            XSSFCellStyle footerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            footerStyle.WrapText = true;
            footerStyle.Alignment = HorizontalAlignment.Center;
            footerStyle.VerticalAlignment = VerticalAlignment.Top;
            footerStyle.BorderBottom = BorderStyle.Thin;
            footerStyle.BorderTop = BorderStyle.Thin;
            footerStyle.BorderLeft = BorderStyle.Thin;
            footerStyle.BorderRight = BorderStyle.Thin;
            footerStyle.FillPattern = FillPattern.SolidForeground;
            footerStyle.FillForegroundColor = IndexedColors.LightGreen.Index;

            string[] Columns = new string[] { "Tanggal", "Item", "Quantity", "HPP", "Purchase", "Sales", "Gross Profit" };

            //string reportName = "Daily Gross Profit";

            //make a header row  
            IRow row1 = sheet1.CreateRow(0);
            int colIndex = 0;
            for (int j = 0; j < Columns.Count(); j++)
            {
                ICell cell = row1.CreateCell(colIndex);
                sheet1.SetColumnWidth(colIndex, 5000);
                String columnName = Columns[j].ToString();
                if (columnName == "TransDate")
                    columnName = "Tanggal";
                else if (columnName == "CatalogName")
                {
                    columnName = "Item";
                }
                else if (columnName == "Qty")
                    columnName = "Quantity";

                if (columnName == "Unit") continue;

                cell.SetCellValue(columnName);
                cell.CellStyle = headerStyle;
                colIndex++;
            }

            decimal Total_Purchase = 0;
            decimal Total_Sales = 0;
            decimal Total_GP = 0;

            List<DateTime> dateList = dataList.Select(t => t.TransDate).Distinct().ToList();
            int rowIndex = 0;
            int prevTotalRowINdex = 0;
            IRow row;
            foreach (DateTime tanggal in dateList)
            {
                decimal totalSales = 0;
                TotalSale ts = totalSale.Where(t => t.TransDate == tanggal).FirstOrDefault();
                if (ts != null)
                {
                    totalSales = ts.Amount;
                }
                List<DailyGrossProfit> items = dataList.Where(t => t.TransDate == tanggal).ToList();
                //loops through data  
                decimal totalPurchase = 0;

                for (int i = 0; i < items.Count; i++)
                {
                    DailyGrossProfit item = items[i];
                    rowIndex++;
                    row = sheet1.CreateRow(rowIndex);

                    colIndex = 0;
                    for (int j = 0; j < Columns.Count(); j++)
                    {
                        ICell cell = row.CreateCell(colIndex);
                        cell.CellStyle = decimalStyle;
                        String columnName = Columns[j].ToString();
                        if (columnName == "Quantity") cell.SetCellValue(string.Format("{0:N2}{1}", item.Quantity, item.Unit));
                        else if (columnName == "HPP") cell.SetCellValue(string.Format("Rp. {0:N2}", item.HPP));
                        else if (columnName == "Purchase")
                        {
                            cell.SetCellValue(string.Format("Rp. {0:N2}", item.Purchase));
                            totalPurchase += item.Purchase;
                            //totalSales += item.Sale;
                        }
                        else if (columnName == "Tanggal")
                        {
                            cell.SetCellValue(string.Format("{0:dd MMM yyyy}", item.TransDate));
                        }
                        else if (columnName == "Item")
                        {
                            cell.SetCellValue(item.Item);
                        }
                        colIndex++;
                    }

                    /*****************************************************************
                     *          MERGE TANGGAL KOLOM PERTAMA
                     * ***************************************************************/
                    if ((rowIndex) < items.Count && string.Format("{0:ddMMyyyy}", item.TransDate) == string.Format("{0:ddMMyyyy}", items[rowIndex].TransDate))
                    {
                        int nextRowIndex = rowIndex + 1;
                        CellRangeAddress crTanggal = new CellRangeAddress(rowIndex, nextRowIndex, 0, 0);
                        sheet1.AddMergedRegion(crTanggal);
                    }
                }

                rowIndex++;
                row = sheet1.CreateRow(rowIndex);

                if (prevTotalRowINdex + 1 != rowIndex - 1)
                {
                    CellRangeAddress crSales = new CellRangeAddress(prevTotalRowINdex + 1, rowIndex - 1, 5, 5);
                    sheet1.AddMergedRegion(crSales);

                    CellRangeAddress crGrossProfit = new CellRangeAddress(prevTotalRowINdex + 1, rowIndex - 1, 6, 6);
                    sheet1.AddMergedRegion(crGrossProfit);
                }

                prevTotalRowINdex = rowIndex;

                colIndex = 0;
                for (int j = 0; j < Columns.Count(); j++)
                {
                    ICell cell1 = row.CreateCell(colIndex);
                    cell1.CellStyle = decimalStyle;
                    String columnName = Columns[j].ToString();
                    if (columnName == "Unit") continue;

                    if (columnName == "Tanggal")
                    {
                        cell1.SetCellValue("Total");
                        cell1.CellStyle = headerStyle;
                    }
                    else if (columnName == "Purchase")
                    {
                        cell1.SetCellValue(string.Format("Rp. {0:N2}", totalPurchase));
                        Total_Purchase += totalPurchase;
                    }
                    else if (columnName == "Sales")
                    {
                        cell1.SetCellValue(string.Format("Rp. {0:N2}", totalSales));
                        Total_Sales += totalSales;

                    }
                    else if (columnName == "Gross Profit")
                    {
                        cell1.SetCellValue(string.Format("Rp. {0:N2}", totalSales - totalPurchase));
                        Total_GP += totalSales - totalPurchase;
                    }
                    colIndex++;

                }

                CellRangeAddress cra1 = new CellRangeAddress(rowIndex, rowIndex, 0, 3);
                sheet1.AddMergedRegion(cra1);
            }


            for (int i = 0; i < 3; i++)
            {
                rowIndex++;
                row = sheet1.CreateRow(rowIndex);
                colIndex = 0;
                for (int j = 0; j < Columns.Count(); j++)
                {
                    ICell cell1 = row.CreateCell(colIndex);
                    if (i == 0)
                    {
                        cell1.CellStyle = footerStyle;
                    }
                    else
                    {
                        cell1.CellStyle = decimalStyle;
                    }

                    String columnName = Columns[j].ToString();
                    if (columnName == "Tanggal")
                    {
                        cell1.SetCellValue(i == 0 ? "Summary per Month" : "");
                    }
                    else if (columnName == "Purchase")
                    {
                        if (i == 2)
                        {

                            //cell1.CellStyle = footerStyle;
                            cell1.SetCellValue("Percentage");
                        }
                        else cell1.SetCellValue(i == 0 ? "Total Purchase" : string.Format("Rp. {0:N2}", Total_Purchase));
                    }
                    else if (columnName == "Sales")
                    {
                        if (i == 2)
                        {
                            CellRangeAddress cra1 = new CellRangeAddress(rowIndex, rowIndex, 4, 5);
                            sheet1.AddMergedRegion(cra1);
                        }
                        else cell1.SetCellValue(i == 0 ? "Total Sales" : string.Format("Rp. {0:N2}", Total_Sales));
                    }
                    else if (columnName == "Gross Profit")
                    {
                        if (i == 2) cell1.SetCellValue(string.Format("{0:N2}%", 100 * Total_GP / Total_Sales));
                        else cell1.SetCellValue(i == 0 ? "Total GP" : string.Format("Rp. {0:N2}", Total_GP));
                    }
                    colIndex++;
                }

                if (i == 2)
                {
                    CellRangeAddress craLast = new CellRangeAddress(rowIndex - 2, rowIndex, 0, 3);
                    sheet1.AddMergedRegion(craLast);
                }

            }



            string dir = Environment.CurrentDirectory;// Environment.GetFolderPath(Environment.SystemDirectory);
            string fileName = string.Format(@"{0}\{1}." + extension, dir, DateTime.Now.ToString("ddMMMyyyyHHmmss"));
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
            }

            //using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
            //{
            //    path = fileName;
            //    workbook.Write(fs);
            //}

            Process.Start(new ProcessStartInfo { FileName = fileName, UseShellExecute = true });

        }

    }
}
