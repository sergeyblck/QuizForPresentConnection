using PresentConnectionInt.Data;
using PresentConnectionInt.Models;
using Microsoft.EntityFrameworkCore;

namespace PresentConnectionInt.Services
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetQuizzesAsync();

        Task SaveHighScoreAsync(HighScore highScore);

        Task<int> SubmitQuizAsync(QuizSubmission submission);

        Task<List<HighScore>> GetTopHighScoresAsync();
    }

    public class QuizService : IQuizService
    {
        private readonly AppDbContext _context;

        public QuizService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all quiz records from the database asynchronously.
        /// </summary>
        public async Task<List<Quiz>> GetQuizzesAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        /// <summary>
        /// Saves a user's score to the high score database asynchronously.
        /// </summary>
        public async Task SaveHighScoreAsync(HighScore highScore)
        {
            _context.HighScores.Add(highScore);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves the top 10 high scores, ordered by score in descending order.
        /// </summary>
        public async Task<List<HighScore>> GetTopHighScoresAsync()
        {
            return await _context.HighScores
                .OrderByDescending(h => h.Score)
                .Take(10)
                .ToListAsync();
        }

        /// <summary>
        /// Evaluates the quiz based on the user's answers and returns the calculated score.
        /// </summary>
        public async Task<int> EvaluateQuizAsync(QuizSubmission submission)
        {
            var quizzes = await _context.Quizzes.ToListAsync();
            int score = 0;

            foreach (var quiz in quizzes)
            {
                if (submission.Answers.TryGetValue(quiz.Id, out var userAnswers))
                {
                    if (quiz.Type == "Checkbox")
                    {
                        var correctAnswersSet = new HashSet<string>(quiz.CorrectAnswers);
                        var userAnswersSet = new HashSet<string>(userAnswers);

                        int correctCheckedCount = userAnswersSet.Intersect(correctAnswersSet).Count();
                        int totalCorrectAnswers = correctAnswersSet.Count;

                        if (totalCorrectAnswers > 0)
                        {
                            int points = (int)Math.Ceiling((100.0 / totalCorrectAnswers) * correctCheckedCount);
                            score += points;
                        }
                    }
                    else if (quiz.Type == "Radio")
                    {
                        if (quiz.CorrectAnswers.Contains(userAnswers.FirstOrDefault()))
                        {
                            score += 100;
                        }
                    }
                    else if (quiz.Type == "Textbox")
                    {
                        var correctAnswer = quiz.CorrectAnswers.FirstOrDefault();
                        if (!string.IsNullOrEmpty(correctAnswer) && string.Equals(correctAnswer, userAnswers.FirstOrDefault(), StringComparison.OrdinalIgnoreCase))
                        {
                            score += 100;
                        }
                    }
                }
            }

            return score;
        }

        /// <summary>
        /// Calculates the score, saves the score to the database, and returns it
        /// </summary>
        public async Task<int> SubmitQuizAsync(QuizSubmission submission)
        {
            var score = await EvaluateQuizAsync(submission);

            var highScore = new HighScore
            {
                Email = submission.Email,
                Score = score,
                DateTime = DateTime.Now
            };

            await SaveHighScoreAsync(highScore);

            return score;
        }
    }
}
