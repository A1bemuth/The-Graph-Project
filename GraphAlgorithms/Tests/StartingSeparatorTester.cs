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
            CollectionAssert.AreEqual(new[] {0, 1, 0}, separator.Paths[0]);
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
            CollectionAssert.AreEqual(new[] {1, 2, 1}, separator.Paths[0]);
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

            Assert.That(separator.Paths.Count,Is.EqualTo(1));
            Assert.That(separator.Segments.Count,Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1, 0}, separator.Paths[0]);
            CollectionAssert.AreEqual(new[] {2, 1}, separator.Segments[0]);
        }
    }
}