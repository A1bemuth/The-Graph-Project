using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        private List<int[]> cycles;
        private IGraph graph;
        public short[][] IncedenceMatrix { get; set; }

        public CyclesSearcher()
        {
        }

        public CyclesSearcher(IGraph graph) : this()
        {
            this.graph = graph;
        }

        public List<int[]> FindCycles()
        {
            if(graph == null)
                throw new NullReferenceException("The graph is null");

            cycles = new List<int[]>();
            //var startingIterator = new GraphIterator(IncedenceMatrix);
            var iterator = new GraphIterator(graph);
            iterator.CycleDetected += DefineNewCycle;
            iterator.IterateAllGraph();
            return cycles;
        }

        public List<int[]> FindCycles(IGraph graph)
        {
            this.graph = graph;
            return FindCycles();
        }

        public IEnumerable<int[]> FindCycles(short[][] incedenceMatrix)
        {
            //IncedenceMatrix = incedenceMatrix;
            //return FindCycles();
            throw new NotImplementedException();
        }


        private void DefineNewCycle(int[] cycle)
        {
            if (IsNewCycle(cycle))
                cycles.Add(cycle);
        }

        private bool IsNewCycle(int[] cycle)
        {
            var comparer = new CycleComparer();
            return cycles.All(intse => !comparer.Equals(cycle, intse));
        }
    }
}