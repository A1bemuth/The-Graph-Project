using System;
using System.ComponentModel;
using GraphAlgorithms;
using GraphDataLayer;
using GraphDataLayer.ExcelImport;
using UI.Models;

namespace UI.Infrastructure
{
    public class GraphLoader : PropertyNotifier
    {
        private BackgroundWorker loadingWorker;
        private static readonly Lazy<GraphLoader> instance = new Lazy<GraphLoader>();
        public static GraphLoader Instance => instance.Value;

        public bool IsLoadStarted
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public string LoadStatus
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public void LoadGraph(string fileName)
        {
            if(fileName == null)
                throw new ArgumentNullException(nameof(fileName));
            IsLoadStarted = true;
            loadingWorker = new BackgroundWorker();
            loadingWorker.DoWork += LoadingWorkerOnDoWork;
            loadingWorker.RunWorkerCompleted +=LoadingWorkerOnRunWorkerCompleted;
            loadingWorker.RunWorkerAsync(fileName);
        }

        private void LoadingWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var fileName = doWorkEventArgs.Argument as string;

            LoadStatus = "Импорт из файла";
            var graph = new NamedExcelImporter<AdjacencyGraph>().GetGraphs(fileName)[0];
            LoadStatus = "Оределение параметров графа";
            var graphInfo = new GraphInfo(graph);
            graphInfo.ArrowCount = graph.ArrowsCount;
            graphInfo.VerticeCount = graph.VerticesCount;

            LoadStatus = "Расчет коэффициента кластеризации";
            graphInfo.ClusteringCoef = graph.ClusteringCoefficient();

            LoadStatus = "Поиск циклов";
            graphInfo.Cycles = graph.FindCycles();

            doWorkEventArgs.Result = graphInfo;
        }

        private void LoadingWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            var graphInfo = runWorkerCompletedEventArgs.Result;
            IsLoadStarted = false;
            LoadStatus = "";
            CommandEventBinder.LoadingComplited.Execute(graphInfo);
        }
    }
}