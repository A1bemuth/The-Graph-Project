using System.Collections.Generic;

namespace GraphDataLayer.ExcelImport
{
    public class ExcelGraphInfo
    {
        public ExcelGraphInfo(string name, string[] verticeNames, int[,] matrix)
        {
            Name = name;
            VerticeNames = verticeNames;
            Matrix = matrix;
        }

        public string Name { get; set; }
        public string[] VerticeNames { get; set; }
        public int[,] Matrix { get; set; }

        internal MatrixType MatrixType
        {
            get
            {
                var symbols = new HashSet<int>();
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        symbols.Add(Matrix[i, j]);
                    }
                }
                switch (symbols.Count)
                {
                    case 2:
                        {
                            if (symbols.IsSubsetOf(new[] { 0, 1 }))
                                return MatrixType.AdjacencyMatrix;
                            if (symbols.IsSubsetOf(new[] { -1, 1 }))
                                return MatrixType.IncidenceMatrix;
                            return MatrixType.Invalid;
                        }
                    case 3:
                        {
                            if (symbols.IsSubsetOf(new[] { -1, 0, 1 }))
                                return MatrixType.IncidenceMatrix;
                            return MatrixType.Invalid;
                        }
                    default:
                        {
                            return MatrixType.Invalid;
                        }
                }
            }
        }
    }
}
