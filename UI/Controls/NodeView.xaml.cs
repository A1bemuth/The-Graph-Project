using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Controls
{
    public partial class NodeView : UserControl, IGraphObject
    {
        public NodeStatus Status { get; private set; }

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
            Status = NodeStatus.Selected;
            foreach (var arrow in Arrows)
            {
                if (arrow.StartNode.Equals(this))
                {
                    arrow.Status = NodeStatus.Selected;
                    arrow.EndNode.Status = NodeStatus.Connected;
                }
                else
                {
                    arrow.Status = NodeStatus.Selected;
                    arrow.StartNode.Status = NodeStatus.Connected;
                }
            }
        }

        public void ChangeViewToDefault()
        {
            Status = NodeStatus.NotInclude;
            EllipseLayout.Fill = Brushes.DarkGray;
        }

        public void ChangeView()
        {
            if(Status == NodeStatus.Selected)
                EllipseLayout.Fill = Brushes.OrangeRed;

            if(Status == NodeStatus.Connected)
                EllipseLayout.Fill = Brushes.MediumAquamarine;
        }
    }
}
