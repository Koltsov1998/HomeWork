namespace AreaCalculator.Calculation.Shapes
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        #region Public properties

        public double X { get; }

        public double Y { get; }

        #endregion

        #region Public methods

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return X.GetHashCode() - Y.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = (Point)obj;

            return this.X == other.X && this.Y == other.Y;
        }

        #endregion
    }
}
