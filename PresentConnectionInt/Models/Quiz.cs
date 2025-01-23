namespace PresentConnectionInt.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; } // "Radio", "Checkbox", or "Textbox"
        public string[] Options { get; set; } // For radio/checkbox questions
        public string[] CorrectAnswers { get; set; } // Correct answers
    }

    public class HighScore
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
    }
}
