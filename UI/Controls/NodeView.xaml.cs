using System.Collections.Generic;
using System.Windows.Controls;
using GraphAlgorithms;
using GraphAlgorithms.Geometry;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for NodeView.xaml
    /// </summary>
    public partial class NodeView : UserControl, INode
    {
        public NodeView()
        {
            InitializeComponent();
            Size = new Size(25, 25);
            Connections = new List<INode>();
        }

        public Size Size {
            get
            {
                return new Size((int)Width, (int)Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public Point Location { get; set; }

        public IList<INode> Connections { get; }

        public INode AddChild(INode node)
        {
            Connections.Add(node);
            return this;
        }

        public INode AddParent(INode node)
        {
            node.AddChild(this);
            return this;
        }

        public bool Disconnect(INode node)
        {
            return Connections.Remove(node);
        }
    }
}
