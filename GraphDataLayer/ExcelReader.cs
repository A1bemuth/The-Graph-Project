using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
// ReSharper disable RedundantArgumentName
// ReSharper disable RedundantArgumentNameForLiteralExpression

namespace GraphDataLayer
{
    public class ExcelReader : IDisposable
    {
        private Excel.Application xlApplication;
        private Excel.Workbook xlWorkbook;

        public ExcelReader(string filename)
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
                var usedRange = worksheet.UsedRange;
                var rangeValues = usedRange.Value2;
                int[,] rangeNumbers;

                if (rangeValues is object[,])
                {
                    rangeNumbers = new int[usedRange.Rows.Count, usedRange.Columns.Count];
                    for (int i = 0; i < usedRange.Rows.Count; i++)
                    {
                        for (int j = 0; j < usedRange.Columns.Count; j++)
                        {
                            var item = rangeValues[i + 1, j + 1];
                            rangeNumbers[i, j] = ParseItem(item);
                        }
                    }
                }
                else if (rangeValues == null)
                {
                    continue;
                }
                else
                {
                    rangeNumbers = new int[1, 1];
                    rangeNumbers[0, 0] = (int) rangeValues;
                }
                ranges.Add(rangeNumbers);
            }
            return ranges;
        }

        private int ParseItem(dynamic item)
        {
            int result;
            return int.TryParse(item?.ToString(), out result) ? result : 0;
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
