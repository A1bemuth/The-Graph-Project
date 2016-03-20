using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    internal class GraphIteratorEventArgs : EventArgs
    {
        internal bool[] VisitedVertices { get; }
        internal List<int> CurrentSequence { get; }
        internal int CurrentVertex { get; }

        internal GraphIteratorEventArgs(bool[] visitedVertices, List<int> currentSequence, int currentVertex)
        {
            VisitedVertices = visitedVertices;
            CurrentSequence = currentSequence;
            CurrentVertex = currentVertex;
        }
    }
}