using System.ComponentModel.DataAnnotations;

namespace MarsRoverApi.Models
{
    public class RoverMoveModel
    {
        [Required]
        [StringLength(50)]
        [RegularExpression("^[LRM]*$", ErrorMessage = "Invalid Movement Command. Valid values are 'L', 'R' and 'M'.")]
        public string MovementInstruction { get; set; }
    }
}