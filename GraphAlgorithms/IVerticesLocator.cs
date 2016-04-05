using System.Collections.Generic;

namespace GraphAlgorithms
{
    public interface IVerticesLocator
    {
        IList<Node> Nodes { get; }

        IVerticesLocator AddNode(Node node);
        bool RemoveNode(Node node);
        void Locate();
        void Clear();
    }
}