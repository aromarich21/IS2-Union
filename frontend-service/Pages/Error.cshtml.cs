using System.Diagnostics;
using System.Text;
using System.Web;
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
        public string TitleError = "������";
        static public string DefaultMessage = "���, ��������� �������������� ������. ����������, ���������� ��� ���.";
        public string Message { get; set; }
        public bool point = false;
        public string Title { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        private readonly ILogger<ErrorModel> _logger;
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            if (ErrorMessage == null || ErrorMessage == "")
            {
                ErrorMessage = DefaultMessage;
            }
            else
                point = true;
            if (this.HttpContext.Response.StatusCode == 404)
            {
                TitleError = "404";
                point = false;
            }
            byte[] utf8title = Encoding.UTF8.GetBytes(TitleError);
            byte[] utf8message = Encoding.UTF8.GetBytes(ErrorMessage);
            Message = Encoding.UTF8.GetString(utf8message);
            Title = Encoding.UTF8.GetString(utf8title);
            Message = HttpUtility.HtmlEncode(ErrorMessage);
            Title = HttpUtility.HtmlEncode(TitleError);
        }     
    }
}
