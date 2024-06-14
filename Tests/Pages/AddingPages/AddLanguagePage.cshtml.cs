using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{

    public class AddLanguagePageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddLanguagePageModel(ApplicationDbContext context)
        {
            _context = context;
            Languages = _context.ProgramLanguages.ToList();
        }
        [BindProperty]
        public ProgramLanguage Language { get; set; }

        public ICollection<ProgramLanguage> Languages { get; set; }

        public void OnGet()
        {
           
        }

        public IActionResult OnPost()
        {
            bool flag = HttpContext.User.HasClaim(op => op.Type == "status" && op.Value == "admin");
            if (Language != null && flag)
            {
                try
                {
                    _context.ProgramLanguages.Add(Language);
                    _context.SaveChanges();
                    Languages = _context.ProgramLanguages.ToList();
                    return Page();
                }
                catch(Exception ex)
                {
                    TempData["ErrorMessage"] = "Произошла ошибка при добавления языка программирования. " +
                        "Возможно у вас недостаточно прав для выполения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                    TempData["ErrorMessage"] += $"Список ошибок - {ex}";
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Произошла ошибка при добавления языка программирования. " +
                        "Возможно у вас недостаточно прав для выполения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                return RedirectToPage("/Error");
            }
        }

        public IActionResult OnGetRemoveLanguage(int? id)
        {
            bool flag = HttpContext.User.HasClaim(op => op.Type == "status" && op.Value == "admin"); // проверяем, имеет ли пользователь стату admin
            ProgramLanguage? language = _context.ProgramLanguages.FirstOrDefault(op => op.Id == id);
            if(language != null && flag)
            {
                try
                {
                    _context.ProgramLanguages.Remove(language);
                    _context.SaveChanges();
                    Languages = _context.ProgramLanguages.ToList();
                    return Page();
                }
                catch
                {
                    TempData["ErrorMessage"] = "Произошла ошибка при удлении языка программирования. " +
                       "Возможно у вас недостаточно прав для выполнения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Произошла ошибка при удлении языка программирования. " +
                      "Возможно у вас недостаточно прав для выполнения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                return RedirectToPage("/Error");
            }
        }
    }
}
