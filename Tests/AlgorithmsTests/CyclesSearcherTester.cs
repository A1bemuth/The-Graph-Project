using System;
using System.Diagnostics;
using GraphAlgorithms;
using GraphDataLayer;
using NUnit.Framework;

namespace Tests.AlgorithmsTests
{
    [TestFixture]
    public class CyclesSearcherTester
    {
        private readonly CyclesSearcher searcher = new CyclesSearcher();

        // 0 → 1 → 2
        [Test]
        public void ThreeVertexGraphWithoutCycelsTest()
        {
            var graph = new AdjacencyGraph(3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        // 0 ↔ 1
        [Test]
        public void TwoVertexGraphWithCycelsTest()
        {
            var graph = new AdjacencyListGraph(2);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 0);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.Contains(result, new[] {0, 1});
        }

        // 0 → 1 ↔ 2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenSecondAndThirdTest()
        {
            var graph = new AdjacencyListGraph(3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 1);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.Contains(result, new[] {1, 2});
        }

        // 0 ↔ 1 ← 2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenFirstAndSecondTest()
        {
            var graph = new AdjacencyListGraph(3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 0);
            graph.AddArrow(2, 1);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.Contains(result, new[] { 0, 1 });
        }

        // 0 ← 3 ↔ 2
        // ↓       ↑
        // 1 ------
        [Test]
        public void OnePathAndOneSegmentTest()
        {
            var graph = new AdjacencyListGraph(4);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 3);
            graph.AddArrow(3, 0);
            graph.AddArrow(3, 2);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(2));
            CollectionAssert.Contains(result, new[] { 0, 1, 2, 3 });
            CollectionAssert.Contains(result, new[] { 2, 3 });
        }

        // 3 ← 0 → 1
        //     ↑   ↓
        //      -- 2
        [Test]
        public void GraphWithCycleAndRemoteVertexTest()
        {
            var graph = new AdjacencyListGraph(4);
            graph.AddArrow(0, 3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 0);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.Contains(result, new[] { 0, 1, 2 });
        }

