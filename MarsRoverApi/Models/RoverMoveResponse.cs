namespace MarsRoverApi.Models
{
    public class RoverMoveResponse
    {
        public int RoverId { get; set; }
        public string RoverName { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public string CurrentDirection { get; set; }
    }
}