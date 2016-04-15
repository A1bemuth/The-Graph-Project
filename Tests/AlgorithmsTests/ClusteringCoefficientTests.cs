using System;
using System.Linq;
using GraphAlgorithms;
using GraphDataLayer;
using NUnit.Framework;
        
namespace Tests.AlgorithmsTests
{
    [TestFixture]
    class ClusteringCoefficientTests
    {
        /*  
            {
                {1, 1, 1, 0, 0, -1, 0, 0, 0, -1, 0 , 0},
                {-1, 0, 0, 1, 1, 1, 0, -1, 0, 0, 0, 0},
                {0, -1, 0, -1, 0, 0, 1, 1, 0, 0, 0, -1},
                {0, 0, -1, 0, 0, 0, -1, 0, 1, 0 ,-1 ,0},
                {0, 0, 0, 0, 0, 0, 0, 0, -1, 1, 1, 0},
                {0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 1 }
            };
        */

        [Test]
        public void SixVerticesGraphClusteringCoefficientForVertex0()
        {
            var graph = new AdjacencyGraph(6)
                 .AddArrow(0, 1)
                 .AddArrow(0, 2)
                 .AddArrow(0, 3)
                 .AddArrow(4, 0)
                 .AddArrow(3, 4)
                 .AddArrow(4, 3)
                 .AddArrow(2, 3)
                 .AddArrow(1, 2)
                 .AddArrow(2, 1)
                 .AddArrow(1, 5)
                 .AddArrow(5, 2)
                 .AddArrow(1, 0);

            ClusteringCoefficient cCoefficient = new ClusteringCoefficient(graph);
            var result = cCoefficient.GetClusteringCoefficientForVertex(0);

            Assert.That(result, Is.EqualTo(0.5));        
        }
   
        [Test]
        public void ExtensionTestForAllGraph()
        {
            var graph = new AdjacencyGraph(6)
                 .AddArrow(0, 1)
                 .AddArrow(0, 2)
                 .AddArrow(0, 3)
                 .AddArrow(4, 0)
                 .AddArrow(3, 4)
                 .AddArrow(4, 3)
                 .AddArrow(2, 3)
                 .AddArrow(1, 2)
                 .AddArrow(2, 1)
                 .AddArrow(1, 5)
                 .AddArrow(5, 2)
                 .AddArrow(1, 0);
            var expectedCoef = Math.Round((3 + 4d/3d) / 6d,4);

            var result = graph.ClusteringCoefficient();

            Assert.That(result, Is.EqualTo(expectedCoef));
        }

        [Test]
        public void ExtensionTestForEachVertex()
        {
            var graph = new AdjacencyGraph(6)
                 .AddArrow(0, 1)
                 .AddArrow(0, 2)
                 .AddArrow(0, 3)
                 .AddArrow(4, 0)
                 .AddArrow(3, 4)
                 .AddArrow(4, 3)
                 .AddArrow(2, 3)
                 .AddArrow(1, 2)
                 .AddArrow(2, 1)
                 .AddArrow(1, 5)
                 .AddArrow(5, 2)
                 .AddArrow(1, 0);
            var expectedResults = new[] {0.5, Math.Round(2d/3d,4), 0.5, Math.Round(2d/3d,4), 1, 1};

            var results = Enumerable.Range(0, graph.VerticesCount).Select(i => graph.ClusteringCoefficientFor(i));

            Assert.That(results, Is.EqualTo(expectedResults));
        }

        [Test]
        public void TestClusteringCoefficientForVertexInGraphWithAloneVertex()
        {
            var graph = new AdjacencyListGraph(4);
            graph.AddArrow(0, 3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 0);

            ClusteringCoefficient cCoefficient = new ClusteringCoefficient(graph);
            var result = cCoefficient.GetClusteringCoefficientForVertex(3);

            double expectedResult = 0d;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestClusteringCoefficientForGraphInGraphWithAloneVertex()
        {
            var graph = new AdjacencyListGraph(4);
            graph.AddArrow(0, 3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 0);

            ClusteringCoefficient cCoefficient = new ClusteringCoefficient(graph);
            var result = cCoefficient.GetClusteringCoefficientForGraph();

            double expectedResult = Math.Round((Math.Round(1d/3d,3)+1+1+0)/4d,4);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
