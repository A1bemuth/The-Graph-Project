using System;
using System.ComponentModel;
using GraphDataLayer;
using GraphDataLayer.ExcelImport;

namespace UI.Infrastructure
{
    public class GraphLoader : PropertyNotifier
    {
        private BackgroundWorker loadingWorker;
        private static readonly Lazy<GraphLoader> instance = new Lazy<GraphLoader>();
        public static GraphLoader Instance => instance.Value;

        public void LoadGraph(string fileName)
        {
            if(fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            loadingWorker = new BackgroundWorker();
            loadingWorker.DoWork += LoadingWorkerOnDoWork;
            loadingWorker.RunWorkerCompleted +=LoadingWorkerOnRunWorkerCompleted;
            loadingWorker.RunWorkerAsync(fileName);
        }

        private void LoadingWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var fileName = doWorkEventArgs.Argument as string;
            var graph = new NamedExcelImporter<AdjacencyGraph>().GetGraphs(fileName)[0];
            doWorkEventArgs.Result = graph;
        }

        private void LoadingWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            var graph = runWorkerCompletedEventArgs.Result;
            CommandEventBinder.LoadingComplited.Execute(graph);
            loadingWorker.Dispose();
        }

    }
}