using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    [Authorize(Policy = "onlyAdmin")]
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
            if (Language != null)
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
                    TempData["ErrorMessage"] = $"Ошибка при добавлении языка программирования({ex})";
                    return RedirectToPage("Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Ошибка при добавлении языка программирования (Language = null)";
                return RedirectToPage("Error");
            }
        }

        public void OnGetRemoveLanguage(int? id)
        {
            ProgramLanguage? language = _context.ProgramLanguages.FirstOrDefault(op => op.Id == id);
            if(language != null)
            {
                try
                {
                    _context.ProgramLanguages.Remove(language);
                    _context.SaveChanges();
                }
                catch
                {
                    TempData["ErrorMessage"] = "Ошибка при удалении языка программирования";
                }
            }
        }
    }
}
