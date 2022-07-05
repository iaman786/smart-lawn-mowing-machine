namespace SmartLawnMowing.Domain.Model
{
    using System;
    using System.Collections.Generic;

    public class CoordinatesResolver
    {
        private IDictionary<Direction, Func<Coordinates, Coordinates>> getCoordinatesCommands = new Dictionary<Direction, Func<Coordinates, Coordinates>>
        {
            {
                Direction.North,
                (currentCoordinates) => new Coordinates(currentCoordinates.X, currentCoordinates.Y+1)
            },
            {
                Direction.South,
                (currentCoordinates) => new Coordinates(currentCoordinates.X, currentCoordinates.Y-1)
            },
            {
                Direction.East,
                (currentCoordinates) => new Coordinates(currentCoordinates.X+1, currentCoordinates.Y)
            },
            {
                Direction.West,
                (currentCoordinates) => new Coordinates(currentCoordinates.X-1, currentCoordinates.Y)
            }
        };

        public Coordinates GetNextCoordinates(Position currentPosition)
        {
            var command = getCoordinatesCommands[currentPosition.Orientation];

            if (command == null)
            {
                throw new ArgumentOutOfRangeException($"No command exists for the orientation {currentPosition.Orientation} to get the next position");
            }

            return command(currentPosition.Coordinates);
        }
    }
}
