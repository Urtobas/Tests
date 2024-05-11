using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages.AddingPages
{
    public class AddTestPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddTestPageModel(ApplicationDbContext context)
        {
            _context = context;
            Languages = _context.ProgramLanguages;
            Levels = _context.DifficultyLevels.ToList();
        }
        [BindProperty]
        public Test AddingTest { get; set; }
        public string? CurrentUserName { get; set; }

        public IEnumerable<DifficultyLevel> Levels { get; set; }
        public IEnumerable<ProgramLanguage> Languages { get; set; }

        public void OnGet()
        { 
            try
            {
                CurrentUserName = HttpContext.User.Identity.Name;
            }
            catch
            {
                CurrentUserName = "";
            }
        }

        public IActionResult OnPost()
        {
            bool flag = true;
            if (AddingTest.Author == "Вход не выполнен") flag = false;

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
                return RedirectToPage("../Index");
            }
            else
            {
                TempData["ErrorMessage"] = $"Возникла ошибка при добавлении теста. " +
                    $"Возможно тест с таким названием уже существует в базе данных или вы не выполнили вход в свой аккаунт. " +
                    $"Вернитесь на страницу добавления теста и попробуйте снова. ";

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

                return RedirectToPage("../Error");
            }
        }
    }
}
