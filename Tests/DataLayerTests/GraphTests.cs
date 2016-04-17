using GraphDataLayer;
using NUnit.Framework;

namespace Tests.DataLayerTests
{
    [TestFixture]
    internal class GraphTests
    {
        private Graph GetTest3X3Graph()
        {
            return new AdjacencyListGraph(3)
                .AddArrow(0, 2)
                .AddArrow(0, 1)
                .AddArrow(2, 1);
        }

        [Test]
        public void TestOneVerticeGraph()
        {
            var graph = new AdjacencyListGraph(1);
            Assert.That(graph.VerticesCount, Is.EqualTo(1));
        }

        [Test]
        public void TestVerticeCanPointToMultipleVertices()
        {
            var graph = new AdjacencyListGraph(3);
            graph.AddArrow(0, 1)
                .AddArrow(0, 2);

            Assert.That(graph.VerticesCount == 3);
            Assert.That(graph.HasArrow(0, 1));
            Assert.That(graph.HasArrow(0, 2));
        }

        [Test]
        public void TestReciprocalPoints()
        {
            var graph = new AdjacencyListGraph(2);
            graph.AddArrow(0, 1)
                .AddArrow(1, 0);

            Assert.That(graph.AreReciprocal(0, 1));
        }

        [Test]
        public void TestOneElementIncidenceMatrix()
        {
            var graph = new AdjacencyListGraph(1);
            var incidenceMatrix = graph.GetIncidenceMatrix();
            
            Assert.That(incidenceMatrix.GetLength(0), Is.EqualTo(1));
            Assert.That(incidenceMatrix.GetLength(1), Is.EqualTo(0));
        }

        [Test]
        public void Test3X3IncidenceMatrix()
        {
            var graph = GetTest3X3Graph();

            var incidenceMatrix = graph.GetIncidenceMatrix();
            var expectedMatrix = new short[,]
            {
                { 1,     1,     0},
                { 0,    -1,    -1},
                {-1,     0,     1}
            };
            Assert.That(incidenceMatrix, Is.EqualTo(expectedMatrix));
        }

        [Test]
        public void FourVerticesGraphIncedenceMatrixTest()
        {
            var graph = new AdjacencyListGraph(4)
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
            var graph = new AdjacencyListGraph(4)
                .AddArrow(0, 1)
                .AddArrow(0, 2)
                .AddArrow(1, 0)
                .AddArrow(0, 3);
            var expectedVertices = new[] {1, 2, 3};

            var actualVertices = graph.GetConnectedVertices(0);

            Assert.That(actualVertices, Is.EqualTo(expectedVertices));
        }

        [Test]
        public void FourVerticesGraphConnectedVertexForSecondVertex()
        {
            var graph = new AdjacencyListGraph(4)
                .AddArrow(0, 1)
                .AddArrow(0, 2)
                .AddArrow(1, 0)
                .AddArrow(0, 3);
            var expectedVertices = new[] {0};

            var actualVertices = graph.GetConnectedVertices(2);

            Assert.That(actualVertices, Is.EqualTo(expectedVertices));
        }
    }
}
