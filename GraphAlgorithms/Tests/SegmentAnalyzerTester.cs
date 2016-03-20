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
    }
}