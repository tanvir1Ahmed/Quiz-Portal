using System.ComponentModel.DataAnnotations;

namespace Quiz_Portal.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public string OptionA { get; set; } = string.Empty;

        [Required]
        public string OptionB { get; set; } = string.Empty;

        [Required]
        public string OptionC { get; set; } = string.Empty;

        [Required]
        public string CorrectAnswer { get; set; } = string.Empty; // "A", "B", or "C"

        public int QuestionNumber { get; set; }
    }
}
