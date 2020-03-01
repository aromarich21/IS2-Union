using System.Collections.Generic;
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
        static public string _token;
        public FileJobServiceReq filejobPage2;
        public string UserChoice { get; set; } // метка выбора итема dropdownа пользователем
        public string SelectId { get; set; }

        public void OnGet()
        {
            DefaultFunction();       
            ShowElements();
        }
        public void DefaultFunction()
        {
            @ErrorModel.ErrorMessage = "";
            JobToken();
            filejobPage2 = new FileJobServiceReq(_token);          
        }
        public async Task OnPostAsync()
        {
            UserChoice = Request.Form["elementsId"];
            DefaultFunction();
            filejobPage2.Integration(UserChoice);
            //RefreshIntegrationProcesss();

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
        public void ShowElements()
        {
            try
            {
                ClientDataJob clientDataJob = new ClientDataJob();
               var data = JsonConvert.DeserializeObject<List<Elements>>(filejobPage2.GetElements(clientDataJob.Types[0], clientDataJob.Entity[0]));
                try
                {
                    Startup.clientData[filejobPage2.IndexClientData].clientData.Current.Elements = data;
                    Startup.clientData[filejobPage2.IndexClientData].Options = new SelectList(Startup.clientData[filejobPage2.IndexClientData].clientData.Current.Elements, nameof(Elements.Id), nameof(Elements.Name));
                }
                catch
                {
                    ErrorModel.ErrorMessage = "Не удалось отобразить элементы";
                    Response.Redirect("/Error");
                }
            }
            catch
            {
                ErrorModel.ErrorMessage = "Не удалось отобразить элементы из сервиса filejob_service";
                Response.Redirect("/Error");
            } 
        }
    }
}