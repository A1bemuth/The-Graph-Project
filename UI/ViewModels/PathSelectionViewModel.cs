using System;
using System.Collections.Generic;
using System.Linq;
using GraphDataLayer;
using UI.Infrastructure;

namespace UI.ViewModels
{
    public class PathSelectionViewModel : PropertyNotifier
    {
        public Tuple<int, int> VerticePair
        {
            get { return Get<Tuple<int, int>>(); }
            set { Set(value); }
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

        public PathSelectionViewModel(NamedGraph graph) : this()
        {
            VerticeNames = Enumerable.Range(0, graph.VerticesCount)
                .Select(v => graph[v])
                .ToList();
        }
    }
}