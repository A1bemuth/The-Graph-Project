using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for Arrow.xaml
    /// </summary>
    public partial class Arrow : UserControl, IGraphObject
    {
        public static DependencyProperty GeometryProperty = DependencyProperty.Register("Geometry", typeof(Geometry), typeof(Arrow),
            new FrameworkPropertyMetadata(null));

        public static DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double)
            , typeof(Arrow), new FrameworkPropertyMetadata(3.0d));

        public Geometry Geometry
        {
            get { return (Geometry) GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }

        public double StrokeThickness
        {
            get { return (double) GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public NodeStatus Status { get; set; }

        public NodeView StartNode { get; set; }

        public NodeView EndNode { get; set; }

        public Arrow()
        {
            InitializeComponent();
        }

        public Arrow(NodeView start, NodeView end) : this()
        {
            StartNode = start;
            EndNode = end;
        }

        public void SetCanvasParameters(Point start, Point end)
        {
            UpdateGeometry(start, end);
        }

        private void UpdateGeometry(Point start, Point end)
        {
            var lineGroup = new GeometryGroup();
            var theta = Math.Atan2(end.Y - start.Y, end.X - start.X) * 180 / Math.PI;

            var arrowGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            var arrowPoint = new Point(start.X + (end.X - start.X)/1.2, start.Y + (end.Y - start.Y)/1.2);
            pathFigure.StartPoint = arrowPoint;

            var lpoint = new Point(arrowPoint.X + 3, arrowPoint.Y + 10);
            var rpoint = new Point(arrowPoint.X - 3, arrowPoint.Y + 10);
            var seg1 = new LineSegment { Point = lpoint };
            pathFigure.Segments.Add(seg1);

            var seg2 = new LineSegment { Point = rpoint };
            pathFigure.Segments.Add(seg2);

            var seg3 = new LineSegment { Point = arrowPoint };
            pathFigure.Segments.Add(seg3);

            arrowGeometry.Figures.Add(pathFigure);
            var transform = new RotateTransform
            {
                Angle = theta + 90,
                CenterX = arrowPoint.X,
                CenterY = arrowPoint.Y
            };
            arrowGeometry.Transform = transform;
            lineGroup.Children.Add(arrowGeometry);

            var connectorGeometry = new LineGeometry
            {
                StartPoint = start,
                EndPoint = end
            };
            lineGroup.Children.Add(connectorGeometry);
            Geometry = lineGroup;
        }

        public void ChangeViewToDefault()
        {
            Status = NodeStatus.NotInclude;
            BorderBrush = Brushes.DarkGray;
            StrokeThickness = 3;
        }

        public void ChangeView()
        {
            if (Status == NodeStatus.Selected)
            {
                BorderBrush = Brushes.MediumAquamarine;
                StrokeThickness = 5;
            }
        }
    }
}
