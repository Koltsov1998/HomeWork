using System.Collections.Generic;
using System.Linq;
using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Solvers
{
    /// <summary>
    ///     My own heuristic implementation
    /// </summary>
    public class Solver : ISolver
    {
        #region Implementation of ISolver

        public double CalculateInfrastructureArea(IEnumerable<ConvexPolygon> infrastructureObjects)
        {
            return CalculateInfrastructureArea(infrastructureObjects, 0);
        }

        #endregion

        #region Private methods

        private double CalculateInfrastructureArea(IEnumerable<ConvexPolygon> infrastructureObjects, double summaryArea)
        {
            if (!infrastructureObjects.Any())
            {
                return 0;
            }

            infrastructureObjects = Filter(infrastructureObjects);

            HashSet<ConvexPolygon> nextOrderInfrastructureObjects = new HashSet<ConvexPolygon>();

            double pureAreaSum = 0;

            // calculate area of polygons not covered by other polygons
            foreach (var shape in infrastructureObjects)
            {
                var areaWithoutIntersections = 0;

                var inters = infrastructureObjects
                    .Except(new[] { shape })
                    .Select(o => PolygonHelper.FindShapesIntersection(o, shape))
                    .Where(i => i != null);

                HashSet<ConvexPolygon> intersections = new HashSet<ConvexPolygon>(inters);

                var pureArea = shape.Area - GetSummaryAreaOfIntersections(intersections);

                pureAreaSum += pureArea;

                // store intersected shapes to handle them in next recursion step
                foreach (var i in intersections)
                {
                    nextOrderInfrastructureObjects.Add(i);
                }
            }

            summaryArea += pureAreaSum + CalculateInfrastructureArea(nextOrderInfrastructureObjects, summaryArea);

            return summaryArea;
        }

        private double GetSummaryAreaOfIntersections(IEnumerable<ConvexPolygon> intersections)
        {
            if (!intersections.Any())
            {
                return 0;
            }

            intersections = Filter(intersections);
            var nextOrderIntersections = GetIntersections(intersections);

            var result = intersections.Sum(i => i.Area) - GetSummaryAreaOfIntersections(nextOrderIntersections);

            return result;
        }

        private IEnumerable<ConvexPolygon> GetIntersections(IEnumerable<ConvexPolygon> infrastructureObjects)
        {
            var intersections = new HashSet<ConvexPolygon>();

            foreach (var infrastructureObject in infrastructureObjects)
            {
                foreach (var convexPolygon in infrastructureObjects)
                {
                    if (convexPolygon != infrastructureObject)
                    {
                        var intersection = PolygonHelper.FindShapesIntersection(convexPolygon, infrastructureObject);

                        intersections.Add(intersection);
                    }
                }
            }

            return intersections.Where(i => i != null);
        }

        /// <summary>
        ///     Filter from shapes that are lying inside another shapes
        /// </summary>
        private IEnumerable<ConvexPolygon> Filter(IEnumerable<ConvexPolygon> shapes)
        {
            var result = shapes.Where(s => !shapes.Except(new[] { s }).Any(s.IsInsideAnotherCp));

            return result;
        }

        #endregion
    }
}
