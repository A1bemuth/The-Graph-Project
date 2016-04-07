using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for NodeView.xaml
    /// </summary>
    public partial class NodeView : UserControl
    {
        private bool isSelected;

        public List<Arrow> Arrows { get; }

        public Point Center { get; set; }

        public double Scale
        {
            set
            {
                Height = value;
                Width = value;
            }
        }

        public NodeView()
        {
            InitializeComponent();
            Arrows = new List<Arrow>();
        }

        private void UserControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isSelected = true;
            EllipseLayout.Fill = Brushes.OrangeRed;
            foreach (var arrow in Arrows)
            {
                arrow.BorderBrush = Brushes.DarkOrange;
            }
        }
    }
}
