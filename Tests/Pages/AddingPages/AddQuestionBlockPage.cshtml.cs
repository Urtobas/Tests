using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Models;

namespace Tests.Pages
{
    public class AddQuestionBlockPageModel : PageModel
    {
        [BindProperty]
        public QuestionBlock QuestionBlockProp { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
