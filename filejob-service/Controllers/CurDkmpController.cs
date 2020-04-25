using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class CurDkmpController : ControllerBase
    {
        [HttpGet]
        public string Get(string token)
        {
            if (token != null && token != "")
            {
                try
                {
                    var jsonElements = new JavaScriptSerializer().Serialize(Startup.sourceClientData.Find((x) => x.Token == token).Current.DcmpElements);
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
                Regex regex = new Regex(@"z(\d*\.?\d*)");
                MatchCollection matches = regex.Matches(value);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        Startup.sourceClientData.Find((x) => x.Token == token).Current.DcmpElements.Add(match.Value);
                    }
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}