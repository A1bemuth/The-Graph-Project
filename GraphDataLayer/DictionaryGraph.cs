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
            if (count < 0)
                throw new ArgumentException("Count can't to be negative");
            
            var startIndex = VerticesCount;
            for (var i = startIndex; i < startIndex + count; i++)
            {
                vertices.Add(i, Tuple.Create(new HashSet<int>(), new HashSet<int>()));
            }
            return this;
        }

        public IGraph AddArrow(int from, int to)
        {
            if (from < 0 || to < 0)
                throw new ArgumentException("Vetrtex index can't to be negative");
            if (from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if(!vertices.ContainsKey(from))
                throw new ArgumentException("Arrow start vertex is not exists");
            if(!vertices.ContainsKey(to))
                throw new ArgumentException("Arrow end vertex is not exists");

            if (vertices[from].Item1.Add(to))
            {
                vertices[to].Item2.Add(from);
                ArrowsCount++;
            }
            return this;
        }

        public List<int> GetNeighbours(int vertice)
        {
            if (vertice < 0)
                throw new ArgumentException("Vetrtex index can't to be negative");
            if (!vertices.ContainsKey(vertice))
                throw new ArgumentException("Vertex is not exists");

            return vertices[vertice].Item1.ToList();
        }

        public List<int> GetIncomingVertex(int vertice)
        {
            if (vertice < 0)
                throw new ArgumentException("Vetrtex index can't to be negative");
            if (!vertices.ContainsKey(vertice))
                throw new ArgumentException("Vertex is not exists");

            return vertices[vertice].Item2.ToList();
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
            if (from < 0 || to < 0)
                throw new ArgumentException("Vetrtex index can't to be negative");
            if (from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if (!vertices.ContainsKey(from))
                throw new ArgumentException("Arrow start vertex is not exists");
            if (!vertices.ContainsKey(to))
                throw new ArgumentException("Arrow end vertex is not exists");

            return vertices[from].Item1.Contains(to);
        }

        public bool HasConnection(int firstVertex, int secondVertex)
        {
            return HasArrow(firstVertex, secondVertex)
                   || HasIncomingArrow(firstVertex, secondVertex);
        }

        public bool HasIncomingArrow(int from, int to)
        {
            if (from < 0 || to < 0)
                throw new ArgumentException("Vetrtex index can't to be negative");
            if (from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if (!vertices.ContainsKey(from))
                throw new ArgumentException("Arrow start vertex is not exists");
            if (!vertices.ContainsKey(to))
                throw new ArgumentException("Arrow end vertex is not exists");

            return vertices[from].Item2.Contains(to);
        }

        public bool AreReciprocal(int verticeOne, int verticeTwo)
        {
            return HasArrow(verticeOne, verticeTwo)
                && HasArrow(verticeTwo, verticeOne);
        }
    }
}