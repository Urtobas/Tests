using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class AddQuestionBlockPageModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public AddQuestionBlockPageModel(ApplicationDbContext context)
        {
            _context = context;
            Tests = _context.Tests; 
        }
        [BindProperty]
        public QuestionBlock QuestionBlockProp { get; set; }

        public IEnumerable<Test> Tests { get; set; }

       

        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
