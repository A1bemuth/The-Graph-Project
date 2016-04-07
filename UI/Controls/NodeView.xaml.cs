using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for NodeView.xaml
    /// </summary>
    public partial class NodeView : UserControl , IGraphObject
    {
        public bool IsSelected { get; private set; }

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
            IsSelected = true;
            EllipseLayout.Fill = Brushes.OrangeRed;
            foreach (var arrow in Arrows)
            {
                arrow.BorderBrush = Brushes.DarkOrange;
                arrow.StrokeThickness = 5;
            }

            e.Handled = true;
        }

        public void ChangeStateToDefault()
        {
            IsSelected = false;
            EllipseLayout.Fill = Brushes.DarkGray;
        }
    }
}
