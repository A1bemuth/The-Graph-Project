using System;
using System.Collections.Generic;
using System.Diagnostics;
using GraphAlgorithms;
using GraphDataLayer;
using NUnit.Framework;
        
namespace Tests.AlgorithmsTests
{
    [TestFixture]
    class ClusteringCoefficientTests
    {
        private readonly ClusteringCoefficient cCoefficient = new ClusteringCoefficient();

        [Test]
        public void returnBoolMatrixNeighbourTest()
        {
            var graph = new AdjacencyListGraph(4);
            graph.AddArrow(0, 1);
            graph.AddArrow(0, 2);
            graph.AddArrow(1, 0);
            graph.AddArrow(0, 3);

            var mia = new ClusteringCoefficient();

            var result = mia.GetClusteringCoefficientGraph(graph);
            Assert.That(3, Is.EqualTo(3));
            CollectionAssert.Contains(result, new[] {false, true,true});
        }
    }
}
