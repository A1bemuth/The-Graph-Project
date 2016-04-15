using System.Collections.Generic;
using GraphDataLayer;
using System;

namespace GraphAlgorithms
{
    public class ClusteringCoefficient
    {
        private double clusteringCoefficicentGraph;
        private short[,] matrixIncidence;
        private IGraph Graph;

        public ClusteringCoefficient(IGraph graph)
        {
            this.Graph = graph;
            clusteringCoefficicentGraph = 0;
            matrixIncidence = Graph.GetIncidenceMatrix();       
        }

        public double GetClusteringCoefficientForGraph()
        {
            for (int indexVertex = 0; indexVertex < matrixIncidence.GetLength(0); indexVertex++)    //по всем вершинам в матрице инцидентности
            {
                clusteringCoefficicentGraph += GetClusteringCoefficientForVertex(indexVertex);
            }
            
            return Math.Round(clusteringCoefficicentGraph/matrixIncidence.GetLength(0),4);
        }

        public double GetClusteringCoefficientForVertex(int fIndexVertex)
        {
            double clusteringCoefficientForVertex=0;
            var neighbour = Graph.GetConnectedVertices(fIndexVertex);      //соседи для текущей вершины
            var countNeighbour = neighbour.Count;                          //кол-во соседей

            if (countNeighbour != 1)
            {
                var countAdjacentNeighbour = GetCountAdjacentNeighbour(neighbour);
                clusteringCoefficientForVertex = (double)2 * countAdjacentNeighbour / (countNeighbour * (countNeighbour - 1));
                return Math.Round(clusteringCoefficientForVertex,4);
            }
            else
            {
                return 0d;
            }
        }

        private int GetCountAdjacentNeighbour(List<int> neighbour)
        {
            int countAdjacentNeighbour = 0;
            for (int indexCurrentNeighbour = 0; indexCurrentNeighbour < neighbour.Count; indexCurrentNeighbour++)
                //по всем соседям
            {
                for (int indexNeighbour = indexCurrentNeighbour; indexNeighbour < neighbour.Count - 1; indexNeighbour++)
                    //по всем соседям после текущего
                {
                    for (int indexEdge = 0; indexEdge < matrixIncidence.GetLength(1); indexEdge++)
                    {
                        if (matrixIncidence[neighbour[indexCurrentNeighbour], indexEdge]*
                            matrixIncidence[neighbour[indexNeighbour + 1], indexEdge] == -1)
                            //если есть связь между сеседями то ++ и шлепаем дальше
                        {
                            countAdjacentNeighbour++;
                            break;
                        }
                    }
                }
            }
            return countAdjacentNeighbour;
        }
    }
}
