using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend_service.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend_service
{
    public class MainModel : PageModel
    {
        private readonly ILogger<MainModel> _logger;
        public string _cookieAccept;
        public string hidden;

        public MainModel(ILogger<MainModel> logger)
        {
            _logger = logger;
        }

        public async Task OnPostAsync() //onclick submit
        {
            CookieAdd();
            Response.Redirect("/Main");
        }

        public void OnGet()
        {
            @ErrorModel.ErrorMessage = "";
            CookiesJob();
        }

        public void CookieAdd() //job with token
        {
            _cookieAccept = Request.Cookies["_cookieAccept"];
            if (_cookieAccept == null || _cookieAccept == "null" || _cookieAccept == "")
            {
                _cookieAccept = Guid.NewGuid().ToString();  //UUID generate
                //token_ = TokenGenerate(5); //token value generate
                Response.Cookies.Append("_cookieAccept", _cookieAccept);   //cookie add [_cookieAccept]
            }
        }

        public void CookiesJob() //job with cookie
        {
            _cookieAccept = Request.Cookies["_cookieAccept"];
            if (_cookieAccept == null || _cookieAccept == "null" || _cookieAccept == "")
            {
                hidden = "";
            }
            else
            {
                hidden = "hidden";
            }
        }
    }
}