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
            Languages = _context.ProgramLanguages.ToList();
            Levels = _context.DifficultyLevels.ToList();
        }
        [BindProperty]
        public Test AddingTest { get; set; }

        public ICollection<DifficultyLevel> Levels { get; set; }
        public ICollection<ProgramLanguage> Languages { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            bool flag = true;

            foreach (var e in _context.Tests)
            {
                if (e.TestTitle.Trim().ToUpper() == AddingTest.TestTitle.Trim().ToUpper())
                {
                    flag = false;
                    break;
                }
            }

            if (ModelState.IsValid && flag)
            {
                _context.Tests.Add(AddingTest);
                _context.SaveChanges();
                return Page();
            }
            else
            {
                TempData["ErrorMessage"] = "Возникла ошибка при добавлении теста, вернитесь на страницу добавления теста и попробуйте снова";
                return RedirectToPage("../Error");
            }
        }
    }
}
