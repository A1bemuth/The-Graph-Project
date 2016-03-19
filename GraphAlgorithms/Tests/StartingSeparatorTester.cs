using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class StartingSeparatorTester
    {
        // 0 → 1 → 2
        [Test]
        public void SeparateThreeVertexGraphWithoutCycles()
        {
            var incedenceMatrix = new[]
            {
                new[] {1, 0},
                new[] {-1, 1},
                new[] {0, -1}
            };
            var separator = new StartingSeparator(incedenceMatrix);

            separator.Separate();

            Assert.That(separator.Cycles.Count, Is.EqualTo(0));
            Assert.That(separator.Segments.Count, Is.EqualTo(0));
        }

        // 0 ↔ 1
        [Test]
        public void SeparateTwoVertexGraphWithCycles()
        {
            var incedenceMatrix = new[]
            {
                new[] {1, -1},
                new[] {-1, 1}
            };
            var separator = new StartingSeparator(incedenceMatrix);

            separator.Separate();

            Assert.That(separator.Segments.Count, Is.EqualTo(0));
            Assert.That(separator.Cycles.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, separator.Cycles[0]);
        }

        // 0 → 1 ↔ 2
        [Test]
        public void SeparateThreeVertexGraphWithCycleBetweenSecondAndThirdTest()
        {
            var incedenceMatrix = new[]
            {
                new[] {1, 0, 0},
                new[] {-1, 1, -1},
                new[] {0, -1, 1}
            };
            var separator = new StartingSeparator(incedenceMatrix);

            separator.Separate();

            Assert.That(separator.Segments.Count, Is.EqualTo(0));
            Assert.That(separator.Cycles.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {1, 2}, separator.Cycles[0]);
        }

        // 0 ↔ 1 ← 2
        [Test]
        public void SeparateThreeVertexGraphWithCycleBetweenFirstAndSecond()
        {
            var incedenceMatrix = new[]
            {
                new[] {1, -1, 0},
                new[] {-1, 1, -1},
                new[] {0, 0, 1}
            };
            var separator = new StartingSeparator(incedenceMatrix);

            separator.Separate();

            Assert.That(separator.Cycles.Count, Is.EqualTo(1));
            Assert.That(separator.Segments.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, separator.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 1}, separator.Segments[0]);
        }

        // 0 ← 3 ↔ 2
        // ↓       ↑
        // 1 ------
        [Test]
        public void SeparateOnePathAndOneSegmentTest()
        {
            var incedenceMatrix = new[]
            {
                new[] {0, -1, 0, 0, 1},
                new[] {1, 0, 0, 0, -1},
                new[] {-1, 0, 1, -1, 0},
                new[] {0, 1, -1, 1, 0}
            };
            var separator = new StartingSeparator(incedenceMatrix);

            separator.Separate();

            Assert.That(separator.Cycles.Count, Is.EqualTo(2));
            Assert.That(separator.Segments.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2, 3}, separator.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, separator.Cycles[1]);
        }

        // 3 ← 0 → 1
        //     ↑   ↓
        //      -- 2
        [Test]
        public void SeparateGraphWithCycleAndRemoteVertexTest()
        {
            var incedenceMatrix = new[]
            {
                new[] {1, 0, 1, -1},
                new[] {0, 1, -1, 0},
                new[] {0, -1, 0, 1},
                new[] {-1, 0, 0, 0}
            };
            var separater = new StartingSeparator(incedenceMatrix);

            separater.Separate();

            Assert.That(separater.Cycles.Count, Is.EqualTo(1));
            Assert.That(separater.Segments.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2}, separater.Cycles[0]);
        }

        // 0 ← 1 ↔ 2
        // ↓   ↑   ↑
        // 4 → 3 --
        [Test]
        public void SeparateHardGraphWithThreeCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                new[] {0,   0,  0, -1,  1,  0,  0},
                new[] {0,   1, -1,  1,  0,  0, -1},
                new[] {-1, -1,  1,  0,  0,  0,  0},
                new[] {1,   0,  0,  0,  0, -1,  1},
                new[] {0,   0,  0,  0, -1,  1,  0}
            };
            var separater = new StartingSeparator(incedenceMatrix);

            separater.Separate();

            Assert.That(separater.Cycles.Count, Is.EqualTo(2));
            Assert.That(separater.Segments.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2, 1}, separater.Cycles[0]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 2, 1}, separater.Cycles[1]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 1}, separater.Segments[0]);
        }

        [Test]
        public void SeparateVeryGraphWithEightCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                //      0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19
                new[] { 1,  1,  0,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0},//0
                new[] {-1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//1
                new[] { 0,  0,  0, -1,  1,  1, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//2
                new[] { 0,  0,  0,  0,  0, -1,  1,  1, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//3
                new[] { 0,  0, -1,  1,  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//4
                new[] { 0, -1,  1,  0,  0,  0,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},//5
                new[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1, -1,  0,  0},//6
                new[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1, -1},//7
                new[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1},//8
                new[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0,  1,  0, -1,  0,  0,  0},//9
                new[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0,  0,  0,  0,  0,  0},//10
                new[] { 0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0,  0,  0, -1,  0,  0,  0,  0,  0},//11
                new[] { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1,  1,  0, -1,  0,  0,  0,  0,  0,  0},//12
            };
            var separater = new StartingSeparator(incedenceMatrix);

            separater.Separate();

            Assert.That(separater.Cycles.Count, Is.EqualTo(6));
            Assert.That(separater.Segments.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(new[] {0, 5, 4, 2}, separater.Cycles[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, separater.Cycles[1]);
            CollectionAssert.AreEqual(new[] { 5, 4, 2, 3 }, separater.Cycles[2]);
            CollectionAssert.AreEqual(new[] {12, 9, 10}, separater.Cycles[3]);
            CollectionAssert.AreEqual(new[] {11, 12, 9}, separater.Cycles[4]);
            CollectionAssert.AreEqual(new[] {7, 8}, separater.Cycles[5]);
            CollectionAssert.AreEqual(new[] {0, 5, 4, 3}, separater.Segments[0]);
            CollectionAssert.AreEqual(new[] {0, 6, 9}, separater.Segments[1]);
            CollectionAssert.AreEqual(new[] {7, 6}, separater.Segments[2]);
        }
    }
}