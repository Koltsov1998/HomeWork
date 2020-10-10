using System;
using AreaCalculator.Calculation.Shapes;

namespace AreaCalculator.Calculation.Lines
{
    public class Segment
    {
        private readonly Point _firstPoint;
        private readonly Point _secondPoint;

        private readonly double _maxX;
        private readonly double _minX;
        private readonly double _maxY;
        private readonly double _minY;

        public Segment(Point firstPoint, Point secondPoint)
        {
            _firstPoint = firstPoint;
            _secondPoint = secondPoint;

            _maxX = Math.Max(firstPoint.X, secondPoint.X);
            _minX = Math.Min(firstPoint.X, secondPoint.X);
            _maxY = Math.Max(firstPoint.Y, secondPoint.Y);
            _minY = Math.Min(firstPoint.Y, secondPoint.Y);

            UnderlyingLine = GetUnderlyingLine(firstPoint, secondPoint);
        }

        #region Public properties

        public readonly Line UnderlyingLine;

        #endregion

        #region Public methods

        public bool LiesOnPoint(Point point)
        {
            var result = UnderlyingLine.LiesOnPoint(point)
                   && point.X <= _maxX && point.X >= _minX
                   && point.Y <= _maxY && point.Y >= _minY;

            return result;
        }

        public override string ToString()
        {
            return $"({_firstPoint.X}, {_firstPoint.Y})---({_secondPoint.X}, {_secondPoint.Y})";
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Get line that goes through points <see cref="firstPoint"/> and <see cref="secondPoint"/>
        /// </summary>
        private Line GetUnderlyingLine(Point firstPoint, Point secondPoint)
        {
            var (x1, x2) = (firstPoint.X, secondPoint.X);
            var (y1, y2) = (firstPoint.Y, secondPoint.Y);

            var dx = x2 - x1;
            var dy = y2 - y1;

            var a = dy;
            var b = -dx;
            var c = (dx * y1 - dy * x1);

            return new Line(a, b, c);
        }

        #endregion

    }
}
