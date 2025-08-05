using System.ComponentModel.DataAnnotations;
using UdamyCourse.Model.Domain;

namespace UdamyCourse.Model.DTOs
{
    public class AddWalkDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name has to be a minimum 5 characters")]
        [MaxLength(100, ErrorMessage = "Name has to be of maximum 100 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(7, ErrorMessage = "Message should have atleast 7 characters")]
        [MaxLength(30, ErrorMessage ="Message should have maximum 30 characters")]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        [Required]

        public string? WalkImageUrl { get; set; }
        [Required]

        public int DifficultyId { get; set; }
        [Required]

        public int RegionId { get; set; }

    }
}
