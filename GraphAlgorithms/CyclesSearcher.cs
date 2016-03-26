using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        private List<int[]> cycles;

        public List<int[]> FindCycles(IGraph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            cycles = new List<int[]>();
            var iterator = new GraphIterator(graph);
            iterator.CycleDetected += DefineNewCycle;
            iterator.Iterate();
            return cycles;
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

    public static class GraphExtension
    {
        public static List<int[]> FindCycles(this IGraph graph)
        {
            var searcher = new CyclesSearcher();
            return searcher.FindCycles(graph);
        }
    }

}