using System.Windows;
using GraphDataLayer;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IGraph graph;
        public MainWindow()
        {
            InitializeComponent();
            graph = new AdjacencyListGraph(5);
            graph.AddArrow(0, 4);
            graph.AddArrow(4, 3);
            graph.AddArrow(3, 1);
            graph.AddArrow(3, 2);
            graph.AddArrow(1, 0);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 1);
        }

        private void DrawGraph(object sender, RoutedEventArgs e)
        {
            GraphView.Graph = graph;
        }
    }
}
