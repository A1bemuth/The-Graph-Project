using System;
using System.Linq;
using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class LinqExtensionTester
    {
        [Test]
        public void IndexesInColumnThrowArgumentNullTest()
        {
            string[][] matrix = null;
            Assert.Throws<ArgumentNullException>(() => matrix.IndexesInColumn(0, v => true).ToArray());
        }

        [Test]
        public void IndexesInColumnThrowDimensionExeptionTest()
        {
            var emptyMatrix = new[]
            {
                new int[0]
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => emptyMatrix.IndexesInColumn(0, v => true).ToArray());
        }

        [Test]
        public void IndexesInColumnThrowIndexExeptionForMatrixTest()
        {
            var matrix = new[]
            {
                new[] {1, 2},
                new[] {2, 3}
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.IndexesInColumn(2, v => true).ToArray());
        }

        [Test]
        public void IndexesInColumnReturnAllIndexesTest()
        {
            var matrix = new[]
            {
                new[] {1, 2}
            };

            var result = matrix.IndexesInColumn(0, v => true).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(matrix.Select((v, i) => i), result);
        }

        [Test]
        public void IndexesInColumnReturnSomeValuesFromSecondColumnTest()
        {
            var matrix = new[]
            {
                new[] {1, 2, 3},
                new[] {1, 2, 2},
                new[] {1, 2, 2},
                new[] {1, 2, 3}
            };

            var result = matrix.IndexesInColumn(2, v => v == 3).ToArray();

            Assert.That(result.Length, Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] {0, 3}, result);
        }

        [Test]
        public void IndexesInColumnRealSituationTest()
        {
            var matrix = new[]
            {
                new[] {1, 0},
                new[] {-1, 1},
                new[] {0, -1}
            };

            var result = matrix.IndexesInColumn(1, v => v == -1).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2}, result);
        }

        [Test]
        public void IndexesInColumnEmptyResult()
        {
            var matrix = new[]
            {
                new[] {1, 2, 3},
                new[] {1, 2, 3}
            };

            var result = matrix.IndexesInColumn(1, v => v == 1).ToArray();

            Assert.That(result.Length, Is.EqualTo(0));
        }

        [Test]
        public void IndexesOfThrowArgumentNullTest()
        {
            int[] firstItems = null;
            var secondItems = new int[2];

            Assert.Throws<ArgumentNullException>(() => firstItems.IndexesOf(v => true).ToArray());
            Assert.Throws<ArgumentNullException>(() => secondItems.IndexesOf(null).ToArray());
        }

        [Test]
        public void IndexOfReturnEmptyCollectionTest()
        {
            var items = new[] {1, 1, 1, 1};

            var result = items.IndexesOf(v => v == 2).ToArray();

            Assert.That(result.Length, Is.EqualTo(0));
        }

        [Test]
        public void IndexOfReturnSomeValuesTest()
        {
            var items = new[] {1, 2, 3, 4};

            var result = items.IndexesOf(v => (v & 1) == 0).ToArray();

            Assert.That(result.Length, Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] {1, 3}, result);
        }
    }
}