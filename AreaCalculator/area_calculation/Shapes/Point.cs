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
    }
}
