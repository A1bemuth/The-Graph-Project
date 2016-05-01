using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class CycleSelectionViewModel : PropertyNotifier
    {
        public IEnumerable<string> Cycles
        {
            get { return Get<IEnumerable<string>>(); }
            private set { Set(value); }
        }

        public int SelectedCycleIndex
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public CycleSelectionViewModel()
        {
        }

        public CycleSelectionViewModel(NamedGraph graph, List<int[]> cycles)
        {
            if (cycles != null)
                Cycles = cycles
                    .Select(c => string.Join(",", c.Select(v => graph[v])));
        }
    }
}