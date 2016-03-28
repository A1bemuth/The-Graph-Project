using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
// ReSharper disable RedundantArgumentName
// ReSharper disable RedundantArgumentNameForLiteralExpression

namespace GraphDataLayer
{
    public class ExcelImporter : IDisposable
    {
        private Excel.Application xlApplication;
        private Excel.Workbook xlWorkbook;

        public ExcelImporter(string filename)
        {
            xlApplication = new Excel.Application();
            xlWorkbook = xlApplication.Workbooks.Open(
                Filename: filename,
                UpdateLinks: 0,
                ReadOnly: true,
                Format: 5,
                Password: "",
                WriteResPassword: "",
                IgnoreReadOnlyRecommended: true,
                Origin: Excel.XlPlatform.xlWindows,
                Delimiter: "\t",
                Editable: false,
                Notify: false,
                Converter: 0,
                AddToMru: true,
                Local: 1,
                CorruptLoad: 0);
        }

        public List<int[,]> GetRanges()
        {
            var ranges = new List<int[,]>(xlWorkbook.Worksheets.Count);
            foreach (Excel.Worksheet worksheet in xlWorkbook.Worksheets)
            {
                var height = worksheet.UsedRange.Rows.Count;
                var width = worksheet.UsedRange.Columns.Count;
                var rangeNumbers = new int[height, width];
                var range = worksheet.UsedRange.Value2;

                if (range is object[,])
                {
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            var item = range[i + 1, j + 1];
                            rangeNumbers[i, j] = ParseItem(item);
                        }
                    }
                }
                else
                {
                    rangeNumbers[0, 0] = (int) range;
                }
                ranges.Add(rangeNumbers);
            }
            return ranges;
        }

        private int ParseItem(dynamic item)
        {
            int result;
            return int.TryParse(item.ToString(), out result) ? result : 0;
        }

        public void Dispose()
        {
            xlWorkbook.Close();
            xlApplication.Quit();
            xlWorkbook = null;
            xlApplication = null;
        }
    }
}
