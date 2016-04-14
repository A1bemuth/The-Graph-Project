using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GraphAlgorithms;
using GraphDataLayer;

namespace UI.Controls
{
    public class GraphCanvas : Panel
    {
        private Point center;
        private double comprasionRatio;
        private double verticesScale;

        private List<NodeView> nodes;
        private List<Arrow> arrows;

        public IVerticesLocator VerticesLocator { get; } = new ForceVerticesLocator();

        public DependencyProperty GraphProperty = DependencyProperty.Register("Graph", typeof (IGraph),
            typeof (GraphCanvas), new FrameworkPropertyMetadata(null, GraphChanded));

        public DependencyProperty SelectedCycleProperty = DependencyProperty.Register("SelectedCycle", typeof(IEnumerable<int>), typeof(GraphCanvas),
            new FrameworkPropertyMetadata(new List<int>(), SelectedCycleChanged));

        private static void SelectedCycleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var graphCanvas = dependencyObject as GraphCanvas;
            if(graphCanvas == null)
                return;
            graphCanvas.SetChildrenDefaultViews();
            graphCanvas.PickOutCycle();
        }

        public IGraph Graph
        {
            get { return (IGraph) GetValue(GraphProperty); }
            set { SetValue(GraphProperty, value); }
        }

        public int[] SelectedCycle
        {
            get { return (int[]) GetValue(SelectedCycleProperty); }
            set { SetValue(SelectedCycleProperty, value); }
        }

        private void SetChildrenDefaultViews()
        {
            foreach (var child in Children)
            {
                var graphObject = child as IGraphObject;
                graphObject?.ChangeViewToDefault();
            }
        }

        private void PickOutCycle()
        {
            if (SelectedCycle == null)
                return;
            for (int i = 0; i < SelectedCycle.Length; i++)
            {
                nodes[SelectedCycle[i]].IncludeInCycle(nodes[SelectedCycle[(i + 1)%SelectedCycle.Length]]);
            }

            foreach (IGraphObject child in Children)
            {
                child.ChangeView();
            }
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
            VerticesLocator.Clear();
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
        }

        private void RelocateGraph()
        {
            Children.Clear();
            
            if(VerticesLocator == null)
                return;
            VerticesLocator.Locate();

            nodes = VerticesLocator.Nodes.Select(n => new NodeView
            {
                Center = new Point(n.Location.X, n.Location.Y)
            }).ToList();
            arrows = new List<Arrow>(Graph.ArrowsCount);

            for (var i = 0; i < nodes.Count; i++)
            {
                SetZIndex(nodes[i], 10);

                foreach (var connection in Graph.GetNeighbours(i))
                {
                    var line = new Arrow(nodes[i], nodes[connection]);
                    Children.Add(line);
                    nodes[i].Arrows.Add(line);
                    nodes[connection].Arrows.Add(line);
                    arrows.Add(line);
                }
                Children.Add(nodes[i]);
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
                    node.Scale = verticesScale;
                    var topLeftPoint = CalcShiftFor(node.Center, verticesScale / 2);
                    child.Arrange(new Rect(topLeftPoint, new Size(verticesScale, verticesScale)));
                }
                else
                {
                    var arrow = child as Arrow;
                    var startArrowPoint = CalcShiftFor(arrow.StartNode.Center);
                    var endArrowPoint = CalcShiftFor(arrow.EndNode.Center);
                    arrow.SetCanvasParameters(startArrowPoint, endArrowPoint, verticesScale);

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

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            SetChildrenDefaultViews();
            base.OnPreviewMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if(e.Source.GetType() != typeof(NodeView))
                return;
            
            foreach (var child in Children)
            {
                var graphObject = child as IGraphObject;
                graphObject?.ChangeView();
            }
        }
    }
}