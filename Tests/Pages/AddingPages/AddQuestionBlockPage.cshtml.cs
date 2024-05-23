using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
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
        public string? CurrentUserName { get; set;}



        public void OnGet()
        {
            ClaimsPrincipal? user = HttpContext.User;
            TempData["SuccessMessage"] = "";
            if (user != null)
            {
                CurrentUserName = HttpContext.User.Identity.Name;
            }
        }

        public IActionResult OnPost()
        {
            ClaimsPrincipal? user = HttpContext.User;
            TempData["SuccessMessage"] = "";
            if (user != null)
            {
                try
                {
                    CurrentUserName = HttpContext.User.Identity.Name;
                }
                catch
                {
                    TempData["SuccessMessage"] = "Произошла непредвиденная ошибка";
                    return RedirectToPage("/Error");
                }
            }
            Test? selectedTest = _context.Tests.FirstOrDefault(op => op.Id == AddingQuestionBlock.TestId);
            if (ModelState.IsValid && CurrentUserName == selectedTest.Author)
            {
                try
                {
                    _context.Add(AddingQuestionBlock);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Данные успешно добавлены";
                    AddingQuestionBlock = new();
                    return Page();
                }
                catch
                {
                    TempData["SuccessMessage"] = "Произошла непредвиденная ошибка";
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Вы не можете добавлять вопросы и ответы, а также редактировать тесты, " +
                        "созданные другими пользователями" + "</br>";

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
                TempData["ErrorMessage"] += "</br>" + "</hr>" + errorMessages;
                return RedirectToPage("/Error");
            }
        }
    }
}
