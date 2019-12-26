using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide.ViewModels
{
    public static class DemoResponse
    {
        public static DataTable DataTable(this ExcelPackage excelPackage)
        {
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.First();
            DataTable dataTable = new DataTable();
            foreach(var firstRowCell in excelWorksheet.Cells[1, 1, 1, excelWorksheet.Dimension.End.Column])
            {
                dataTable.Columns.Add(firstRowCell.Text);
            }
            for (var rowNumber = 2; rowNumber <= excelWorksheet.Dimension.End.Row; rowNumber++)
            {
                var row = excelWorksheet.Cells[rowNumber, 1, rowNumber, excelWorksheet.Dimension.End.Column];
                var newRow = dataTable.NewRow(); 
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                dataTable.Rows.Add(newRow);
            }
            return dataTable;
        }
    }
}
