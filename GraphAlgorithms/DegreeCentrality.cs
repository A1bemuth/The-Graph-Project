using GraphDataLayer;

namespace GraphAlgorithms
{
    class DegreeCentrality
    {
        public DegreeCentrality(Graph graph)
        {
            this.graph = graph;
        }

        public int GetVerticeInDegreeCentrality(int index)
        {
            return graph.GetIncomingVertex(index).Count;
        }

        public int GetVerticeOutDegreeCentrality(int index)
        {
            return graph.GetNeighbours(index).Count;
        }

        

        private Graph graph;
    }
}
