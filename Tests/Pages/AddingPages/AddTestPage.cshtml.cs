using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages.AddingPages
{
    public class AddTestPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddTestPageModel(ApplicationDbContext context)
        {
            _context = context;
            Languages = _context.ProgramLanguages;
            Levels = _context.DifficultyLevels.ToList();
        }
        [BindProperty]
        public Test AddingTest { get; set; }


        public IEnumerable<DifficultyLevel> Levels { get; set; }
        public IEnumerable<ProgramLanguage> Languages { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            bool flag = true;

            foreach (var e in _context.Tests)
            {
                if (e.TestTitle.Trim().ToUpper() == AddingTest.TestTitle.Trim().ToUpper())
                {
                    flag = false;
                    break;
                }
            }

            if (ModelState.IsValid && flag)
            {
                _context.Tests.Add(AddingTest);
                _context.SaveChanges();
                return Page();
            }
            else
            {
                TempData["ErrorMessage"] = $"�������� ������ ��� ���������� �����, ��������� �� �������� ���������� ����� � ���������� ����� - ";

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

                return RedirectToPage("../Error");
            }
        }
    }
}
