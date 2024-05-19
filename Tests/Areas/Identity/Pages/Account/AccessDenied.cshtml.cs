#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tests.Areas.Identity.Pages.Account
{

    public class AccessDeniedModel : PageModel
    {

        public void OnGet()
        {
            TempData["ErrorMessage"] = "У вас недостаточно прав для доступа к этому функционалу. Обратитесь за помощью к администратору";
        }
    }
}
