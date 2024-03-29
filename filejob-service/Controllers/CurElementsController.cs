﻿using filejob_service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
                ClientDataJob jobGetUnitElements = new ClientDataJob(token, _typeUnit, _entity);
                return jobGetUnitElements.Json;
            }
            return "Token undefined";
        }  

        [HttpPost]
        public async Task<IActionResult> Post(string name, string id, string level, string number, string status, string type, string formalization, string token)
        {
            if (token != null && token != "")
            {
                Elements inputElement = new Elements(name, id, level, number, status, type, formalization);
                ClientDataJob jobAddUnitElements = new ClientDataJob(token, Startup.sourceClientData);
                jobAddUnitElements.AddElement(Startup.sourceClientData, _typeUnit, inputElement);
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
                jobDelete.DeleteElements(Startup.sourceClientData, _typeUnit);
                return Ok();
            }
            return BadRequest();
        }
    }
}

   