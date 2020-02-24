using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class ResElementsController : ControllerBase
    {
        [HttpGet]
        public string Get(string token)
        {
            List<SourceElements> source_Elements = Startup.sourceResElements;
            foreach (SourceElements item in source_Elements)
            {
                if (item.Token == token)
                {
                    var elements = item.elements.AsEnumerable();
                    string json = new JavaScriptSerializer().Serialize(elements);
                    return json;
                }
            }
            return "Not found";
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name, string id, string level, string number, string status, string type, string formalization, string token)
        {
            var checkToken = false;
            Elements inputElement = new Elements(name, id, level, number, status, type, formalization);
            if (token != null && token != "")
            {
                foreach (SourceElements item in Startup.sourceResElements)
                {
                    if (item.Token == token)
                    {
                        item.elements.Add(inputElement);
                        checkToken = true;
                        return Ok();
                    }
                }
                if (checkToken == false)
                {
                    SourceElements s_elements = new SourceElements();
                    s_elements.elements.Add(inputElement);
                    s_elements.Token = token;
                    Startup.sourceResElements.Add(s_elements);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string token)
        {
            if (token != null && token != "")
            {
                foreach (SourceElements item in Startup.sourceResElements)
                {
                    if (item.Token == token)
                    {
                        item.elements.Clear();
                        //checkToken = true;
                        return Ok();
                    }
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}