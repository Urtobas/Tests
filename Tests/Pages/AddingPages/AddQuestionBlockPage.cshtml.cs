using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class AddQuestionBlockPageModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AddQuestionBlockPageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

            Tests = _context.Tests;
        }
        [BindProperty]
        public QuestionBlock AddingQuestionBlock { get; set; }

        public IEnumerable<Test> Tests { get; set; }



        public void OnGet()
        {
            var user = HttpContext.User;
            TempData["SuccessMessage"] = "";
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(AddingQuestionBlock);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Данные успешно добавлены";
                    return Page();
                }
                catch
                {
                    TempData["SuccessMessage"] = "Ошибка";
                    return Page();
                }

            }
            else
            {
                TempData["ErrorMessage"] = $"Произошла ошибка при добавлении блока вопрос-ответ - {ModelState.IsValid}";
                string errorMessages = "";
                // проходим по всем элементам в ModelState
                foreach (var item in ModelState)
                {
                    // если для определенного элемента имеются ошибки
                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                        // пробегаемся по всем ошибкам
                        foreach (var error in item.Value.Errors)
                        {
                            errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                        }
                    }
                }
                TempData["ErrorMessage"] += errorMessages;
                return RedirectToPage("/Error");
            }
        }
    }
}
