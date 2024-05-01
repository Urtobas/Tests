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
        public QuestionBlock AddingQuestionBlock { get; set; }

        public IEnumerable<Test> Tests { get; set; }



        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _context.Add(AddingQuestionBlock);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Данные успешно добавлены";
                return Page();
            }
            else
            {
                TempData["ErrorMessage"] = "Произощла ошибка при добавлении блока вопрос-ответы";
                return RedirectToPage("Error");
            }
        }
    }
}
