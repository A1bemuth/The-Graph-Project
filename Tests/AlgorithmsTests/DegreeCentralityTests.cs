using System;
using GraphDataLayer;
using GraphAlgorithms;
using NUnit.Framework;

namespace Tests.AlgorithmsTests
{
    [TestFixture]
    class DegreeCentralityTests
    {
        [Test]
        public void TestMaximumCentrality() //ну и хватит
        {
            var graph = FullCentralGraph();
            var centrality = new DegreeCentrality(graph);

            var graphIn = centrality.GetGraphIndegreeCentrality();
            var graphOut = centrality.GetGraphOutdegreeCentrality();
            var verticeIn = centrality.GetVerticeIndegreeCentrality(0);
            var verticeOut = centrality.GetVerticeOutdegreeCentrality(0);
            Assert.That(graphIn, Is.EqualTo(graphOut).And.EqualTo(1));
            Assert.That(verticeIn, Is.EqualTo(verticeOut).And.EqualTo(3));
        }

        private static Graph FullCentralGraph()
        {
            var graph = (Graph) new AdjacencyGraph(4);
            for (int i = 1; i < 4; i++)
            {
                graph = graph.AddArrow(0, i);
                graph = graph.AddArrow(i, 0);
            }
            return graph;
        }
    }
}
