using System.Collections.Generic;

namespace GraphDataLayer
{
    public interface IGraph
    {
        int VerticesCount { get; }

        int EdgesCount { get; }

        void AddArrow(int from, int to);

        void AddVertice();

        void AddVertices(int count);

        List<int> GetNeighbours(int vertice);

        short[][] GetIncidenceMatrix();

        bool HasArrow(int from, int to);

        bool AreReciprocal(int verticeOne, int verticeTwo);
    }
}
