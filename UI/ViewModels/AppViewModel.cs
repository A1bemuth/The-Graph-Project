using System;
using System.Collections.Generic;
using GraphAlgorithms;
using UI.Infrastructure;
using UI.Models;

namespace UI.ViewModels
{
    public class AppViewModel : PropertyNotifier
    {
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

        public GraphInfo GraphInfo
        {
            get { return Get<GraphInfo>(); }
            set
            {
                Set(value);
                IsGraphLoaded = GraphInfo != null;
                GraphInformationModel = new GraphInformationViewModel(GraphInfo);
            }
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
                ? new VerticeInformationViewModel(SelectedVerticeIndex, GraphInfo)
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
            GraphLoader.Instance.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName.Equals("IsLoadStarted"))
                    IsGraphLoading = GraphLoader.Instance.IsLoadStarted;
                if (args.PropertyName.Equals("LoadStatus"))
                    LoadingStatus = GraphLoader.Instance.LoadStatus;
            };
        }

        private void LoadGraph(object parameter)
        {
            CommandEventBinder.CloseMenuCommand.Execute();
            IsModalOpened = true;
            IsGraphLoading = true;
            var fileName = Navigator.OpenFile();
            if (fileName == null)
            {
                IsModalOpened = false;
                return;
            }
            GraphLoader.Instance.LoadGraph(fileName);
        }

        private void GraphLoadedHandling(object result)
        {
            GraphInfo = result as GraphInfo;
            Status = "Граф успешно загружен";
            IsModalOpened = false;
            IsGraphLoading = true;

        }

        private void CloseMenu(object parameter)
        {
            IsMenuOpened = false;
        }

        private void ShowCycles(object parameter)
        {
            IsModalOpened = true;
            var model = new CycleSelectionViewModel(GraphInfo);
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
            if (GraphInfo != null)
            {
                var cycle = new List<int>(GraphInfo.Cycles[selectedCycleIndex]);
                cycle.Add(cycle[0]);
                VisitedPath = cycle.ToArray();
                Status = "Цикл успешно отображен";
            }
        }

        private void ShowPath(object o)
        {
            IsModalOpened = true;
            var modal = new PathSelectionViewModel(GraphInfo);
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
            VisitedPath = GraphInfo.Graph.FindPath(selectedIndexes.Item1, selectedIndexes.Item2);
            Status = VisitedPath != null ? "Путь успешно построен" : "Между вершинами нет пути";
        }

        private void RefreshGraph(object o)
        {
            CommandEventBinder.CloseMenuCommand.Execute();
            var graph = GraphInfo.Graph;
            GraphInfo = null;
            GraphInfo = new GraphInfo(graph);
            Status = "Граф обновлен.";
        }
    }
}