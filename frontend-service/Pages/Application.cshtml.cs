using System;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using frontend_service.Pages;
using frontend_service.Models;
using filejob_service.Models;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace frontend_service
{
    public class ApplicationModel : PageModel
    {
        private IHostingEnvironment _environment;
        private readonly ILogger<ApplicationModel> _logger;
        public FileJobServiceReq filejobPage1;
        [BindProperty]
        public IFormFile Upload { get; set; } //form fileinput current diagramm
        [BindProperty]
        public IFormFile Upload2 { get; set; } // form fileinput integr diagramm
        public bool goodSchemaFile = false; // метка проверки схемы XML
        //static public string url_filejobservice_api = "https://localhost:44337/api/filejob-service/"; //url dev default
        static public string _token; //token
        public string currentType = "cur"; //typeDiagramm
        public string intType = "int"; //typeDiagramm

        ///// body
        public void OnGet() //loaded page
        {
            DefaultFunction();
        }
        public ApplicationModel(IHostingEnvironment environment, ILogger<ApplicationModel> logger)
        {
            _environment = environment;
            _logger = logger;
        }
        public async Task OnPostAsync() //onclick submit
        {
            DefaultFunction();
            try
            {
                UploadToFileJobService();
                if (ErrorModel.ErrorMessage != "Неправильный формат файла.")
                {
                    Response.Redirect("/ApplicationStep2");
                    //Response.Redirect("/QA"); //qa-bridge
                }
            }

            catch
            {
                ErrorModel.ErrorMessage = "Ошибка загрузки";
                Response.Redirect("/Error");
            }
        }
        //////// functions
        public void DefaultFunction() //clear error_message + init host of filejob-service + use tokenjob
        {
            @ErrorModel.ErrorMessage = "";
            JobToken();
            filejobPage1 = new FileJobServiceReq(_token);
        }
        public void JobToken() //job with token
        {
            _token = Request.Cookies["token_"];
            if (_token == null || _token == "null" || _token == "")
            {
                _token = Guid.NewGuid().ToString();  //UUID generate
                //token_ = TokenGenerate(5); //token value generate
                Response.Cookies.Append("token_", _token);   //cookie add [token_]
            }
        }
        private string TokenGenerate(int size) //finc 4 token value generate
        {
            var s_value = "SVMyLXNlcnZpY2U=";
            var def_value_1 = "-_-";
            var def_value_2 = "NHlvdXJ0b2tlbg==";
            var token = s_value + def_value_1;
            for (int i = 0; i < size; i++)
            {
                token += Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            }
            token += def_value_2;
            for (int i = 0; i < 2; i++)
            {
                token += Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            }
            return token;
        }
        public void UploadToFileJobService() // func 4 process send date to filejob-service
        {
            try
            {
                try
                {
                    filejobPage1.ClearElements(currentType);
                    filejobPage1.ClearLinks(currentType);
                    //filejobPage1.Clear
                    ParseFileIs2(Upload, currentType);

                    try
                    {
                        filejobPage1.ClearElements(intType);
                        filejobPage1.ClearLinks(intType);
                        ParseFileIs2(Upload2, intType);
                    }
                    catch
                    {
                        ErrorModel.ErrorMessage = "Ошибка загрузки интегрируемой диаграммы";
                        Response.Redirect("/Error");
                    }
                }
                catch
                {
                    ErrorModel.ErrorMessage = "Ошибка загрузки текущей диаграммы";
                    Response.Redirect("/Error");
                }
            }
            catch
            {
                ErrorModel.ErrorMessage = "Ошибка соединения с сервисом filejob_service";
                Response.Redirect("/Error");
            }
        }
        public void ParseFileIs2(IFormFile form, string typeDiagramm) //parse file .is2 
        {
            try
            {
                XDocument xdoc = XDocument.Load(form.OpenReadStream());
                string nameCookie = "";
                var i = 0;
                var nodeElements = 0;
                var nodeLinks = 0;
                var nodeDkmp = 0;
                foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("actions").Elements("pd"))
                { nodeElements++; }
                if (nodeElements != 0)
                {
                    Elements[] d_elements = new Elements[nodeElements];
                    foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("actions").Elements("pd"))
                    {
                        XAttribute nameAttribute = elements.Attribute("name");
                        XAttribute idAttribute = elements.Attribute("id");
                        XAttribute levelAttribute = elements.Attribute("level");
                        XAttribute numberAttribute = elements.Attribute("number");
                        XAttribute statusAttribute = elements.Attribute("status");
                        XAttribute typeAttribute = elements.Attribute("type");
                        XAttribute formalizationAttribute = elements.Attribute("formalization");

                        if (nameAttribute != null && idAttribute != null)
                        {
                            d_elements[i] = new Elements(nameAttribute.Value, idAttribute.Value, levelAttribute.Value, numberAttribute.Value, statusAttribute.Value, typeAttribute.Value, formalizationAttribute.Value);
                            try
                            {
                                filejobPage1.AddElement(d_elements[i], typeDiagramm);
                            }
                            catch
                            {
                                ErrorModel.ErrorMessage = "Ошибка в POST запросе: {element}.";
                                Response.Redirect("/Error");
                            }
                        }
                    }
                    goodSchemaFile = true;
                }
                else
                    goodSchemaFile = false;

                foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("links").Elements("link"))
                { nodeLinks++; }

                if (nodeLinks != 0)
                {
                    Links[] d_links = new Links[nodeLinks];
                    foreach (XElement elements in xdoc.Element("project").Element("models").Elements("primaryModel").Elements("spd").Elements("links").Elements("link"))
                    {
                        XAttribute afe1Attribute = elements.Attribute("afe1");
                        XAttribute afe2Attribute = elements.Attribute("afe2");
                        XAttribute afe3Attribute = elements.Attribute("afe3");
                        XAttribute typeLinkAttribute = elements.Attribute("type");

                        if (afe1Attribute != null && afe2Attribute != null && afe3Attribute != null && typeLinkAttribute != null)
                        {
                            d_links[i] = new Links(afe1Attribute.Value, afe2Attribute.Value, afe3Attribute.Value, typeLinkAttribute.Value);
                            try
                            {
                                filejobPage1.AddLink(d_links[i], typeDiagramm);
                            }
                            catch
                            {
                                ErrorModel.ErrorMessage = "Ошибка в POST запросе: {link}.";
                                Response.Redirect("/Error");
                            }
                        }
                    }
                    goodSchemaFile = true;
                }
                else
                    goodSchemaFile = false;

                foreach (XElement item in xdoc.Element("project").Element("ModuleParams").Elements("Module").Elements("param"))
                { nodeDkmp++; }

                if (nodeDkmp != 0)
                {
                    //XElement elem = xdoc.Element("project").Element("ModuleParams").Elements("Module").Nodes("param")
                    string d_decStr;
                    //Startup.testvalue.Add(d_decStr);
                    
                    foreach (XElement item in xdoc.Element("project").Element("ModuleParams").Elements("Module").Elements("param"))
                    {
                        XAttribute decStr = item.Attribute("decStr");

                        if (decStr != null)
                        {
                            d_decStr = decStr.Value;
                            //Startup.qaData.DecStrElementsString = d_decStr; //qa-bridge
                            try
                            {                                                             
                                Regex regex = new Regex(@"z(\d*\.?\d*)");
                                MatchCollection matches = regex.Matches(d_decStr);
                                if (matches.Count > 0)
                                {
                                    foreach (Match match in matches)
                                    {
                                        //Startup.qaData.DecStrElements.Add(match.Value); //qa-bridge
                                        filejobPage1.AddDkmp(match.Value, typeDiagramm);
                                    }             
                                }
                                else
                                {
                                    //Startup.qaData.DecStrElements.Add("Совпадений не найдено в " + typeDiagramm + " диаграмме!"); //qa-bridge
                                }                   
                            }
                            catch
                            {
                                ErrorModel.ErrorMessage = "Ошибка в POST запросе: {dkmp}.";
                                Response.Redirect("/Error");
                            }
                        }
                    }
                    goodSchemaFile = true;
                }
                else
                    goodSchemaFile = false;
            }
            catch
            {
                ErrorModel.ErrorMessage = "Неправильный формат файла.";
                Response.Redirect("/Error");
            }
        }
    }
}