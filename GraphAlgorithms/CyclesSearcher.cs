using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphDataLayer;

namespace GraphAlgorithms
{
    public class CyclesSearcher
    {
        public List<int[]> FindCycles(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            var cycles = new HashSet<int[]>(new CycleComparer());

            var iterator = new GraphIterator(graph);
            iterator.CycleDetected += c => cycles.Add(c);
            iterator.Iterate();
            return cycles.ToList();
        }

        public async Task<List<int[]>> FindCyclesAsync(Graph graph, IProgress<int[]> progress, CancellationToken ct)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));
            if(progress == null)
                throw  new ArgumentNullException(nameof(progress));
            if (ct == null)
                throw new ArgumentNullException(nameof(ct));

            return await Task.Run(() => 
            {
                var cycles = new ConcurrentHashSet<int[]>(new CycleComparer());
                var iterator = new GraphIterator(graph);
                iterator.CycleDetected += cycle =>
                {
                    if (cycles.Add(cycle))
                    {
                        progress.Report(cycle);
                    }                    
                };
                iterator.Iterate();
                return cycles.ToList();
            }, ct);
        }
    }
}