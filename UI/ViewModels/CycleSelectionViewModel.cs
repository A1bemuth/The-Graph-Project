using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class CycleSelectionViewModel : PropertyNotifier
    {
        private NamedGraph graph;
        public List<string> GroupCycles {
            get
            {
                if (SelectedGroup != null && Cycles.ContainsKey(SelectedGroup.Item1))
                    return Cycles[SelectedGroup.Item1]
                        .Select(c => string.Join(",", c.Select(v => graph[v])))
                        .ToList();
                return new List<string>();
            }
        }

        public IEnumerable<Tuple<int, int>> Groups => 
            Cycles.Keys
            .Select(k => Tuple.Create(k, Cycles[k].Count));

        public Tuple<int, int> SelectedGroup
        {
            get { return Get<Tuple<int, int>>(); }
            set
            {
                Set(value);
                RaisePropertyChanged(nameof(GroupCycles));
                SelectedCycleIndex = -1;
            }
        }

        public int SelectedCycleIndex
        {
            get { return Get<int>(); }
            set
            {
                Set(value);
                if(value != -1)
                    SelectedCycle = Cycles[SelectedGroup.Item1][SelectedCycleIndex];
            }
        }

        public int[] SelectedCycle
        {
            get { return Get<int[]>(); }
            set { Set(value); }
        }

        public Dictionary<int, List<int[]>> Cycles
        {
            get { return Get<Dictionary<int, List<int[]>>>(); }
            private set { Set(value);}
        } 

        public CycleSelectionViewModel()
        {
            Cycles =  Enumerable
                .Empty<int[]>()
                .GroupBy(i => i.Length)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public CycleSelectionViewModel(NamedGraph graph, List<int[]> cycles)
        {
            this.graph = graph;
            if(cycles != null)
                Cycles = cycles
                .GroupBy(cycle => cycle.Length)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}