namespace PresentConnectionInt.Models
{
    public class QuizSubmission
    {
        public string Email { get; set; } // User's email
        public Dictionary<int, string[]> Answers { get; set; } // Key is Quiz ID, value is user answers
    }


}
