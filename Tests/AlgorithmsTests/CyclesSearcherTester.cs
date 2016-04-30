using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphAlgorithms;
using GraphDataLayer;
using GraphDataLayer.ExcelImport;
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
//            var expectedResult = new[] {new[] {1, 0}};

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(0));
           // Assert.That(CompareResult(expectedResult, result));
        }

        private bool CompareResult(IEnumerable<int[]> expectedResult, IEnumerable<int[]> actualResult)
        {
            var comparer = new CycleComparer();
            return actualResult.All(r => expectedResult.Contains(r, comparer));
        }

        // 0 → 1 ↔ 2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenSecondAndThirdTest()
        {
            var graph = new AdjacencyListGraph(3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 1);
//            var expectedResult = new[] {new[] {1, 2}};

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(0));
//            Assert.That(CompareResult(expectedResult, result));
        }

        // 0 ↔ 1 ← 2
        [Test]
        public void ThreeVertexGraphWithCycleBetweenFirstAndSecondTest()
        {
            var graph = new AdjacencyListGraph(3);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 0);
            graph.AddArrow(2, 1);
            //var expectedResult = new[] {new[] {0, 1}};


            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(0));
            //Assert.That(CompareResult(expectedResult, result));
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

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EquivalentTo(new[] { 3, 2, 1, 0 }));
            //Assert.That(result[1], Is.EquivalentTo(new[] { 3, 2 }));
        }

        // 3 ← 0 → 1
        //     ↑   ↓
        //      -- 2
        [Test]
        public void GraphWithCycleAndRemoteVertexTest()
        {
            var graph = new AdjacencyListGraph(4)
                .AddArrow(0, 3)
                .AddArrow(0, 1)
                .AddArrow(1, 2)
                .AddArrow(2, 0);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EquivalentTo(new[] { 0, 1, 2 }));
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

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EquivalentTo(new[] {0, 4, 3, 1}));
//            Assert.That(result[1], Is.EquivalentTo(new[] {2, 1}));
            Assert.That(result[1], Is.EquivalentTo(new[] {0, 4, 3, 2, 1}));
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

            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result[0], Is.EquivalentTo(new[] { 0, 5, 4, 2 }));
//            Assert.That(result[1], Is.EquivalentTo(new[] { 2, 3 }));
            Assert.That(result[1], Is.EquivalentTo(new[] { 5, 4, 2, 3 }));
            Assert.That(result[2], Is.EquivalentTo(new[] { 0, 5, 4, 3, 2 }));
            Assert.That(result[3], Is.EquivalentTo(new[] { 5, 4, 3 }));
            Assert.That(result[4], Is.EquivalentTo(new[] { 12, 9, 10 }));
            Assert.That(result[5], Is.EquivalentTo(new[] { 11, 12, 9 }));
//            Assert.That(result[7], Is.EquivalentTo(new[] { 7, 8 }));
        }

        [Test]
        public void BigGrahpWithThreeCyclesTest()
        {
            var graph = new AdjacencyListGraph(8)
                .AddArrow(0, 1)
                .AddArrow(1, 2)
                .AddArrow(2, 0)
                .AddArrow(3, 1)
                .AddArrow(3, 2)
                .AddArrow(5, 2)
                .AddArrow(4, 3)
                .AddArrow(3, 4)
                .AddArrow(4, 5)
                .AddArrow(6, 5)
                .AddArrow(5, 6)
                .AddArrow(7, 4);

            var result = searcher.FindCycles(graph);

            Assert.That(result.Count, Is.EqualTo(1));
            //CollectionAssert.AreEqual(new[] {0, 1, 2}, result[0]);
            //CollectionAssert.AreEqual(new[] {3, 4}, result[1]);
            //CollectionAssert.AreEqual(new[] {5, 6}, result[2]);
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

            Assert.That(result.Count, Is.EqualTo(10));
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

            Assert.That(result.Count, Is.EqualTo(1));
            //CollectionAssert.AreEqual(new[] { 0, 1, 2 }, result[0]);
            //CollectionAssert.AreEqual(new[] { 3, 4 }, result[1]);
            //CollectionAssert.AreEqual(new[] { 5, 6 }, result[2]);
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

        [Test]
        public void PerformanceTest()
        {
            var fileNmae = $"{AppDomain.CurrentDomain.BaseDirectory}\\TestSamples\\муниципалитет.xlsx";
            var graph = new NamedExcelImporter<AdjacencyGraph>().GetGraphs(fileNmae)[0];
            var timer = new Stopwatch();

            timer.Start();
            var result = graph.FindCycles();
            timer.Stop();

            Console.WriteLine($"Elapsed time: {timer.Elapsed}");
            Console.WriteLine($"Total cycles: {result.Count}");
        }

        [Test]
        public async Task VeryHardGraphAsyncTest()
        {
            var graph = new AdjacencyGraph(15)
                .AddArrow(8, 4)
                .AddArrow(3, 4)
                .AddArrow(5, 10)
                .AddArrow(0, 1)
                .AddArrow(11, 9)
                .AddArrow(9, 11)
                .AddArrow(14, 7)
                .AddArrow(1, 3)
                .AddArrow(10, 6)
                .AddArrow(11, 5)
                .AddArrow(5, 11)
                .AddArrow(8, 13)
                .AddArrow(13, 8)
                .AddArrow(4, 2)
                .AddArrow(2, 0)
                .AddArrow(9, 12)
                .AddArrow(12, 9)
                .AddArrow(12, 8)
                .AddArrow(8, 12)
                .AddArrow(7, 13)
                .AddArrow(1, 6)
                .AddArrow(6, 14)
                .AddArrow(0, 5)
                .AddArrow(9, 2)
                .AddArrow(7, 3);

            var progressChangedInvockingCount = 0;
            var result =
                await
                    graph.FindCyclesAsync(new Progress<int[]>(cycle =>
                    {
                        Console.WriteLine(string.Join(",", cycle));
                        progressChangedInvockingCount++;
                    }),
                        CancellationToken.None);

            Assert.That(result.Count, Is.EqualTo(10));
            Assert.That(progressChangedInvockingCount, Is.EqualTo(10));
        }
    }
}