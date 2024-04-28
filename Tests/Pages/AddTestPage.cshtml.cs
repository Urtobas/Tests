using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class AddTestPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddTestPageModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Test TestProp { get; set; }

        public ICollection<ProgramLanguage> Languages { get; set; }

        public void OnGet()
        {
            Languages = _context.ProgramLanguages.ToList();
        }

        public void OnPost()
        {
        }
    }
}
