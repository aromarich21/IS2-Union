using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend_service
{
    public class VersionModel : PageModel
    {
        public string serviceName;
        public string serviceVersion;

        public void OnGet()
        {
            serviceName = Properties.Resources.name_service;
            serviceVersion = Startup.version;    
        }
    }
}