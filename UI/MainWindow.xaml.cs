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
            graph = new AdjacencyListGraph(15);
            graph.AddArrow(8, 4);
            graph.AddArrow(3, 4);
            graph.AddArrow(5, 10);
            graph.AddArrow(0, 1);
            graph.AddArrow(11, 9);
            graph.AddArrow(9, 11);
            graph.AddArrow(14, 7);
            graph.AddArrow(1, 3);
            graph.AddArrow(10, 6);
            graph.AddArrow(11, 5);
            graph.AddArrow(5, 11);
            graph.AddArrow(8, 13);
            graph.AddArrow(13, 8);
            graph.AddArrow(4, 2);
            graph.AddArrow(2, 0);
            graph.AddArrow(9, 12);
            graph.AddArrow(12, 9);
            graph.AddArrow(12, 8);
            graph.AddArrow(8, 12);
            graph.AddArrow(7, 13);
            graph.AddArrow(1, 6);
            graph.AddArrow(6, 14);
            graph.AddArrow(0, 5);
            graph.AddArrow(9, 2);
            graph.AddArrow(7, 3);
        }

        private void DrawGraph(object sender, RoutedEventArgs e)
        {
            GraphView.Graph = graph;
        }
    }
}
