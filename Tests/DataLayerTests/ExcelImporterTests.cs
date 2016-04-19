using System;
using GraphDataLayer;
using NUnit.Framework;

namespace Tests.DataLayerTests
{
    [TestFixture]
    class ExcelImporterTests
    {
        [Test]
        public void TwoVerticeGraphImportTest()
        {
            var importer = new ExcelImporter<AdjacencyGraph>();
            var graphs = importer.GetGraphs(Filename("2-by-2-negative-numbers.xlsx"));

            Assert.That(graphs.Count, Is.EqualTo(1));

            var graph = graphs[0];
            Assert.That(graph.HasArrow(0, 1));
            Assert.That(graph.HasArrow(1, 0));
        }

        [Test]
        public void ComplexGraphTest()
        {
            var importer = new ExcelImporter<AdjacencyListGraph>();
            var graphs = importer.GetGraphs(Filename("complex-graph.xlsx"));

            Assert.That(graphs.Count, Is.EqualTo(1));

            var graph = graphs[0];
            Assert.That(graph.ArrowsCount, Is.EqualTo(7));
            Assert.That(graph.VerticesCount, Is.EqualTo(6));
        }

        [Test]
        public void ComplexGraphDoubleConversionTest()
        {
            var importer = new ExcelImporter<AdjacencyListGraph>();
            var graph = importer.GetGraphs(Filename("complex-graph.xlsx"))[0];

            int[,] rawMatrix;
            using (var reader = new ExcelReader(Filename("complex-graph.xlsx")))
            {
                rawMatrix = reader.GetRanges()[0];
            }

            var incidenceMatrix = graph.GetIncidenceMatrix();

            Console.WriteLine("Матрица из Excel");
            Print(rawMatrix);
            Console.WriteLine("Матрица после чтения");
            Print(incidenceMatrix);
            
            Assert.That(incidenceMatrix, Is.EquivalentTo(rawMatrix));
        }

        [Test]
        public void IncidenceAndAdjacencySameGraphTest()
        {
            var importer = new ExcelImporter<AdjacencyListGraph>();
            var graphs = importer.GetGraphs(Filename("incidence-and-adjacency.xlsx"));

            var expectedArrows = 4;
            var graphFromIncidence = graphs[0];
            var graphFromAdjacency = graphs[1];
            Assert.That(graphFromIncidence.ArrowsCount, Is.EqualTo(graphFromAdjacency.ArrowsCount).And.EqualTo(expectedArrows));
            var expectedVertices = 3;
            Assert.That(graphFromIncidence.VerticesCount, Is.EqualTo(graphFromAdjacency.VerticesCount).And.EqualTo(expectedVertices));

            for (int i = 0; i < expectedVertices; i++)
            {
                for (int j = 0; j < expectedVertices; j++)
                {
                    Assert.That(graphFromIncidence.HasArrow(i, j), Is.EqualTo(graphFromAdjacency.HasArrow(i, j)));
                }
            }
        }

        private void Print(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i,j]}\t");
                }
                Console.WriteLine();
            }
        }

        private void Print(short[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i,j]}\t");
                }
                Console.WriteLine();
            }
        }

        private string Filename(string name)
        {
            return $"{System.AppDomain.CurrentDomain.BaseDirectory}\\TestSamples\\{name}";
        }
    }
}
