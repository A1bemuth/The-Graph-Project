using System.Collections.Generic;
using NUnit.Framework;
using GraphDataLayer;

namespace Tests.DataLayerTests
{
    [TestFixture]
    class ExcelReaderTests
    {
        private List<int[,]> GetRangesFromFile(string filename)
        {
            List<int[,]> ranges;
            var path = $"{System.AppDomain.CurrentDomain.BaseDirectory}\\TestSamples\\{filename}";

            using (var importer = new ExcelReader(path))
            {
                ranges = importer.GetRanges();
            }
            return ranges;
        }

        [Test]
        public void OneCellFileOpenTest()
        {
            var ranges = GetRangesFromFile("1-by-1.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(1));
            var range = ranges[0];
            range.AssertDimensionsAre(1, 1);
            Assert.That(range[0,0], Is.EqualTo(1));
        }

        [Test]
        public void EmptySpreadsheetsFileOpenTest()
        {
            var ranges = GetRangesFromFile("1-by-1-empty-spreadsheets.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(2));
            var range = ranges[0];
            range.AssertDimensionsAre(1, 1);
            Assert.That(range[0,0], Is.EqualTo(1));
        }

        [Test]
        public void TwoByTwoFileOpenTest()
        {
            var ranges = GetRangesFromFile("2-by-2.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(1));
            var range = ranges[0];
            range.AssertDimensionsAre(2, 2);
            Assert.That(range, Is.EqualTo(new [,]
            {
                {1, 2},
                {3, 4}
            }));
        }

        [Test]
        public void NegativeNumbersFileOpenTest()
        {
            var ranges = GetRangesFromFile("2-by-2-negative-numbers.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(1));
            var range = ranges[0];
            range.AssertDimensionsAre(2, 2);
            Assert.That(range, Is.EqualTo(new [,]
            {
                {1, -1},
                {-1, 1}
            }));
        }

        [Test]
        public void DoubleSpreadsheetFileOpenTest()
        {
            var ranges = GetRangesFromFile("2-by-2-two-spreadsheets.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(2));
            ranges[0].AssertDimensionsAre(2, 2);
            Assert.That(ranges[0], Is.EqualTo(new [,]
            {
                {1, 2},
                {3, 4}
            }));
            ranges[1].AssertDimensionsAre(3, 3);
            Assert.That(ranges[1], Is.EqualTo(new [,]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            }));
        }

        [Test]
        public void SparceFileOpenTest()
        {
            var ranges = GetRangesFromFile("3-by-3-sparce.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(1));
            var range = ranges[0];
            range.AssertDimensionsAre(3, 3);
            Assert.That(range, Is.EqualTo(new [,]
            {
                {1, 0, 3},
                {0, 5, 0},
                {7, 0, 0}
            }));
        }
    }
}
