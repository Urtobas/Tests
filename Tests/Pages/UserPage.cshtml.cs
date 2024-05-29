using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class UserPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public UserPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Test> UserTests { get; set; }
        public string? UserName { get; set; }
        public string UserId { get; set; }

        public void OnGet(string id)
        {
            var currentUser = HttpContext.User.Identity;
            if (currentUser != null)
            {
                UserName = currentUser.Name;
                UserId = _context.Users.FirstOrDefault(op => op.UserName == UserName).Id;
            }
            UserTests = _context.Tests.Where(op => op.Author == UserName);
        }
    }
}
