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
                var nodeView = new NodeView();

                SetLeft(nodeView, node.Location.X + centerX - nodeView.Width/2);
                SetTop(nodeView, centerY + node.Location.Y - nodeView.Height/2);

                foreach (var connection in node.Connections)
                {
                    var startPoint = new Point(node.Location.X + centerX, node.Location.Y + centerY);
                    var endPoint = new Point(connection.Location.X + centerX, connection.Location.Y + centerY);
                    var line = DrawLinkArrow(startPoint, endPoint);
                    panel.Children.Add(line);
                }
                panel.Children.Add(nodeView);
            }
        }

        private static Shape DrawLinkArrow(Point p1, Point p2)
        {
            var lineGroup = new GeometryGroup();
            var theta = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180 / Math.PI;

            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            var p = new Point(p1.X + (p2.X - p1.X) / 1.1, p1.Y + (p2.Y - p1.Y) / 1.1);
            pathFigure.StartPoint = p;

            var lpoint = new Point(p.X + 3, p.Y + 10);
            var rpoint = new Point(p.X - 3, p.Y + 10);
            var seg1 = new LineSegment { Point = lpoint };
            pathFigure.Segments.Add(seg1);

            var seg2 = new LineSegment { Point = rpoint };
            pathFigure.Segments.Add(seg2);

            var seg3 = new LineSegment { Point = p };
            pathFigure.Segments.Add(seg3);

            pathGeometry.Figures.Add(pathFigure);
            var transform = new RotateTransform
            {
                Angle = theta + 90,
                CenterX = p.X,
                CenterY = p.Y
            };
            pathGeometry.Transform = transform;
            lineGroup.Children.Add(pathGeometry);

            var connectorGeometry = new LineGeometry
            {
                StartPoint = p1,
                EndPoint = p2
            };
            lineGroup.Children.Add(connectorGeometry);
            var path = new Path
            {
                Data = lineGroup,
                StrokeThickness = 2
            };
            path.Stroke = path.Fill = Brushes.DarkGray;

            return path;
        }

        public IVerticesLocator VerticesLocator
        {
            get { return (IVerticesLocator)GetValue(VerticesLocatorProperty); }
            set { SetValue(VerticesLocatorProperty, value); }
        }
    }
}