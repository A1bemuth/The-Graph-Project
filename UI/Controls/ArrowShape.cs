using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UI.Controls
{
    public class ArrowShape : Shape
    {
        private Point start;
        private Point end;

        protected override Geometry DefiningGeometry
        {
            get
            {
                var lineGroup = new GeometryGroup();
                var theta = Math.Atan2(end.Y - start.Y, end.X - start.X) * 180 / Math.PI;

                var arrowGeometry = new PathGeometry();
                var pathFigure = new PathFigure();
                var arrowPoint = new Point(start.X + (end.X - start.X) / 1.2, start.Y + (end.Y - start.Y) / 1.2);
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
                return lineGroup;
            }
        }

        public static DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(Point),
           typeof(Arrow), new FrameworkPropertyMetadata(new Point()));

        public static DependencyProperty EndPointProperty = DependencyProperty.Register("EndPoint", typeof(Point),
            typeof(Arrow), new FrameworkPropertyMetadata(new Point(1, 1)));

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

        public ArrowShape(Point start, Point end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public void SetCanvasParameters(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }
    }
}