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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Cycles.Count, Is.EqualTo(0));
            Assert.That(searcher.Segments.Count, Is.EqualTo(0));
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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Segments.Count, Is.EqualTo(0));
            Assert.That(searcher.Cycles.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, searcher.Cycles[0]);
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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Segments.Count, Is.EqualTo(0));
            Assert.That(searcher.Cycles.Count, Is.EqualTo(1));
            Assert.That(searcher.NewCycles.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {1, 2}, searcher.Cycles[0]);
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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Cycles.Count, Is.EqualTo(1));
            Assert.That(searcher.Segments.Count, Is.EqualTo(1));
            Assert.That(searcher.NewCycles.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1}, searcher.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 1}, searcher.Segments[0]);
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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Cycles.Count, Is.EqualTo(2));
            Assert.That(searcher.Segments.Count, Is.EqualTo(0));
            Assert.That(searcher.NewCycles.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2, 3}, searcher.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, searcher.Cycles[1]);
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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Cycles.Count, Is.EqualTo(1));
            Assert.That(searcher.Segments.Count, Is.EqualTo(0));
            Assert.That(searcher.NewCycles.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2}, searcher.Cycles[0]);
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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Cycles.Count, Is.EqualTo(2));
            Assert.That(searcher.Segments.Count, Is.EqualTo(1));
            Assert.That(searcher.NewCycles.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2, 1}, searcher.Cycles[0]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 2, 1}, searcher.Cycles[1]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 1}, searcher.Segments[0]);
            CollectionAssert.AreEqual(new[] { 0, 4, 3, 1 }, searcher.NewCycles[0]);
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
            searcher.IncedenceMatrix = incedenceMatrix;

            searcher.FindCycles();

            Assert.That(searcher.Cycles.Count, Is.EqualTo(6));
            Assert.That(searcher.Segments.Count, Is.EqualTo(3));
            Assert.That(searcher.NewCycles.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] {0, 5, 4, 2}, searcher.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, searcher.Cycles[1]);
            CollectionAssert.AreEqual(new[] { 5, 4, 2, 3 }, searcher.Cycles[2]);
            CollectionAssert.AreEqual(new[] {12, 9, 10}, searcher.Cycles[3]);
            CollectionAssert.AreEqual(new[] {11, 12, 9}, searcher.Cycles[4]);
            CollectionAssert.AreEqual(new[] {7, 8}, searcher.Cycles[5]);
            CollectionAssert.AreEqual(new[] {0, 5, 4, 3}, searcher.Segments[0]);
            CollectionAssert.AreEqual(new[] {0, 6, 9}, searcher.Segments[1]);
            CollectionAssert.AreEqual(new[] {7, 6}, searcher.Segments[2]);
            CollectionAssert.AreEqual(new[] {0, 5, 4, 3, 2}, searcher.NewCycles[0]);
            CollectionAssert.AreEqual(new[] { 5, 4, 3 }, searcher.NewCycles[1]);
        }
    }
}