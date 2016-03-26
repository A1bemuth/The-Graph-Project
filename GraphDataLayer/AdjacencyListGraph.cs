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
            if(!vertices[from].Contains(to))
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

        public short[,] GetIncidenceMatrix()
        {
            var matrix = new short[vertices.Length, ArrowsCount];
            for (int i = 0; i < vertices.Length; i++)
            {
                for (int j = 0; j < vertices[i]?.Count; j++)
                {
                    matrix[i, i + j] = -1;
                    matrix[vertices[i][j], i + j] = 1;
                }
            }
            return matrix;
        }

        public bool HasArrow(int from, int to)
        {
            return vertices[from].Contains(to);
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
