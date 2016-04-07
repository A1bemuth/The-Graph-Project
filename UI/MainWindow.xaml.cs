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
            graph = new AdjacencyGraph(15)
                .AddArrow(8, 4)
                .AddArrow(3, 4)
                .AddArrow(5, 10)
                .AddArrow(0, 1)
                .AddArrow(11, 9)
                .AddArrow(9, 11)
                .AddArrow(14, 7)
                .AddArrow(1, 3)
                .AddArrow(10, 6)
                .AddArrow(11, 5)
                .AddArrow(5, 11)
                .AddArrow(8, 13)
                .AddArrow(13, 8)
                .AddArrow(4, 2)
                .AddArrow(2, 0)
                .AddArrow(9, 12)
                .AddArrow(12, 9)
                .AddArrow(12, 8)
                .AddArrow(8, 12)
                .AddArrow(7, 13)
                .AddArrow(1, 6)
                .AddArrow(6, 14)
                .AddArrow(0, 5)
                .AddArrow(9, 2)
                .AddArrow(7, 3);
        }

        private void DrawGraph(object sender, RoutedEventArgs e)
        {
            GraphView.Graph = graph;
        }
    }
}
