namespace SmartLawnMowing.Domain.Presentation
{
    using Views;
    using Model;

    public static class TransformationExtensions
    {
        public static PositionView ToView(this Position position)
        {
            return new PositionView
            {
                X = position.Coordinates.X,
                Y = position.Coordinates.Y,
                Orientation = position.Orientation.ToString()
            };
        }
    }
}
