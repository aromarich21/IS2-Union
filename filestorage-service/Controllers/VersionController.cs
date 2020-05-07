using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace filestorage_service.Controllers
{
    [Route("api/filestorage-service/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Name: " + Startup.nameService + ";" + " Version: " + Startup.version + ";" + " Uptime: " + Startup._uptime;
        }
    }
}