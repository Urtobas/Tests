using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class EditTestParametersPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EditTestParametersPageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public Test? EditingTest { get; set; }

        public IEnumerable<DifficultyLevel> DifficultyLevels { get; set; }
        public IEnumerable<ProgramLanguage> Languages { get; set; }
        public string? CurrentUserName { get; set; }

        public IActionResult OnGet(int? id) // здесь Id относится к классу Test
        {
            if (id != null) EditingTest = _context.Tests.FirstOrDefault(op => op.Id == id);
            else
            {
                TempData["ErrorMessage"] = "Произошла ошибка - запрашиваемый тест не найден";
                return RedirectToPage("/Error");
            }
            DifficultyLevels = _context.DifficultyLevels;
            Languages = _context.ProgramLanguages;
            return Page();
        }

        public IActionResult OnPost() 
        {
            DifficultyLevels = _context.DifficultyLevels;
            Languages = _context.ProgramLanguages;
            Test? test = _context.Tests.FirstOrDefault(op => op.Id == EditingTest.Id);
            if(test != null && EditingTest != null)
            {
                test.TestTitle = EditingTest.TestTitle;
                test.DifficultyLevel = EditingTest.DifficultyLevel;
                test.Language = EditingTest.Language;
                _context.Tests.Update(test);
                int res = _context.SaveChanges();
                if (res > 0)
                {
                    return RedirectToPage("/UserPage");
                }
                else
                {
                    TempData["ErrorMessage"] = "Произошла ошибка при сохранении изменений теста в базе данных";
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Произошла ошибка при сохранении изменений теста в базе данных";
                return RedirectToPage("/Error");
            }
        }
    }
}
