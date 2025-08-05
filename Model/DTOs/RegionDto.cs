using System.ComponentModel.DataAnnotations;

namespace UdamyCourse.Model.DTOs
{
    public class RegionDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum 3 characters")]
        [MaxLength(3, ErrorMessage = "code has to be of maximum 3 characters")]
        public string Code { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Name has to be a minimum 5 characters")]
        [MaxLength(100, ErrorMessage = "Name has to be of maximum 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
