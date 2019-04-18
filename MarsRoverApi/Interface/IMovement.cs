using MarsRoverApi.Enums;
using MarsRoverApi.Models;
using System.Collections.Generic;

namespace MarsRoverApi.Interface
{
    public interface IMovement
    {
        Position Position { get; set; }

        void Move(IEnumerable<Movement> movements);
    }
}