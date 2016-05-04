using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class GraphIterator
    {
        private readonly Graph graph;
        private readonly bool[] visitedVertices;
        internal event Action<int[]> CycleDetected;

        internal GraphIterator(Graph graph)
        {
            this.graph = graph;
            visitedVertices = new bool[graph.VerticesCount];
        }

        internal void Iterate(CancellationToken? token = null)
        {
            var ct = token ?? CancellationToken.None;
            while (ThereAreNotVisitedVertices())
            {
                var vertex = visitedVertices.IndexOf(v => !v);
                var currentSequence = ImmutableList<int>.Empty;
                var verticeInSequence = new bool[graph.VerticesCount].ToImmutableArray();
                InspectVertex(vertex, currentSequence, verticeInSequence, ct);
            }
        }

        private bool ThereAreNotVisitedVertices()
        {
            return visitedVertices.Any(v => !v);
        }

        private void InspectVertex(int vertex, ImmutableList<int> currentSequence, ImmutableArray<bool> verticesInSequence, CancellationToken ct)
        {
            if (verticesInSequence[vertex])
            {
                var previousIndex = currentSequence.IndexOf(vertex);
                var cycle = currentSequence.Skip(previousIndex).ToArray();
                if (cycle.Length > 2)
                    OnCycleDetected(cycle);
            }
            else
            {
                var currentSequenceWithVertex = currentSequence.Add(vertex);
                verticesInSequence = verticesInSequence.SetItem(vertex, true);
                visitedVertices[vertex] = true;

                var tasks = graph
                    .GetNeighbours(vertex)
                    .Select(n => Task.Run(() => InspectVertex(n, currentSequenceWithVertex, verticesInSequence, ct), ct))
                    .ToArray();
                try
                {
                    Task.WaitAll(tasks);
                }
                catch (OperationCanceledException)
                {
                }
                catch (AggregateException)
                {
                }
            }
        }

        private void OnCycleDetected(int[] cycle)
        {
            CycleDetected?.Invoke(cycle);
        }
    }
}