using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Lines
{
    /// <summary>
    ///     Base class for linear objects
    /// </summary>
    public class Line
    {
        public Line(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        #region Public properties

        public double A { get; }

        public double B { get; }

        public double C { get; }

        #endregion

        #region Public methods

        public bool LiesOnPoint(Point point)
        {
            return A * point.X + B * point.Y + C == 0;
        }

        public static bool operator >(Line line, Point point)
        {
            return line.A * point.X + line.B * point.Y + line.C > 0;
        }

        public static bool operator <(Line line, Point point)
        {
            return line.A * point.X + line.B * point.Y + line.C < 0;
        }

        #endregion
    }
}
