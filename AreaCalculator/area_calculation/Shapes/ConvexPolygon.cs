using System;
using System.Collections.Generic;
using System.Linq;
using AreaCalculator.Calculation.Lines;

namespace AreaCalculator.Calculation.Shapes
{
    public class ConvexPolygon : IPlaneShape
    {
        private ConvexPolygon(Point[] points)
        {
            var leftPoint = points.OrderBy(p => p.X).ThenByDescending(p => p.Y).First();

            var sorted = points.Except(new []{ leftPoint }).OrderByDescending(p =>
            {
                var dy = p.Y - leftPoint.Y;
                var dx = p.X - leftPoint.X;
                var L = Math.Sqrt(dx*dx + dy*dy);
                var sin = dy / L;

                return sin;
            });

            Points = Enumerable.Concat(new [] { leftPoint }, sorted).ToArray();

            Area = CalculateSquare();
            BorderSegments = InitBorderLines();
        }

        public static ConvexPolygon Create(IEnumerable<Point> points)
        {
            return new ConvexPolygon(points.ToArray());
        }

        public static ConvexPolygon Create(params Point[] points)
        {
            return new ConvexPolygon(points);
        }

        #region Public properties

        public readonly Point[] Points;

        public double Area { get; }

        public Segment[] BorderSegments { get; }

        #endregion

        #region Public methods

        public bool PointLiesInside(Point point)
        {
            var centralPointX = Points.Average(p => p.X);
            var centralPointY = Points.Average(p => p.Y);

            var centralPoint = new Point(centralPointX, centralPointY);

            var result = BorderSegments.Select(s => s.UnderlyingLine).All(l => l >= centralPoint == l >= point);

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Base implementation
        /// </summary>
        private double CalculateSquare()
        {
            double square = 0;

            var firstPoint = Points.First();
            var normalizedPoints = Points.Select(p => p - new Vector(firstPoint)).ToArray();

            for (int i = 1; i < normalizedPoints.Length; i++)
            {
                var currentPoint = normalizedPoints[i];
                var nextPoint = i == normalizedPoints.Length - 1 ? normalizedPoints[0] : normalizedPoints[i + 1];
                square += (currentPoint.X + nextPoint.X) * (currentPoint.Y - nextPoint.Y);
            }

            square /= 2;

            return square;
        }

        private Segment[] InitBorderLines()
        {
            List<Segment> borders = new List<Segment>();

            for (int i = 0; i < Points.Length; i++)
            {
                var currentPoint = Points[i];
                var nextPoint = i == Points.Length - 1 ? Points[0] : Points[i + 1];
                borders.Add(new Segment(currentPoint, nextPoint));
            }

            return borders.ToArray();
        }

        #endregion
    }
}
