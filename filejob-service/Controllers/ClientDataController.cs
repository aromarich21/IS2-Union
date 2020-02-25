using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class ClientDataController : ControllerBase
    {
        [HttpGet]
        public string Get(string token, string type, string entity)
        {
            if (token != null && token != "")
            {   
                if (type != null)
                {
                    if (entity != null)
                    {
                        ClientDataJob clientDataJobFull = new ClientDataJob(token, type, entity);
                        return clientDataJobFull.Json;
                    }
                    ClientDataJob clientDataJobType = new ClientDataJob(token, type);
                    return clientDataJobType.Json;
                }
                if (entity != null)
                {
                    return "Type undefined";
                }
                ClientDataJob clientDataJob = new ClientDataJob(token, type);
                return clientDataJob.Json;
            }
            return "Token undefined";
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name, string id, string level, string number, string status, string type, string formalization, string token)
        {
            var checkToken = false;
            Elements inputElement = new Elements(name, id, level, number, status, type, formalization);
            if (token != null && token != "")
            {
                foreach (SourceElements item in Startup.sourceCurElements)
                {
                    if (item.Token == token)
                    {
                        item.elements.Add(inputElement);
                        return Ok();
                    }
                }
                if (checkToken == false)
                {
                    SourceElements s_elements = new SourceElements();
                    s_elements.elements.Add(inputElement);
                    s_elements.Token = token;
                    Startup.sourceCurElements.Add(s_elements);
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