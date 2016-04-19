using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDataLayer
{
    public class AdjacencyGraph : NamedGraph
    {
        private readonly Dictionary<int, Tuple<HashSet<int>, HashSet<int>>> vertices;

        private int arrowsCount;

        public AdjacencyGraph() : this(0)
        {
        }

        public AdjacencyGraph(int verticesCount)
        {
            vertices = Enumerable.Range(0, verticesCount)
                .ToDictionary(k => k, v => Tuple.Create(new HashSet<int>(), new HashSet<int>()));
        }

        public override int VerticesCount => vertices.Count;

        public override int ArrowsCount => arrowsCount;

        public override Graph AddVertices(int count)
        {
            if (count < 0)
                throw new ArgumentException("Count can not be negative.");
            
            var startIndex = VerticesCount;
            for (var i = startIndex; i < startIndex + count; i++)
            {
                vertices.Add(i, Tuple.Create(new HashSet<int>(), new HashSet<int>()));
            }
            return this;
        }

        public override Graph AddArrow(int from, int to)
        {
            if (from < 0 || to < 0)
                throw new ArgumentException("Vertex index can not be negative.");
            if (from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if(!vertices.ContainsKey(from))
                throw new ArgumentException("Arrow start vertex does not exist.");
            if(!vertices.ContainsKey(to))
                throw new ArgumentException("Arrow end vertex does not exist.");

            if (vertices[from].Item1.Add(to))
            {
                vertices[to].Item2.Add(from);
                arrowsCount++;
            }
            return this;
        }

        public override List<int> GetNeighbours(int vertice)
        {
            if (vertice < 0)
                throw new ArgumentException("Vertex index can not be negative.");
            if (!vertices.ContainsKey(vertice))
                throw new ArgumentException("Vertex does not exist.");

            return vertices[vertice].Item1.ToList();
        }

        public override List<int> GetIncomingVertex(int vertice)
        {
            if (vertice < 0)
                throw new ArgumentException("Vertex index can not be negative.");
            if (!vertices.ContainsKey(vertice))
                throw new ArgumentException("Arrow start vertex does not exist.");

            return vertices[vertice].Item2.ToList();
        }

        public override List<int> GetConnectedVertices(int vertice)
        {
            var connectedVertices = new List<int>(vertices[vertice].Item1);
            connectedVertices.AddRange(vertices[vertice].Item2
                .Where(vertex => !vertices[vertice].Item1.Contains(vertex)));
            return connectedVertices;
        }

        public override short[,] GetIncidenceMatrix()
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

        public override bool HasArrow(int from, int to)
        {
            if (from < 0 || to < 0)
                throw new ArgumentException("Vertex index can not be negative.");
            if (from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if (!vertices.ContainsKey(from))
                throw new ArgumentException("Arrow start vertex does not exist.");
            if (!vertices.ContainsKey(to))
                throw new ArgumentException("Arrow end vertex does not exist.");

            return vertices[from].Item1.Contains(to);
        }

        public override bool HasConnection(int firstVertex, int secondVertex)
        {
            return HasArrow(firstVertex, secondVertex)
                   || HasIncomingArrow(firstVertex, secondVertex);
        }

        public bool HasIncomingArrow(int from, int to)
        {
            if (from < 0 || to < 0)
                throw new ArgumentException("Vertex index can not be negative.");
            if (from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if (!vertices.ContainsKey(from))
                throw new ArgumentException("Arrow start vertex does not exist.");
            if (!vertices.ContainsKey(to))
                throw new ArgumentException("Arrow end vertex does not exist.");

            return vertices[from].Item2.Contains(to);
        }

        public override bool AreReciprocal(int verticeOne, int verticeTwo)
        {
            return HasArrow(verticeOne, verticeTwo)
                && HasArrow(verticeTwo, verticeOne);
        }
    }
}