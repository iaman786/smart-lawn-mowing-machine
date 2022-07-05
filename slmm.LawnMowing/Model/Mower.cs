namespace SmartLawnMowing.Domain.Model
{
    using System.Threading;
    using Exceptions;

    public class Mower
    {
        private readonly Garden garden;
        private Position position;
        private readonly CoordinatesResolver coordinatesResolver = new CoordinatesResolver();
        private readonly DirectionResolver orientationResolver = new DirectionResolver();

        private const int TimeToTurn = 2000;
        private const int TimeToMove = 5000;

        public Mower(Garden garden, Position startingPosition)
        {
            this.garden = garden;
            this.position = startingPosition;
        }

        public Position GetPosition()
        {
            return this.position;
        }

        public void Move()
        {
            var newCoordinates = coordinatesResolver.GetNextCoordinates(this.position);
            if (!this.garden.CellIsInsideGarden(newCoordinates))
            {
                throw new OutOfRangeException();
            }

            Thread.Sleep(TimeToMove);
            this.position = new Position(newCoordinates, this.position.Orientation);
        }

        public void Turn(Rotate turnDirection)
        {
            var newOrientation = orientationResolver.GetNextOrientation(this.position.Orientation, turnDirection);

            Thread.Sleep(TimeToTurn);

            this.position = new Position(this.position.Coordinates, newOrientation);
        }
    }
}
