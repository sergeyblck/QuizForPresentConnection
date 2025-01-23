using Microsoft.AspNetCore.Mvc;
using PresentConnectionInt.Models;
using PresentConnectionInt.Services;

namespace PresentConnectionInt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("quizzes")]
        public async Task<IActionResult> GetQuizzes()
        {
            var quizzes = await _quizService.GetQuizzesAsync();
            return Ok(quizzes);
        }

        [HttpPost("highscores")]
        public async Task<IActionResult> SaveHighScore([FromBody] HighScore highScore)
        {
            await _quizService.SaveHighScoreAsync(highScore);
            return Ok();
        }

        [HttpGet("highscores")]
        public async Task<IActionResult> GetHighScores()
        {
            var highScores = await _quizService.GetTopHighScoresAsync();
            return Ok(highScores);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] QuizSubmission submission)
        {
            var score = await _quizService.SubmitQuizAsync(submission);
            return Ok(new { score });
        }

    }

}
