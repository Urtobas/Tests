using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class AddQuestionBlockPageModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        public AddQuestionBlockPageModel(ApplicationDbContext context)
        {
            _context = context;
            Tests = _context.Tests;
        }
        [BindProperty]
        public QuestionBlock AddingQuestionBlock { get; set; }

        public IEnumerable<Test> Tests { get; set; }



        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(AddingQuestionBlock);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "������ ������� ���������";
                return Page();
            }
            else
            {
                TempData["ErrorMessage"] = $"��������� ������ ��� ���������� ����� ������-����� - {ModelState.IsValid}";
                string errorMessages = "";
                // �������� �� ���� ��������� � ModelState
                foreach (var item in ModelState)
                {
                    // ���� ��� ������������� �������� ������� ������
                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        errorMessages = $"{errorMessages}\n������ ��� �������� {item.Key}:\n";
                        // ����������� �� ���� �������
                        foreach (var error in item.Value.Errors)
                        {
                            errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                        }
                    }
                }
                TempData["ErrorMessage"] += errorMessages;
                return RedirectToPage("/Error");
            }
        }
    }
}
