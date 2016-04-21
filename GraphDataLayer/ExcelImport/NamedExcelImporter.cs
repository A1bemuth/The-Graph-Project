using System;
using System.Collections.Generic;

namespace GraphDataLayer.ExcelImport
{
    public class NamedExcelImporter<TNamedGraph> : ExcelImporter<TNamedGraph> where TNamedGraph : NamedGraph, new()
    {
        public override List<TNamedGraph> GetGraphs(string filename)
        {
            List<ExcelGraphInfo> graphInfos;

            using (var reader = new ExcelReader(filename))
            {
                graphInfos = reader.GetGraphInfos();
            }

            var results = new List<TNamedGraph>(graphInfos.Count);
            foreach (var graphInfo in graphInfos)
            {
                var graph = Fill(graphInfo);
                SetNames(graph, graphInfo);
                results.Add(graph);
            }
            return results;
        }

        private TNamedGraph Fill(ExcelGraphInfo info)
        {
            TNamedGraph graph;
            switch (info.MatrixType)
            {
                case MatrixType.AdjacencyMatrix:
                    graph = FillFromAdjacencyMatrix(info.Matrix);
                    break;
                case MatrixType.IncidenceMatrix:
                    graph = FillFromIncidenceMatrix(info.Matrix);
                    break;
                default:
                    throw new InvalidOperationException("Файл содержит неподдерживаемый формат матрицы графа.");
            }
            return graph;
        }

        private void SetNames(TNamedGraph graph, ExcelGraphInfo info)
        {
            graph.Name = info.Name;
            for (int i = 0; i < info.VerticeNames.Length; i++)
            {
                graph[i] = info.VerticeNames[i];
            }
        }
    }
}
