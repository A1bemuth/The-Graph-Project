using System.Collections.Generic;
using System.Linq;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class CycleSelectionViewModel : ViewModel
    {
        private IEnumerable<string> cycles;
        private int selectedCycleIndex = -1;

        public IEnumerable<string> Cycles
        {
            get
            {
                return cycles;
            }
            set
            {
                cycles = value;
                OnPropertyChanged();
            }
        }

        public int SelectedCycleIndex
        {
            get { return selectedCycleIndex; }
            set
            {
                selectedCycleIndex = value;
                OnPropertyChanged();
            }
        }

        public CycleSelectionViewModel() { }

        public CycleSelectionViewModel(GraphInfo graphInfo)
        {
            //Cycles = cycles.Select(c => string.Join(",", c.Select(v => graph[v])));
            Cycles = graphInfo.Cycles.Select(c => string.Join(",", c.Select(v => "Test")));
        }

        public override void Dispose()
        {
        }
    }
}