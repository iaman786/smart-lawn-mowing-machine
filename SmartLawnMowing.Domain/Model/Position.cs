namespace SmartLawnMowing.Domain.Model
{
    using System;

    public class Position
    {
        public Position(Coordinates coordinates, Direction orientation)
        {
            this.Coordinates = coordinates;
            this.Orientation = orientation;
        }

        public Coordinates Coordinates { get; }
        public Direction Orientation { get; }
    }
}
