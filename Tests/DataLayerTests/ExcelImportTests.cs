using System.Collections.Generic;
using NUnit.Framework;
using GraphDataLayer;

namespace Tests.DataLayerTests
{
    [TestFixture]
    class ExcelImportTests
    {
        private List<int[,]> GetRangesFromFile(string filename)
        {
            List<int[,]> ranges;
            var path = $"{System.AppDomain.CurrentDomain.BaseDirectory}\\TestSamples\\{filename}";

            using (var importer = new ExcelImporter(path))
            {
                ranges = importer.GetRanges();
            }
            return ranges;
        }

        [Test]
        public void TestOneCellFileOpen()
        {
            var ranges = GetRangesFromFile("1-by-1.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(1));
            var range = ranges[0];
            range.AssertDimensions(1, 1);
            Assert.That(range[0,0], Is.EqualTo(1));
        }

        [Test]
        public void TestTwoByTwoCellFileOpen()
        {
            var ranges = GetRangesFromFile("2-by-2.xlsx");
            Assert.That(ranges.Count, Is.EqualTo(1));
            var range = ranges[0];
            range.AssertDimensions(2, 2);
            Assert.That(range, Is.EqualTo(new [,]
            {
                {1, 2},
                {3, 4}
            }));
        }
    }
}
