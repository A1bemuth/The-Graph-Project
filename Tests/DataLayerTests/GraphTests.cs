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

        [Test]
        public void TestGraphProperties()
        {
            short[][] directedGraph ={
                new short[] {1,-1,-1,0,0 },
                new short[] {0,1,0,1,-1 },
                new short[] {0, 0,1,-1,0 },
                new short[] {-1, 0,0,0,1 },
            };
            short[][] undirectedGraph ={
                new short[] {1,1,1,0,0 },
                new short[] {0,1,0,1,1 },
                new short[] {0, 0,1,1,0 },
                new short[] {1, 0,0,0,1 },
            };
            var graph = new GraphStructure(directedGraph);
            Assert.That(graph.VerticesCount == 4);
            Assert.That(graph.EdgesCount == 5);
            Assert.True(graph.IsDirected());

            graph = new GraphStructure(undirectedGraph);
            Assert.False(graph.IsDirected());
        }
    }
}
