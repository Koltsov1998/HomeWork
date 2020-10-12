using System;
using System.IO;
using AreaCalculator.Calculation.Shapes;
using AreaCalculator.Calculation.Solvers;
using AreaCalculator.CLI;
using Xunit;

namespace AreaCalculator.Calculation.Tests
{
    public class SolutionTests
    {
        private const double _tol = 10e-1;

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


            var solver = new Solver();

            var area = solver.CalculateInfrastructureArea(new[] { shape1, shape2, shape3 });
            
            Assert.Equal(8, area);
        }

        [Fact]
        public void TestComplexAreaEvaluation2()
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
        public void TestComplexAreaEvaluation3()
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

        [Fact]
        public void TestStreamInput()
        {
            var inputStream = new MemoryStream();
            var outputStream = new MemoryStream();

            StreamWriter inputWriter = new StreamWriter(inputStream);
            StreamReader outputReader = new StreamReader(outputStream);

            var streamReader = new StreamReader(inputStream);
            var streamWriter = new StreamWriter(outputStream);

            var inputProcessor = new InputTextProcessor(streamReader, streamWriter);

            inputWriter.WriteLine("2");
            inputWriter.WriteLine("0 0 0 10 10 10 10 0");
            inputWriter.WriteLine("2");
            inputWriter.WriteLine("1 0 1 -5");
            inputWriter.WriteLine("1 1 0 -5");

            inputWriter.Flush();

            inputStream.Position = 0;

            inputProcessor.ProcessInput();

            outputStream.Position = 0;

            outputReader.ReadLine();
            outputReader.ReadLine();
            outputReader.ReadLine();
            outputReader.ReadLine();
            outputReader.ReadLine();

            var area = double.Parse(outputReader.ReadLine());
            Assert.True(Math.Abs(19 - area) < _tol);
        }

        [Fact]
        public void TestStreamInput2()
        {
            var inputStream = new MemoryStream();
            var outputStream = new MemoryStream();

            StreamWriter inputWriter = new StreamWriter(inputStream);
            StreamReader outputReader = new StreamReader(outputStream);

            var streamReader = new StreamReader(inputStream);
            var streamWriter = new StreamWriter(outputStream);

            var inputProcessor = new InputTextProcessor(streamReader, streamWriter);

            inputWriter.WriteLine("2");
            inputWriter.WriteLine("0 0 0 10 10 10 10 0");
            inputWriter.WriteLine("3");
            inputWriter.WriteLine("1 0 1 -5");
            inputWriter.WriteLine("1 1 0 -5");
            inputWriter.WriteLine("1 1 1 -10");

            inputWriter.Flush();

            inputStream.Position = 0;

            inputProcessor.ProcessInput();

            outputStream.Position = 0;

            outputReader.ReadLine();
            outputReader.ReadLine();
            outputReader.ReadLine();
            outputReader.ReadLine();
            outputReader.ReadLine();

            var area = double.Parse(outputReader.ReadLine());
            Assert.True(Math.Abs(31.2 - area) < _tol);
        }
    }
}
