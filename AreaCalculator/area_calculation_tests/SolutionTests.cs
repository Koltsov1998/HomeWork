using AreaCalculator.Calculation.Shapes;
using AreaCalculator.Calculation.Solvers;
using Xunit;

namespace AreaCalculator.Calculation.Tests
{
    public class SolutionTests
    {
        [Fact]
        public void TestSimpleAreaEvaluation()
        {
            var shape1 = ConvexPolygon.Create(
                new Point(0, 0),
                new Point(0, 2),
                new Point(2, 2),
                new Point(2, 0)
            );

            var shape2 = ConvexPolygon.Create(
                new Point(1, 1),
                new Point(1, 3),
                new Point(3, 3),
                new Point(3, 1)
            );

            var solver = new Solver();

            var area = solver.CalculateInfrastructureArea(new[] { shape1, shape2 });

            Assert.Equal(7, area);
        }

        [Fact]
        public void TestComplexAreaEvaluation2()
        {
            var shape1 = ConvexPolygon.Create(
                new Point(0, 0),
                new Point(0, 2),
                new Point(2, 2),
                new Point(2, 0)
            );

            var shape3 = ConvexPolygon.Create(
                new Point(0, 1),
                new Point(0, 3),
                new Point(2, 3),
                new Point(2, 1)
            );

            var shape2 = ConvexPolygon.Create(
                new Point(1, 1),
                new Point(1, 3),
                new Point(3, 3),
                new Point(3, 1)
            );


            var solver = new Solver();

            var area = solver.CalculateInfrastructureArea(new[] { shape1, shape2, shape3 });

            Assert.Equal(8, area);
        }

        [Fact]
        public void TestComplexAreaEvaluation3()
        {
            var shape1 = ConvexPolygon.Create(
                new Point(0, 0),
                new Point(0, 5),
                new Point(5, 5),
                new Point(5, 0)
            );

            var shape3 = ConvexPolygon.Create(
                new Point(0, 1),
                new Point(0, 4),
                new Point(5, 4),
                new Point(5, 1)
            );

            var shape2 = ConvexPolygon.Create(
                new Point(0, 2),
                new Point(0, 3),
                new Point(5, 3),
                new Point(5, 2)
            );


            var solver = new Solver();

            var area = solver.CalculateInfrastructureArea(new[] { shape1, shape2, shape3 });

            Assert.Equal(25, area);
        }

        [Fact]
        public void TestComplexAreaEvaluation()
        {
            var shape1 = ConvexPolygon.Create(
                new Point(0, 0),
                new Point(0, 2),
                new Point(2, 2),
                new Point(2, 0)
            );

            var shape3 = ConvexPolygon.Create(
                new Point(0, 1),
                new Point(0, 3),
                new Point(2, 3),
                new Point(2, 1)
            );

            var shape2 = ConvexPolygon.Create(
                new Point(1, 1),
                new Point(1, 3),
                new Point(3, 3),
                new Point(3, 1)
            );

            var shape4 = ConvexPolygon.Create(
                new Point(1, 0),
                new Point(1, 2),
                new Point(3, 2),
                new Point(3, 0)
            );

            var solver = new Solver();

            var area = solver.CalculateInfrastructureArea(new[] { shape1, shape2, shape3, shape4 });

            Assert.Equal(9, area);
        }
    }
}
