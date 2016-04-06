using System.Collections.Generic;
using GraphAlgorithms.Geometry;

namespace GraphAlgorithms
{
    public interface IVerticesLocator
    {
        IList<Node> Nodes { get; }

        IVerticesLocator AddNode(Node node);
        bool RemoveNode(Node node);
        Size Locate();
        void Clear();
    }
}