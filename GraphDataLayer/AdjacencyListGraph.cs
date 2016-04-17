using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDataLayer
{
    public class AdjacencyListGraph : NamedGraph
    {
        public AdjacencyListGraph() : this(0)
        {
        }

        public AdjacencyListGraph(int verticeCount)
        {
            vertices = Enumerable.Repeat(0, verticeCount)
                .Select(v => new List<int>())
                .ToArray();
        }

        private AdjacencyListGraph(List<int>[] vertices)
        {
            this.vertices = vertices;
        }

        
        ///<param name="matrix">Матрица инцидентности по которой будет построен граф.</param>
        ///<exception cref="ArgumentException">Матрица инцидентности некорректна.</exception>
        public static AdjacencyListGraph FromIncidenceMatrix(int[,] matrix)
        {
            var graph = new AdjacencyListGraph(matrix.GetLength(0));
            int? from = null, to = null;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    switch (matrix[j, i])
                    {
                        case 0:
                            continue;
                        case -1:
                        {
                            if (from != null)
                                throw new ArgumentException(
                                    "Матрица инцидентности содержит более одного значения -1 в одном столбце.");
                            from = j;
                            break;
                        }
                        case 1:
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
                graph.AddArrow((int)from, (int)to);
            }
            return new AdjacencyListGraph(new List<int>[0]);
        }

        public override Graph AddArrow(int from, int to)
        {
            if(from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if (!vertices[from].Contains(to))
                vertices[from].Add(to);
            return this;
        }

        public override Graph AddVertices(int verticesCount)
        {
            Array.Resize(ref vertices, vertices.Length + verticesCount);
            return this;
        }

        public override List<int> GetNeighbours(int vertice)
        {
            return vertices[vertice];
        }

        public override List<int> GetIncomingVertex(int vertice)
        {
            throw new NotImplementedException();
        }

        public override List<int> GetConnectedVertices(int vertice)
        {
            var connectedVertices = new HashSet<int>(GetNeighbours(vertice));
            for (var i = 0; i < VerticesCount; i++)
            {
                if(i == vertice)
                    continue;
                if(vertices[i].Contains(vertice))
                    connectedVertices.Add(i);
            }
            return connectedVertices.ToList();
        }

        public override short[,] GetIncidenceMatrix()
        {
            var matrix = new short[VerticesCount, ArrowsCount];
            var edgeIndex = 0;
            for (var i = 0; i < VerticesCount; i++)
            {
                foreach (var neighbour in vertices[i])
                {
                    matrix[i, edgeIndex] = 1;
                    matrix[neighbour, edgeIndex] = -1;
                    edgeIndex++;
                }
            }
            return matrix;
        }

        public override bool HasArrow(int from, int to)
        {
            return vertices[from].Contains(to);
        }

        public override bool HasConnection(int @from, int to)
        {
            throw new NotImplementedException();
        }

        public override bool AreReciprocal(int verticeOne, int verticeTwo)
        {
            return HasArrow(verticeOne, verticeTwo) 
                && HasArrow(verticeTwo, verticeOne);
        }

        public override int VerticesCount => vertices.Length;

        public override int ArrowsCount => vertices.Sum(arrows => arrows?.Count ?? 0);
        private List<int>[] vertices;
    }
}
