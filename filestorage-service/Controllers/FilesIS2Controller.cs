using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using filestorage_service.Models;
using Grpc.Core;
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
        public ActionResult Get(string token)
        {
            try
            {
                if (Directory.Exists(Startup.directoryFiles))
                {
                    string[] files = Directory.GetFiles(Startup.directoryFiles);
                    foreach (string item in files)
                    {
                        var filename = token + ".is2";
                        if (item == (@"Files\" + filename))
                        {
                            Response.ContentType = "APPLICATION/OCTET-STREAM";
                            // Записываем настоящее имя файла.
                            string Header = "Attachment; Filename=" + filename;
                            //Response.AppendHeader("Content-Disposition", Header);
                            Response.AppendTrailer("Content-Disposition", Header);
                            // Указываем путь к файлу.
                            Response.WriteAsync(item);
                            return Ok();
                        }
                    }
                }
                return BadRequest("Not found");
            }
            catch
            {
                return BadRequest("Error");
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