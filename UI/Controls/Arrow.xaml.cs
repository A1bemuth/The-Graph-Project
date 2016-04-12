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

        public Point StartPoint { get; private set; }
        
        public Point EndPoint { get; private set; }

        public double Offset { get; private set; }

        public Arrow()
        {
            InitializeComponent();
        }

        public Arrow(NodeView start, NodeView end) : this()
        {
            StartNode = start;
            EndNode = end;
            StartPoint = StartNode.Center;
            EndPoint = EndNode.Center;
        }

        public void SetCanvasParameters(Point start, Point end, double offset)
        {
            StartPoint = start;
            EndPoint = end;
            Offset = offset/2 + 5;
            UpdateGeometry();
        }

        private void UpdateGeometry()
        {
            var mainGeometry = new GeometryGroup();
            var angle = 8d;
            var theta = Math.Atan2(EndPoint.Y - StartPoint.Y, EndPoint.X - StartPoint.X);
            StartPoint = new Point(StartPoint.X + Math.Cos(theta + angle*Math.PI/180)*Offset,
                StartPoint.Y + Math.Sin(theta + angle*Math.PI/180)*Offset);
            EndPoint = new Point(EndPoint.X - Math.Cos(theta - angle*Math.PI/180)*Offset,
                EndPoint.Y - Math.Sin(theta - angle*Math.PI/180)*Offset);

            var edgeGeometry = CreateEdgeGeometry();
            var triangleGeometry = CreateTriangleGeometry(theta);
            mainGeometry.Children.Add(edgeGeometry);
            mainGeometry.Children.Add(triangleGeometry);
            Geometry = mainGeometry;
        }

        private PathGeometry CreateEdgeGeometry()
        {
            var length = Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));
            var edge = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = StartPoint,
                IsClosed = false,
                IsFilled = false
            };
            figure.Segments.Add(new ArcSegment
            {
                Point = EndPoint,
                Size = new Size(length*1.5, length*1.5)
            });
            edge.Figures.Add(figure);
            return edge;
        }

        private PathGeometry CreateTriangleGeometry(double theta)
        {
            var headWidth = 5;
            var headHeight = 13;

            var startArrowPoint = new Point(EndPoint.X, EndPoint.Y);

            var leftPoint = new Point(startArrowPoint.X - headWidth, startArrowPoint.Y + headHeight);
            var rightPoint = new Point(startArrowPoint.X + headWidth, startArrowPoint.Y + headHeight);

            var triangleGeometry = new PathGeometry();
            var segment = new PathFigure(startArrowPoint, new PathSegment[] 
            {
                new LineSegment(leftPoint, true), 
                new LineSegment(rightPoint, true)
            }, true)
            {
                IsFilled = true,
                IsClosed = true
            };
            var transform = new RotateTransform
            {
                Angle = theta * 180 / Math.PI + 75,
                CenterX = EndPoint.X,
                CenterY = EndPoint.Y
            };
            triangleGeometry.Transform = transform;
            triangleGeometry.Figures.Add(segment);
            triangleGeometry.FillRule = FillRule.Nonzero;
            return triangleGeometry;
        }

        public void ChangeViewToDefault()
        {
            Status = NodeStatus.NotInclude;
            BorderBrush = Brushes.DarkGray;
            Panel.SetZIndex(this, 0);
        }

        public void ChangeView()
        {
            if (Status == NodeStatus.NotInclude)
            {
                BorderBrush = Brushes.Gainsboro;
            }
            else if (Status == NodeStatus.Incomming)
            {
                BorderBrush = Brushes.Gold;
                Panel.SetZIndex(this, 5);
            }
            else if (Status == NodeStatus.Outgoing)
            {
                BorderBrush = Brushes.DodgerBlue;
                Panel.SetZIndex(this, 5);
            }
            else
            {
                BorderBrush = Brushes.Gray;
            }
        }
    }
}
