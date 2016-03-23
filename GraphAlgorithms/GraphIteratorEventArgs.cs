using System;
using System.Collections.Generic;

namespace GraphAlgorithms
{
    internal class GraphIteratorEventArgs : EventArgs
    {
        internal bool[] VerticesInSequence { get; }
        internal List<int> CurrentSequence { get; }
        internal int CurrentVertex { get; }

        internal GraphIteratorEventArgs(bool[] verticesInSequence, List<int> currentSequence, int currentVertex)
        {
            VerticesInSequence = verticesInSequence;
            CurrentSequence = currentSequence;
            CurrentVertex = currentVertex;
        }
    }
}