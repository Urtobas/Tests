namespace Tests.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public DateTime DatePassing { get; set; }
        public int RightAnswersCount { get; set; } = 0;
        public int WrongAnswersCount { get; set; } = 0;

        public Test Test { get; set; }// navigation property
        public int TestId { get; set; }// foreign key

        //public TestingUser TestingUser { get; set; } // navigation property
        //public string TestingUserId { get; set; } // foreign key
    }
}
