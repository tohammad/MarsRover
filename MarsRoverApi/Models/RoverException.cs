using System;

namespace MarsRoverApi.Models
{
    public class RoverException : Exception
    {
        public RoverException()
        {
        }

        public RoverException(string message)
            : base(message)
        {
        }
    }
}