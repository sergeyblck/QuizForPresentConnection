namespace PresentConnectionInt.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public string[] Options { get; set; }
        public string[] CorrectAnswers { get; set; }
    }

}
