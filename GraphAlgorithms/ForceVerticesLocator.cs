using System;
using System.Collections.Generic;
using System.Linq;
using GraphAlgorithms.Geometry;

namespace GraphAlgorithms
{
    public class ForceVerticesLocator : IVerticesLocator
    {
        private static double nodeSize = 20;
        private static double Attraction { get; } = 0.1;
        private static double Repulsion { get; } = 10000;
        private static double Damping { get; } = 0.5;

        private static int SpringLength { get; } = 100;
        private static int MaxIterations { get; } = 10000;

        private readonly List<Node> nodes;

        public IList<Node> Nodes => nodes.AsReadOnly();

        internal class NodeLayoutInfo
        {
            internal Node Node { get; }
            internal Vector Velocity { get; set; }
            internal Point NextPosition { get; set; }

            public NodeLayoutInfo(Node node, Vector velocity, Point nextPosition)
            {
                Node = node;
                Velocity = velocity;
                NextPosition = nextPosition;
            }
        }

        public ForceVerticesLocator()
        {
            nodes = new List<Node>();
        }

        public IVerticesLocator AddNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (!nodes.Contains(node))
                nodes.Add(node);

            return this;
        }

        public bool RemoveNode(Node node)
        {
            foreach (var other in nodes)
            {
                if ((other != node) && other.Connections.Contains(node))
                    other.Disconnect(node);
            }
            return nodes.Remove(node);
        }

        public void Clear()
        {
            nodes.Clear();
        }

        public void Locate()
        {
            var layout = LocateToStartLayout();
            var stopCount = 0;
            var iterations = 0;

            while (true)
            {
                var totalDisplacement = 0.0D;

                foreach (var current in layout)
                {
                    var currentPosition = new Vector(Point.DistanceBetween(Point.Empty, current.Node.Location),
                        Point.BearingAngleBetween(Point.Empty, current.Node.Location));
                    var netForce = new Vector(0, 0);

                    netForce = nodes
                        .Where(other => other != current.Node)
                        .Aggregate(netForce, (current1, other) => current1 + CalcRepulsionForce(current.Node, other));

                    netForce = current.Node.Connections
                        .Aggregate(netForce, (current1, child) => current1 + CalcAttractionForce(current.Node, child, SpringLength));
                    netForce = nodes
                        .Where(parent => parent.Connections.Contains(current.Node))
                        .Aggregate(netForce, (current1, parent) => current1 + CalcAttractionForce(current.Node, parent, SpringLength));

                    current.Velocity = (current.Velocity + netForce) * Damping;
                    current.NextPosition = (currentPosition + current.Velocity).ToPoint();
                }

                foreach (var current in layout)
                {
                    totalDisplacement += current.Node.Location.Distance(current.NextPosition);
                    current.Node.Location = current.NextPosition;
                }

                iterations++;
                if (totalDisplacement < 10) stopCount++;
                if (stopCount > 15) break;
                if (iterations > MaxIterations) break;
            }

            var diffLocation = DifferentWithCenter();

            foreach (var node in nodes)
            {
                var location = new Point(node.Location.X - diffLocation.X, node.Location.Y - diffLocation.Y);
                node.Location = location;
            }
        }

        private NodeLayoutInfo[] LocateToStartLayout()
        {
            var rand = new Random(0);
            return Enumerable.Range(0, nodes.Count)
                .Select(i => new NodeLayoutInfo(nodes[i], new Vector(), Point.Empty)
                {
                    Node =
                    {
                        Location = new Point(rand.Next(-50,50), rand.Next(-50,50))
                    }
                })
                .ToArray();
        }

        private Vector CalcRepulsionForce(Node x, Node y)
        {
            var proximity = Math.Max(x.Location.Distance(y.Location), 1);

            // Coulomb's Law: F = k(Qq/r^2)
            var force = -(Repulsion / proximity / proximity);
            var angle = x.Location.BearingAngle(y.Location);

            return new Vector(force, angle);
        }

        private Vector CalcAttractionForce(Node x, Node y, double springLength)
        {
            var proximity = Math.Max(x.Location.Distance(y.Location), 1);

            // Hooke's Law: F = -kx
            var force = Attraction * Math.Max(proximity - springLength, 0);
            var angle = x.Location.BearingAngle(y.Location);

            return new Vector(force, angle);
        }

        private Point DifferentWithCenter()
        {
            var minX = int.MaxValue;
            var minY = int.MaxValue;
            var maxX = int.MinValue;
            var maxY = int.MinValue;

            foreach (var node in nodes)
            {
                if (node.Location.X < minX) minX = node.Location.X;
                if (node.Location.X > maxX) maxX = node.Location.X;
                if (node.Location.Y < minY) minY = node.Location.Y;
                if (node.Location.Y > maxY) maxY = node.Location.Y;
            }

            var width = Math.Abs(maxX - minX);
            var height = Math.Abs(maxY - minY);

            return new Point(minX + width / 2, minY + height / 2);
        }
    }
}