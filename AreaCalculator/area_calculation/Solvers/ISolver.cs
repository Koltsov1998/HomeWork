using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Solvers
{
    public interface ISolver
    {
        double CalculateInfrastructureArea(ConvexPolygon[] infrastructureObjects);
    }
}
