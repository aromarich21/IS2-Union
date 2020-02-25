using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using filejob_service.Models;
using frontend_service.Models;
using frontend_service.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace frontend_service
{
    public class ApplicationStep2Model : PageModel
    {
        
        static public string url_filejobservice_api = "https://localhost:44337/api/filejob-service/"; //url dev default
        static public string token_;     
        public string UserChoice { get; set; } // метка выбора итема dropdownа пользователем
        public string SelectId { get; set; }
        public int index;


        public void OnGet()
        {
            DefaultFunction();       
            ShowElements();
        }

        public async Task OnPostAsync()
        {
            UserChoice = Request.Form["elementsId"];
            ShowElements();
            //RefreshIntegrationProcesss();
            Integration(UserChoice);
        }
        int AssignClientData()
        {
            int indexClientData;
            Models.ClientData _clientData = new Models.ClientData(token_);
            if (Startup.clientData != null)
            {
                var count = 0;
                foreach (Models.ClientData item in Startup.clientData)
                {
                    if (item.Token == token_)
                    {
                        indexClientData = count;
                        return indexClientData;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            Startup.clientData.Add(_clientData);
            indexClientData = Startup.clientData.Count - 1;
            return indexClientData;
        }
        public void WorkToken()
        {
            token_ = Request.Cookies["token_"];
            if (token_ == null || token_ == "null" || token_ == "")
            {
                ErrorModel.ErrorMessage = "403. token undefined";
                Response.Redirect("/Error");
            }
        }

        public void DefaultFunction()
        {
            @ErrorModel.ErrorMessage = "";
            if (Properties.Resources.prod == "true")
                url_filejobservice_api = Properties.Resources.host_filejob_service + "/api/filejob-service/";
            WorkToken();
            index = AssignClientData();
        }

        public void ShowElements()
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<Elements>>(Getelements(url_filejobservice_api, "cur"));
                
                try
                {
                    Startup.clientData[index].SourceCurElements = data;
                    Startup.clientData[index].Options = new SelectList(Startup.clientData[index].SourceCurElements, nameof(Elements.Id), nameof(Elements.Name));
                }
                catch
                {
                    ErrorModel.ErrorMessage = "Не удалось отобразить элементы из сервиса filejob_service";
                    Response.Redirect("/Error");
                }
            }
            catch
            {
                ErrorModel.ErrorMessage = "Не удалось отобразить элементы из сервиса filejob_service";
                Response.Redirect("/Error");
            } 
        }

        private static string Getelements(string url_api, string typeDiagramm)
        {
            var controller_name = typeDiagramm + "elements";
            var reqUrl = url_api + controller_name + "?token=" + token_;
            WebRequest req = WebRequest.Create(reqUrl);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }
        public WebResponse Integration(string id) //post element to fj-service
        {
            var controller_name = "integration";
            var postedData = "id=" + id + "&" + "token=" + token_;
            var postUrl = url_filejobservice_api + controller_name + "/?" + postedData;
            WebRequest reqPOST = WebRequest.Create(postUrl);
            reqPOST.Method = "POST"; // Устанавливаем метод передачи данных в POST
            reqPOST.Timeout = 120000; // Устанавливаем таймаут соединения
            reqPOST.ContentType = "application/x-www-form-urlencoded"; // указываем тип контента
            Stream sendStream = reqPOST.GetRequestStream();
            sendStream.Close();
            WebResponse result = reqPOST.GetResponse();
            return result;
        }
        public WebResponse RefreshIntegrationProcesss() //delete req all elements to fj-service
        {
            var controller_name = "integration";
            var delUrl = url_filejobservice_api + controller_name + "?token=" + token_;
            WebRequest request = WebRequest.Create(delUrl);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }

    }
}