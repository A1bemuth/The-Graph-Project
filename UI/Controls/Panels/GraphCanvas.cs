using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GraphAlgorithms;
using GraphDataLayer;

namespace UI.Controls.Panels
{
    public class GraphCanvas : Canvas
    {
        private Point center;
        private double comprasionRatio;
        private double verticesScale;

        private Arrow[] arrows;
        private NodeView[] nodes;

        public GraphCanvas() : base()
        {
            arrows = new Arrow[0];
            nodes = new NodeView[0];
        }

        public IVerticesLocator VerticesLocator { get; } = new ForceVerticesLocator();


        public DependencyProperty GraphProperty = DependencyProperty.Register("Graph", typeof (IGraph),
            typeof (GraphCanvas), new FrameworkPropertyMetadata(null, GraphChanded));

        public IGraph Graph
        {
            get { return (IGraph) GetValue(GraphProperty); }
            set { SetValue(GraphProperty, value); }
        }

        private static void GraphChanded(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var graphCanvas = dependencyObject as GraphCanvas;
            if(graphCanvas == null)
                return;
            graphCanvas.CreateGraphMathModel();
            graphCanvas.RelocateGraph();
        }

        private void CreateGraphMathModel()
        {
            var vertices = Enumerable.Range(0, Graph.VerticesCount)
                .Select(i => new Node())
                .ToArray();
            for (int i = 0; i < vertices.Length; i++)
            {
                foreach (var neighbour in Graph.GetNeighbours(i))
                {
                    vertices[i].AddChild(vertices[neighbour]);
                }
                VerticesLocator.AddNode(vertices[i]);
            }

            nodes = new NodeView[Graph.VerticesCount];
            arrows = new Arrow[Graph.ArrowsCount];
        }

        private void RelocateGraph()
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
                SetZIndex(nodeView, 10);
                
                foreach (var connection in node.Connections)
                {
                    var startPoint = new Point(node.Location.X, node.Location.Y);
                    var endPoint = new Point(connection.Location.X, connection.Location.Y);
                    var line = new Arrow(startPoint, endPoint);
                    Children.Add(line);
                    nodeView.Arrows.Add(line);
                }
                Children.Add(nodeView);
            }
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
                    var nodeScale = node.IsSelected ? verticesScale + 10 : verticesScale;
                    node.Scale = nodeScale;
                    var topLeftPoint = CalcShiftFor(node.Center, nodeScale / 2);
                    child.Arrange(new Rect(topLeftPoint, new Size(nodeScale, nodeScale)));
                }
                else
                {
                    var arrow = child as Arrow;
                    var startArrowPoint = CalcShiftFor(arrow.StartPoint);
                    var endArrowPoint = CalcShiftFor(arrow.EndPoint);
                    arrow.SetCanvasParameters(startArrowPoint, endArrowPoint);

                    arrow.Arrange(new Rect(new Point(), arrangeSize));
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
            verticesScale = Math.Min(side/verticesCount, side/15);
        }

        private Point CalcShiftFor(Point point, double shift = 0.0D)
        {
            return new Point(center.X + point.X * comprasionRatio - shift,
                        center.Y - point.Y * comprasionRatio - shift);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if(e.Handled)
                return;
            foreach (var child in Children)
            {
                var graphObject = child as IGraphObject;
                graphObject?.ChangeStateToDefault();
            }
        }
    }
}