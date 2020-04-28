using System.Collections.Generic;
using filejob_service.Models;
using frontend_service.Models;
using frontend_service.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace frontend_service
{
    public class ApplicationResultModel : PageModel
    {
        static public string _token;
        public FileJobServiceReq filejobPage3;
        private readonly ILogger<ApplicationStep2Model> _logger;
        public void OnGet()
        {
            DefaultFunction();
            ShowResult();
        }

        public ApplicationResultModel(ILogger<ApplicationStep2Model> logger)
        {
            _logger = logger;
        }

        public void DefaultFunction()
        {
            @ErrorModel.ErrorMessage = "";
            JobToken();
            filejobPage3 = new FileJobServiceReq(_token);
        }
        public void JobToken()
        {
            _token = Request.Cookies["token_"];
            if (_token == null || _token == "null" || _token == "")
            {
                ErrorModel.ErrorMessage = "403. token undefined";
                Response.Redirect("/Error");
            }
        }

        public string GetRequestResDecstr()
        {
            ClientDataJob clientDataJob = new ClientDataJob();
            try
            {
                List<string> sourceDcmp = new List<string>();
                string res = "";
                var result = JsonConvert.DeserializeObject<List<string>>(filejobPage3.GetClientData(clientDataJob.Types[2], clientDataJob.Entity[2]));
                foreach (string item in result)
                {
                    sourceDcmp.Add(item);
                    res += item + "; ";
                }
                if (res == "" || res == null)
                {
                    res = "null";
                }
                return res;
            }
            catch
            {
                return filejobPage3.GetClientData(clientDataJob.Types[0], clientDataJob.Entity[2]);
            }
        }


        public void ShowResult()
        {
            try
            {
                ClientDataJob clientDataJob = new ClientDataJob();
                var data1 = JsonConvert.DeserializeObject<List<Elements>>(filejobPage3.GetClientData(clientDataJob.Types[2], clientDataJob.Entity[0]));
                var data2 = JsonConvert.DeserializeObject<List<Links>>(filejobPage3.GetClientData(clientDataJob.Types[2], clientDataJob.Entity[1]));
                var data3 = JsonConvert.DeserializeObject<List<string>>(filejobPage3.GetClientData(clientDataJob.Types[2], clientDataJob.Entity[2]));
                try
                {
                    Startup.clientData[filejobPage3.IndexClientData].clientData.Result.Elements = data1;
                    Startup.clientData[filejobPage3.IndexClientData].clientData.Result.Links = data2;
                    Startup.clientData[filejobPage3.IndexClientData].clientData.Result.DcmpElements = data3;
                    Startup.clientData[filejobPage3.IndexClientData].ResultElements.Clear();
                    Startup.clientData[filejobPage3.IndexClientData].ResultLinks.Clear();

                    foreach (var item in data1)
                    {
                        var result = "<pd ";
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
                        Startup.clientData[filejobPage3.IndexClientData].ResultElements.Add(result);
                    }         
                    foreach (var item in data2)
                    {
                        var result = "<link ";
                        string afe1 = "afe1=" + '\u0022' + item.Afe1 + '\u0022';
                        string afe2 = "afe2=" + '\u0022' + item.Afe2 + '\u0022';
                        string afe3 = "afe3=" + '\u0022' + item.Afe3 + '\u0022';
                        string type = "type=" + '\u0022' + item.Type + '\u0022';
                        result += afe1 + " " + afe2 + " " + afe3 + " " + type + "/>";
                        Startup.clientData[filejobPage3.IndexClientData].ResultLinks.Add(result);
                    }
                    var result3 = "<param decStr=" + '\u0022';
                    foreach (var item in data3)
                    {             
                        result3 += item + ";";
                    }
                    result3 += '\u0022' + "/>";
                    Startup.clientData[filejobPage3.IndexClientData].ResultDcmp = result3;
                }
                catch
                {
                    ErrorModel.ErrorMessage = "Не удалось отобразить элементы";
                    Response.Redirect("/Error");
                }
            }
            catch
            {
                ErrorModel.ErrorMessage = "Не удалось получить элементы из сервиса filejob_service";
                Response.Redirect("/Error");
            }
        }
    }
}