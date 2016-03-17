using GraphDataLayer;
using NUnit.Framework;

namespace Tests.DataLayerTests
{
    [TestFixture]
    internal class GraphTests
    {
        [Test]
        public void TestOneVerticeGraph()
        {
            var graph = new AdjacencyListGraph();
            
            graph.AddVertice();
            Assert.That(graph.VerticesCount == 1);
        }

        [Test]
        public void TestVerticeCanPointToMultipleVertices()
        {
            var graph = new AdjacencyListGraph();
            graph.AddVertices(3);
            graph.AddArrow(0, 1);
            graph.AddArrow(0, 2);

            Assert.That(graph.VerticesCount == 3);
            Assert.That(graph.HasArrow(0, 1));
            Assert.That(graph.HasArrow(0, 2));
        }
    }
}
