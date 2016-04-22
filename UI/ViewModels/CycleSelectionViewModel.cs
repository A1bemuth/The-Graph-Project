using System.Collections.Generic;
using System.Linq;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class CycleSelectionViewModel : PropertyNotifier
    {
        public IEnumerable<string> Cycles
        {
            get { return Get<IEnumerable<string>>(nameof(Cycles)); }
            set { Set(nameof(Cycles), value); }
        }

        public int SelectedCycleIndex
        {
            get { return Get<int>(nameof(SelectedCycleIndex)); }
            set { Set(nameof(SelectedCycleIndex), value); }
        }

        public CycleSelectionViewModel() { }

        public CycleSelectionViewModel(GraphInfo graphInfo)
        {
            Cycles = graphInfo.Cycles.Select(c => string.Join(",", c.Select(v => graphInfo.Graph[v])));
        }
    }
}