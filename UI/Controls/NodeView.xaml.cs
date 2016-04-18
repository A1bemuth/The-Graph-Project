using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UI.Annotations;
using UI.Infrastructure;

namespace UI.Controls
{
    public partial class NodeView : UserControl, IGraphObject, INotifyPropertyChanged
    {
        public static DependencyProperty NumberProperty = DependencyProperty.Register("Number", typeof (int), typeof (NodeView));

        public int Number
        {
            get { return (int) GetValue(NumberProperty); }
            private set { SetValue(NumberProperty, value); }
        }

        public static DependencyProperty RadiusProperty = DependencyProperty.Register("Radis", typeof(double), typeof(NodeView),
            new FrameworkPropertyMetadata(25.0, RadiusChanged));

        private static void RadiusChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var nodeView = dependencyObject as NodeView;
            if (nodeView != null)
            {
                nodeView.Ellipse.Height = nodeView.Radius*2;
                nodeView.Ellipse.Width = nodeView.Radius*2;
            }
        }

        public double Radius
        {
            get { return (double) GetValue(RadiusProperty); }
            set
            {
                SetValue(RadiusProperty, value);
                OnPropertyChanged();
            }
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof (string),
            typeof (NodeView));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            private set { SetValue(TitleProperty, value); }
        }

        public NodeStatus Status { get; private set; }

        public List<ArrowView> Arrows { get; }
        
        public Point Center { get; set; }

        public NodeView()
        {
            InitializeComponent();
            Arrows = new List<ArrowView>();
        }

        public NodeView(int number, string title) : this()
        {
            Number = number;
            Title = title;
        }

        public Point ShiftPointFromTitle(Point topLeftPoint)
        {
            var y = topLeftPoint.Y - TitlePlace.DesiredSize.Height;
            return new Point(topLeftPoint.X, y);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            TitlePlace.Measure(constraint);
            var height = Radius*2 + TitlePlace.DesiredSize.Height;
            var width = Math.Max(Radius*2, Radius + TitlePlace.DesiredSize.Width);
            return new Size(width, height);
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

        public void IncludeView(NodeView nextNode)
        {
            Status = NodeStatus.InCycle;
            var edge = Arrows.FirstOrDefault(a => a.EndNode.Equals(nextNode));
            if (edge != null)
                edge.Status = NodeStatus.InCycle;
        }

        public void ChangeViewToDefault()
        {
            Status = NodeStatus.NotInclude;
            BorderBrush = Brushes.DarkGray;
        }

        public void ChangeView()
        {
            switch (Status)
            {
                case NodeStatus.Selected:
                    BorderBrush = Brushes.OrangeRed;
                    break;
                case NodeStatus.Incomming:
                    BorderBrush = Brushes.Gold;
                    break;
                case NodeStatus.Outgoing:
                    BorderBrush = Brushes.DodgerBlue;
                    break;
                case NodeStatus.Outgoing | NodeStatus.Incomming:
                    BorderBrush = Brushes.YellowGreen;
                    break;
                case NodeStatus.InCycle:
                    BorderBrush = Brushes.LimeGreen;
                    break;
                default:
                    BorderBrush = Brushes.Gainsboro;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
