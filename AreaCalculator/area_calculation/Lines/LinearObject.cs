using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Lines
{
    public class LinearObject : Line
    {
        public LinearObject(double w, double a, double b, double c) : base(a, b, c)
        {
            W = w;
        }

        #region Public properties

        public double W { get; }

        #endregion

        #region Public methods

        public (Line, Line) GetBorderLines()
        {
            Vector normalVector = new Vector(A, B);

            var gap = - W / 2 * normalVector.Lenght;

            var firstLine = new Line(A, B, C + gap);

            var secondLine = new Line(A, B, C - gap);

            return (firstLine, secondLine);
        }

        #endregion
    }
}
