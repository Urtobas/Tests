namespace Tests.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public DateTime DatePassing { get; set; }
        public int RightAnswersCount { get; set; } = 0;
        public int WrongAnswersCount { get; set; } = 0;
        public int TestId { get; set; }
        public string TestingUserId { get; set; }
    }
}
