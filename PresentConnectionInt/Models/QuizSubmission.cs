namespace PresentConnectionInt.Models
{
    public class QuizSubmission
    {
        public string Email { get; set; }
        public Dictionary<int, string[]> Answers { get; set; }
    }


}
