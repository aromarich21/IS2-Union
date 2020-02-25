using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using filejob_service.Models;
using System.Linq;
using System.Threading.Tasks;
using Nancy.Json;

namespace filejob_service.API.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class CurElementsController : ControllerBase
    {
        public string _typeUnit = "cur";
        public string _entity = "elements";

        [HttpGet]
        public string Get(string token)
        {
            if (token != null && token != "")
            {
                ClientDataJob jobGetCurUnitElements = new ClientDataJob(token, _typeUnit, _entity);
                return jobGetCurUnitElements.Json;
            }
            return "Token undefined";
        }  

        [HttpPost]
        public async Task<IActionResult> Post(string name, string id, string level, string number, string status, string type, string formalization, string token)
        {
            if (token != null && token != "")
            {
                Elements inputElement = new Elements(name, id, level, number, status, type, formalization);
                ClientDataJob jobAddCurUnitElements = new ClientDataJob(token, Startup.sourceClientData);
                jobAddCurUnitElements.AddElement(Startup.sourceClientData, _typeUnit, inputElement);
                return Ok();
            }
            return BadRequest();         
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string token)
        {
            if (token != null && token != "")
            {
                foreach (SourceElements item in Startup.sourceCurElements)
                {
                    if (item.Token == token)
                    {
                        item.elements.Clear();
                        return Ok();
                    }
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}

   