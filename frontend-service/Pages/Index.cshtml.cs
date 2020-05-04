using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend_service.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string _cookieAccept;
        public bool point;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            @ErrorModel.ErrorMessage = "";
            CookiesJob();
        }

        public void CookiesJob() //job with cookie
        {
            _cookieAccept = Request.Cookies["token_"];
            if (_cookieAccept == null || _cookieAccept == "null" || _cookieAccept == "")
            {
                point = true;
            }
            else
            {
                point = false;
            }
        }
    }
}
