
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
                    Recoder recoder = new Recoder
                        (token, id, Startup.sourceCurElements, Startup.sourceCurLinks,
                        Startup.sourceIntElements, Startup.sourceIntLinks, 
                        Startup.sourceResElements, Startup.sourceResLinks);
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
                recoder.RefreshRecoder(Startup.sourceResElements, Startup.sourceResLinks);
                return Ok("Done");
            }
            return BadRequest();
        }
    }

}