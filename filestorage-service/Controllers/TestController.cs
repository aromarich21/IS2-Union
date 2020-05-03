using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using filejob_service.Models;
using filestorage_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace filestorage_service.Controllers
{
    [Route("api/filestorage-service/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get(string token)
        {
            var result = "";
            try
            {
                if (Directory.Exists(Startup.directoryFiles))
                {
                    string[] files = Directory.GetFiles(Startup.directoryFiles);
                    foreach (string item in files)
                    {
                        result += item + ";" + @"Files\";
                    }
                }
                return result;
            }
            catch
            {
                return "error";
            }
            /*
            [HttpGet]
            public string Get(string token)
            {
                var result = "";
                try
                {
                    foreach (string item in Startup.fileStorage.Find((x) => x.Uuid == token).Strokes)
                    {
                        result += item;
                    }
                    if (result != "")
                        return result;
                    else
                        return null;
                }
                catch
                {
                    return "error";
                }
            }*/
            /*
            [HttpPost]
            public async Task<IActionResult> Post(List<string> list)
            {
                try
                {
                    //await Json new = value;
                    Startup.Test.Add(list[0]);
                    return Ok("Done");
                }
                catch
                {
                    return BadRequest();
                }
            }*/

            /*
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Elements element)
        {
            try
            {
                Startup.Test.Add(element.Name);              
                return Ok("Done");
            }
            catch
            {
                return BadRequest();
            }
        }*/
        }
    }
}