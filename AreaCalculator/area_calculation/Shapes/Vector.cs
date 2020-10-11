using System;

namespace AreaCalculator.Calculation.Shapes
{
    public class Vector
    {
        public Vector(Point point)
        {
            X = point.X;
            Y = point.Y;
            Lenght = Math.Sqrt(X * X + Y * Y);
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
            Lenght = Math.Sqrt(X * X + Y * Y);
        }

        public Vector(Point start, Point end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
            Lenght = Math.Sqrt(X * X + Y * Y);
        }

        #region Public properties

        public readonly double X;

        public readonly double Y;

        public readonly double Lenght;

        #endregion

        #region Public methods

        public Vector Normalize()
        {
            return this / Lenght;
        }

        public static double operator *(Vector first, Vector second)
        {
            return first.X * second.X + first.Y * second.Y;
        }

        public static Vector operator *(Vector vector, double multiplier)
        {
            return new Vector(vector.X * multiplier, vector.Y * multiplier);
        }

        public static Vector operator /(Vector vector, double divider)
        {
            return new Vector(vector.X / divider, vector.Y / divider);
        }

        public static Vector operator +(Vector first, Vector second)
        {
            return new Vector(first.X + second.X, first.Y + second.Y);
        }

        public static Vector operator -(Vector first, Vector second)
        {
            return new Vector(first.X - second.X, first.Y - second.Y);
        }

        #endregion

    }
}
