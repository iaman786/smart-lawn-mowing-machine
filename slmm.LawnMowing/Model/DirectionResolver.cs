namespace SmartLawnMowing.Domain.Model
{
    using System;
    using System.Collections.Generic;

    public class DirectionResolver
    {
        private IDictionary<Direction, Func<Rotate, Direction>> getNewOrientationCommands = new Dictionary<Direction, Func<Rotate, Direction>>
        {
            {
                Direction.North,
                (rotate) => rotate == Rotate.Clockwise ?  Direction.East : Direction.West
            },
            {
                Direction.South,
                (rotate) => rotate == Rotate.Clockwise ?  Direction.West : Direction.East
            },
            {
                Direction.East,
                (rotate) => rotate == Rotate.Clockwise ?  Direction.South : Direction.North
            },
            {
                Direction.West,
                (rotate) => rotate == Rotate.Clockwise ?  Direction.North : Direction.South
            }
        };

        public Direction GetNextOrientation(Direction currentOrientation, Rotate direction)
        {

            var command = getNewOrientationCommands[currentOrientation];

            if (command == null)
            {
                throw new ArgumentOutOfRangeException($"No command exists for the orientation {currentOrientation} to get a new orientation");
            }

            return command(direction);
        }
    }
}
