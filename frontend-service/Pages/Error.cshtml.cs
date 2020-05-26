using System;
using System.Diagnostics;
using System.Text;
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
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        private readonly ILogger<ErrorModel> _logger;
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            if(ErrorModel.ErrorMessage != "" && ErrorModel.ErrorMessage != null)
            {
                
            }   
            else
            {
                ErrorMessage = DefaultMessage;
            }
            if (this.HttpContext.Response.StatusCode == 404)
            {
                TitleError = "404";
                ErrorMessage = "���������� �������� �� �������!";
            }
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytesTitle = utf8.GetBytes(TitleError);
            Byte[] encodedBytesMessage = utf8.GetBytes(ErrorMessage);
            TitleError = utf8.GetString(encodedBytesTitle);
            ErrorMessage = utf8.GetString(encodedBytesMessage);
        }     
    }
}
