using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
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
        public int TestsCount { get; set; }
        public int TotalAnswersCount { get; set; }
        public int TotalRightAnswersCount { get; set; }
        public int TotalWrongAnswersCount { get; set; }
        public double AverageResult { get; set; }

        public UserStatisticModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(string id)
        {
            SelectedUser = _context.TestingUsers.FirstOrDefault(op => op.Id == id);
            TestResults = _context.TestResults.Where(op => op.TestingUserId == id);
            HttpContext.Session.SetString("SelectedUserId", SelectedUser.Id);
            StatisticModels = new();
            if (SelectedUser != null && TestResults != null)
            {
                foreach (var e in TestResults)
                {
                    StatisticModel statMod = new StatisticModel()
                    {
                        Id = e.Id,
                        UserAliasName = SelectedUser.AliasName,
                        UserEmail = SelectedUser.Email,
                        RightAnswersCount = e.RightAnswersCount,
                        WrongAnswersCount = e.WrongAnswersCount,
                        TotalAnswersCount = e.RightAnswersCount + e.WrongAnswersCount,
                        DatePassing = e.DatePassing
                    };
                    statMod.RelativeResult = 1;
                    statMod.TestTitle = _context.Tests.FirstOrDefault(op => op.Id == e.TestId).TestTitle;
                    StatisticModels.Add(statMod);
                }
                TestsCount = StatisticModels.Count;
                
                TotalRightAnswersCount = StatisticModels.Sum(op => op.RightAnswersCount);
                TotalWrongAnswersCount = StatisticModels.Sum(op => op.WrongAnswersCount);
                TotalAnswersCount = TotalRightAnswersCount + TotalWrongAnswersCount;
                AverageResult = Math.Round(((double)TotalRightAnswersCount / TotalAnswersCount), 2) * 100; 
                return Page();
            }
            else return RedirectToPage("/Error");
        }

        public IActionResult OnGetDeleteTestResult(int id)
        {
            SelectedUser = _context.TestingUsers.FirstOrDefault(op => op.Id == HttpContext.Session.GetString("SelectedUserId"));

            TestResult? deletingResult = _context.TestResults.FirstOrDefault(op => op.Id == id);
            // Находим текущего (авторизованного) пользователя
            ClaimsPrincipal authorizedUser = HttpContext.User;

            // Находим пользователя - создателя теста
            TestingUser? testOwnerUser = new();
            if (deletingResult != null)
            {
                testOwnerUser = _context.TestingUsers.FirstOrDefault(op => op.Id == deletingResult.TestingUserId);
            }

            // проверяем на null значения пользователя- создателя теста и текущего (авторизованного) пользователя
            // и удаляем результаты теста, если это один и тот же пользователь
            if (testOwnerUser != null && authorizedUser != null) 
            {
                if (deletingResult != null && testOwnerUser.UserName == authorizedUser.Identity.Name)
                {
                    _context.TestResults.Remove(deletingResult);
                    _context.SaveChanges();
                    // Снова получаем коллекцию результатов
                    TestResults = _context.TestResults.Where(op => op.TestingUserId == deletingResult.TestingUserId);
                    // инициируем коллекцию StatisticModels, заполняя её результатами
                    StatisticModels = new();
                    if (SelectedUser != null && TestResults != null)
                    {
                        foreach (var e in TestResults)
                        {
                            StatisticModel statMod = new StatisticModel()
                            {
                                Id = e.Id,
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
                        TestsCount = StatisticModels.Count;

                        TotalRightAnswersCount = StatisticModels.Sum(op => op.RightAnswersCount);
                        TotalWrongAnswersCount = StatisticModels.Sum(op => op.WrongAnswersCount);
                        TotalAnswersCount = TotalRightAnswersCount + TotalWrongAnswersCount;
                        AverageResult = Math.Round(((double)TotalRightAnswersCount / TotalAnswersCount), 2) * 100;
                        return Page();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Непредвиденная ошибка";
                        return RedirectToPage("/Error");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Вы не можете удалить результаты не своих тестов";
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Непредвиденная ошибка";
                return RedirectToPage("/Error");
            }
        }
    }

    // класс модели для отображения данных на странице статистики
    public class StatisticModel
    {
        public int Id { get; set; }
        public string TestTitle { get; set; } = "";
        public string UserAliasName { get; set; } = "";
        public string UserEmail { get; set; } = "";
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
