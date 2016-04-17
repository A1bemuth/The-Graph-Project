using System;

namespace GraphAlgorithms.VerticeLocation.Geometry
{
    public struct Vector
    {
        public double Length { get; set; }

        public double Direction { get; set; }

        public Vector(double length, double direction)
        {
            Length = length;
            Direction = direction;

            if (Length < 0)
            {
                Length = -Length;
                Direction = (180.0 + Direction) % 360;
            }
            if (Direction < 0) Direction = (360.0 + Direction);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            var aX = a.Length*Math.Cos(Math.PI/180.0*a.Direction);
            var aY = a.Length*Math.Sin(Math.PI/180.0*a.Direction);
            var bX = b.Length*Math.Cos(Math.PI/180.0*b.Direction);
            var bY = b.Length*Math.Sin(Math.PI/180.0*b.Direction);

            aX += bX;
            aY += bY;

            var length = Math.Sqrt(aX*aX + aY*aY);
            double direction;
            if (Math.Abs(length) < 0)
                direction = 0;
            else
                direction = 180.0/Math.PI*Math.Atan2(aY, aX);
            return new Vector(length, direction);
        }

        public Point ToPoint()
        {
            var aX = Length * Math.Cos(Math.PI / 180.0 * Direction);
            var aY = Length * Math.Sin(Math.PI / 180.0 * Direction);

            return new Point((int)aX, (int)aY);
        }

        public static Vector operator *(Vector vector, double multiplier)
        {
            return new Vector(vector.Length * multiplier, vector.Direction);
        }

        public override string ToString()
        {
            return $"L: {Length} D: {Direction}°";
        }
    }
}