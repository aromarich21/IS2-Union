using frontend_service.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend_service
{
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
            @ErrorModel.ErrorMessage = "";
        }
    }
}