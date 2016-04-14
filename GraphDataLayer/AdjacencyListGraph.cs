using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDataLayer
{
    public class AdjacencyListGraph : IGraph
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

        public IGraph AddArrow(int from, int to)
        {
            if(from == to)
                throw new ArgumentException("Vertice can not point to itself.");
            if (!vertices[from].Contains(to))
                vertices[from].Add(to);
            return this;
        }

        public IGraph AddVertices(int verticesCount)
        {
            Array.Resize(ref vertices, vertices.Length + verticesCount);
            return this;
        }

        public List<int> GetNeighbours(int vertice)
        {
            return vertices[vertice];
        }

        public List<int> GetIncomingVertex(int vertice)
        {
            throw new NotImplementedException();
        }

        public List<int> GetConnectedVertices(int vertice)
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

        public short[,] GetIncidenceMatrix()
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

        public bool HasArrow(int from, int to)
        {
            return vertices[from].Contains(to);
        }

        public bool HasConnection(int @from, int to)
        {
            throw new NotImplementedException();
        }

        public bool AreReciprocal(int verticeOne, int verticeTwo)
        {
            return HasArrow(verticeOne, verticeTwo) 
                && HasArrow(verticeTwo, verticeOne);
        }

        public int VerticesCount => vertices.Length;
        public int ArrowsCount => vertices.Sum(arrows => arrows?.Count ?? 0);

        private List<int>[] vertices;
    }
}
