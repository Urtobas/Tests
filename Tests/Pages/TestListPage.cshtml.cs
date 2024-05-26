using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class TestListPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<Test> TestList { get; set; }

        public TestListPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            TestList = _context.Tests;
        }
    }
}
