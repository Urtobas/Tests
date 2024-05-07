using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            Blocks = _context.QuestionBlocks;
        }


        public Test? CurrentTest { get; set; }
        public int Count { get; set; }
        public IEnumerable<QuestionBlock> Blocks { get; set; }
        public Dictionary<int, string> ResultDict { get; set; }



        public void OnGet(int? id)
        {
            CurrentTest = _context.Tests.FirstOrDefault(op => op.Id == id);
            if (CurrentTest != null)
            {
                Blocks = Blocks.Where(op => op.TestId == CurrentTest.Id);
                Count = Blocks.Count();
            }
        }



        public IActionResult OnPost()
        {
            return Content("");
        }

        public void OnGetNextBlock(int? id)
        {

        }
    }
}
