using System.Collections.Generic;
using System.IO;
using System.Linq;
using AreaCalculator.Calculation.Lines;
using AreaCalculator.Calculation.Shapes;
using AreaCalculator.Calculation.Solvers;

namespace AreaCalculator.CLI
{
    /// <summary>
    ///     Abstract inputs processor. Allows to test whole app outside of console
    /// </summary>
    public class InputTextProcessor
    {
        private readonly TextReader _input;
        private readonly TextWriter _output;

        public InputTextProcessor(TextReader input, TextWriter output)
        {
            _input = input;
            _output = output;
        }

        public void ProcessInput()
        {
            _output.WriteLine("Specify count of boundary area vertices");

            var k = int.Parse(_input.ReadLine());

            _output.WriteLine("Specify coordinates of boundary area vertices in clockwise order");

            var points = ParsePointsCoordinates(_input.ReadLine());

            _output.WriteLine("Specify count of linear objects");

            var n = int.Parse(_input.ReadLine());

            _output.WriteLine("Specify linear objects parameters");

            var linearObjects = ParseLinearObjects(n);

            var area = CalculateArea(points, linearObjects);

            _output.WriteLine("Area under linear objects");
            _output.WriteLine(area);

            _output.Flush();
        }

        private double CalculateArea(Point[] boundaryAreaVertices, LinearObject[] linearObjects)
        {
            var boundaryArea = ConvexPolygon.Create(boundaryAreaVertices);

            var infrastructureObjects = linearObjects.Select(o => PolygonHelper.BuildThickLineRectangle(boundaryArea, o)).ToArray();

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
                var parametersString = _input.ReadLine();
                var parameters = parametersString.Split(" ").Select(double.Parse).ToArray();
                result.Add(new LinearObject(parameters[0], parameters[1], parameters[2], parameters[3]));
            }

            return result.ToArray();
        }
    }
}
