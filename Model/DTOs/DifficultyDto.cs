using System.ComponentModel.DataAnnotations;

namespace UdamyCourse.Model.DTOs
{
    public class DifficultyDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
