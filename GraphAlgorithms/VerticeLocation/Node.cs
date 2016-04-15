using System.Collections.Generic;
using GraphAlgorithms.VerticeLocation.Geometry;

namespace GraphAlgorithms.VerticeLocation
{
    public class Node
    {
        public int Number { get; }
        public Point Location { get; set; }
        public IList<Node> Connections { get; }

        public Node(int number)
        {
            Number = number;
            Connections = new List<Node>();
        }

        public Node AddChild(Node node)
        {
            Connections.Add(node);
            return this;
        }

        public Node AddParent(Node node)
        {
            node.AddChild(this);
            return this;
        }

        public bool Disconnect(Node node)
        {
            return Connections.Remove(node);
        }
    }
}