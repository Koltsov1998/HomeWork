using System;
using AreaCalculator.Calculation.Lines;
using AreaCalculator.Calculation.Shapes;
using AreaCalculator.Calculation.Solvers;
using Xunit;

namespace AreaCalculator.Calculation.Tests
{
    public class Tests
    {
        private double _tolearance = 10e-1;

        [Fact]
        public void TestPointedShapeEvaluation()
        {
            var shape = ConvexPolygon.Create(
                new Point(0, 0),
                new Point(0, 4),
                new Point(4, 4),
                new Point(6, 0),
                new Point(4, -4),
                new Point(0, -4)
            );

            Assert.True(Math.Abs(shape.Area - 40) < _tolearance);
        }

        [Fact]
        public void TestBodrerLinesEvalueation()
        {
            var thickLine = new ThickLine(2, 1, 0, 0);

            var (firstLine, secondLine) = thickLine.GetBorderLines();
            
            Assert.Equal(1, firstLine.A);
            Assert.Equal(0, firstLine.B);
            Assert.Equal(-1, firstLine.C);

            Assert.Equal(1, secondLine.A);
            Assert.Equal(0, secondLine.B);
            Assert.Equal(1, secondLine.C);
        }

        [Fact]
        public void TestFindingLinesIntersection()
        {
            var line1 = new Line(1, 1, 1);
            var line2 = new Line(1, -1, 1);

            var intersectionPoint = LineHelper.FindLinesIntersection(line1, line2);
            
            Assert.Equal(-1, intersectionPoint.X);
            Assert.Equal(0, intersectionPoint.Y);
        }

        [Fact]
        public void TestSegmentCreation()
        {
            var segment = new Segment(new Point(1, 0), new Point(0, 1));

            var underlyingLine = segment.UnderlyingLine;

            Assert.Equal(1, underlyingLine.A);
            Assert.Equal(1, underlyingLine.B);
            Assert.Equal(-1, underlyingLine.C);
        }

        [Fact]
        public void TestFindingShapesIntersection()
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

            var intersectionShape = ShapeHelper.FindShapesIntersection(shape1, shape2);

            Assert.Equal(1, intersectionShape.Area);
        }

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

            var area = solver.CalculateInfrastructureArea(new[] {shape1, shape2});

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
