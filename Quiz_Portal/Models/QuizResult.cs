using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz_Portal.Models
{
    public class QuizResult
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int Score { get; set; }

        public int TotalQuestions { get; set; } = 20;

        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        public int TimeTakenSeconds { get; set; }
    }
}
