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
            var intersections = new HashSet<ConvexPolygon>();

            foreach (var infrastructureObject in infrastructureObjects)
            {
                foreach (var convexPolygon in infrastructureObjects)
                {
                    if(convexPolygon != infrastructureObject)
                    {
                        var intersection = ShapeHelper.FindShapesIntersection(convexPolygon, infrastructureObject);
                        if (!intersections.Contains(intersection))
                        {
                            intersections.Add(intersection);
                        }
                    }
                }
            }

            return infrastructureObjects.Sum(o => o.Area) - intersections.Sum(i => i.Area);
        }
    }
}
