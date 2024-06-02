using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class EditTestPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EditTestPageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Tests = _context.Tests;
        }


        public IEnumerable<Test> Tests { get; set; }
        public Test? SelectedTest { get; set; }
        public IEnumerable<QuestionBlock> QuestionBlocks { get; set; }
        public string? CurrentUserName { get; set; }

        public void OnGet(int? testId) // здесь testId относитс€ к классу Test
        {
           
            if(testId != null) SelectedTest = _context.Tests.FirstOrDefault(op => op.Id == testId);
            QuestionBlocks = _context.QuestionBlocks.Where(op => op.TestId == testId);
        }

        public IActionResult OnGetDeleteQuestion(int? testId, int? questionId) // здесь testId относитс€ к классу Test, questionId относитс€ к классу QuestionBlock
        {
            QuestionBlock? deletingBlock = _context.QuestionBlocks.FirstOrDefault(op => op.Id == questionId);
            QuestionBlocks = _context.QuestionBlocks.Where(op => op.TestId == testId);
            if (testId != null) SelectedTest = _context.Tests.FirstOrDefault(op => op.Id == testId);
            if (deletingBlock != null)
            {
                _context.Remove(deletingBlock);
                _context.SaveChanges();
                return Page();
            }
            else
            {
                TempData["ErrorMessage"] = "ѕроизошла ошибка при удалении блока вопросов. " +
                    "¬ернитесь на страницу редактировани€ тестов и попробуйте снова";
                return RedirectToPage("/Error");
            }
            
        }
    }
}
