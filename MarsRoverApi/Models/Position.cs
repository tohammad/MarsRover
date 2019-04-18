using MarsRoverApi.Enums;

namespace MarsRoverApi.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public Position Clone()
        {
            return new Position(X, Y, Direction);
        }
    }
}