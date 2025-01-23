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

        public async Task<List<Quiz>> GetQuizzesAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task SaveHighScoreAsync(HighScore highScore)
        {
            _context.HighScores.Add(highScore);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HighScore>> GetTopHighScoresAsync()
        {
            return await _context.HighScores
                .OrderByDescending(h => h.Score)
                .Take(10)
                .ToListAsync();
        }
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
                        // Calculate score for Checkbox type
                        var correctAnswersSet = new HashSet<string>(quiz.CorrectAnswers);
                        var userAnswersSet = new HashSet<string>(userAnswers);

                        // Get the number of correctly checked answers
                        int correctCheckedCount = userAnswersSet.Intersect(correctAnswersSet).Count();
                        int totalCorrectAnswers = correctAnswersSet.Count;

                        // Calculate score for checkbox question
                        if (totalCorrectAnswers > 0)
                        {
                            int points = (int)Math.Ceiling((100.0 / totalCorrectAnswers) * correctCheckedCount);
                            score += points;
                        }
                    }
                    else if (quiz.Type == "Radio")
                    {
                        // Calculate score for Radio buttons type (correct answer)
                        if (quiz.CorrectAnswers.Contains(userAnswers.FirstOrDefault()))
                        {
                            score += 100;
                        }
                    }
                    else if (quiz.Type == "Textbox")
                    {
                        // Calculate score for Textbox type (case-insensitive match)
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
