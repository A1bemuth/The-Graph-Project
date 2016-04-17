using System.Collections.Generic;

namespace GraphDataLayer
{
    public abstract class Graph
    {
        public abstract int VerticesCount { get; }
        public abstract int ArrowsCount { get; }

        public abstract Graph AddArrow(int from, int to);

        public abstract Graph AddVertices(int count);

        public abstract List<int> GetNeighbours(int vertice);

        public abstract List<int> GetIncomingVertex(int vertice);

        public abstract List<int> GetConnectedVertices(int vertice);

        public abstract short[,] GetIncidenceMatrix();

        public abstract bool HasArrow(int from, int to);

        public abstract bool HasConnection(int from, int to);

        public abstract bool AreReciprocal(int verticeOne, int verticeTwo);
    }
}
