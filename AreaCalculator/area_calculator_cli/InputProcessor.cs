using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AreaCalculator.Calculation.Lines;
using AreaCalculator.Calculation.Shapes;
using AreaCalculator.Calculation.Solvers;

namespace AreaCalculator.CLI
{
    public class InputProcessor
    {
        private readonly TextReader _reader;
        private readonly TextWriter _writer;

        public InputProcessor(TextReader reader, TextWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void ProcessInpit()
        {
            _writer.WriteLine("Specify count of boundary area vertices");

            var k = int.Parse(_reader.ReadLine());

            _writer.WriteLine("Specify coordinates of boundary area vertices in clockwise order");

            var points = ParsePointsCoordinates(_reader.ReadLine());

            _writer.WriteLine("Specify count of linear objects");

            var n = int.Parse(_reader.ReadLine());

            var linearObjects = ParseLinearObjects(n);

            var area = ProcessInput(points, linearObjects);

            _writer.WriteLine("Area under linear objects");
            _writer.WriteLine(area);
        }

        private double ProcessInput(Point[] boundaryAreaVertices, LinearObject[] linearObjects)
        {
            var boundaryArea = ConvexPolygon.Create(boundaryAreaVertices);

            var infrastructureObjects = linearObjects.Select(o => ShapeHelper.BuildThickLineRectangle(boundaryArea, o)).ToArray();

            Solver solver = new Solver();

            var area = solver.CalculateInfrastructureArea(infrastructureObjects);

            return area;
        }

        private Point[] ParsePointsCoordinates(string coordinatesString)
        {
            var result = new List<Point>();

            var coordinates = coordinatesString.Split(" ").Select(double.Parse).ToArray();

            for (int i = 0; i < coordinates.Length - 1; i += 2)
            {
                result.Add(new Point(coordinates[i], coordinates[i + 1]));
            }

            return result.ToArray();
        }

        private LinearObject[] ParseLinearObjects(int count)
        {
            var result = new List<LinearObject>();

            for (int i = 0; i < count; i++)
            {
                var parametersString = _reader.ReadLine();
                var parameters = parametersString.Split(" ").Select(double.Parse).ToArray();
                result.Add(new LinearObject(parameters[0], parameters[1], parameters[2], parameters[3]));
            }

            return result.ToArray();
        }
    }
}
