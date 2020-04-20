using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Mvc;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(string id, string token)
        {
            if (token != null && token != "")
            {
                try
                {
                    
                    if (Startup.sourceClientData[Startup.sourceClientData.FindIndex((x) => x.Token == token)].Result.Elements.Count != 0)
                    {
                        Startup.sourceClientData[Startup.sourceClientData.FindIndex((x) => x.Token == token)].Result.Elements.Clear();
                        Startup.sourceClientData[Startup.sourceClientData.FindIndex((x) => x.Token == token)].Result.Links.Clear();
                    }
                    Recoder recoder = new Recoder(token, Startup.sourceClientData, id);
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string token)
        {
            if (token != null && token != "")
            {
                Recoder recoder = new Recoder(token);
                recoder.RefreshRecoder(Startup.sourceClientData);
                return Ok("Done");
            }
            return BadRequest();
        }
    }
}