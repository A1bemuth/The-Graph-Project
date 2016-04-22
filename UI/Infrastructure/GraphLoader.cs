using System;
using GraphDataLayer;
using GraphDataLayer.ExcelImport;
using UI.Models;

namespace UI.Infrastructure
{
    public class GraphLoader : PropertyNotifier
    {
        private static readonly Lazy<GraphLoader> instance = new Lazy<GraphLoader>();
        public static GraphLoader Instance => instance.Value;

        public bool IsLoadStarted
        {
            get { return Get<bool>(nameof(IsLoadStarted)); }
            set { Set(nameof(IsLoadStarted), value); }
        }

        public string LoadStatus
        {
            get { return Get<string>(nameof(LoadStatus)); }
            set { Set(nameof(LoadStatus), value); }
        }

        public GraphInfo LoadGraph(string fileName)
        {
            if(fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            IsLoadStarted = true;
            LoadStatus = "Импорт из файла";
            var graph = new NamedExcelImporter<AdjacencyGraph>().GetGraphs(fileName)[0];

            LoadStatus = "Оределение параметров графа";
            var graphInfo = new GraphInfo(graph);
            IsLoadStarted = false;
            return graphInfo;
        }
    }
}