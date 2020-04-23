using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filejob_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;

namespace filejob_service.Controllers
{
    [Route("api/filejob-service/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get(string token)
        {
            try
            {
                var jsonElements = new JavaScriptSerializer().Serialize(Startup.sourceClientData[Startup.sourceClientData.FindIndex((x) => x.Token == token)].Result.Elements);
                var data = JsonConvert.DeserializeObject<List<Elements>>(jsonElements);
                string result="";
                foreach (var item in data)
                {
                    //< pd id = "1" name = "Расчеты клиноременной передачи" level = "1" number = "1" status = "1" type = "2" formalization = "2" symbol = "122" mark = "" />
                    result += "<pd ";
                    string id = "id=" + '\u0022' + item.Id + '\u0022';
                    string name = "name=" + '\u0022' + item.Name + '\u0022';                
                    string level = "level=" + '\u0022' + item.Level + '\u0022';
                    string number = "number=" + '\u0022' + item.Number + '\u0022';
                    string status = "status=" + '\u0022' + item.Status + '\u0022';
                    string type = "type=" + '\u0022' + item.Type + '\u0022';
                    string form = "formalization=" + '\u0022' + item.Formalization + '\u0022';
                    string symbol = "symbol=" + '\u0022' + item.Symbol + '\u0022';
                    string mark = "mark=" + '\u0022' + item.Mark + '\u0022';
                    result += id + " " + name + " " + level + " " + number + " " + status + " " + type + " " + form + " " + symbol + " " + mark + " " + "/>";
                }
                return result;     
            }
            catch 
            { 
                return "гавной воняет"; 
            }     
        }
    }
}