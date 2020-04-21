using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get(string token)
        {
            try
            {
                var jsonElements = new JavaScriptSerializer().Serialize(Startup.sourceClientData[Startup.sourceClientData.FindIndex((x) => x.Token == token)].Current.Elements);
                return jsonElements;
            }
            catch 
            { 
                return "гавной воняет"; 
            }     
        }
    }
}