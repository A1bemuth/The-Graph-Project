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
                    arrow.Status |= NodeStatus.Outgoing;
                    arrow.EndNode.Status |= NodeStatus.Outgoing;
                }
                else
                {
                    arrow.Status |= NodeStatus.Incomming;
                    arrow.StartNode.Status |= NodeStatus.Incomming;
                }
            }
        }

        public void ChangeViewToDefault()
        {
            Status = NodeStatus.NotInclude;
            EllipseLayout.Fill = Brushes.DarkGray;
            EllipseLayout.Opacity = 1;
        }

        public void ChangeView()
        {
            if(Status == NodeStatus.Selected)
            {
                EllipseLayout.Fill = Brushes.OrangeRed;
            }
            else if(Status == NodeStatus.Incomming)
            {
                EllipseLayout.Fill = Brushes.Gold;
                
            }
            else if (Status == NodeStatus.Outgoing)
            {
                EllipseLayout.Fill = Brushes.DodgerBlue;
            }
            else if (Status == (NodeStatus.Outgoing | NodeStatus.Incomming))
            {
                EllipseLayout.Fill = Brushes.YellowGreen;
            }
            else
            {
                EllipseLayout.Fill = Brushes.Gainsboro;

            }
        }
    }
}
