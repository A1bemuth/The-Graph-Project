using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
// ReSharper disable RedundantArgumentName
// ReSharper disable RedundantArgumentNameForLiteralExpression

namespace GraphDataLayer.ExcelImport
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

        [Obsolete("Use GetGraphInfos and ExcelGraphInfo's Matrix property.")]
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

        public List<ExcelGraphInfo> GetGraphInfos()
        {
            var graphInfos = new List<ExcelGraphInfo>(xlWorkbook.Worksheets.Count);
            foreach (Excel.Worksheet worksheet in xlWorkbook.Worksheets)
            {
                var usedRange = worksheet.UsedRange;
                var firstColumn = usedRange.Resize[ColumnSize: 1];
                var names = GetVerticeNames(firstColumn);

                var debug = usedRange.Value2;
                
                var noFirstColumn = usedRange
                    .Offset[ColumnOffset: 1]
                    .Resize[ColumnSize: usedRange.Columns.Count - 1];
                var debug2 = noFirstColumn.Value2;
                
                var rangeNumbers = GetMatrix(names == null ? usedRange : noFirstColumn);
                var graphName = worksheet.Name;
                graphInfos.Add(new ExcelGraphInfo(graphName, names, rangeNumbers));
            }
            return graphInfos;
        }

        private string[] GetVerticeNames(Excel.Range range)
        {
            var rangeValues = range.Value2;
            if (!(range.Value2 is object[,])) return null;
            if (IsNumber(rangeValues[0 + 1, 0 + 1])) return null;

            var names = new string[((object[,]) rangeValues).GetLength(0)];
            for (var i = 0; i < range.Rows.Count; i++)
            {
                try
                {
                    names[i] = rangeValues[i + 1, 0 + 1];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return names;
                }
            }
            return names;
        }

        private bool IsNumber(dynamic item)
        {
            var itemString = item.ToString();
            var numberPattern = new Regex(@"\d+");
            return numberPattern.IsMatch(itemString);
        }

        private int[,] GetMatrix(Excel.Range range)
        {
            int[,] rangeNumbers;

            var rangeValues = range.Value2;
            if (rangeValues is object[,])
            {
                rangeNumbers = new int[range.Rows.Count, range.Columns.Count];
                for (int i = 0; i < range.Rows.Count; i++)
                {
                    for (int j = 0; j < range.Columns.Count; j++)
                    {
                        var item = rangeValues[i + 1, j + 1];
                        rangeNumbers[i, j] = ParseItem(item);
                    }
                }
            }
            else if (rangeValues == null)
            {
                return null;
            }
            else
            {
                rangeNumbers = new int[1, 1];
                rangeNumbers[0, 0] = (int) rangeValues;
            }
            return rangeNumbers;
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
