using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class UserStatisticModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<TestResult>? TestResults { get; set; }
        public List<StatisticModel> StatisticModels { get; set; }
        public TestingUser? SelectedUser { get; set; }

        public UserStatisticModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(string id)
        {
            SelectedUser = _context.TestingUsers.FirstOrDefault(op => op.Id == id);
            TestResults = _context.TestResults.Where(op => op.TestingUserId == id);
            StatisticModels = new();
            if (SelectedUser != null && TestResults != null)
            {
                foreach (var e in TestResults)
                {
                    StatisticModel statMod = new StatisticModel()
                    {
                        UserAliasName = SelectedUser.AliasName,
                        UserEmail = SelectedUser.Email,
                        RightAnswersCount = e.RightAnswersCount,
                        WrongAnswersCount = e.WrongAnswersCount,
                        TotalAnswersCount = e.RightAnswersCount + e.WrongAnswersCount,
                        DatePassing = e.DatePassing
                    };
                    statMod.RelativeResult = 1;
                    StatisticModels.Add(statMod);
                }
                return Page();
            }
            else return RedirectToPage("/Error");
        }
    }

    // класс модели для отображения данных на странице статистики
    public class StatisticModel
    {
        public string UserAliasName { get; set; } = "";
        public string UserEmail { get; set; }
        public int RightAnswersCount { get; set; }
        public int WrongAnswersCount { get; set; }
        public DateTime DatePassing { get; set; }

        private double relativeResult;
        public double RelativeResult
        {
            get { return relativeResult; }
            set {
                if (WrongAnswersCount == 0) relativeResult = 100;
                else if (RightAnswersCount == 0) relativeResult = 0;
                else relativeResult = Math.Round((double)RightAnswersCount / TotalAnswersCount, 2) * 100;
            }
        }
        public int TotalAnswersCount { get; set; }

    }
}
