using System.Collections.Generic;
using GraphAlgorithms.Geometry;

namespace GraphAlgorithms
{
    public interface IVerticesLocator
    {
        Size Size { get; }
        IList<Node> Nodes { get; }

        IVerticesLocator AddNode(Node node);
        bool RemoveNode(Node node);
        void Locate();
        void Clear();
    }
}