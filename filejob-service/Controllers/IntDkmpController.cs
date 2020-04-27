using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class IntDkmpController : ControllerBase
    {
        [HttpGet]
        public string Get(string token)
        {
            if (token != null && token != "")
            {
                try
                {
                    var jsonElements = new JavaScriptSerializer().Serialize(Startup.sourceClientData.Find((x) => x.Token == token).Integration.DcmpElements);
                    return jsonElements;
                }
                catch
                {
                    return "Не найдено";
                }
                
            }
            return "Token undefined";
        }

        [HttpPost]
        public async Task<IActionResult> Post(string token, string value)
        {
            if (token != null && token != "")
            {
                Startup.sourceClientData.Find((x) => x.Token == token).Integration.DcmpElements.Add(value);
                return Ok();
            }
            return BadRequest();
        }
    }
}