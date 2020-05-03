using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using filestorage_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy;

namespace filestorage_service.Controllers
{
    [Route("api/filestorage-service/[controller]")]
    [ApiController]
    public class FilesIS2Controller : ControllerBase
    {
        [HttpGet]
        public string Get(string token)
        {
            try
            {
                return Startup.fileStorage.Find((x) => x.Uuid == token).Name;
            }
            catch
            {
                return "null";
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(string token, [FromBody] Stroke stroke)
        {
            if (token != null && token != "")
            {
                try
                {
                    FileStorageJob fsjob = new FileStorageJob(token, Startup.directoryFiles);
                    fsjob.FileStrokeAdd(Startup.fileStorage, stroke.Value);
                    //fsjob.FilePatch(Startup.directoryFiles, stroke.Value);
                    return Ok("Done");
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string token)
        {
            if (token != null && token != "")
            {
                try
                {
                    FileStorageJob fsjob = new FileStorageJob(token);
                    fsjob.FileReWrite(Startup.directoryFiles, Startup.fileStorage);
                    return Ok("Done");
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
                try
                {
                    FileStorageJob fsjob = new FileStorageJob(token);
                    fsjob.FileDelete(Startup.directoryFiles, Startup.fileStorage);
                    try
                    {
                        fsjob.FileStrokeClear(Startup.fileStorage);
                    }
                    catch
                    {

                    }
                    return Ok("Done");
                }
                catch
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}