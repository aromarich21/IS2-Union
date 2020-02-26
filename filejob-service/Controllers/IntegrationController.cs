using System;
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
                    var _index = Int32.Parse(new ClientDataJob(token, Startup.sourceClientData).IndexClientData);
                    Recoder recoder = new Recoder
                        (token, id, Startup.sourceClientData[_index].Current.Elements, Startup.sourceClientData[_index].Current.Links,
                        Startup.sourceClientData[_index].Integration.Elements, Startup.sourceClientData[_index].Integration.Links,
                        Startup.sourceClientData[_index].Result.Elements, Startup.sourceClientData[_index].Result.Links);
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