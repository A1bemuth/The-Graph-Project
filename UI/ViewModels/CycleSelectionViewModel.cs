using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GraphDataLayer;
using UI.Annotations;

namespace UI.ViewModels
{
    public class CycleSelectionViewModel : INotifyPropertyChanged
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

        public CycleSelectionViewModel(NamedGraph graph, IEnumerable<int[]> cycles)
        {
            //Cycles = cycles.Select(c => string.Join(",", c.Select(v => graph[v])));
            Cycles = cycles.Select(c => string.Join(",", c.Select(v => "Test")));
        }

        public CycleSelectionViewModel() { }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}