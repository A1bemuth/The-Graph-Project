using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class ClusteringCoefficient
    {
        private short clusteringCoefficicentGraph;
        private short[] clusteringCoefficientVertex;
        private short[,] matrixIncidence;

        public ClusteringCoefficient()
        {
        }

        public int GetClusteringCoefficientGraph(IGraph fGraph)
        {
            matrixIncidence = fGraph.GetIncidenceMatrix();
            for (int indexVertex = 0; indexVertex < matrixIncidence.GetLength(0); indexVertex++)
            {
                List<int> neighbour = new List<int>();
                GetNeighbour(indexVertex, neighbour);
            }
            
            return 0;
        }

        private void GetNeighbour(int fIndexVertex, List<int> fNeighbour)
        {
            var matrixBoolNeighbour = new bool[matrixIncidence.GetLength(0)];
            fNeighbour.Clear();
            for (int indexVertex = 0; indexVertex < matrixIncidence.GetLength(0); indexVertex++)
            {
                for (int indexEdge = 0; indexEdge < matrixIncidence.GetLength(1); indexEdge++)
                {
                    if (matrixIncidence[indexVertex,indexEdge]==1 || matrixIncidence[indexVertex, indexEdge] == -1)
                    {
                        
                    }
                }
            }



            return fNeighbour;
        }
    }
}
