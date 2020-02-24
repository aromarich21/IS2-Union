using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using filejob_service.Models;
using System.Net;
using Newtonsoft.Json;
using frontend_service.Pages;
using RestSharp;
using Microsoft.Ajax.Utilities;
using System;
using Amazon.CloudFront.Model;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Amazon.SimpleEmail.Model;

namespace frontend_service
{
    public class ApplicationModel : PageModel
    {
        private IHostingEnvironment _environment;
        [BindProperty]
        public IFormFile Upload { get; set; } //form fileinput current diagramm
        [BindProperty]
        public IFormFile Upload2 { get; set; } // form fileinput integr diagramm
        public string auth_; //авторизация
        public int settingsTimeSecondsCookies = 1;  ///  cookies_config. время куки
        public bool goodSchemaFile = false; // метка проверки схемы XML
        static public string url_filejobservice_api = "https://localhost:44337/api/filejob-service/"; //url dev default
        static public string token_; //token
        public string currentType = "cur"; //typeDiagramm
        public string intType = "int"; //typeDiagramm

        ///// body
        public void OnGet() //loaded page
        {
            DefaultFunction();
        }
        public ApplicationModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public async Task OnPostAsync() //onclick submit
        {
            try
            {
                UploadToFileJobService();
                if (ErrorModel.ErrorMessage != "Неправильный формат файла.")
                    Response.Redirect("/ApplicationStep2");
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
            if (Properties.Resources.prod == "true")
                url_filejobservice_api = Properties.Resources.host_filejob_service + "/api/filejob-service/";
            JobToken();
        }
        public void JobToken() //job with token
        {
            token_ = Request.Cookies["token_"];
            if (token_ == null || token_ == "null" || token_ == "")
            {
                token_ = Guid.NewGuid().ToString();  //UUID generate
                //token_ = TokenGenerate(5); //token value generate
                Response.Cookies.Append("token_", token_);   //cookie add [token_]
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
                    ClearElements(currentType);
                    ClearLinks(currentType);
                    ParseFileIs2(Upload, currentType);

                    try
                    {
                        ClearElements(intType);
                        ClearLinks(intType);
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
                                AddElement(d_elements[i], typeDiagramm);
                            }
                            catch
                            {
                                ErrorModel.ErrorMessage = "Ошибка в POST запросе.";
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
                                AddLink(d_links[i], typeDiagramm);
                            }
                            catch
                            {
                                ErrorModel.ErrorMessage = "Ошибка в POST запросе.";
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
        public WebResponse AddElement(Elements element, string typeDiagramm) //post element to fj-service
        {
            var controller_name = typeDiagramm + "elements";
            var postedData = "name=" + element.Name + "&" + "id=" + element.Id + "&" + "level=" + element.Level + "&" + "number=" + element.Number + "&" + "status=" + element.Status + "&" + "type=" + element.Type + "&" + "formalization=" + element.Formalization + "&" + "token=" + token_;
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
        public WebResponse AddLink(Links link, string typeDiagramm) //post req link to fj-service
        {
            var controller_name = typeDiagramm + "links";
            var postedData = "afe1=" + link.Afe1 + "&" + "afe2=" + link.Afe2 + "&" + "afe3=" + link.Afe3 + "&" + "type=" + link.Type + "&" + "token=" + token_;
            var postUrl = url_filejobservice_api + controller_name + "/?" + postedData;
            WebRequest reqPOST = System.Net.WebRequest.Create(postUrl);
            reqPOST.Method = "POST"; // Устанавливаем метод передачи данных в POST
            reqPOST.Timeout = 120000; // Устанавливаем таймаут соединения
            reqPOST.ContentType = "application/x-www-form-urlencoded"; // указываем тип контента
            Stream sendStream = reqPOST.GetRequestStream();
            sendStream.Close();
            WebResponse result = reqPOST.GetResponse();
            return result;
        }
        public WebResponse ClearElements(string typeDiagramm) //delete req all elements to fj-service
        {
            var controller_name = typeDiagramm + "elements";
            var delUrl = url_filejobservice_api + controller_name + "?token=" + token_;
            WebRequest request = WebRequest.Create(delUrl);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        public WebResponse ClearLinks(string typeDiagramm) //delete req all links to fj-service
        {
            var controller_name = typeDiagramm + "links";
            var delUrl = url_filejobservice_api + controller_name + "?token=" + token_;
            WebRequest request = WebRequest.Create(delUrl);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }

    }
} 