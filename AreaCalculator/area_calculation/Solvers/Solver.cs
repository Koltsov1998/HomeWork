using System;
using System.Collections.Generic;
using System.Linq;
using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Solvers
{
    public class Solver : ISolver
    {
        public double CalculateInfrastructureArea(ConvexPolygon[] infrastructureObjects)
        {
            var result = GetAreaOfIntersections(infrastructureObjects);

            return result;
        }

        public double GetAreaOfIntersections(IEnumerable<ConvexPolygon> intersections)
        {
            if (!intersections.Any())
            {
                return 0;
            }

            var nextOrderIntersections = GetIntersections(intersections);

            var sum = intersections.Sum(i => i.Area);
            var intesectionsArea = GetAreaOfIntersections(nextOrderIntersections);
            var result = sum - intesectionsArea;

            return result;
        }

        public IEnumerable<ConvexPolygon> GetIntersections(IEnumerable<ConvexPolygon> infrastructureObjects)
        {
            var intersections = new HashSet<ConvexPolygon>();

            foreach (var infrastructureObject in infrastructureObjects)
            {
                foreach (var convexPolygon in infrastructureObjects)
                {
                    if (convexPolygon != infrastructureObject)
                    {
                        var intersection = ShapeHelper.FindShapesIntersection(convexPolygon, infrastructureObject);

                        intersections.Add(intersection);
                    }
                }
            }

            return intersections;
        }
    }
}
