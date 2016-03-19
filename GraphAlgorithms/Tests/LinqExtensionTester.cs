using System;
using System.Linq;
using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class LinqExtensionTester
    {
        [Test]

        public void IndexesForColumnThrowArgumentNullTest()
        {
            string[][] matrix = null;
            Assert.Throws<ArgumentNullException>(() => matrix.IndexesForColumn(0, v => true).ToArray());
        }

        [Test]
        public void IndexesForColumnThrowDimensionExeptionTest()
        {
            var emptyMatrix = new[]
            {
                new int[0]
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => emptyMatrix.IndexesForColumn(0, v => true).ToArray());
        }

        [Test]
        public void IndexesForColumnThrowIndexExeptionForMatrixTest()
        {
            var matrix = new[]
            {
                new[] {1, 2},
                new[] {2, 3}
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.IndexesForColumn(2, v => true).ToArray());
        }

        [Test]
        public void IndexesForColumnReturnAllIndexesTest()
        {
            var matrix = new[]
            {
                new[] {1, 2}
            };

            var result = matrix.IndexesForColumn(0, v => true).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(matrix.Select((v, i) => i), result);
        }

        [Test]
        public void IndexesForColumnReturnSomeValuesFromSecondColumnTest()
        {
            var matrix = new[]
            {
                new[] {1, 2, 3},
                new[] {1, 2, 2},
                new[] {1, 2, 2},
                new[] {1, 2, 3}
            };

            var result = matrix.IndexesForColumn(2, v => v == 3).ToArray();

            Assert.That(result.Length, Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] {0, 3}, result);
        }

        [Test]
        public void IndexesForColumnRealSituationTest()
        {
            var matrix = new[]
            {
                new[] {1, 0},
                new[] {-1, 1},
                new[] {0, -1}
            };

            var result = matrix.IndexesForColumn(1, v => v == -1).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2}, result);
        }

        [Test]
        public void IndexesForColumnEmptyResult()
        {
            var matrix = new[]
            {
                new[] {1, 2, 3},
                new[] {1, 2, 3}
            };

            var result = matrix.IndexesForColumn(1, v => v == 1).ToArray();

            Assert.That(result.Length, Is.EqualTo(0));
        }
    }
}