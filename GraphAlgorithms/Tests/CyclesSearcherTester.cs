using System.Linq;
using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class CyclesSearcherTester
    {
        private readonly CyclesSearcher searcher = new CyclesSearcher();

        // 0 → 1 → 2
        [Test]
        public void ThreeVertexGraphWithoutCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, 0},
                new short[] {-1, 1},
                new short[] {0, -1}
            };

            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(0));
        }

        // 0 ↔ 1
        [Test]
        public void TwoVertexGraphWithCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, -1},
                new short[] {-1, 1}
            };
            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, result[0]);
        }

        // 0 → 1 ↔ 2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenSecondAndThirdTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, 0, 0},
                new short[] {-1, 1, -1},
                new short[] {0, -1, 1}
            };

            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {1, 2}, result[0]);
        }

        // 0 ↔ 1 ← 2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenFirstAndSecondTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, -1, 0},
                new short[] {-1, 1, -1},
                new short[] {0, 0, 1}
            };
            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, result[0]);
        }

        // 0 ← 3 ↔ 2
        // ↓       ↑
        // 1 ------
        [Test]
        public void OnePathAndOneSegmentTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {0, -1, 0, 0, 1},
                new short[] {1, 0, 0, 0, -1},
                new short[] {-1, 0, 1, -1, 0},
                new short[] {0, 1, -1, 1, 0}
            };

            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] {0, 1, 2, 3}, result[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, result[1]);
        }

        // 3 ← 0 → 1
        //     ↑   ↓
        //      -- 2
        [Test]
        public void GraphWithCycleAndRemoteVertexTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, 0, 1, -1},
                new short[] {0, 1, -1, 0},
                new short[] {0, -1, 0, 1},
                new short[] {-1, 0, 0, 0}
            };

            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1, 2}, result[0]);
        }

        // 0 ← 1 ↔ 2
        // ↓   ↑   ↑
        // 4 → 3 --
        [Test]
        public void HardGraphWithThreeCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {0,   0,  0, -1,  1,  0,  0},
                new short[] {0,   1, -1,  1,  0,  0, -1},
                new short[] {-1, -1,  1,  0,  0,  0,  0},
                new short[] {1,   0,  0,  0,  0, -1,  1},
                new short[] {0,   0,  0,  0, -1,  1,  0}
            };

            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(new[] {2, 1}, result[0]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 2, 1}, result[1]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 1}, result[2]);
        }

        [Test]
        public void VeryGraphWithEightCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                //            0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19
                new short[] { 1,  1,  0,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0},//0
                new short[] {-1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//1
                new short[] { 0,  0,  0, -1,  1,  1, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//2
                new short[] { 0,  0,  0,  0,  0, -1,  1,  1, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//3
                new short[] { 0,  0, -1,  1,  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//4
                new short[] { 0, -1,  1,  0,  0,  0,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//5
                new short[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1, -1,  0,  0},//6
                new short[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1, -1},//7
                new short[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1},//8
                new short[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0,  1,  0, -1,  0,  0,  0},//9
                new short[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0,  0,  0,  0,  0,  0},//10
                new short[] { 0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0,  0,  0, -1,  0,  0,  0,  0,  0},//11
                new short[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0, -1,  0,  0,  0,  0,  0,  0},//12
            };

            var result = searcher.FindCycles(incedenceMatrix).ToList();

            Assert.That(result.Count, Is.EqualTo(8));
            CollectionAssert.AreEqual(new[] {0, 5, 4, 2}, result[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, result[1]);
            CollectionAssert.AreEqual(new[] { 5, 4, 2, 3 }, result[2]);
            CollectionAssert.AreEqual(new[] { 0, 5, 4, 3, 2 }, result[3]);
            CollectionAssert.AreEqual(new[] { 5, 4, 3 }, result[4]);
            CollectionAssert.AreEqual(new[] {12, 9, 10}, result[5]);
            CollectionAssert.AreEqual(new[] {11, 12, 9}, result[6]);
            CollectionAssert.AreEqual(new[] {7, 8}, result[7]);
        }
    }
}