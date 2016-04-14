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
        public void SixVerticesGraphClusteringCoefficientForEverVertex()
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
            var resultArray = new double[graph.VerticesCount];
            for(int i = 0; i<graph.VerticesCount;i++)
            {
                resultArray[i] = cCoefficient.GetClusteringCoefficientForVertex(i);
            }
            var coeffArray = new double[]{0.5,(double)2/3,0.5,(double)2/3,1,1 };
            Assert.That(resultArray, Is.EqualTo(coeffArray));
        }
        [Test]
        public void SixVerticesGraphClusteringCoefficientForGraph()
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
            //var coeffArray = new double[] { 0.5, (double)2 / 3, 0.5, (double)2 / 3, 1, 1 };
            var coeffGrapg = (3 +  ((double)4 /3))/6;
            Assert.That(cCoefficient.GetClusteringCoefficientForGraph(), Is.EqualTo(coeffGrapg));
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
            var expectedCoef = (3 + (double)4 / 3) / 6;

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
            var expectedResults = new[] {0.5, (double)2/3, 0.5, (double)2/3, 1, 1};

            var results = Enumerable.Range(0, graph.VerticesCount).Select(i => graph.ClusteringCoefficientFor(i));

            Assert.That(results, Is.EqualTo(expectedResults));
        }
    }
}
