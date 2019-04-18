using MarsRoverApi.Enums;
using MarsRoverApi.Interface;
using System;
using System.Collections.Generic;

namespace MarsRoverApi.Models
{
    public class Rover : IRover
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }

        private Direction GetDirection(int degree)
        {
            degree = degree % 360;
            if (degree < 0)
                degree = 360 - (Math.Abs(degree));
            return (Direction)degree;
        }

        private void MoveForward(Position position)
        {
            switch (position.Direction)
            {
                case Direction.E:
                    position.X += 1;
                    break;

                case Direction.W:
                    position.X -= 1;
                    break;

                case Direction.N:
                    position.Y += 1;
                    break;

                case Direction.S:
                    position.Y -= 1;
                    break;
            }
        }

        private void TurnLeft(Position position)
        {
            position.Direction = GetDirection((int)position.Direction - 90);
        }

        private void TurnRight(Position position)
        {
            position.Direction = GetDirection((int)position.Direction + 90);
        }

        private bool IsValid(Position position)
        {
            return position.X >= 0 && position.Y >= 0;
        }

        public void Move(IEnumerable<Movement> movements)
        {
            var p = Position.Clone();
            foreach (var m in movements)
            {
                switch (m)
                {
                    case Movement.Left:
                        TurnLeft(p);
                        break;

                    case Movement.Right:
                        TurnRight(p);
                        break;

                    case Movement.Move:
                        MoveForward(p);
                        break;
                }
            }
            if (!IsValid(p))
            {
                throw new RoverException("Invalid Move.");
            }
            Position = p;
        }
    }
}