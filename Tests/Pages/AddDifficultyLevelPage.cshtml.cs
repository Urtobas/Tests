using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class AddDifficultyLevelPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddDifficultyLevelPageModel(ApplicationDbContext context)
        {
            _context = context;
            DifficultyLevels = _context.DifficultyLevels.ToList();
        }
        [BindProperty]
        public DifficultyLevel? DifficultyLevelBP { get; set; }
        public ICollection<DifficultyLevel>  DifficultyLevels { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnGetRemoveLevel(int? id)
        {
            if(id != null)
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
            }
            return RedirectToPage("/Error");
        }

        public IActionResult OnPost()
        {
            if(DifficultyLevelBP != null)
            {
                _context.DifficultyLevels.Add(DifficultyLevelBP);
                _context.SaveChanges();
                DifficultyLevels = _context.DifficultyLevels.ToList();
                return RedirectToPage("/AddDifficultyLevelPage");
            }
            return RedirectToPage("/Error");
        }
    }
}