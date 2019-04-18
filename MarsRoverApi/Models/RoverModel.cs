using System.ComponentModel.DataAnnotations;

namespace MarsRoverApi.Models
{
    public class RoverModel
    {
        // [Range(1, Int32.MaxValue, ErrorMessage = "Id should be a positive integer value.")]
        public int RoverId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-z0-9]*$", ErrorMessage = "Invalid Name. Only characters are allowed.")]
        public string RoverName { get; set; }
    }
}