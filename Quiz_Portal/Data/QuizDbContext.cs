using Microsoft.EntityFrameworkCore;
using Quiz_Portal.Models;

namespace Quiz_Portal.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint on phone number
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            // Seed the 20 questions
            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    QuestionNumber = 1,
                    QuestionText = "What is the capital city of Bangladesh?",
                    OptionA = "Chittagong",
                    OptionB = "Dhaka",
                    OptionC = "Khulna",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 2,
                    QuestionNumber = 2,
                    QuestionText = "In which year did Bangladesh gain independence?",
                    OptionA = "1947",
                    OptionB = "1971",
                    OptionC = "1952",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 3,
                    QuestionNumber = 3,
                    QuestionText = "What is the national flower of Bangladesh?",
                    OptionA = "Rose",
                    OptionB = "Water Lily (Shapla)",
                    OptionC = "Lotus",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 4,
                    QuestionNumber = 4,
                    QuestionText = "Which river is known as the lifeline of Bangladesh?",
                    OptionA = "Meghna",
                    OptionB = "Jamuna",
                    OptionC = "Padma",
                    CorrectAnswer = "C"
                },
                new Question
                {
                    Id = 5,
                    QuestionNumber = 5,
                    QuestionText = "What is the official language of Bangladesh?",
                    OptionA = "Urdu",
                    OptionB = "Bengali",
                    OptionC = "Hindi",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 6,
                    QuestionNumber = 6,
                    QuestionText = "The Sundarbans, the largest mangrove forest in the world, is home to which famous animal?",
                    OptionA = "Asian Elephant",
                    OptionB = "Royal Bengal Tiger",
                    OptionC = "Indian Rhinoceros",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 7,
                    QuestionNumber = 7,
                    QuestionText = "What does the green color in the Bangladesh flag represent?",
                    OptionA = "Islam",
                    OptionB = "Lush greenery of the land",
                    OptionC = "Peace",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 8,
                    QuestionNumber = 8,
                    QuestionText = "Which poet wrote the national anthem of Bangladesh?",
                    OptionA = "Kazi Nazrul Islam",
                    OptionB = "Rabindranath Tagore",
                    OptionC = "Jasimuddin",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 9,
                    QuestionNumber = 9,
                    QuestionText = "What is the currency of Bangladesh?",
                    OptionA = "Rupee",
                    OptionB = "Taka",
                    OptionC = "Dollar",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 10,
                    QuestionNumber = 10,
                    QuestionText = "Cox's Bazar is famous for having the world's longest what?",
                    OptionA = "River",
                    OptionB = "Unbroken sea beach",
                    OptionC = "Bridge",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 11,
                    QuestionNumber = 11,
                    QuestionText = "Which industry is Bangladesh one of the world's largest exporters in?",
                    OptionA = "Electronics",
                    OptionB = "Ready-made garments",
                    OptionC = "Automobiles",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 12,
                    QuestionNumber = 12,
                    QuestionText = "What is the national bird of Bangladesh?",
                    OptionA = "Peacock",
                    OptionB = "Oriental Magpie-Robin (Doel)",
                    OptionC = "Kingfisher",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 13,
                    QuestionNumber = 13,
                    QuestionText = "International Mother Language Day (February 21) commemorates which event in Bangladesh?",
                    OptionA = "Independence Day",
                    OptionB = "Language Movement of 1952",
                    OptionC = "Victory Day",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 14,
                    QuestionNumber = 14,
                    QuestionText = "Which bay borders Bangladesh to the south?",
                    OptionA = "Bay of Bengal",
                    OptionB = "Arabian Sea",
                    OptionC = "Gulf of Thailand",
                    CorrectAnswer = "A"
                },
                new Question
                {
                    Id = 15,
                    QuestionNumber = 15,
                    QuestionText = "Who is known as the Father of the Nation of Bangladesh?",
                    OptionA = "Ziaur Rahman",
                    OptionB = "Sheikh Mujibur Rahman",
                    OptionC = "A.K. Fazlul Huq",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 16,
                    QuestionNumber = 16,
                    QuestionText = "What is the largest city in Bangladesh by population?",
                    OptionA = "Chittagong",
                    OptionB = "Sylhet",
                    OptionC = "Dhaka",
                    CorrectAnswer = "C"
                },
                new Question
                {
                    Id = 17,
                    QuestionNumber = 17,
                    QuestionText = "Which UNESCO World Heritage Site in Bangladesh features ancient Buddhist ruins?",
                    OptionA = "Lalbagh Fort",
                    OptionB = "Paharpur (Somapura Mahavihara)",
                    OptionC = "Ahsan Manzil",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 18,
                    QuestionNumber = 18,
                    QuestionText = "What is the national sport of Bangladesh?",
                    OptionA = "Cricket",
                    OptionB = "Kabaddi (Hadudu)",
                    OptionC = "Football",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 19,
                    QuestionNumber = 19,
                    QuestionText = "Which neighboring country shares the longest border with Bangladesh?",
                    OptionA = "Myanmar",
                    OptionB = "India",
                    OptionC = "Nepal",
                    CorrectAnswer = "B"
                },
                new Question
                {
                    Id = 20,
                    QuestionNumber = 20,
                    QuestionText = "What is the national fruit of Bangladesh?",
                    OptionA = "Mango",
                    OptionB = "Jackfruit",
                    OptionC = "Banana",
                    CorrectAnswer = "B"
                }
            );
        }
    }
}
