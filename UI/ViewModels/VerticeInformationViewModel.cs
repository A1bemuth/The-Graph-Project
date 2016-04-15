using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GraphAlgorithms;
using UI.Annotations;

namespace UI.ViewModels
{
    public class VerticeInformationViewModel : INotifyPropertyChanged
    {
        private int index;
        private double clusteringCoef;
        private IEnumerable<IEnumerable<int>> includedInCycles;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public IEnumerable<IEnumerable<int>> IncludedInCycles
        {
            get { return includedInCycles; }
            set
            {
                includedInCycles = value;
                OnPropertyChanged(nameof(IncludedInCycles));
            }
        }

        public double ClusteringCoefficient
        {
            get { return clusteringCoef; }
            set
            {
                clusteringCoef = value;
                OnPropertyChanged(nameof(ClusteringCoefficient));
            }
        }

        public void UpdateVerticeInformation(AppViewModel appModel)
        {
            if(appModel.Graph == null)
                return;
            Index = appModel.SelectedVerticeIndex;
            SetDefaultValues();
            if (Index != -1)
            {
                IncludedInCycles = appModel.GraphInformationModel.Cycles
                    .Where(c => c.Contains(Index));
                ClusteringCoefficient = appModel.Graph.ClusteringCoefficientFor(Index);
            }
        }

        private void SetDefaultValues()
        {
            IncludedInCycles = null;
            ClusteringCoefficient = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}