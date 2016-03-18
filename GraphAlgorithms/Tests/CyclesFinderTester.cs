using System.Linq;
using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class CyclesFinderTester
    {
        private CyclesFinder cyclesFinder;

        [SetUp]
        public void SetupTester()
        {
            cyclesFinder = new CyclesFinder();
        }

        // 1 → 2
        [Test]
        public void TwoVertexGraphWithoutCyclesTest()
        {
            var incidenceMatrix = new[]
            {
                new[] {1},
                new[] {-1}
            };

            var result = cyclesFinder.Find(incidenceMatrix);

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        // 1 → 2 → 3
        [Test]
        public void ThreeVertexGraphWithoutCyclesTest()
        {
            var incidenceMatrix = new[]
            {
                new[] {1, 0},
                new[] {-1, 1},
                new[] {0, -1}
            };

            var result = cyclesFinder.Find(incidenceMatrix);

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        // 1 → 2
        // ↑   ↓
        //  ---3
        [Test]
        public void ThreeVertexGraphWithCycleTest()
        {
            var incidenceMatrix = new[]
            {
                new[] {1, 0, -1},
                new[] {-1, 1, 0},
                new[] {0, -1, 1}
            };

            var result = cyclesFinder.Find(incidenceMatrix).ToArray();

            Assert.That(result.Length, Is.EqualTo(0));
            CollectionAssert.AreEqual(new[] {3, 2, 1}, result[0]);
        }

        // 1 ↔ 2
        [Test]
        public void TwoVertexGraphWithCycleTest()
        {
            var incidenceMatrix = new[]
            {
                new[] {1, -1},
                new[] {-1, 1}
            };

            var result = cyclesFinder.Find(incidenceMatrix).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2, 1}, result[0]);
        }

        // 1 → 2
        //     ↕
        //     3
        [Test]
        public void ThreeVertexGraphWithCycleBetweenSecondAndThirdTest()
        {
            var incidenceMatrix = new[]
            {
                new[] {1, 0, 0},
                new[] {-1, 1, -1},
                new[] {0, -1, 1}
            };

            var result = cyclesFinder.Find(incidenceMatrix).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {3, 2}, result[0]);
        }

        // 1 ↔ 2
        //     ↓
        //     3
        [Test]
        public void ThreeVertexGraphWithCycleBetweenFirstAndSecondTest()
        {
            var incidenceMatrix = new[]
            {
                new[] {1, 0, -1},
                new[] {-1, 1, 1},
                new[] {0, -1, 0}
            };

            var result = cyclesFinder.Find(incidenceMatrix).ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {2, 1}, result[0]);
        }
    }
}