using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphAlgorithms;
using GraphDataLayer;
using UI.Infrastructure;

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
                ? new VerticeInformationViewModel(SelectedVerticeIndex, Graph, GraphInformationModel.Cycles)
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

        public bool IsAllCycleFound
        {
            get { return Get<bool>(); }
            private set { Set(value); }
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
            CommandEventBinder.AllCycleFound.OnExecute += o => IsAllCycleFound = true;
            CommandEventBinder.SaveCycleToFile.OnExecute += SaveCycles;
        }

        private void SaveCycles(object o)
        {
            CommandEventBinder.CloseMenuCommand.Execute();
            if(!IsAllCycleFound)
                return;
            var fileName = Navigator.SaveFile();
            if(fileName == null)
                return;
            File.Delete(fileName);
            using (var writeStream = new StreamWriter(File.OpenWrite(fileName)))
            {
                var groups = GraphInformationModel.Cycles
                    .GroupBy(cycle => cycle.Length)
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => g.Key, g => g.ToList());
                foreach (var cycles in groups)
                {
                    writeStream.WriteLine(
                        $"------------------->>>>> Циклы из {cycles.Key} актаторов (всего {cycles.Value.Count}):");
                    foreach (var cycle in cycles.Value)
                    {
                        writeStream.WriteLine(string.Join(",", cycle.Select(i => Graph[i])));
                    }
                }
            }
            Status = "Циклы успешно сохранены!";
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
            IsAllCycleFound = false;
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
            var selectedCycle = index as int[];
            if (selectedCycle == null)
            {
                Status = "Цикл не был выбран.";
                return;
            }
            var cycle = selectedCycle.ToList();
            cycle.Add(cycle[0]);
            SelectedVerticeIndex = -1;
            VisitedPath = cycle.ToArray();
            Status = "Цикл успешно отображен";
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