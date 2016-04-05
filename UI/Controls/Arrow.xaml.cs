using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for Arrow.xaml
    /// </summary>
    public partial class Arrow : UserControl
    {
        public static DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point),
            typeof(Arrow), new FrameworkPropertyMetadata(new Point()));

        public static DependencyProperty EndPointProperty = DependencyProperty.Register("EndPoint", typeof(Point),
            typeof(Arrow), new FrameworkPropertyMetadata(new Point(1, 1)));

        public static DependencyProperty GeometryProperty = DependencyProperty.Register("Geometry", typeof(Geometry), typeof(Arrow),
            new FrameworkPropertyMetadata(null));

        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }

        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        public Geometry Geometry
        {
            get { return (Geometry) GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }

        public Arrow()
        {
            InitializeComponent();
        }

        public Arrow(Point start, Point end) : this()
        {
            StartPoint = start;
            EndPoint = end;
        }

        public void SetShifts(double horizontalShift, double verticalShift)
        {
            var startShiftedPoint = new Point(StartPoint.X + horizontalShift, verticalShift - StartPoint.Y);
            var endShiftedPoint = new Point(EndPoint.X + horizontalShift, verticalShift - EndPoint.Y);
            UpdateGeometry(startShiftedPoint, endShiftedPoint);
        }

        private void UpdateGeometry(Point start, Point end)
        {
            var lineGroup = new GeometryGroup();
            var theta = Math.Atan2(end.Y - start.Y, end.X - start.X) * 180 / Math.PI;

            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            var p = new Point(start.X + (end.X - start.X) / 1.1, start.Y + (end.Y - start.Y) / 1.1);
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
                StartPoint = start,
                EndPoint = end
            };
            lineGroup.Children.Add(connectorGeometry);
            Geometry = lineGroup;
        }
    }
}
