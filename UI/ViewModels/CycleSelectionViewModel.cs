using System.Collections.Generic;
using System.Linq;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class CycleSelectionViewModel : PropertyNotifier
    {
        private GraphInfo info;

        public IEnumerable<string> Cycles
        {
            get
            {
                return info.Cycles
                    .Select(c => string.Join(",", c));
            }
        }

        public int SelectedCycleIndex
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public CycleSelectionViewModel()
        {
        }

        public CycleSelectionViewModel(GraphInfo graphInfo)
        {
            info = graphInfo;
        }
    }
}