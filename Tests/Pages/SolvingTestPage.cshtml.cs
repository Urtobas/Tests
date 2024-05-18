using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class SolvingTestPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<TestingUser> _userManager;
        public SolvingTestPageModel(ApplicationDbContext context)
        {
            _context = context;
            ResultDict = new();
        }


        public Test? CurrentTest { get; set; }
        public int Count { get; set; }
        public int CountRightAnswers { get; set; }
        public IEnumerable<QuestionBlock> Blocks { get; set; }
        public Dictionary<int, int> ResultDict { get; set; }



        public void OnGet(int? id)
        {
            CurrentTest = _context.Tests.FirstOrDefault(op => op.Id == id);
            if (CurrentTest != null)
            {
                Blocks = _context.QuestionBlocks.Where(op => op.TestId == CurrentTest.Id);
                Count = Blocks.Count();
            }
        }


        public IActionResult OnPost(int currentTestId, int answer1, int answer2, int answer3, int answer4, int answer5, 
            int answer6, int answer7, int answer8, int answer9, int answer10,
            int answer11, int answer12, int answer13, int answer14, int answer15,
            int answer16, int answer17, int answer18, int answer19, int answer20)
        {
            CurrentTest = _context.Tests.FirstOrDefault(op => op.Id == currentTestId);
            int[] answers = {answer1, answer2, answer3, answer4, answer5, answer6, answer7, answer8, answer9, answer10,
            answer11, answer12, answer13, answer14, answer15, answer16, answer17, answer18, answer19, answer20 };
            
            string res = "";
            

            if (CurrentTest != null) Blocks = _context.QuestionBlocks.Where(op => op.TestId == CurrentTest.Id);
            else return RedirectToPage("/Error");

            Count = Blocks.Count();
            for (int i = 0; i < Count; i++)
            {
                ResultDict.Add(i+1, answers[i]);
                if(answers[i] == Blocks.ElementAt(i).RightAnswerNum) res += "<span class='text-success'>" + "вопрос №" + ResultDict.ElementAt(i).Key + " - " + ResultDict.ElementAt(i).Value + "</span></br>";
                else res += "<span class='text-danger text-decoration-line-through'>" + "вопрос №" + ResultDict.ElementAt(i).Key + " - " + ResultDict.ElementAt(i).Value + "</span>" + " " + Blocks.ElementAt(i).RightAnswerNum + "</br>";
                if (answers[i] == Blocks.ElementAt(i).RightAnswerNum)
                {
                    CountRightAnswers++;
                }
            }
            TempData["result"] = res;
            TempData["RightAnswers"] = CountRightAnswers;
            string userEmail = HttpContext.User?.Identity?.Name ?? "гость"; // если null, то вместо почты "гость"
            IdentityUser? user = _context.Users.FirstOrDefault(op => op.Email == userEmail);
            
            if(user != null)
            {
                TestResult result = new()
                {
                    DatePassing = DateTime.Now,
                    RightAnswersCount = CountRightAnswers,
                    WrongAnswersCount = ResultDict.Count() - CountRightAnswers,
                    TestId = CurrentTest.Id,
                    TestingUserId = user.Id
                };
                _context.TestResults.Add(result);
                _context.SaveChanges();
                return Page();
            }
            else
            {
                TempData["ErrorMessage"] = $"Ошибка. Пользователь с такими данными не найден";
                return RedirectToPage("/Error");
            }
        }
    }
}
