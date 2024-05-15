using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tests.Pages.AddingPages
{
    //[Authorize(Policy = "admin")]
    [Authorize]
    public class AddingPanelModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
