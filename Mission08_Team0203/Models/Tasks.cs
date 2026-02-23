using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0203.Models
{
    public class Tasks
    {
        [Required]
        public string Task { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        public string? Category { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}
