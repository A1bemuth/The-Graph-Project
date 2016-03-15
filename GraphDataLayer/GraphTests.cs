using NUnit.Framework;

namespace GraphDataLayer
{
    [TestFixture]
    class GraphTests
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
