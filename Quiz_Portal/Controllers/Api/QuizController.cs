using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Portal.Data;
using Quiz_Portal.Models;
using Quiz_Portal.Models.DTOs;

namespace Quiz_Portal.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public QuizController(QuizDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all questions (without correct answers)
        /// </summary>
        [HttpGet("questions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questions = await _context.Questions
                .OrderBy(q => q.QuestionNumber)
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    QuestionNumber = q.QuestionNumber,
                    QuestionText = q.QuestionText,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    CorrectAnswer = q.CorrectAnswer
                })
                .ToListAsync();

            return Ok(questions);
        }

        /// <summary>
        /// Submit quiz answers and get score
        /// </summary>
        [HttpPost("submit")]
        public async Task<ActionResult<QuizResultDto>> SubmitQuiz([FromBody] SubmitQuizDto submitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify user exists
            var user = await _context.Users.FindAsync(submitDto.UserId);
            if (user == null)
            {
                return BadRequest(new { success = false, message = "User not found" });
            }

            // Get all questions with correct answers
            var questions = await _context.Questions.ToListAsync();
            var questionDict = questions.ToDictionary(q => q.Id, q => q.CorrectAnswer);

            // Calculate score
            int score = 0;
            foreach (var answer in submitDto.Answers)
            {
                if (questionDict.TryGetValue(answer.QuestionId, out var correctAnswer))
                {
                    if (answer.SelectedAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                    {
                        score++;
                    }
                }
            }

            // Save quiz result
            var quizResult = new QuizResult
            {
                UserId = submitDto.UserId,
                Score = score,
                TotalQuestions = questions.Count,
                TimeTakenSeconds = submitDto.TimeTakenSeconds,
                CompletedAt = DateTime.UtcNow
            };

            _context.QuizResults.Add(quizResult);
            await _context.SaveChangesAsync();

            // Calculate rank
            var rank = await _context.QuizResults
                .CountAsync(r => r.Score > score) + 1;

            double percentage = (double)score / questions.Count * 100;

            string message;
            if (percentage >= 90)
                message = "Excellent! Outstanding performance!";
            else if (percentage >= 70)
                message = "Great job! Well done!";
            else if (percentage >= 50)
                message = "Good effort! Keep learning!";
            else
                message = "Keep practicing! You can do better!";

            return Ok(new QuizResultDto
            {
                Score = score,
                TotalQuestions = questions.Count,
                Percentage = Math.Round(percentage, 2),
                Message = message,
                Rank = rank
            });
        }

        /// <summary>
        /// Check if user has already taken the quiz
        /// </summary>
        [HttpGet("check-attempt/{userId}")]
        public async Task<ActionResult<object>> CheckAttempt(int userId)
        {
            var hasAttempted = await _context.QuizResults
                .AnyAsync(r => r.UserId == userId);

            var lastResult = await _context.QuizResults
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CompletedAt)
                .FirstOrDefaultAsync();

            return Ok(new
            {
                hasAttempted,
                lastScore = lastResult?.Score,
                totalQuestions = lastResult?.TotalQuestions,
                completedAt = lastResult?.CompletedAt
            });
        }
    }
}
