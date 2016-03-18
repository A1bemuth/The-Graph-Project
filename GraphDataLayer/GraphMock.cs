using System;
using System.Collections.Generic;

namespace GraphDataLayer
{
    class GraphMock 
    {
        public void AddArrow(int @from, int to)
        {
            throw new NotImplementedException();
        }

        public void AddVertice()
        {
            throw new NotImplementedException();
        }

        public void AddVertices(int count)
        {
            throw new NotImplementedException();
        }

        public List<int> GetNeighbours(int vertice)
        {
            throw new NotImplementedException();
        }

        public byte[][] GetIncidenceMatrix()
        {
            throw new NotImplementedException();
        }

        public bool HasArrow(int @from, int to)
        {
            throw new NotImplementedException();
        }

        public bool AreReciprocal(int verticeOne, int verticeTwo)
        {
            throw new NotImplementedException();
        }
    }
}
