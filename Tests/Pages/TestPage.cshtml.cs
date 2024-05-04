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

        public void OnGet()
        {

        }
    }
}
