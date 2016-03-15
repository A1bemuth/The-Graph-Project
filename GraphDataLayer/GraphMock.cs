using System;

namespace GraphDataLayer
{
    class GraphMock : IGraph
    {
        public void AddArrow(long @from, long to)
        {
            throw new NotImplementedException();
        }

        public void AddVertice(long vertice)
        {
            throw new NotImplementedException();
        }

        public long[] GetNeighbours(long vertice)
        {
            throw new NotImplementedException();
        }

        public bool[][] GetIncidenceMatrix()
        {
            throw new NotImplementedException();
        }

        public bool HasArrow(long @from, long to)
        {
            throw new NotImplementedException();
        }

        public bool AreReciprocal(long verticeOne, long verticeTwo)
        {
            throw new NotImplementedException();
        }
    }
}
