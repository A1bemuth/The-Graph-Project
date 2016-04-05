using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GraphAlgorithms;

namespace UI.Controls.Panels
{
    public class GraphCanvas : Canvas
    {
        public DependencyProperty VerticesLocatorProperty = DependencyProperty.Register("VerticesLocator",
           typeof(IVerticesLocator), typeof(GraphCanvas), new FrameworkPropertyMetadata(null, LocatorChanged));

        private static void LocatorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var panel = dependencyObject as GraphCanvas;
            panel.Children.Clear();
            var centerX = panel.ActualWidth / 2;
            var centerY = panel.ActualHeight / 2;

            panel.VerticesLocator.Locate();
            foreach (var node in panel.VerticesLocator.Nodes)
            {
                var nodeView = NodeView.Create(node);

                SetLeft(nodeView, node.Location.X + centerX - nodeView.Width/2);
                SetTop(nodeView, centerY - node.Location.Y - nodeView.Height/2);
                SetZIndex(nodeView, 10);

                foreach (var connection in node.Connections)
                {
                    var startPoint = new Point(node.Location.X, node.Location.Y);
                    var endPoint = new Point(connection.Location.X, connection.Location.Y);
                    var line = new Arrow(startPoint, endPoint);
                    line.SetShifts(centerX, centerY);
                    panel.Children.Add(line);
                }
                panel.Children.Add(nodeView);
            }
        }

        public IVerticesLocator VerticesLocator
        {
            get { return (IVerticesLocator)GetValue(VerticesLocatorProperty); }
            set { SetValue(VerticesLocatorProperty, value); }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var centerX = arrangeSize.Width/2;
            var centerY = arrangeSize.Height/2;

            foreach (UIElement child in Children)
            {
                if (child is NodeView)
                {
                    var node = child as NodeView;
                    SetLeft(child, centerX + node.Center.X - node.Width/2);
                    SetTop(child, centerY - node.Center.Y - node.Height/2);
                }else if (child is Arrow)
                {
                    var arrow = child as Arrow;
                    arrow.SetShifts(centerX, centerY);
                }
            }

            return base.ArrangeOverride(arrangeSize);
        }
    }
}