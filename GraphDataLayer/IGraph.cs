namespace GraphDataLayer
{
    public interface IGraph
    {
        void AddArrow(long from, long to);

        void AddVertice();

        void AddVertices(long count);

        long[] GetNeighbours(long vertice);

        bool[][] GetIncidenceMatrix();

        bool HasArrow(long from, long to);

        bool AreReciprocal(long verticeOne, long verticeTwo);
    }
}