        // 0 ← 1 ↔ 2
        // ↓   ↑   ↑
        // 4 → 3 --
        [Test]
        public void MediumGraphWithThreeCyclesTest()
        {
            var graph = new AdjacencyListGraph(5);
            graph.AddArrow(0, 4);
            graph.AddArrow(4, 3);
            graph.AddArrow(3, 1);
            graph.AddArrow(3, 2);
            graph.AddArrow(1, 0);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 1);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(new[] { 0, 4, 3, 1 }, result[0]);
            CollectionAssert.AreEqual(new[] { 1, 2 }, result[1]);
            CollectionAssert.AreEqual(new[] { 0, 4, 3, 2, 1 }, result[2]);
        }

        [Test]
        public void HardGraphWithEightCyclesTest()
        {
            var graph = new AdjacencyListGraph(13);
            graph.AddArrow(0, 1);
            graph.AddArrow(0, 5);
            graph.AddArrow(5, 4);
            graph.AddArrow(4, 2);
            graph.AddArrow(2, 0);
            graph.AddArrow(2, 3);
            graph.AddArrow(3, 2);
            graph.AddArrow(3, 5);
            graph.AddArrow(4, 3);
            graph.AddArrow(4, 11);
            graph.AddArrow(11, 12);
            graph.AddArrow(12, 9);
            graph.AddArrow(9, 10);
            graph.AddArrow(10, 12);
            graph.AddArrow(9, 11);
            graph.AddArrow(0, 6);
            graph.AddArrow(6, 9);
            graph.AddArrow(7, 6);
            graph.AddArrow(7, 8);
            graph.AddArrow(8, 7);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(8));
            CollectionAssert.AreEqual(new[] {0, 5, 4, 2}, result[0]);
            CollectionAssert.AreEqual(new[] {2, 3}, result[1]);
            CollectionAssert.AreEqual(new[] { 5, 4, 2, 3 }, result[2]);
            CollectionAssert.AreEqual(new[] { 0, 5, 4, 3, 2 }, result[3]);
            CollectionAssert.AreEqual(new[] { 5, 4, 3 }, result[4]);
            CollectionAssert.AreEqual(new[] {12, 9, 10}, result[5]);
            CollectionAssert.AreEqual(new[] {11, 12, 9}, result[6]);
            CollectionAssert.AreEqual(new[] {7, 8}, result[7]);
        }

        [Test]
        public void BigGrahpWithThreeCyclesTest()
        {
            var graph = new AdjacencyListGraph(8);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 0);
            graph.AddArrow(3, 1);
            graph.AddArrow(3, 2);
            graph.AddArrow(5, 2);
            graph.AddArrow(4, 3);
            graph.AddArrow(3, 4);
            graph.AddArrow(4, 5);
            graph.AddArrow(6, 5);
            graph.AddArrow(5, 6);
            graph.AddArrow(7, 4);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(new[] {0, 1, 2}, result[0]);
            CollectionAssert.AreEqual(new[] {3, 4}, result[1]);
            CollectionAssert.AreEqual(new[] {5, 6}, result[2]);
        }

        [Test]
        public void VeryHardGraphWithFifteenCyclesTest()
        {
            var graph = new AdjacencyListGraph(15);
            graph.AddArrow(8, 4);
            graph.AddArrow(3, 4);
            graph.AddArrow(5, 10);
            graph.AddArrow(0, 1);
            graph.AddArrow(11, 9);
            graph.AddArrow(9, 11);
            graph.AddArrow(14, 7);
            graph.AddArrow(1, 3);
            graph.AddArrow(10, 6);
            graph.AddArrow(11, 5);
            graph.AddArrow(5, 11);
            graph.AddArrow(8, 13);
            graph.AddArrow(13, 8);
            graph.AddArrow(4, 2);
            graph.AddArrow(2, 0);
            graph.AddArrow(9, 12);
            graph.AddArrow(12, 9);
            graph.AddArrow(12, 8);
            graph.AddArrow(8, 12);
            graph.AddArrow(7, 13);
            graph.AddArrow(1, 6);
            graph.AddArrow(6, 14);
            graph.AddArrow(0, 5);
            graph.AddArrow(9, 2);
            graph.AddArrow(7, 3);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(15));
        }

        [Test]
        public void ExtensionTest()
        {
            var graph = new AdjacencyListGraph(8);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 0);
            graph.AddArrow(3, 1);
            graph.AddArrow(3, 2);
            graph.AddArrow(5, 2);
            graph.AddArrow(4, 3);
            graph.AddArrow(3, 4);
            graph.AddArrow(4, 5);
            graph.AddArrow(6, 5);
            graph.AddArrow(5, 6);
            graph.AddArrow(7, 4);

            var result = graph.FindCycles();

            Assert.That(result.Count, Is.EqualTo(3));
            CollectionAssert.AreEqual(new[] { 0, 1, 2 }, result[0]);
            CollectionAssert.AreEqual(new[] { 3, 4 }, result[1]);
            CollectionAssert.AreEqual(new[] { 5, 6 }, result[2]);
        }

        [Test]
        public void SpecialTestForZvereva()
        {
            var graph = new AdjacencyGraph(12)
                .AddArrow(0, 6)
                .AddArrow(0, 7)
                .AddArrow(1, 0)
                .AddArrow(1, 3)
                .AddArrow(1, 4)
                .AddArrow(2, 3)
                .AddArrow(2, 11)
                .AddArrow(3, 11)
                .AddArrow(4, 11)
                .AddArrow(5, 11)
                .AddArrow(6, 5)
                .AddArrow(7, 1)
                .AddArrow(7, 2)
                .AddArrow(8, 11)
                .AddArrow(9, 10)
                .AddArrow(9, 11)
                .AddArrow(10, 11)
                .AddArrow(11, 0)
                .AddArrow(11, 1)
                .AddArrow(11, 2)
                .AddArrow(11, 3)
                .AddArrow(11, 4)
                .AddArrow(11, 5)
                .AddArrow(11, 6)
                .AddArrow(11, 7)
                .AddArrow(11, 8)
                .AddArrow(11, 9)
                .AddArrow(11, 10);
            var timer = new Stopwatch();

            timer.Start();
            var result = graph.FindCycles();
            timer.Stop();

            Console.WriteLine($"Elapsed time: {timer.Elapsed}");
            Console.WriteLine($"Total cycles: {result.Count}");
            foreach (var cycle in result)
            {
                Console.WriteLine(string.Join(" ", cycle));
            }
        }
    }
}