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
            graph = new AdjacencyListGraph(12);
            graph.AddArrow(0, 6);
            graph.AddArrow(0, 7);
            graph.AddArrow(1, 0);
            graph.AddArrow(1, 3);
            graph.AddArrow(1, 4);
            graph.AddArrow(2, 3);
            graph.AddArrow(2, 11);
            graph.AddArrow(3, 11);
            graph.AddArrow(4, 11);
            graph.AddArrow(5, 11);
            graph.AddArrow(6, 5);
            graph.AddArrow(7, 1);
            graph.AddArrow(7, 2);
            graph.AddArrow(8, 11);
            graph.AddArrow(9, 10);
            graph.AddArrow(9, 11);
            graph.AddArrow(10, 11);
            graph.AddArrow(11, 0);
            graph.AddArrow(11, 1);
            graph.AddArrow(11, 2);
            graph.AddArrow(11, 3);
            graph.AddArrow(11, 4);
            graph.AddArrow(11, 5);
            graph.AddArrow(11, 6);
            graph.AddArrow(11, 7);
            graph.AddArrow(11, 8);
            graph.AddArrow(11, 9);
            graph.AddArrow(11, 10);
        }

        private void DrawGraph(object sender, RoutedEventArgs e)
        {
            GraphView.Graph = graph;
        }
    }
}
