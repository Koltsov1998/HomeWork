using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AreaCalculator.Calculation.Lines;

namespace AreaCalculator.Calculation.Shapes
{
    public static class ShapeHelper
    {
        public static IPlaneShape BuildThickLineRectangle(Line firstLine, Line secondLine, Segment[] areaBorders)
        {
            var points1 = areaBorders.Select(b => LineHelper.FindLinesIntersection(b, firstLine)).Where(p => p != null).ToArray();
            var points2 = areaBorders.Select(b => LineHelper.FindLinesIntersection(b, secondLine)).Where(p => p != null).ToArray();

            var intersectionPoints = Enumerable.Concat(points1, points2);

            if (intersectionPoints.Count() != 4)
            {
                throw  new ArgumentException("Area borders has wrong configuration");
            }

            var infrastractureArea = ConvexPolygon.Create(intersectionPoints);

            return infrastractureArea;
        }

        public static ConvexPolygon FindShapesIntersection(ConvexPolygon shape1, ConvexPolygon shape2)
        {
            List<Point> intersectionPoints = new List<Point>();

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

            // adding points of figure that are lying inside another figure
            intersectionPoints.AddRange(shape1.Points.Where(p => shape2.PointLiesInside(p)));
            intersectionPoints.AddRange(shape2.Points.Where(p => shape1.PointLiesInside(p)));

            return ConvexPolygon.Create(intersectionPoints);
        }
    }
}
