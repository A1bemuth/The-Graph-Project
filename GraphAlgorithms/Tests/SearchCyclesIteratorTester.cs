using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class SearchCyclesIteratorTester
    {
        // 0 → 1 → 2
        [Test]
        public void SeparateThreeVertexGraphWithoutCycles()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, 0},
                new short[] {-1, 1},
                new short[] {0, -1}
            };
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(0));
            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(0));
        }

        // 0 ↔ 1
        [Test]
        public void SeparateTwoVertexGraphWithCycles()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, -1},
                new short[] {-1, 1}
            };
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(0));
            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, cyclesIterator.Cycles[0]);
        }

        // 0 → 1 ↔ 2
        [Test]
        public void SeparateThreeVertexGraphWithCycleBetweenSecondAndThirdTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, 0, 0},
                new short[] {-1, 1, -1},
                new short[] {0, -1, 1}
            };
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(0));
            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {1, 2}, cyclesIterator.Cycles[0]);
        }

        // 0 ↔ 1 ← 2
        [Test]
        public void SeparateThreeVertexGraphWithCycleBetweenFirstAndSecond()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, -1, 0},
                new short[] {-1, 1, -1},
                new short[] {0, 0, 1}
            };
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(1));
            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, cyclesIterator.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 1}, cyclesIterator.Segments[0]);
        }

        // 0 ← 3 ↔ 2
        // ↓       ↑
        // 1 ------
        [Test]
        public void SeparateOnePathAndOneSegmentTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {0, -1, 0, 0, 1},
                new short[] {1, 0, 0, 0, -1},
                new short[] {-1, 0, 1, -1, 0},
                new short[] {0, 1, -1, 1, 0}
            };
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(2));
            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2, 3}, cyclesIterator.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, cyclesIterator.Cycles[1]);
        }

        // 3 ← 0 → 1
        //     ↑   ↓
        //      -- 2
        [Test]
        public void SeparateGraphWithCycleAndRemoteVertexTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, 0, 1, -1},
                new short[] {0, 1, -1, 0},
                new short[] {0, -1, 0, 1},
                new short[] {-1, 0, 0, 0}
            };
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(1));
            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2}, cyclesIterator.Cycles[0]);
        }

        // 0 ← 1 ↔ 2
        // ↓   ↑   ↑
        // 4 → 3 --
        [Test]
        public void SeparateHardGraphWithThreeCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {0,   0,  0, -1,  1,  0,  0},
                new short[] {0,   1, -1,  1,  0,  0, -1},
                new short[] {-1, -1,  1,  0,  0,  0,  0},
                new short[] {1,   0,  0,  0,  0, -1,  1},
                new short[] {0,   0,  0,  0, -1,  1,  0}
            };
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(2));
            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2, 1}, cyclesIterator.Cycles[0]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 2, 1}, cyclesIterator.Cycles[1]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 1}, cyclesIterator.Segments[0]);
        }

        [Test]
        public void SeparateVeryGraphWithEightCyclesTest()
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
            var cyclesIterator = new SearchCyclesIterator(incedenceMatrix);

            cyclesIterator.Iterate();

            Assert.That(cyclesIterator.Cycles.Count, Is.EqualTo(6));
            Assert.That(cyclesIterator.Segments.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(new[] {0, 5, 4, 2}, cyclesIterator.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, cyclesIterator.Cycles[1]);
            CollectionAssert.AreEqual(new[] { 5, 4, 2, 3 }, cyclesIterator.Cycles[2]);
            CollectionAssert.AreEqual(new[] {12, 9, 10}, cyclesIterator.Cycles[3]);
            CollectionAssert.AreEqual(new[] {11, 12, 9}, cyclesIterator.Cycles[4]);
            CollectionAssert.AreEqual(new[] {7, 8}, cyclesIterator.Cycles[5]);
            CollectionAssert.AreEqual(new[] {0, 5, 4, 3}, cyclesIterator.Segments[0]);
            CollectionAssert.AreEqual(new[] {0, 6, 9}, cyclesIterator.Segments[1]);
            CollectionAssert.AreEqual(new[] {7, 6}, cyclesIterator.Segments[2]);
        }
    }
}