using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDataLayer
{
    public class AdjacencyGraph : IGraph
    {
        private readonly Dictionary<int, Tuple<HashSet<int>, HashSet<int>>> vertices;

        public AdjacencyGraph(int verticesCount)
        {
            vertices = Enumerable.Range(0, verticesCount)
                .ToDictionary(k => k, v => Tuple.Create(new HashSet<int>(), new HashSet<int>()));
        }

        public int VerticesCount => vertices.Count;

        public int ArrowsCount { get; private set; }

        public IGraph AddVertices(int count)
        {
            var startIndex = VerticesCount;
            for (var i = startIndex; i < startIndex + count; i++)
            {
                vertices.Add(i, Tuple.Create(new HashSet<int>(), new HashSet<int>()));
            }
            return this;
        }

        public IGraph AddArrow(int from, int to)
        {
            if (from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            //if (!vertices[from].Contains(to))
            //    vertices[from].Add(to);
            if (vertices[from].Item1.Add(to))
            {
                if (vertices[to].Item2.Add(from))
                {
                    ArrowsCount++;
                }
                else
                {
                    vertices[from].Item1.Remove(to);
                }
            }
            return this;
        }

        public List<int> GetNeighbours(int vertice)
        {
            return vertices[vertice].Item1.ToList();
        }

        public List<int> GetConnectedVertices(int vertice)
        {
            var connectedVertices = new List<int>(vertices[vertice].Item1);
            connectedVertices.AddRange(vertices[vertice].Item2
                .Where(vertex => !vertices[vertice].Item1.Contains(vertex)));
            return connectedVertices;
        }

        public short[,] GetIncidenceMatrix()
        {
            var matrix = new short[VerticesCount, ArrowsCount];
            var edgeIndex = 0;
            for (var i = 0; i < VerticesCount; i++)
            {
                foreach (var neighbour in vertices[i].Item1)
                {
                    matrix[i, edgeIndex] = 1;
                    matrix[neighbour, edgeIndex] = -1;
                    edgeIndex++;
                }
            }
            return matrix;
        }

        public bool HasArrow(int from, int to)
        {
            return vertices[from].Item1.Contains(to);
        }

        public bool AreReciprocal(int verticeOne, int verticeTwo)
        {
            return HasArrow(verticeOne, verticeTwo)
                && HasArrow(verticeTwo, verticeOne);
        }
    }
}