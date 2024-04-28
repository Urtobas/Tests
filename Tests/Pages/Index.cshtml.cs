using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int CountProp { get; set; }
        public ICollection<Test> Tests { get; set; }

        public void OnGet()
        {
            CountProp = _context.Tests.Count();
            Tests = _context.Tests.ToList();
        }
    }
}