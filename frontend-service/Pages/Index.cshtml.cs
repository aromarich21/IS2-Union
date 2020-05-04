using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SHDocVw;

namespace frontend_service.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string _cookieAccept;
        public string hidden;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnPostAsync() //onclick submit
        {
            CookieAdd();
            Response.Redirect("/Index");
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
