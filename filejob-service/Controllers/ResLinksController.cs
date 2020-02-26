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
    public class ResLinksController : ControllerBase
    {
        public string _typeUnit = "res";
        public string _entity = "links";

        [HttpGet]
        public string Get(string token)
        {
            if (token != null && token != "")
            {
                ClientDataJob jobGetUnitLinks = new ClientDataJob(token, _typeUnit, _entity);
                return jobGetUnitLinks.Json;
            }
            return "Token undefined";
        }

        [HttpPost]
        public async Task<IActionResult> Post(string afe1, string afe2, string afe3, string type, string token)
        {
            if (token != null && token != "")
            {
                Links inputLink = new Links(afe1, afe2, afe3, type);
                ClientDataJob jobAddUnitLinks = new ClientDataJob(token, Startup.sourceClientData);
                jobAddUnitLinks.AddLink(Startup.sourceClientData, _typeUnit, inputLink);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string token)
        {
            if (token != null && token != "")
            {
                ClientDataJob jobDelete = new ClientDataJob(token, Startup.sourceClientData);
                jobDelete.DeleteLinks(Startup.sourceClientData, _typeUnit);
                return Ok();
            }
            return BadRequest();
        }
    }
}