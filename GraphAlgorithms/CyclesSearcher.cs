using System.Collections.Generic;
using System.Linq;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        private short[][] incedenceMatrix;
        private GraphIterator iterator;
        private SegmentAnalyzer analyzer;

        internal List<int[]> Cycles { get; private set; }
        internal List<int[]> Segments { get; private set; }
        internal List<int[]> NewCycles { get; private set; }

        public short[][] IncedenceMatrix
        {
            get { return incedenceMatrix; }
            set
            {
                incedenceMatrix = value;
                iterator = new GraphIterator(value);
                iterator.VisitVisitedVertex += DefineCycleOrSegment;
                Cycles = new List<int[]>();
                Segments = new List<int[]>();
                NewCycles = new List<int[]>();
            }
        }

        public CyclesSearcher()
        {
            Cycles = new List<int[]>();
            Segments = new List<int[]>();
            NewCycles = new List<int[]>();
            
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
                analyzer = new SegmentAnalyzer(IncedenceMatrix);
                var cycles = analyzer.CheckSegment(segment.ToArray());
                foreach (var cycle in cycles)
                {
                    if(IsNewCycle(cycle))
                        NewCycles.Add(cycle);
                }
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

        private bool IsNewCycle(int[] cycle)
        {
            var comparer = new CycleComparer();
            return Cycles.All(intse => !comparer.Equals(cycle, intse));
        }

        public void FindCycles()
        {
            iterator.IterateAllGraph();
        }
    }
}