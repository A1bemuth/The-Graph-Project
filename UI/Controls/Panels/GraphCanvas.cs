using System;
using System.Windows;
using System.Windows.Controls;
using GraphAlgorithms;

namespace UI.Controls.Panels
{
    public class GraphCanvas : Canvas
    {
        public Point Center { get; set; }
        private Point center;
        private double comprasionRatio;
        private double verticesScale;


        public DependencyProperty VerticesLocatorProperty = DependencyProperty.Register("VerticesLocator",
           typeof(IVerticesLocator), typeof(GraphCanvas), new FrameworkPropertyMetadata(null, LocatorChanged));

        private static void LocatorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var panel = dependencyObject as GraphCanvas;
            panel.RelocateGraph();
        }

        public void RelocateGraph()
        {
            Children.Clear();
            if(VerticesLocator == null)
                return;
            VerticesLocator.Locate();

            foreach (var node in VerticesLocator.Nodes)
            {
                var nodeView = new NodeView
                {
                    Center = new Point(node.Location.X, node.Location.Y)
                };
                
                foreach (var connection in node.Connections)
                {
                    var startPoint = new Point(node.Location.X, node.Location.Y);
                    var endPoint = new Point(connection.Location.X, connection.Location.Y);
                    var line = new Arrow(startPoint, endPoint);
                    Children.Add(line);
                }
                Children.Add(nodeView);
            }
        }

        public IVerticesLocator VerticesLocator
        {
            get { return (IVerticesLocator)GetValue(VerticesLocatorProperty); }
            set { SetValue(VerticesLocatorProperty, value); }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            center = new Point(arrangeSize.Width/2, arrangeSize.Height/2);
            CalcVerticeScale();
            CalcComprasionRatio(arrangeSize);

            foreach (UIElement child in Children)
            {
                if (child is NodeView)
                {
                    var node = child as NodeView;
                    node.Scale = verticesScale;
                    var topLeftPoint = CalcShiftFor(node.Center, verticesScale / 2);
                    child.Arrange(new Rect(topLeftPoint, new Size(verticesScale, verticesScale)));
                }
                else
                {
                    var arrow = child as Arrow;
                    var startArrowPoint = CalcShiftFor(arrow.StartPoint);
                    var endArrowPoint = CalcShiftFor(arrow.EndPoint);
                    arrow.SetCanvasParameters(startArrowPoint, endArrowPoint);

                    arrow.Arrange(new Rect(new Point(), arrow.DesiredSize));
                }
            }
            return arrangeSize;
        }

        private void CalcComprasionRatio(Size arrangeSize)
        {
            if (VerticesLocator == null)
                return;
            var verticalScaleFactor = (arrangeSize.Height - 2 * verticesScale)/VerticesLocator.Size.Height;
            var horizontalScaleFactor = (arrangeSize.Width - 2 * verticesScale)/VerticesLocator.Size.Width;
            comprasionRatio = Math.Min(verticalScaleFactor, horizontalScaleFactor);
        }

        private void CalcVerticeScale()
        {
            var side = Math.Min(ActualHeight, ActualWidth);
            var verticesCount = VerticesLocator?.Nodes.Count ?? 0;
            verticesScale = Math.Min(side/verticesCount, side/10);
        }

        private Point CalcShiftFor(Point point, double shift = 0.0D)
        {
            return new Point(center.X + point.X * comprasionRatio - shift,
                        center.Y - point.Y * comprasionRatio - shift);
        }
    }
}