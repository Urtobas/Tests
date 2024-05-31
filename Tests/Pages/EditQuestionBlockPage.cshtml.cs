using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Tests.Data;
using Tests.Models;

namespace Tests.Pages
{
    public class EditQuestionBlockPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EditQuestionBlockPageModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public QuestionBlock? EditingQuestionBlock { get; set; }

        public Test? SelectedTest { get; set; }
        public string TestTitle { get; set; }
        public string CurrentUserName { get; set; }

        public void OnGet(int? testId, int? questionId)
        {
            ClaimsPrincipal? user = HttpContext.User;
            TempData["SuccessMessage"] = "";
            if (HttpContext.User.Identity != null && user != null)
            {
                if(HttpContext.User.Identity.Name != null) CurrentUserName = HttpContext.User.Identity.Name;
            }
            SelectedTest = _context.Tests.FirstOrDefault(op => op.Id == testId);
            EditingQuestionBlock = _context.QuestionBlocks.FirstOrDefault(op => op.Id == questionId);
        }

        public IActionResult OnPost(int? testId, int? questionId)
        {
            ClaimsPrincipal? user = HttpContext.User;
            TempData["SuccessMessage"] = "";
            if (HttpContext.User.Identity != null && user != null)
            {
                try
                {
                    if (HttpContext.User.Identity.Name != null)
                    {
                        CurrentUserName = HttpContext.User.Identity.Name;
                    }
                }
                catch
                {
                    TempData["ErrorMessage"] = "��������� �������������� ������";
                    return RedirectToPage("/Error");
                }
            }
            SelectedTest = _context.Tests.FirstOrDefault(op => op.Id == testId);
            QuestionBlock? block = _context.QuestionBlocks.FirstOrDefault(op => op.Id == questionId);
            if(EditingQuestionBlock != null && block != null)
            {
                block.Question = EditingQuestionBlock.Question;
                block.Answer1 = EditingQuestionBlock.Answer1;
                block.Answer2 = EditingQuestionBlock.Answer2;
                block.Answer3 = EditingQuestionBlock.Answer3;
                block.Answer4 = EditingQuestionBlock.Answer4;
                block.RightAnswerNum = EditingQuestionBlock.RightAnswerNum;
                _context.QuestionBlocks.Update(block);
                int res = _context.SaveChanges();
                if(res > 0)TempData["SuccessMessage"] = "������ ������� ��������!";
                return Page();
            }
            else
            {
                TempData["SuccessMessage"] = "��������� �������������� ������";
                return RedirectToPage("/Error");
            }
           

            //if(SelectedTest != null)
            //{
            //    if (ModelState.IsValid && CurrentUserName == SelectedTest.Author)
            //    {
            //        try
            //        {
            //            if (EditingQuestionBlock != null)
            //            {
            //                _context.QuestionBlocks.Update(EditingQuestionBlock);
            //                int res = _context.SaveChanges();
            //                TempData["SuccessMessage"] = "������ ������� ��������!";
            //                return Page();
            //            }
            //            else
            //            {
            //                TempData["ErrorMessage"] = "��������� �������������� ������";
            //                return RedirectToPage("/Error");
            //            }
            //        }
            //        catch
            //        {
            //            TempData["ErrorMessage"] = "��������� �������������� ������";
            //            return RedirectToPage("/Error");
            //        }
            //    }
            //    else
            //    {
            //        TempData["ErrorMessage"] = "�� �� ������ ��������� ������� � ������, � ����� ������������� �����, " +
            //                "��������� ������� ��������������" + "</br>";

            //        string errorMessages = "";
            //        // �������� �� ���� ��������� � ModelState
            //        foreach (var item in ModelState)
            //        {
            //            // ���� ��� ������������� �������� ������� ������
            //            if (item.Value.ValidationState == ModelValidationState.Invalid)
            //            {
            //                errorMessages = $"{errorMessages}\n������ ��� �������� {item.Key}:\n";
            //                // ����������� �� ���� �������
            //                foreach (var error in item.Value.Errors)
            //                {
            //                    errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
            //                }
            //            }
            //        }
            //        TempData["ErrorMessage"] += "</br>" + "</hr>" + errorMessages;
            //        return RedirectToPage("/Error");
            //    }
            //}
            //else
            //{
            //    TempData["ErrorMessage"] = "��������� �������������� ������";
            //    return RedirectToPage("/Error");
            //}
        }
    }
}
