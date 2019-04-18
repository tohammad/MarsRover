namespace MarsRoverApi.Interface
{
    public interface IRover : IMovement
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}