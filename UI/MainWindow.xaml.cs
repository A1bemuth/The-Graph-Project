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
            graph = new AdjacencyGraph(12)
                .AddArrow(0, 6)
                .AddArrow(0, 7)
                .AddArrow(1, 0)
                .AddArrow(1, 3)
                .AddArrow(1, 4)
                .AddArrow(2, 3)
                .AddArrow(2, 11)
                .AddArrow(3, 11)
                .AddArrow(4, 11)
                .AddArrow(5, 11)
                .AddArrow(6, 5)
                .AddArrow(7, 1)
                .AddArrow(7, 2)
                .AddArrow(8, 11)
                .AddArrow(9, 10)
                .AddArrow(9, 11)
                .AddArrow(10, 11)
                .AddArrow(11, 0)
                .AddArrow(11, 1)
                .AddArrow(11, 2)
                .AddArrow(11, 3)
                .AddArrow(11, 4)
                .AddArrow(11, 5)
                .AddArrow(11, 6)
                .AddArrow(11, 7)
                .AddArrow(11, 8)
                .AddArrow(11, 9)
                .AddArrow(11, 10);
            //graph = new AdjacencyGraph(2)
            //    .AddArrow(0, 1)
            //    .AddArrow(1, 0);
        }

        private void DrawGraph(object sender, RoutedEventArgs e)
        {
            GraphView.Graph = graph;
        }
    }
}
