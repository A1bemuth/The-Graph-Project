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
            graph = new AdjacencyListGraph(8);
            graph.AddArrow(0, 1);
            graph.AddArrow(1, 2);
            graph.AddArrow(2, 0);
            graph.AddArrow(3, 1);
            graph.AddArrow(3, 2);
            graph.AddArrow(5, 2);
            graph.AddArrow(4, 3);
            graph.AddArrow(3, 4);
            graph.AddArrow(4, 5);
            graph.AddArrow(6, 5);
            graph.AddArrow(5, 6);
            graph.AddArrow(7, 4);
        }

        private void DrawGraph(object sender, RoutedEventArgs e)
        {
            GraphView.Graph = graph;
        }
    }
}
