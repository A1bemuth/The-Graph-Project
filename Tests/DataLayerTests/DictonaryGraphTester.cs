using System;
using GraphDataLayer;
using NUnit.Framework;

namespace Tests.DataLayerTests
{
    [TestFixture]
    public class DictonaryGraphTester
    {
        [Test]
        public void TestOneVerticeGraph()
        {
            var graph = new AdjacencyGraph(1);
            Assert.That(graph.VerticesCount, Is.EqualTo(1));
        }

        [Test]
        public void AddOneVertexTest()
        {
            var graph = new AdjacencyGraph(0)
                .AddVertices(1);

            Assert.That(graph.VerticesCount, Is.EqualTo(1));
        }

        [Test]
        public void AddSeveralVertexTest()
        {
            var graph = new AdjacencyGraph(15)
                .AddVertices(300);

            Assert.That(graph.VerticesCount, Is.EqualTo(315));
        }

        [Test]
        public void TestVerticeCanPointToMultipleVertices()
        {
            var graph = new AdjacencyGraph(3);
            graph.AddArrow(0, 1)
                .AddArrow(0, 2);

            Assert.That(graph.VerticesCount == 3);
            Assert.That(graph.HasArrow(0, 1));
            Assert.That(graph.HasArrow(0, 2));
        }

        [Test]
        public void AddExistedArrow()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(0, 1)
                .AddArrow(2, 3)
                .AddArrow(0, 1)
                .AddArrow(2, 3);

            Assert.That(graph.ArrowsCount, Is.EqualTo(2));
            Assert.That(graph.HasArrow(0, 1));
            Assert.That(graph.HasArrow(2, 3));
        }

        [Test]
        public void ThrowWhenAddArrowWithNotExistedStartTest()
        {
            var graph = new AdjacencyGraph(1);

            Assert.Throws<ArgumentException>(() => graph.AddArrow(1, 0));
        }

        [Test]
        public void ThrowWhenAddArrowWithNotExistedEndTest()
        {
            var graph = new AdjacencyGraph(1);

            Assert.Throws<ArgumentException>(() => graph.AddArrow(0, 1));
        }

        [Test]
        public void ThrowWhenAddArrowItself()
        {
            var graph = new AdjacencyGraph(1);

            Assert.Throws<ArgumentException>(() => graph.AddArrow(0, 0));
        }

        [Test]
        public void GetTwoNeighboursForFirstVertexTest()
        {
            var graph = new AdjacencyGraph(3)
                .AddArrow(0, 1)
                .AddArrow(0, 2)
                .AddArrow(2, 1);
            var expectedNeighbours = new[] {1, 2};

            Assert.That(graph.GetNeighbours(0), Is.EqualTo(expectedNeighbours));
        }

        [Test]
        public void GetEmptyNeighboursListForFourthVertexTest()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(0, 1)
                .AddArrow(0, 2)
                .AddArrow(2, 1);

            Assert.That(graph.GetNeighbours(3), Is.EqualTo(new int[0]));
        }

        [Test]
        public void ThrowWhenGetNeighboursForNotExistedVertexTest()
        {
            var graph = new AdjacencyGraph(0);

            Assert.Throws<ArgumentException>(() => graph.GetNeighbours(1));
        }

        [Test]
        public void TestReciprocalPoints()
        {
            var graph = new AdjacencyGraph(2);
            graph.AddArrow(0, 1)
                .AddArrow(1, 0);

            Assert.That(graph.AreReciprocal(0, 1));
        }

        [Test]
        public void TestOneElementIncidenceMatrix()
        {
            var graph = new AdjacencyGraph(1);
            var incidenceMatrix = graph.GetIncidenceMatrix();

            Assert.That(incidenceMatrix.GetLength(0), Is.EqualTo(1));
            Assert.That(incidenceMatrix.GetLength(1), Is.EqualTo(0));
        }

        [Test]
        public void Test3X3IncidenceMatrix()
        {
            var graph = new AdjacencyGraph(3)
                .AddArrow(0, 2)
                .AddArrow(0, 1)
                .AddArrow(2, 1);

            var incidenceMatrix = graph.GetIncidenceMatrix();
            var expectedMatrix = new short[,]
            {
                {1,    1,     0},
                {0,     -1,      -1},
                {-1,     0,      1}
            };
            Assert.That(incidenceMatrix, Is.EqualTo(expectedMatrix));
        }

        [Test]
        public void FourVerticesGraphIncedenceMatrixTest()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(0, 1)
                .AddArrow(0, 2)
                .AddArrow(1, 0)
                .AddArrow(0, 3);

            var expectedMatrix = new short[,]
            {
                {1, 1, 1, -1},
                {-1, 0, 0, 1},
                {0, -1, 0, 0},
                {0, 0, -1, 0}
            };

            Assert.That(graph.GetIncidenceMatrix(), Is.EqualTo(expectedMatrix));
        }

        [Test]
        public void FourVerticesGraphConnectedVertexForZeroVertex()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(0, 1)
                .AddArrow(0, 2)
                .AddArrow(1, 0)
                .AddArrow(0, 3);
            var expectedVertices = new[] { 1, 2, 3 };

            var actualVertices = graph.GetConnectedVertices(0);

            Assert.That(actualVertices, Is.EqualTo(expectedVertices));
        }

        [Test]
        public void FourVerticesGraphConnectedVertexForSecondVertex()
        {
            var graph = new AdjacencyGraph(4)
                .AddArrow(0, 1)
                .AddArrow(0, 2)
                .AddArrow(1, 0)
                .AddArrow(0, 3);
            var expectedVertices = new[] { 0 };

            var actualVertices = graph.GetConnectedVertices(2);

            Assert.That(actualVertices, Is.EqualTo(expectedVertices));
        }
    }
}