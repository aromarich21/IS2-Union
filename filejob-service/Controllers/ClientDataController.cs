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

        [HttpDelete]
        public async Task<IActionResult> Delete(string token, string type)
        {
            if (token != null && token != "")
            {
                if (type != null)
                {
                    ClientDataJob clientDataJobDelete = new ClientDataJob(token, Startup.sourceClientData);
                    clientDataJobDelete.DeleteElements(Startup.sourceClientData, type);
                    clientDataJobDelete.DeleteLinks(Startup.sourceClientData, type);
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}