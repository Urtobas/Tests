using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class StatisticPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<TestingUser> TestingUsersList { get; set; }

        public StatisticPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            TestingUsersList = _context.TestingUsers;
        }
    }


}
