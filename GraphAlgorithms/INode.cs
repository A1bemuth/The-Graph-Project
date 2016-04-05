using System.Collections.Generic;
using GraphAlgorithms.Geometry;

namespace GraphAlgorithms
{
    public interface INode
    {
        Size Size { get; }
        Point Location { get; set; }
        IList<INode> Connections { get; }

        INode AddChild(INode node);
        INode AddParent(INode node);
        bool Disconnect(INode node);
    }
}