using System.ComponentModel.DataAnnotations;

namespace Quiz_Portal.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\+880\d{10}$", ErrorMessage = "Phone number must be in format +880 followed by 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    }
}
