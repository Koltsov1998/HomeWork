using AreaCalculator.Calculation.Shapes;
using MathNet.Numerics.LinearAlgebra.Double;
using Vector = MathNet.Numerics.LinearAlgebra.Double.Vector;

namespace AreaCalculator.Calculation.Lines
{
    /// <summary>
    ///     Utility functions to process lines
    /// </summary>
    public static class LineHelper
    {
        public static Point FindLinesIntersection(Line firstLine, Line secondLine)
        {
            var (a1, b1, c1) = (firstLine.A, firstLine.B, firstLine.C);
            var (a2, b2, c2) = (secondLine.A, secondLine.B, secondLine.C);

            var m = Matrix.Build.DenseOfRowArrays(new[] {a1, b1}, new[] {a2, b2});
            var v = Vector.Build.DenseOfArray(new[] {-c1, -c2});
            if (m.Determinant() == 0)
            {
                return null;
            }
            var solution = m.Solve(v);

            var (x, y) = (solution[0], solution[1]);

            // handle parallel lines
            if (double.IsNaN(x) || double.IsInfinity(x) || double.IsNaN(y) || double.IsInfinity(y))
            {
                return null;
            }

            return new Point(x, y);
        }

        public static Point FindLinesIntersection(Segment segment, Line line)
        {
            var intersectionPoint = FindLinesIntersection(segment.UnderlyingLine, line);
            if (intersectionPoint == null)
            {
                return null;
            }

            return segment.PointBelongsToSegment(intersectionPoint) ? intersectionPoint : null;
        }

        public static Point FindLinesIntersection(Segment segment1, Segment segment2)
        {
            var intersectionPoint = FindLinesIntersection(segment1.UnderlyingLine, segment2.UnderlyingLine);

            if (intersectionPoint == null)
            {
                return null;
            }

            return segment1.PointBelongsToSegment(intersectionPoint) && segment2.PointBelongsToSegment(intersectionPoint) ? intersectionPoint : null;
        }
    }
}
