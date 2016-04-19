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
            var initialCount = vertices.Length;
            Array.Resize(ref vertices, initialCount + verticesCount);

            for (int i = initialCount; i < verticesCount; i++)
                vertices[i] = new List<int>();
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
