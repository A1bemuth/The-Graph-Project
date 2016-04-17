using System;

namespace UI.Infrastructure
{
    [Flags]
    public enum NodeStatus
    {
        NotInclude = 0,
        Selected = 1,
        Incomming = 2,
        Outgoing = 4,
        InCycle = 8
    }
}