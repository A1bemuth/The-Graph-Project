using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UI.Infrastructure;

namespace UI.Controls
{
    public partial class ArrowView : UserControl, IGraphObject
    {
        private double offsetFromNodeCenter;
        private const double OffsetFromCentralAxis = 8.0d*Math.PI/180;
        private const int ArrowHeadWidth = 5;
        private const int ArrowHeadHeight = 13;

        public static DependencyProperty GeometryProperty = DependencyProperty.Register("Geometry", typeof(Geometry), typeof(ArrowView),
            new FrameworkPropertyMetadata(null));

        public static DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double)
            , typeof(ArrowView), new FrameworkPropertyMetadata(3.0d));

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

        public Point StartDrawingPoint { get; private set; }
        
        public Point EndDrawingPoint { get; private set; }

        public ArrowView()
        {
            InitializeComponent();
        }

        public ArrowView(NodeView start, NodeView end) : this()
        {
            StartNode = start;
            EndNode = end;
            StartDrawingPoint = StartNode.Center;
            EndDrawingPoint = EndNode.Center;
        }

        public void SetCanvasParameters(Point start, Point end, double offset)
        {
            StartDrawingPoint = start;
            EndDrawingPoint = end;
            offsetFromNodeCenter = offset/2 + 5;
            UpdateGeometry();
        }

        private void UpdateGeometry()
        {
            var mainGeometry = new GeometryGroup();
            var theta = CalculateAngleBetweenStartAndEndPoints();
            StartDrawingPoint = MoveStartDrawingPoint(theta);
            EndDrawingPoint = MoveEndDrawingPoint(theta);

            var edgeGeometry = CreateEdgeGeometry();
            var triangleGeometry = CreateTriangleGeometry(theta);
            mainGeometry.Children.Add(edgeGeometry);
            mainGeometry.Children.Add(triangleGeometry);
            Geometry = mainGeometry;
        }

        private double CalculateAngleBetweenStartAndEndPoints()
        {
            return Math.Atan2(EndDrawingPoint.Y - StartDrawingPoint.Y, EndDrawingPoint.X - StartDrawingPoint.X);
        }

        private Point MoveStartDrawingPoint(double theta)
        {
            return new Point(StartDrawingPoint.X + Math.Cos(theta + OffsetFromCentralAxis) * offsetFromNodeCenter,
                StartDrawingPoint.Y + Math.Sin(theta + OffsetFromCentralAxis) * offsetFromNodeCenter);
        }

        private Point MoveEndDrawingPoint(double theta)
        {
            return new Point(EndDrawingPoint.X - Math.Cos(theta - OffsetFromCentralAxis) * offsetFromNodeCenter,
                EndDrawingPoint.Y - Math.Sin(theta - OffsetFromCentralAxis) * offsetFromNodeCenter);
        }

        private PathGeometry CreateEdgeGeometry()
        {
            var length = CalulateLengthBetweenStartAndEndPoints();
            var edge = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = StartDrawingPoint,
                IsClosed = false,
                IsFilled = false
            };
            figure.Segments.Add(new ArcSegment
            {
                Point = EndDrawingPoint,
                Size = new Size(length*1.5, length*1.5)
            });
            edge.Figures.Add(figure);
            return edge;
        }

        private PathGeometry CreateTriangleGeometry(double theta)
        {
            var leftArrowPoint = new Point(EndDrawingPoint.X - ArrowHeadWidth, EndDrawingPoint.Y + ArrowHeadHeight);
            var rightArrowPoint = new Point(EndDrawingPoint.X + ArrowHeadWidth, EndDrawingPoint.Y + ArrowHeadHeight);

            var triangleGeometry = new PathGeometry();
            var segment = new PathFigure(EndDrawingPoint, new[]
            {
                new LineSegment(leftArrowPoint, true),
                new LineSegment(rightArrowPoint, true)
            }, true)
            {
                IsFilled = true
            };
            var transform = new RotateTransform
            {
                Angle = theta * 180 / Math.PI + 75,
                CenterX = EndDrawingPoint.X,
                CenterY = EndDrawingPoint.Y
            };
            triangleGeometry.Transform = transform;
            triangleGeometry.Figures.Add(segment);
            return triangleGeometry;
        }

        private double CalulateLengthBetweenStartAndEndPoints()
        {
            return
                Math.Sqrt(Math.Pow(EndDrawingPoint.X - StartDrawingPoint.X, 2) +
                          Math.Pow(EndDrawingPoint.Y - StartDrawingPoint.Y, 2));
        }

        public void ChangeViewToDefault()
        {
            Status = NodeStatus.NotInclude;
            BorderBrush = Brushes.DarkGray;
            Panel.SetZIndex(this, 0);
        }

        public void ChangeView()
        {
            switch (Status)
            {
                case NodeStatus.Incomming:
                    BorderBrush = Brushes.Gold;
                    Panel.SetZIndex(this, 5);
                    break;
                case NodeStatus.Outgoing:
                    BorderBrush = Brushes.DodgerBlue;
                    Panel.SetZIndex(this, 5);
                    break;
                case NodeStatus.InCycle:
                    BorderBrush = Brushes.LightSeaGreen;
                    Panel.SetZIndex(this, 5);
                    break;
                default:
                    BorderBrush = Brushes.Gainsboro;
                    break;
            }
        }
    }
}
