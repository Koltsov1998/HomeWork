using System.Collections.Generic;
using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Solvers
{
    public interface ISolver
    {
        double CalculateInfrastructureArea(IEnumerable<ConvexPolygon> infrastructureObjects);
    }
}
