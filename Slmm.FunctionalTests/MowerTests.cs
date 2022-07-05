using SmartLawnMowing.Domain.Model;
using System;
using Xunit;
using FluentAssertions;
using SmartLawnMowing.Domain.Exceptions;

namespace Slmm.FunctionalTests
{
    public class MowerTests
    {
        public class MowerTestContext
        {
            public Garden Garden { get; internal set; }
            public Mower Mower { get; internal set; }

            public MowerTestContext(int gardenLength, int gardenWidth, int currXPos, int currYPos, Direction orientation)
            {
                Garden = new Garden(gardenLength, gardenWidth);
                Mower = CreateMower(Garden, currXPos, currYPos, orientation);
            }
        }

        [Theory]
        [InlineData(5,5,0,0, Direction.South)]
        [InlineData(2, 2, 1, 1, Direction.South)]
        public void VerifyInitialPosition(int gardenLength, int gardenWidth, int currXPos, int currYPos, Direction orientation)
        {
            var context = new MowerTestContext(gardenLength, gardenWidth, currXPos, currYPos, orientation);
            Position currentPosition = context.Mower.GetPosition();
           this.VerifyPosition(currentPosition, currXPos, currYPos, orientation);
        }

        [Theory]
        [InlineData(2, 2, 1, 1, Direction.South, 1, 0)]
        [InlineData(2, 2, 1, 1, Direction.East, 2, 1)]
        [InlineData(5, 5, 2, 2, Direction.North, 2, 3)]
        [InlineData(2, 2, 2, 2, Direction.West, 1, 2)]
        [InlineData(5, 5, 2, 2, Direction.South, 2, 1)]
        public void MoveMowerInsideGarden(int gardenLength, int gardenWidth, int currXPos, int currYPos, Direction orientation, int expectedX, int expectedY)
        {
            var context = new MowerTestContext(gardenLength, gardenWidth, currXPos, currYPos, orientation);
            context.Mower.Move();
           this.VerifyPosition(context.Mower.GetPosition(), expectedX, expectedY, orientation);
        }

        [Theory]
        [InlineData(2, 2, 0, 0, Direction.South)]
        [InlineData(2, 2, 0, 0, Direction.West)]
        [InlineData(2, 2, 2, 2, Direction.North)]
        [InlineData(2, 2, 2, 2, Direction.East)]
        public void MoveMowerOutsideGarden(int gardenLength, int gardenWidth, int currXPos, int currYPos, Direction orientation)
        {
            var context = new MowerTestContext(gardenLength, gardenWidth, currXPos, currYPos, orientation);
            Action moveAction = null;
            moveAction = new Action(() => context.Mower.Move());
            moveAction.Should().Throw<OutOfRangeException>();
        }

        [Theory]
        [InlineData(Direction.North, Rotate.Clockwise, Direction.East)]
        [InlineData(Direction.West, Rotate.Clockwise, Direction.North)]
        [InlineData(Direction.South, Rotate.Clockwise, Direction.West)]
        [InlineData(Direction.East, Rotate.Clockwise, Direction.South)]
        [InlineData(Direction.North, Rotate.AntiClockwise, Direction.West)]
        [InlineData(Direction.West, Rotate.AntiClockwise, Direction.South)]
        [InlineData(Direction.South, Rotate.AntiClockwise, Direction.East)]
        [InlineData(Direction.East, Rotate.AntiClockwise, Direction.North)]
        public void TurnMower (Direction orientation, Rotate turnDirection, Direction expectedOrientation)
        {
            var context = new MowerTestContext(5, 5, 0, 0, orientation);
            context.Mower.Turn(turnDirection);
            context.Mower.GetPosition().Orientation.Should().Be(expectedOrientation);
        }


        private void VerifyPosition(Position currentPosition, int xPos, int yPos, Direction orientation)
        {
            currentPosition.Should().NotBeNull();
            currentPosition.Coordinates.X.Should().Be(xPos);
            currentPosition.Coordinates.Y.Should().Be(yPos);
            currentPosition.Orientation.Should().Be(orientation);
        }

        private static Mower CreateMower(Garden garden, int currXPos, int currYPos, Direction orientation)
        {
            var newMower = new Mower(garden, new Position(new Coordinates(currXPos, currYPos), orientation));
            return newMower;
        }
    }
}
