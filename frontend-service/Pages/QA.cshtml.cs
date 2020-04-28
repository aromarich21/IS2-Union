using filejob_service.Models;
using frontend_service.Models;
using frontend_service.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace frontend_service
{
    public class QAModel : PageModel
    {
        static public string _token;
        private readonly ILogger<QAModel> _logger;
        FileJobServiceReq QAfilejob;

        public void OnGet()
        {
            DefaultFunction();
        }

        public string TestGetRequestResDecstr()
        {
            ClientDataJob clientDataJob = new ClientDataJob();
            try
            {
                List<string> sourceDcmp = new List<string>();
                string res = "";
                var result = JsonConvert.DeserializeObject<List<string>>(QAfilejob.GetClientData(clientDataJob.Types[2], clientDataJob.Entity[2]));
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
                return QAfilejob.GetClientData(clientDataJob.Types[0], clientDataJob.Entity[2]);
            }
        }

        public string TestGetRequestIntDecstr()
        {
            ClientDataJob clientDataJob = new ClientDataJob();
            try
            {
                List<string> sourceDcmp = new List<string>();
                string res = "";
                var result = JsonConvert.DeserializeObject<List<string>>(QAfilejob.GetClientData(clientDataJob.Types[1], clientDataJob.Entity[2]));
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
                return QAfilejob.GetClientData(clientDataJob.Types[0], clientDataJob.Entity[2]);
            }
        }

        public string TestGetRequestCurrentDecstr()
        {
            ClientDataJob clientDataJob = new ClientDataJob();
            try
            {
                List<string> sourceDcmp = new List<string>();
                string res = "";
                var result = JsonConvert.DeserializeObject<List<string>>(QAfilejob.GetClientData(clientDataJob.Types[0], clientDataJob.Entity[2]));
                foreach(string item in result)
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
                return QAfilejob.GetClientData(clientDataJob.Types[0], clientDataJob.Entity[2]);
            }  
        }

        public QAModel(ILogger<QAModel> logger)
        {
            _logger = logger;
        }

        public void DefaultFunction()
        {
            @ErrorModel.ErrorMessage = "";
            JobToken();
            QAfilejob = new FileJobServiceReq(_token);
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
        
        public string TestDecstrElementsToString()
        { 
            var resultTest1 = "";
            try
            {
                foreach (string item in Startup.qaData.DecStrElements)
                {
                    resultTest1 += item;
                }
                return resultTest1;
            }
            catch { resultTest1 = "error or empty"; return resultTest1; }

        }
        public string TestDecstrElementsString()
        {
            var resultTest2 = "";
            try
            {
                resultTest2 = Startup.qaData.DecStrElementsString;
                if (resultTest2 == "")
                    resultTest2 = "Ничего не найдено!";
                return resultTest2;
            }
            catch { resultTest2 = "error or empty"; return resultTest2; }
        }
    }
}