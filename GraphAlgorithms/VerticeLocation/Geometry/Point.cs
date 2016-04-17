using System;

namespace GraphAlgorithms.VerticeLocation.Geometry
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Distance(Point b)
        {
            double xDist = X - b.X;
            double yDist = Y - b.Y;
            return (int)Math.Sqrt(Math.Pow(xDist, 2) + Math.Pow(yDist, 2));
        }

        public double BearingAngle(Point end)
        {
            var half = new Point(X + (end.X - X) / 2, Y + (end.Y - Y) / 2);

            double diffX = half.X - X;
            double diffY = half.Y - Y;

            if (Math.Abs(diffX) < 0.001) diffX = 0.001;
            if (Math.Abs(diffY) < 0.001) diffY = 0.001;

            double angle;
            if (Math.Abs(diffX) > Math.Abs(diffY))
            {
                angle = Math.Tanh(diffY / diffX) * (180.0 / Math.PI);
                if (((diffX < 0) && (diffY > 0)) || ((diffX < 0) && (diffY < 0))) angle += 180;
            }
            else {
                angle = Math.Tanh(diffX / diffY) * (180.0 / Math.PI);
                if (((diffY < 0) && (diffX > 0)) || ((diffY < 0) && (diffX < 0))) angle += 180;
                angle = (180 - (angle + 90));
            }

            return angle;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }

        public static Point Empty => new Point(0, 0);

        public static int DistanceBetween(Point a, Point b)
        {
            return a.Distance(b);
        }

        public static double BearingAngleBetween(Point start, Point end)
        {
            return start.BearingAngle(end);
        }
    }
}