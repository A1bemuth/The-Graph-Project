using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataLayer
{
    public class GraphStructure : IGraph
    {
        private short[][] incidenceMatrix;


        public int VerticesCount
        {
            get
            {
                return GetIncidenceMatrix().Length;
            }
        }
        public int EdgesCount
        {
            get
            {
                return GetIncidenceMatrix()[0].Length;
            }
        }

        public GraphStructure(short[][] incidenceMatrix)
        {
            this.incidenceMatrix = incidenceMatrix;
        }

        public void AddArrow(int from, int to)
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

        public bool AreReciprocal(int verticeOne, int verticeTwo)
        {
            throw new NotImplementedException();
        }

        public short[][] GetIncidenceMatrix()
        {
            return incidenceMatrix;
        }

        public List<int> GetNeighbours(int vertice)
        {
            throw new NotImplementedException();
        }

        public bool HasArrow(int from, int to)
        {
            throw new NotImplementedException();
        }
    }
}
