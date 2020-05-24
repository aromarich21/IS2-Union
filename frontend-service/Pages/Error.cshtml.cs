using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend_service.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }
        static public string ErrorMessage { get; set; }
        public string TitleError = "ОШИБКА";
        static public string DefaultMessage = "Упс, произошла непредвиденная ошибка. Пожалуйста, попробуйте еще раз.";
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        private readonly ILogger<ErrorModel> _logger;
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            if (this.HttpContext.Response.StatusCode == 404)
            {
                TitleError = "404";
                ErrorMessage = "Запрашивая страница не найдена!";
            }
        }
        
    }
}
