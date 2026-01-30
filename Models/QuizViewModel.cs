namespace QuizApp.Models
{
    public class QuizViewModel
    {
        public List<Question> Questions { get; set; }
        public Dictionary<int, int> Answers { get; set; } = new();
        public int Score { get; set; }
    }
}
