using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class TestPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public TestPageModel(ApplicationDbContext context)
        {
            _context = context;
            Tests = _context.Tests;
            RandomTests = IndexModel.GetRandomElements(Tests, 5);
        }
        
        public int TestCount { get; set; }
        public IEnumerable<Test> Tests { get; set; }
        public IEnumerable<Test> RandomTests { get; set; }
        public IEnumerable<Test> SelectedTests { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public IEnumerable<string> DifficaltyLevels { get; set; }

        public void OnGet()
        {
            Languages = _context.ProgramLanguages.Select(op => op.LanguageTitle);
            DifficaltyLevels = _context.DifficultyLevels.Select(op => op.Level);
            SelectedTests = _context.Tests;
        }

        public void OnPost()
        {
            Languages = _context.ProgramLanguages.Select(op => op.LanguageTitle);
            DifficaltyLevels = _context.DifficultyLevels.Select(op => op.Level);
            SelectedTests = _context.Tests;
        }

        public void OnGetByLanguage(string? option)
        {
            if(!String.IsNullOrEmpty(option))
            {
                Languages = _context.ProgramLanguages.Select(op => op.LanguageTitle);
                SelectedTests = _context.Tests.Where(op => op.Language == option);
                DifficaltyLevels = _context.DifficultyLevels.Select(op => op.Level);
            }
            else SelectedTests = _context.Tests;
        }
    }
}
