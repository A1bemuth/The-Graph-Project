using GraphDataLayer;

namespace GraphAlgorithms
{
    class Reciprocity
    {
        Graph graph;

        Reciprocity(Graph graph)
        {
            this.graph = graph; 
        }

        private int GetReciprocityCount()
        {
            int reciprocityCount = 0;
            for (int i=0; i<graph.VerticesCount;i++ )
                for (int j = 0; j < graph.VerticesCount; j++)
                {
                    if (graph.AreReciprocal(i, j))
                        reciprocityCount++;
                }
            return reciprocityCount;
        }

        public double GetFirstReciprocity()
        {
            return (double)GetReciprocityCount() / graph.ArrowsCount * 100;
        }

        public double GetSecondReciprocity()
        {
            return (double)GetReciprocityCount() / (graph.VerticesCount* (graph.VerticesCount-1)) * 100;
        }
    }
}
