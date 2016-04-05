using System.Collections.Generic;

namespace GraphAlgorithms
{
    public interface IVerticesLocator
    {
        IList<INode> Nodes { get; }

        IVerticesLocator AddNode(INode node);
        bool RemoveNode(INode node);
        void Locate();
        void Clear();
    }
}