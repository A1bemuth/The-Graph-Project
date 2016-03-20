using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphAlgorithms
{
    internal class SearchCyclesIterator
    {
        private readonly GraphIterator iterator;
        private bool iterationComplete;

        internal List<int[]> Cycles { get; }
        internal List<int[]> Segments { get; }

        internal SearchCyclesIterator(short[][] incedenceMatrix)
        {
            iterator = new GraphIterator(incedenceMatrix);
            Cycles = new List<int[]>();
            Segments = new List<int[]>();
            iterator.VisitVisitedVertex += DefineCycleOrSegment;
            iterator.SequenceEnded += AllVertexVisited;
        }

        private void AllVertexVisited(GraphIteratorEventArgs args)
        {
            if (ThereAreNotVisitedVertices(args.VisitedVertices))
            {
                iterator.Iterate(args.VisitedVertices.IndexesOf(v => !v).First());
            }
            else
            {
                iterationComplete = true;
            }
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

        internal void Iterate()
        {
            iterator.Iterate(0);
            while (!iterationComplete)
            {
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
        }

        private bool ThereAreNotVisitedVertices(bool[] visitedVertices)
        {
            return visitedVertices.Any(v => !v);
        }

        private int FindPreviousIndex(List<int> currentSequence, int currentVertex)
        {
            return currentSequence
                .IndexOf((v, index) => v == currentVertex && index < currentSequence.Count - 1);
        }

        private bool IsCycle(int previousVertexIndex)
        {
            return previousVertexIndex != -1;
        }
    }
}