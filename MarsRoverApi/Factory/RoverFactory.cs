using MarsRoverApi.Enums;
using MarsRoverApi.Interface;
using MarsRoverApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverApi.Factory
{
    public class RoverFactory
    {
        private static readonly List<IRover> Rovers = new List<IRover>();

        public static IRover Create(int id, string name, int startX, int startY, Direction initialDirection)
        {
            if (IsExist(id))
            {
                throw new RoverException("EXISTS");
            }
            var rover = new Rover
            {
                Position = new Position(startX, startY, initialDirection),
                Id = id,
                Name = name
            };
            Rovers.Add(rover);
            return rover;
        }

        public static IRover Update(int id, string name)
        {
            var rover = Rovers.FirstOrDefault(r => r.Id == id);
            if (rover == null)
                throw new RoverException("Rover doesn't exist.");
            rover.Name = name;
            return rover;
        }

        public static IRover GetRover(int id)
        {
            return Rovers.FirstOrDefault(r => r.Id == id);
        }

        public static bool IsExist(int id)
        {
            return Rovers.Any(r => r.Id == id);
        }
    }
}