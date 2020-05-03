using filejob_service.Models;
using System.IO;
using System.Net;


namespace frontend_service.Models
{
    public class FileJobServiceReq
    {
        public string Token { get; set; }
        public string Url_fjservice { get; set; }
        public string Url_filestorage { get; set; }
        public int IndexClientData { get; set; }
        public FileJobServiceReq(string token)
        {
            Token = token;
            Url_fjservice = "https://localhost:44337/api/filejob-service/";
            Url_filestorage = "https://localhost:44352/api/filestorage-service/";
            if (Properties.Resources.prod == "true")
                Url_fjservice = Properties.Resources.host_filejob_service + "/api/filejob-service/";
            IndexClientData = AssignClientData();
        }
        public FileJobServiceReq(string token, string url)
        {
            Token = token;
            Url_fjservice = url;
            IndexClientData = AssignClientData();
        }
        public WebResponse AddElement(Elements element, string typeDiagramm) //post element to fj-service
        {
            var controller_name = typeDiagramm + "elements";
            var postedData = "name=" + element.Name + "&" + "id=" + element.Id + "&" + "level=" + element.Level + "&" + "number=" + element.Number + "&" + "status=" + element.Status + "&" + "type=" + element.Type + "&" + "formalization=" + element.Formalization + "&" + "token=" + Token;
            var postUrl = Url_fjservice + controller_name + "?" + postedData;
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
            var postedData = "afe1=" + link.Afe1 + "&" + "afe2=" + link.Afe2 + "&" + "afe3=" + link.Afe3 + "&" + "type=" + link.Type + "&" + "token=" + Token;
            var postUrl = Url_fjservice + controller_name + "?" + postedData;
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
            var delUrl = Url_fjservice + controller_name + "?token=" + Token;
            WebRequest request = WebRequest.Create(delUrl);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        public WebResponse ClearLinks(string typeDiagramm) //delete req all links to fj-service
        {
            var controller_name = typeDiagramm + "links";
            var delUrl = Url_fjservice + controller_name + "?token=" + Token;
            WebRequest request = WebRequest.Create(delUrl);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        public int AssignClientData()
        {
            int indexClientData;
            ClientDataFront _clientData = new ClientDataFront(Token);
            if (Startup.clientData != null)
            {
                var count = 0;
                foreach (ClientDataFront item in Startup.clientData)
                {
                    if (item.clientData.Token == Token)
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
        public string GetClientData(string typeUnit, string entity)
        {
            var controllerName = "clientdata";
            //typeDiagramm + "elements";
            var _params = "type=" + typeUnit + "&" + "entity=" + entity;
            var reqUrl = Url_fjservice + controllerName + "?" + _params + "&token=" + Token;
            WebRequest req = WebRequest.Create(reqUrl);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return Out;
        }
        public WebResponse Integration(string id) //
        {
            var controller_name = "integration";
            var postedData = "id=" + id + "&" + "token=" + Token;
            var postUrl = Url_fjservice + controller_name + "?" + postedData;
            WebRequest reqPOST = WebRequest.Create(postUrl);
            reqPOST.Method = "POST"; // Устанавливаем метод передачи данных в POST
            reqPOST.Timeout = 120000; // Устанавливаем таймаут соединения
            reqPOST.ContentType = "application/x-www-form-urlencoded"; // указываем тип контента
            Stream sendStream = reqPOST.GetRequestStream();
            sendStream.Close();
            WebResponse result = reqPOST.GetResponse();
            return result;
        }

        public WebResponse RefreshIntegrationProcesss() //
        {
            var controller_name = "integration";
            var delUrl = Url_fjservice + controller_name + "?token=" + Token;
            WebRequest request = WebRequest.Create(delUrl);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }

        public WebResponse AddDkmp(string value, string typeDiagramm) //post req link to fj-service
        {
            var controller_name = typeDiagramm + "dkmp";
            var postedData = "value=" + value +"&" + "token=" + Token;
            var postUrl = Url_fjservice + controller_name + "?" + postedData;
            WebRequest reqPOST = WebRequest.Create(postUrl);
            reqPOST.Method = "POST"; // Устанавливаем метод передачи данных в POST
            reqPOST.Timeout = 120000; // Устанавливаем таймаут соединения
            reqPOST.ContentType = "application/x-www-form-urlencoded"; // указываем тип контента
            Stream sendStream = reqPOST.GetRequestStream();
            sendStream.Close();
            WebResponse result = reqPOST.GetResponse();
            return result;
        }

        public string GetFile()
        {
            var controllerName = "download";
            //typeDiagramm + "elements";
            var reqUrl = Url_filestorage + controllerName + "?" + "token=" + Token;
            WebRequest req = WebRequest.Create(reqUrl);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            return "Ok";
        }
    }
}
