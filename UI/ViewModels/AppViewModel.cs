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
            get { return Get<GraphInformationViewModel>(nameof(GraphInformationModel)); }
            set { Set(nameof(GraphInformationModel), value); }
        }

        public VerticeInformationViewModel VerticeInformationModel
        {
            get { return Get<VerticeInformationViewModel>(nameof(VerticeInformationModel)); }
            private set { Set(nameof(VerticeInformationModel), value); }
        }

        public GraphInfo GraphInfo
        {
            get { return Get<GraphInfo>(nameof(GraphInfo)); }
            set
            {
                Set(nameof(GraphInfo), value);
                IsGraphLoaded = GraphInfo != null;
                GraphInformationModel = new GraphInformationViewModel(GraphInfo);
            }
        }

        public int SelectedVerticeIndex
        {
            get { return Get<int>(nameof(SelectedVerticeIndex)); }
            set
            {
                Set(nameof(SelectedVerticeIndex), value);
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
            get { return Get<bool>(nameof(IsMenuOpened)); }
            set { Set(nameof(IsMenuOpened), value); }
        }

        public bool IsGraphLoaded
        {
            get { return Get<bool>(nameof(IsGraphLoaded)); }
            set { Set(nameof(IsGraphLoaded), value); }
        }

        public bool IsVerticeSelected
        {
            get { return Get<bool>(nameof(IsVerticeSelected)); }
            set { Set(nameof(IsVerticeSelected), value);}
        }

        public bool IsModalOpened
        {
            get { return Get<bool>(nameof(IsModalOpened)); }
            set { Set(nameof(IsModalOpened), value);}
        }

        public bool IsGraphLoading
        {
            get { return Get<bool>(nameof(IsGraphLoading)); }
            set { Set(nameof(IsGraphLoading), value); }
        }

        public string LoadingStatus
        {
            get { return Get<string>(nameof(LoadingStatus)); }
            set { Set(nameof(LoadingStatus), value); }
        }

        public int[] VisitedPath
        {
            get { return Get<int[]>(nameof(VisitedPath)); }
            set { Set(nameof(VisitedPath), value);}
        }

        public string Status
        {
            get { return Get<string>(nameof(Status)); }
            set { Set(nameof(Status), value);}
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