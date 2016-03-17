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
    }
}
