using System.Collections.Generic;
using System.Linq;
using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Solvers
{
    public class Solver : ISolver
    {
        private double _result = 0;

        public double CalculateInfrastructureArea(IEnumerable<ConvexPolygon> infrastructureObjects)
        {
            if (!infrastructureObjects.Any())
            {
                return 0;
            }

            infrastructureObjects = Filter(infrastructureObjects).ToArray();

            HashSet<ConvexPolygon> intersectionsArea = new HashSet<ConvexPolygon>();

            double sum = 0;

            foreach(var shape in infrastructureObjects)
            {
                var areaWithoutIntersections = 0;

                var otherShapes = infrastructureObjects.Except(new[] {shape}).ToArray();
                var inters = otherShapes
                    .Select(o => ShapeHelper.FindShapesIntersection(o, shape))
                    .Where(i => i != null)
                    .ToArray();

                HashSet<ConvexPolygon> intersections =new HashSet<ConvexPolygon>(inters);

                var pureArea = shape.Area - GetSummaryAreaOfIntersections(intersections);

                sum += pureArea;

                foreach(var i in intersections)
                {
                    intersectionsArea.Add(i);
                }
            }

            _result += sum + CalculateInfrastructureArea(intersectionsArea);

            return _result;
        }

        public double GetSummaryAreaOfIntersections(IEnumerable<ConvexPolygon> intersections)
        {
            if(!intersections.Any())
            {
                return 0;
            }

            intersections = Filter(intersections);
            var nextOrderIntersections = GetIntersections(intersections);

            var result = intersections.Sum(i => i.Area) - GetSummaryAreaOfIntersections(nextOrderIntersections);

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

            return intersections.Where(i => i != null);
        }

        public IEnumerable<ConvexPolygon> Filter(IEnumerable<ConvexPolygon> shapes)
        {
            var result = shapes.Where(s => !shapes.Except(new []{s}).Any(s.IsInsideAnotherCp));

            return result;
        }
    }
}
