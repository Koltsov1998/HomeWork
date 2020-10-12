using System;
using System.Collections.Generic;
using System.Text;

namespace AreaCalculator.Calculation.Shapes
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }

        public double Y { get; }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return X.GetHashCode() - Y.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = (Point) obj;

            return this.X == other.X && this.Y == other.Y;
        }
    }
}
