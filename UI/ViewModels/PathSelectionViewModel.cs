using System;
using System.Collections.Generic;
using System.Linq;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class PathSelectionViewModel : PropertyNotifier
    {
        public Tuple<int, int> VerticePair
        {
            get { return Get<Tuple<int, int>>(nameof(VerticePair)); }
            set { Set(nameof(VerticePair), value); }
        }

        public int StartVerticeIndex
        {
            get { return VerticePair.Item1; }
            set { VerticePair = Tuple.Create(value, VerticePair.Item2); }
        }

        public int EndVerticeIndex
        {
            get { return VerticePair.Item2; }
            set { VerticePair = Tuple.Create(VerticePair.Item1, value); }
        }

        public IEnumerable<string> VerticeNames { get; set; }

        public PathSelectionViewModel()
        {
            VerticePair = new Tuple<int, int>(0,0);
        }

        public PathSelectionViewModel(GraphInfo graphInfo) : this()
        {
            VerticeNames = Enumerable.Range(0, graphInfo.VerticeCount)
                .Select(v => graphInfo.Graph[v])
                .ToList();
        }
    }
}