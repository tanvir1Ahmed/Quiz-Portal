using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Portal.Data;
using Quiz_Portal.Models.DTOs;

namespace Quiz_Portal.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public LeaderboardController(QuizDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get leaderboard - top scores
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardEntryDto>>> GetLeaderboard([FromQuery] int top = 50)
        {
            // Fetch all results with users, then process on client side
            var allResults = await _context.QuizResults
                .Include(r => r.User)
                .ToListAsync();

            // Get best score for each user (client-side processing)
            var leaderboard = allResults
                .GroupBy(r => r.UserId)
                .Select(g => g.OrderByDescending(r => r.Score)
                              .ThenBy(r => r.TimeTakenSeconds)
                              .First())
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.TimeTakenSeconds)
                .Take(top)
                .ToList();

            var result = leaderboard.Select((r, index) => new LeaderboardEntryDto
            {
                Rank = index + 1,
                UserName = r.User?.Name ?? "Unknown",
                PhoneNumber = MaskPhoneNumber(r.User?.PhoneNumber ?? ""),
                Score = r.Score,
                TotalQuestions = r.TotalQuestions,
                Percentage = Math.Round((double)r.Score / r.TotalQuestions * 100, 2),
                CompletedAt = r.CompletedAt,
                TimeTakenSeconds = r.TimeTakenSeconds
            }).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Get user's rank
        /// </summary>
        [HttpGet("rank/{userId}")]
        public async Task<ActionResult<object>> GetUserRank(int userId)
        {
            var userBestResult = await _context.QuizResults
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.TimeTakenSeconds)
                .FirstOrDefaultAsync();

            if (userBestResult == null)
            {
                return NotFound(new { success = false, message = "No quiz results found for this user" });
            }

            // Fetch all results and calculate rank on client side
            var allResults = await _context.QuizResults.ToListAsync();
            
            var bestScores = allResults
                .GroupBy(r => r.UserId)
                .Select(g => g.OrderByDescending(r => r.Score)
                              .ThenBy(r => r.TimeTakenSeconds)
                              .First())
                .ToList();

            var rank = bestScores.Count(r => r.Score > userBestResult.Score ||
                (r.Score == userBestResult.Score && r.TimeTakenSeconds < userBestResult.TimeTakenSeconds)) + 1;

            var totalParticipants = await _context.QuizResults
                .Select(r => r.UserId)
                .Distinct()
                .CountAsync();

            return Ok(new
            {
                success = true,
                rank,
                totalParticipants,
                score = userBestResult.Score,
                totalQuestions = userBestResult.TotalQuestions,
                percentage = Math.Round((double)userBestResult.Score / userBestResult.TotalQuestions * 100, 2)
            });
        }

        private static string MaskPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length < 10)
                return phoneNumber;

            // Format: +8801XXXXXXX67 - show +880, first digit, mask middle, show last 2
            // Phone stored as +880XXXXXXXXXX (14 chars total)
            if (phoneNumber.StartsWith("+880") && phoneNumber.Length == 14)
            {
                return phoneNumber[..5] + "XXXXXXX" + phoneNumber[^2..];
            }
            
            return phoneNumber[..4] + "XXXXXX" + phoneNumber[^2..];
        }
    }
}
