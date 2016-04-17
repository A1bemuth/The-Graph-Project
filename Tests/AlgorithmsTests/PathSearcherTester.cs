using GraphAlgorithms;
using NUnit.Framework;
using GraphDataLayer;

namespace Tests.AlgorithmsTests
{
    [TestFixture]
    public class PathSearcherTester
    {

        // 0 → 1
        [Test]
        public void TwoVerticeGraphWithoutPath()
        {
            var graph = new AdjacencyGraph(2)
                .AddArrow(0, 1);

            var path = new PathSearcher(graph).FindPath(1, 0);

            Assert.That(path, Is.Null);
        }

        // 0 → 1
        [Test]
        public void TwoVerticeGraphWithPath()
        {
            var graph = new AdjacencyGraph(2)
                .AddArrow(0, 1);

            var path = new PathSearcher(graph).FindPath(0, 1);

            Assert.That(path, Is.EqualTo(new[] {0, 1}));
        }

        // 1 ↔ 2 ← 3
        //  ↓ ↓
        //   0 
        [Test]
        public void FourVerticeGraphWithPathBetweenZeroAndFirst()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(1, 0)
                .AddArrow(2, 0)
                .AddArrow(1, 2)
                .AddArrow(2, 1)
                .AddArrow(3, 2);

            var path = new PathSearcher(graph).FindPath(2, 0);

            Assert.That(path, Is.EqualTo(new[] {2, 0}));
        }

        // 1 ↔ 2 ← 3
        //  ↓ ↓
        //   0 
        [Test]
        public void FourVerticeGraphWithPathBetweenZeroAndThird()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(1, 0)
                .AddArrow(2, 0)
                .AddArrow(1, 2)
                .AddArrow(2, 1)
                .AddArrow(3, 2);

            var path = new PathSearcher(graph).FindPath(3, 0);

            Assert.That(path, Is.EqualTo(new[] {3, 2, 0}));
        }

        // 1 ↔ 2 → 3
        //  ↓ ↓
        //   0
        [Test]
        public void FourVerticeGraphWhereNotExistsPath()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(1, 0)
                .AddArrow(2, 0)
                .AddArrow(2, 1)
                .AddArrow(2, 3);

            var path = new PathSearcher(graph).FindPath(3, 0);

            Assert.That(path, Is.Null);
        }

        [TestCase(0, 1, ExpectedResult = null)]
        [TestCase(0, 2, ExpectedResult = null)]
        [TestCase(0, 3, ExpectedResult = null)]
        [TestCase(0, 4, ExpectedResult = null)]
        [TestCase(0, 5, ExpectedResult = new[] {0, 5})]
        [TestCase(1, 0, ExpectedResult = new[] {1, 2, 0})]
        [TestCase(1, 2, ExpectedResult = new[] {1, 2})]
        [TestCase(1, 3, ExpectedResult = new[] {1, 3})]
        [TestCase(1, 4, ExpectedResult = new[] {1, 4})]
        [TestCase(1, 5, ExpectedResult = new[] {1, 3, 5})]
        [TestCase(2, 0, ExpectedResult = new[] {2, 0})]
        [TestCase(2, 1, ExpectedResult = new[] {2, 3, 1})]
        [TestCase(2, 3, ExpectedResult = new[] {2, 3})]
        [TestCase(2, 4, ExpectedResult = new[] {2, 3, 4})]
        [TestCase(2, 5, ExpectedResult = new[] {2, 0, 5})]
        [TestCase(3, 0, ExpectedResult = new[] {3, 1, 2, 0})]
        [TestCase(3, 1, ExpectedResult = new[] {3, 1})]
        [TestCase(3, 2, ExpectedResult = new[] {3, 1, 2})]
        [TestCase(3, 4, ExpectedResult = new[] {3, 4})]
        [TestCase(3, 5, ExpectedResult = new[] {3, 5})]
        [TestCase(4, 0, ExpectedResult = new[] {4, 1, 2, 0})]
        [TestCase(4, 1, ExpectedResult = new[] {4, 1})]
        [TestCase(4, 2, ExpectedResult = new[] {4, 1, 2})]
        [TestCase(4, 3, ExpectedResult = new[] {4, 1, 3})]
        [TestCase(4, 5, ExpectedResult = new[] {4, 5})]
        [TestCase(5, 0, ExpectedResult = null)]
        [TestCase(5, 1, ExpectedResult = null)]
        [TestCase(5, 2, ExpectedResult = null)]
        [TestCase(5, 3, ExpectedResult = null)]
        [TestCase(5, 4, ExpectedResult = null)]
        public int[] SixVerticeGraphWithAllPathSearching(int from, int to)
        {
            var graph = new AdjacencyGraph(6)
                .AddArrow(0, 5)
                .AddArrow(1, 2)
                .AddArrow(1, 3)
                .AddArrow(1, 4)
                .AddArrow(2, 0)
                .AddArrow(2, 3)
                .AddArrow(3, 1)
                .AddArrow(3, 4)
                .AddArrow(3, 5)
                .AddArrow(4, 1)
                .AddArrow(4, 5);

            return new PathSearcher(graph).FindPath(from, to);
        }
    }
}