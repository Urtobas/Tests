using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    //[Authorize(Policy = "onlyAdmin")]
    public class AddDifficultyLevelPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AddDifficultyLevelPageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            DifficultyLevels = _context.DifficultyLevels.ToList();
        }

        [BindProperty]
        public DifficultyLevel? DifficultyLevelBP { get; set; }

        public IEnumerable<DifficultyLevel> DifficultyLevels { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnGetRemoveLevel(int? id)
        {
            bool flag = HttpContext.User.HasClaim(op => op.Type == "status" && op.Value == "admin");
            if (id != null && flag)
            {
                DifficultyLevel? level = _context.DifficultyLevels.FirstOrDefault(op => op.Id == id);
                if (level != null)
                {
                    _context.DifficultyLevels.Remove(level);
                    _context.SaveChanges();
                    DifficultyLevels = _context.DifficultyLevels.ToList();
                    DifficultyLevelBP = null;
                    return Page();
                }
                else
                {
                    TempData["ErrorMessage"] = "Произошла ошибка при удалении уровня сложности. " +
                        "Возможно у вас недостаточно прав для выполения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Произошла ошибка при удалении уровня сложности. " +
                        "Возможно у вас недостаточно прав для выполения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                return RedirectToPage("/Error");
            } 
        }

        public IActionResult OnPost()
        {
            bool flag = HttpContext.User.HasClaim(op => op.Type == "status" && op.Value == "admin");
            if (DifficultyLevelBP != null && flag)
            {
                try
                {
                    _context.DifficultyLevels.Add(DifficultyLevelBP);
                    _context.SaveChanges();
                    DifficultyLevels = _context.DifficultyLevels.ToList();
                    return Page();
                }
                catch
                {
                    TempData["ErrorMessage"] = "Произошла ошибка при добавлении уровня сложности. " +
                       "Возможно у вас недостаточно прав для выполения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                    return RedirectToPage("/Error");
                }
               
            }
            else
            {
                TempData["ErrorMessage"] = "Произошла ошибка при добавлении уровня сложности. " +
                      "Возможно у вас недостаточно прав для выполения данного действия. Для получения таких прав обратитесь к администратору" + "</br>";
                return RedirectToPage("/Error");
            }

        }
    }
}