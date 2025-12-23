namespace Quiz_Portal.Models.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
    }
}
