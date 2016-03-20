using System.Linq;
using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class SegmentAnalyzerTester
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
            var analyzer = new SegmentAnalyzer(incedenceMatrix);

            var result = analyzer.CheckSegment(new[] {0, 1}).ToArray();

            Assert.That(result.Length, Is.EqualTo(0));
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
            var analyzer = new SegmentAnalyzer(incedenceMatrix);

            var result = analyzer.CheckSegment(new[] {0, 1}).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {1, 2}, result[0]);
        }

        // 0 ↔ 1
        // ↑   ↑
        // 2 --
        [Test]
        public void TwoVertexGraphWithCycleTest()
        {
            var incidenceMatrix = new[]
            {
                new short[] {1, -1, -1, 0},
                new short[] {-1, 1, 0, -1},
                new short[] {0, 0, 1, 1}
            };
            var firstAnalyzer = new SegmentAnalyzer(incidenceMatrix);
            var secondAnalyzer = new SegmentAnalyzer(incidenceMatrix);

            var firstResult = firstAnalyzer.CheckSegment(new[] {2, 0}).ToArray();
            var secondResult = secondAnalyzer.CheckSegment(new[] {2, 1}).ToArray();

            Assert.That(firstResult.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, firstResult[0]);
            Assert.That(secondResult.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {1, 0}, secondResult[0]);
        }

        // 0 → 1
        // ↓
        // 2 → 3
        [Test]
        public void GraphWithoutCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {1, 1, 0},
                new short[] {0, -1, 0},
                new short[] {-1, 0, 1},
                new short[] {0, 0, -1},
            };
            var analyzer = new SegmentAnalyzer(incedenceMatrix);

            var result = analyzer.CheckSegment(new[] {0, 2}).ToArray();

            Assert.That(result.Length, Is.EqualTo(0));
        }

        // 0 ← 1 ↔ 2
        // ↓   ↑   ↑
        // 4 → 3 --
        [Test]
        public void SeparateHardGraphWithThreeCyclesTest()
        {
            var incedenceMatrix = new[]
            {
                new short[] {0, 0, 0, -1, 1, 0, 0},
                new short[] {0, 1, -1, 1, 0, 0, -1},
                new short[] {-1, -1, 1, 0, 0, 0, 0},
                new short[] {1, 0, 0, 0, 0, -1, 1},
                new short[] {0, 0, 0, 0, -1, 1, 0}
            };
            var analyzer = new SegmentAnalyzer(incedenceMatrix);

            var result = analyzer.CheckSegment(new[] { 0, 4, 3, 1 }).ToArray();

            Assert.That(result.Length, Is.EqualTo(2));
            CollectionAssert.AreEqual(new[] {1, 2}, result[0]);
            CollectionAssert.AreEqual(new[] {0, 4, 3, 1}, result[1]);
        }
    }
}