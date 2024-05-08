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
        public SolvingTestPageModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public Test? CurrentTest { get; set; }
        public int Count { get; set; }
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
            ResultDict = new();
            Blocks = _context.QuestionBlocks.Where(op => op.TestId == CurrentTest.Id);
            Count = Blocks.Count();
            for (int i = 0; i < Count; i++)
            {
                ResultDict.Add(i+1, answers[i]);
                res += ResultDict.ElementAt(i).Key + " - " + ResultDict.ElementAt(i).Value + "</br>";
            }
            //TempData["result"] = answer1.ToString() + " - " + answer2.ToString() + " - " + answer3.ToString();
            TempData["result"] = res;
            return Page();
        }
    }
}
