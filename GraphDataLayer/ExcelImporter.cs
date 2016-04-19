using System;
using System.Collections.Generic;

namespace GraphDataLayer
{
    public class ExcelImporter<TGraph> where TGraph : Graph, new()
    {
        public List<TGraph> GetGraphs(string filename)
        {
            List<int[,]> matrices;
            using (var reader = new ExcelReader(filename))
            {
                matrices = reader.GetRanges();
            }
            List<TGraph> graphs = new List<TGraph>(matrices.Count);
            foreach (var matrix in matrices)
            {
                switch (DefineMatrixType(matrix))
                {
                    case MatrixType.AdjacencyMatrix:
                        graphs.Add(FillFromAdjacencyMatrix(matrix));
                        break;
                    case MatrixType.IncidenceMatrix:
                        graphs.Add(FillFromIncidenceMatrix(matrix));
                        break;
                    default:
                        throw new InvalidOperationException("Файл содержит неподдерживаемый формат матрицы графа.");
                }
            }
            return graphs;
        }

        private MatrixType DefineMatrixType(int[,] matrix)
        {
            var symbols = new HashSet<int>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    symbols.Add(matrix[i, j]);
                }
            }
            switch (symbols.Count)
            {
                case 2:
                {
                    if (symbols.IsSubsetOf(new[] {0, 1}))
                        return MatrixType.AdjacencyMatrix;
                    if (symbols.IsSubsetOf(new[] {-1, 1}))
                        return MatrixType.IncidenceMatrix;
                    return MatrixType.Invalid;
                }
                case 3:
                {
                    if (symbols.IsSubsetOf(new[] {-1, 0, 1}))
                        return MatrixType.IncidenceMatrix;
                    return MatrixType.Invalid;
                }
                default:
                {
                    return MatrixType.Invalid;
                }
            }
        }

        private TGraph FillFromIncidenceMatrix(int[,] matrix)
        {
            var graph = new TGraph();
            graph.AddVertices(matrix.GetLength(0));

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                int? from = null, to = null;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    switch (matrix[j, i])
                    {
                        case 0:
                            continue;
                        case 1:
                        {
                            if (from != null)
                                throw new ArgumentException(
                                    "Матрица инцидентности содержит более одного значения -1 в одном столбце.");
                            from = j;
                            break;
                        }
                        case -1:
                        {
                            if (to != null)
                                throw new ArgumentException(
                                    "Матрица инцидентности содержит более одного значения 1 в одном столбце.");
                            to = j;
                            break;
                        }
                        default:
                        {
                            throw new ArgumentException("Матрица инцидентности содержит недопустимые значения.");
                        }
                    }
                }
                if (from == null || to == null)
                    throw new ArgumentException("Матрица инцидентности содержит некорректные столбцы.");
                graph.AddArrow((int) from, (int) to);
            }
            return graph;
        }

        private TGraph FillFromAdjacencyMatrix(int[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            if (rows != cols)
                throw new ArgumentException(
                    $"Матрица смежности должна иметь одинаковые измерения. Строк: {rows}, столбцов: {cols}.");
            var graph = new TGraph();
            graph.AddVertices(rows);

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    switch (matrix[j, i])
                    {
                        case 0:
                            break;
                        case 1:
                            graph.AddArrow(j, i);
                            break;
                        default:
                            throw new ArgumentException("Матрица инцидентности содержит недопустимые значения.");
                    }
                }
            }
            return graph;
        }

        private enum MatrixType
        {
            AdjacencyMatrix,
            IncidenceMatrix,
            Invalid
        }
    }
}
