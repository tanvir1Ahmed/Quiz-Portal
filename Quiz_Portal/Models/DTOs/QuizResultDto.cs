namespace Quiz_Portal.Models.DTOs
{
    public class QuizResultDto
    {
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}
