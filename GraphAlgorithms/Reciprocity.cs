using System;
using GraphDataLayer;

namespace GraphAlgorithms
{
    class Reciprocity
    {
        Graph graph;

        public Reciprocity(Graph graph)
        {
            this.graph = graph; 
        }

        private int GetReciprocityCount()
        {
            int reciprocityCount = 0;
            for (int i=0; i<graph.VerticesCount;i++ )
                for (int j = 0; j < graph.VerticesCount; j++)
                {
                    if(i == j)
                        continue;
                    if (graph.AreReciprocal(i, j))
                        reciprocityCount++;
                }
            return reciprocityCount;
        }

        public double GetFirstReciprocity()
        {
            return Math.Round((double)GetReciprocityCount() / graph.ArrowsCount * 100,4);
        }

        public double GetSecondReciprocity()
        {
            return Math.Round((double)GetReciprocityCount() / (graph.VerticesCount* (graph.VerticesCount-1)) * 100, 4);
        }
    }
}
