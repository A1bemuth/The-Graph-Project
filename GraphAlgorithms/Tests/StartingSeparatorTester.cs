using System.Linq;
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

            Assert.That(separator.Paths.Count, Is.EqualTo(0));
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
            Assert.That(separator.Paths.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, separator.Paths[0]);
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
            Assert.That(separator.Paths.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {1, 2}, separator.Paths[0]);
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

            Assert.That(separator.Paths.Count, Is.EqualTo(1));
            Assert.That(separator.Segments.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, separator.Paths[0]);
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

            Assert.That(separator.Paths.Count, Is.EqualTo(2));
            Assert.That(separator.Segments.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2, 3}, separator.Paths[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, separator.Paths[1]);
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

            Assert.That(separater.Paths.Count, Is.EqualTo(1));
            Assert.That(separater.Segments.Count, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {0, 1, 2}, separater.Paths[0]);
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

            Assert.That(separater.Paths.Count, Is.EqualTo(2));
            Assert.That(separater.Segments.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2, 1}, separater.Paths[0]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 2, 1}, separater.Paths[1]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 1}, separater.Segments[0]);
        }
    }
}