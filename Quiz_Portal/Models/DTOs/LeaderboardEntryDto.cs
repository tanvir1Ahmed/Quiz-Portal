namespace Quiz_Portal.Models.DTOs
{
    public class LeaderboardEntryDto
    {
        public int Rank { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
        public DateTime CompletedAt { get; set; }
        public int TimeTakenSeconds { get; set; }
    }
}
