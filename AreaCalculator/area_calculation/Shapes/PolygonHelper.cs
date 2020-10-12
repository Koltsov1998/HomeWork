using System.Collections.Generic;
using System.Linq;
using AreaCalculator.Calculation.Lines;

namespace AreaCalculator.Calculation.Shapes
{
    /// <summary>
    ///     Utility functions to process polygon shapes
    /// </summary>
    public static class PolygonHelper
    {
        public static ConvexPolygon BuildThickLineRectangle(ConvexPolygon boundaryArea, LinearObject linearObject)
        {
            var (firstLine, secondLine) = linearObject.GetBorderLines();

            var points1 = boundaryArea.BorderSegments.Select(b => LineHelper.FindLinesIntersection(b, firstLine)).Where(p => p != null).ToArray();
            var points2 = boundaryArea.BorderSegments.Select(b => LineHelper.FindLinesIntersection(b, secondLine)).Where(p => p != null).ToArray();

            var innerPoints = boundaryArea.Points.Where(p => firstLine > p == secondLine < p);

            var intersectionPoints = points1
                .Concat(points2)
                .Concat(innerPoints);

            var infrastractureArea = ConvexPolygon.Create(intersectionPoints);

            return infrastractureArea;
        }

        public static ConvexPolygon FindShapesIntersection(ConvexPolygon shape1, ConvexPolygon shape2)
        {
            HashSet<Point> intersectionPoints = new HashSet<Point>();

            foreach (var shape1BorderSegment in shape1.BorderSegments)
            {
                foreach (var shape2BorderSegment in shape2.BorderSegments)
                {
                    var intersectionPoint = LineHelper.FindLinesIntersection(shape1BorderSegment, shape2BorderSegment);
                    if (intersectionPoint != null)
                    {
                        intersectionPoints.Add(intersectionPoint);
                    }
                }
            }

            var result = new List<Point>(intersectionPoints);

            // adding points of figure that are lying inside another figure
            result.AddRange(shape1.Points.Where(p => shape2.PointLiesStrictlyInside(p)));
            result.AddRange(shape2.Points.Where(p => shape1.PointLiesStrictlyInside(p)));

            return ConvexPolygon.Create(result);
        }
    }
}
