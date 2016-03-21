using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        private GraphIterator iterator;

        internal List<int[]> Cycles { get; private set; }
        internal List<int[]> Segments { get; private set; }

        public short[][] IncedenceMatrix
        {
            set
            {
                iterator = new GraphIterator(value);
                iterator.VisitVisitedVertex += DefineCycleOrSegment;
                Cycles = new List<int[]>();
                Segments = new List<int[]>();
            }
        }

        public CyclesSearcher()
        {
            Cycles = new List<int[]>();
            Segments = new List<int[]>();
            
        }

        public CyclesSearcher(short[][] incedenceMatrix) : this()
        {
            iterator = new GraphIterator(incedenceMatrix);
            iterator.VisitVisitedVertex += DefineCycleOrSegment;
        }

        private void DefineCycleOrSegment(GraphIteratorEventArgs args)
        {
            var previousVertexIndex = FindPreviousIndex(args.CurrentSequence, args.CurrentVertex);
            if (IsCycle(previousVertexIndex))
            {
                var cycle = args.CurrentSequence.Skip(previousVertexIndex);
                Cycles.Add(cycle.ToArray());
            }
            else
            {
                var segment = new List<int>(args.CurrentSequence) { args.CurrentVertex };
                Segments.Add(segment.ToArray());
            }
        }

        private bool IsCycle(int previousVertexIndex)
        {
            return previousVertexIndex != -1;
        }

        private int FindPreviousIndex(List<int> currentSequence, int currentVertex)
        {
            return currentSequence
                .IndexOf((v, index) => v == currentVertex && index < currentSequence.Count - 1);
        }

        public void FindCycles()
        {
            iterator.IterateAllGraph();
        }
    }
}