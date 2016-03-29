using System.Collections.Generic;

namespace GraphDataLayer
{
    public interface IGraph
    {
        int VerticesCount { get; }
<<<<<<< HEAD

        int EdgesCount { get; }

        void AddArrow(int from, int to);
=======
>>>>>>> origin/develop

        IGraph AddArrow(int from, int to);

        IGraph AddVertices(int count);

        List<int> GetNeighbours(int vertice);

<<<<<<< HEAD
        short[][] GetIncidenceMatrix();
=======
        short[,] GetIncidenceMatrix();
>>>>>>> origin/develop

        bool HasArrow(int from, int to);

        bool AreReciprocal(int verticeOne, int verticeTwo);
    }
}
