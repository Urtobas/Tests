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
        }
        public Test? CurrentTest { get; set; }
        public IEnumerable<QuestionBlock> Blocks { get; set; }

        public void OnGet(int? id)
        {
            CurrentTest = _context.Tests.FirstOrDefault(op => op.Id == id);
            Blocks = _context.QuestionBlocks.Where(op => op.TestId == id);
        }
        public void OnGetNextBlock(int? id)
        {

        }
    }
}
