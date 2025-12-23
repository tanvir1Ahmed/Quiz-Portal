using System.ComponentModel.DataAnnotations;

namespace Quiz_Portal.Models.DTOs
{
    public class SubmitQuizDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();

        public int TimeTakenSeconds { get; set; }
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; } = string.Empty; // "A", "B", or "C"
    }
}
