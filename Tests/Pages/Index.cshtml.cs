using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private int tagsCount = 6;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            tagsCount = 10;

        }
        public int TestCount { get; set; }
        public IEnumerable<Test> Tests { get; set; }
        public IEnumerable<Test> RandomTests { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                TestCount = _context.Tests.Count();
                Tests = _context.Tests.ToList();
                RandomTests = GetRandomElements(Tests, tagsCount).ToList();
                return Page();
            }
            catch
            {
                Tests = new List<Test>();
                RandomTests = new List<Test>();
                TempData["ErrorMessage"] = "Произошла ошибка, вызванная проблемами соединения с базой данных";
                return RedirectToPage("/Error");
            }
        }

        //перенести потом в отдельную библитеку
        public static IEnumerable<T> GetRandomElements<T>(IEnumerable<T> collection, int count)
        {
            var random = new Random();
            try
            {
                var shuffledCollection = collection.OrderBy(x => random.Next()).ToList();
                return shuffledCollection.Take(count);
            }
            catch
            {
                return collection;
            }
        }
    }

}