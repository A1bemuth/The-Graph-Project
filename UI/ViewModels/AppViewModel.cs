using System;
using System.Collections.Generic;
using GraphAlgorithms;
using GraphDataLayer;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class AppViewModel : PropertyNotifier
    {
        public NamedGraph Graph
        {
            get { return Get<NamedGraph>(); }
            private set
            {
                Set(value);
                IsGraphLoaded = value != null;
                GraphInformationModel = new GraphInformationViewModel(value);
                SelectedVerticeIndex = -1;
            }
        }
        public GraphInformationViewModel GraphInformationModel
        {
            get { return Get<GraphInformationViewModel>(); }
            set { Set(value); }
        }

        public VerticeInformationViewModel VerticeInformationModel
        {
            get { return Get<VerticeInformationViewModel>(); }
            private set { Set(value); }
        }

        public int SelectedVerticeIndex
        {
            get { return Get<int>(); }
            set
            {
                Set(value);
                SelectedVerticeIndexChange();
            }
        }

        private void SelectedVerticeIndexChange()
        {
            IsVerticeSelected = SelectedVerticeIndex != -1;
            VerticeInformationModel = IsVerticeSelected
                ? new VerticeInformationViewModel(SelectedVerticeIndex, Graph)
                : new VerticeInformationViewModel();
        }

        public bool IsMenuOpened
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public bool IsGraphLoaded
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public bool IsVerticeSelected
        {
            get { return Get<bool>(); }
            set { Set(value);}
        }

        public bool IsModalOpened
        {
            get { return Get<bool>(); }
            set { Set(value);}
        }

        public bool IsGraphLoading
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public string LoadingStatus
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public int[] VisitedPath
        {
            get { return Get<int[]>(); }
            set { Set(value);}
        }

        public string Status
        {
            get { return Get<string>(); }
            set { Set(value);}
        }

        public AppViewModel()
        {
            GraphInformationModel = new GraphInformationViewModel();
            VerticeInformationModel = new VerticeInformationViewModel();
        }

        public void BindEvent()
        {
            CommandEventBinder.LoadGraphCommand.OnExecute += LoadGraph;
            CommandEventBinder.LoadingComplited.OnExecute += GraphLoadedHandling;
            CommandEventBinder.SelectCycleCommand.OnExecute += SelectCycle;
            CommandEventBinder.SelectPathCommand.OnExecute += SelectPath;
            CommandEventBinder.RefreshCommand.OnExecute += RefreshGraph;
            CommandEventBinder.CloseMenuCommand.OnExecute += CloseMenu;
            CommandEventBinder.ShowCyclesModalCommand.OnExecute += ShowCycles;
            CommandEventBinder.CloseCyclesModalCommand.OnExecute += CloseModal;
            CommandEventBinder.ShowPathModalCommand.OnExecute += ShowPath;
            CommandEventBinder.ClosePathModalCommand.OnExecute += CloseModal;
        }

        private void LoadGraph(object parameter)
        {
            CommandEventBinder.CloseMenuCommand.Execute();
            IsModalOpened = true;
            IsGraphLoading = true;
            LoadingStatus = "Импорт из файла";
            var fileName = Navigator.OpenFile();
            if (fileName == null)
            {
                IsModalOpened = false;
                IsGraphLoading = true;
                return;
            }
            GraphInformationModel.StopSearch();
            LoadingStatus = "Импорт из файла";
            GraphLoader.Instance.LoadGraph(fileName);
        }

        private void GraphLoadedHandling(object result)
        {
            Graph = result as NamedGraph;
            Status = "Граф успешно загружен";
            IsModalOpened = false;
            IsGraphLoading = true;
            LoadingStatus = "";
        }

        private void CloseMenu(object parameter)
        {
            IsMenuOpened = false;
        }

        private void ShowCycles(object parameter)
        {
            IsModalOpened = true;
            var model = new CycleSelectionViewModel(Graph, GraphInformationModel.Cycles);
            CommandEventBinder.CloseMenuCommand.Execute();
            Navigator.OpenCycleModal(model);
        }

        private void CloseModal(object o)
        {
            IsModalOpened = false;
        }

        private void SelectCycle(object index)
        {
            Navigator.CloseCycleModal();
            var selectedCycleIndex = (int) index;
            if (selectedCycleIndex == -1)
            {
                Status = "Цикл не был выбран.";
                return;
            }
            if (GraphInformationModel.Cycles != null)
            {
                var cycle = new List<int>(GraphInformationModel.Cycles[selectedCycleIndex]);
                cycle.Add(cycle[0]);
                SelectedVerticeIndex = -1;
                VisitedPath = cycle.ToArray();
                Status = "Цикл успешно отображен";
            }
        }

        private void ShowPath(object o)
        {
            IsModalOpened = true;
            var modal = new PathSelectionViewModel(Graph);
            CommandEventBinder.CloseMenuCommand.Execute();
            Navigator.OpenPathModal(modal);
        }

        private void SelectPath(object o)
        {
            Navigator.ClosePathModal();
            var selectedIndexes = (Tuple<int, int>)o;
            if(selectedIndexes.Item1 == -1 || selectedIndexes.Item2 == -1)
            {
                Status = "Не выбрана начальная или конечная вершина";
                return;
            }
            if(selectedIndexes.Item1 == selectedIndexes.Item2)
            {
                Status = "Начальная и конечная вершины совпадают";
                return;
            }
            VisitedPath = Graph.FindPath(selectedIndexes.Item1, selectedIndexes.Item2);
            SelectedVerticeIndex = -1;
            Status = VisitedPath != null ? "Путь успешно построен" : "Между вершинами нет пути";
        }

        private void RefreshGraph(object o)
        {
            CommandEventBinder.CloseMenuCommand.Execute();
            Status = "Граф обновлен.";
        }
    }
}