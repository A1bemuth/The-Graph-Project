using System.Linq;
using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class StartingSeparatorTester
    {
        // 1 → 2 → 3
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

            Assert.That(separator.Paths.Count(), Is.EqualTo(0));
            Assert.That(separator.Segments.Count(), Is.EqualTo(0));
        }
    }
}