using System.Collections.Generic;

namespace GraphDataLayer
{
    public interface IGraph
    {
        IGraph AddArrow(int from, int to);

        IGraph AddVertices(int count);

        List<int> GetNeighbours(int vertice);

        short[,] GetIncidenceMatrix();

        bool HasArrow(int from, int to);

        bool AreReciprocal(int verticeOne, int verticeTwo);
    }
}
