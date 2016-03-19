using System.Linq;
using NUnit.Framework;

namespace GraphAlgorithms.Tests
{
    [TestFixture]
    public class CyclesSearcherTester
    { 
        // 0 → 1
        [Test]
        public void TwoVertexGraphWithoutCyclesTest()
        {
            var incidenceMatrix = new []
            {
                new short[] {1},
                new short[] {-1}
            };
            var cyclesSearcher = new CyclesSearcher(incidenceMatrix);

            var result = cyclesSearcher.FindCycles();

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        // 0 → 1 → 2
        [Test]
        public void ThreeVertexGraphWithoutCyclesTest()
        {
            var incidenceMatrix = new[]
            {
                new short[] {1, 0},
                new short[] {-1, 1},
                new short[] {0, -1}
            };
            var cyclesSearcher = new CyclesSearcher(incidenceMatrix);

            var result = cyclesSearcher.FindCycles();

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        // 0 → 1
        // ↑   ↓
        //  ---2
        [Test]
        public void ThreeVertexGraphWithCycleTest()
        {
            var incidenceMatrix = new[]
            {
                new short[] {1, 0, -1},
                new short[] {-1, 1, 0},
                new short[] {0, -1, 1}
            };
            var cyclesSearcher = new CyclesSearcher(incidenceMatrix);

            var result = cyclesSearcher.FindCycles().ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1, 2}, result[0]);
        }

        // 0 ↔ 1
        [Test]
        public void TwoVertexGraphWithCycleTest()
        {
            var incidenceMatrix = new[]
            {
                new short[] {1, -1},
                new short[] {-1, 1}
            };
            var cyclesSearcher = new CyclesSearcher(incidenceMatrix);

            var result = cyclesSearcher.FindCycles().ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, result[0]);
        }

        // 0 → 1
        //     ↕
        //     2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenSecondAndThirdTest()
        {
            var incidenceMatrix = new[]
            {
                new short[] {1, 0, 0},
                new short[] {-1, 1, -1},
                new short[] {0, -1, 1}
            };
            var cyclesSearcher = new CyclesSearcher(incidenceMatrix);

            var result = cyclesSearcher.FindCycles().ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {1, 2}, result[0]);
        }

        // 0 ↔ 1
        //     ↓
        //     2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenFirstAndSecondTest()
        {
            var incidenceMatrix = new[]
            {
                new short[] {1, 0, -1},
                new short[] {-1, 1, 1},
                new short[] {0, -1, 0}
            };
            var cyclesSearcher = new CyclesSearcher(incidenceMatrix);

            var result = cyclesSearcher.FindCycles().ToArray();

            Assert.That(result.Length, Is.EqualTo(1));
            CollectionAssert.AreEqual(new[] {0, 1}, result[0]);
        }
    }
}