using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphAlgorithms;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for NodeView.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {

        public Point Center { get; set; }

        public NodeView()
        {
            InitializeComponent();
        }

        public static NodeView Create(Node node)
        {
            return new NodeView
            {
                Center = new Point(node.Location.X, node.Location.Y)
            };
        }

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            EllipseLayout.Fill = Brushes.Red;
        }
    }
}
