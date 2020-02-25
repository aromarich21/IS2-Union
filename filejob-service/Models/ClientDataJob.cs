using Nancy.Json;
using System.Collections.Generic;
using System.Linq;

namespace filejob_service.Models
{
    public class ClientDataJob
    {
        public List<string> Types { get; set; }
        public List<string> Entity { get; set; }
        public string Json { get; set; }
        public readonly string _token;
        public readonly string _index;
        public ClientDataJob(string token)
        {
            _token = token;
            DefaultFunction();
            Json = GetData(_token, "null", "null");
            
        }
        public ClientDataJob(string token, List<ClientData> sourceClientData)
        {
            _token = token;
            DefaultFunction();
            Json = GetData(_token, "null", "null");
            _index = FindIndexSourceClientData(sourceClientData);
        }
        public ClientDataJob(string token,string type)
        {
            _token = token;
            DefaultFunction();
            Json = GetData(_token, type, "null");
        }
        public ClientDataJob(string token, string type, string entity)
        {
            _token = token;
            DefaultFunction();
            Json = GetData(_token, type, entity);
        }

        public void DefaultFunction()
        {
            Types = new List<string>();
            Entity = new List<string>();
            Types.Add("cur"); //0
            Types.Add("int"); //1
            Types.Add("res"); //2
            Entity.Add("elements"); //0
            Entity.Add("links"); //1
        }
        public string GetData(string token, string type, string entity)
        {
            foreach (ClientData clientData in Startup.clientData)
            {
                if (clientData.Token == token)
                {
                    if (type == Types[0])
                    {
                        if (entity == Entity[0])
                        {
                            return GetElements(clientData.Current);
                        }
                        if (entity == Entity[1])
                        {
                            return GetLinks(clientData.Current);
                        }
                        return GetUnit(clientData.Current);
                    }
                    if (type == Types[1])
                    {
                        if (entity == Entity[0])
                        {
                            return GetElements(clientData.Integration);
                        }
                        if (entity == Entity[1])
                        {
                            return GetLinks(clientData.Integration);
                        }
                        return GetUnit(clientData.Integration);
                    }
                    if (type == Types[2])
                    {
                        if (entity == Entity[0])
                        {
                            return GetElements(clientData.Result);
                        }
                        if (entity == Entity[1])
                        {
                            return GetLinks(clientData.Result);
                        }
                        return GetUnit(clientData.Result);
                    }
                    return GetClientDataUnits(clientData);
                }            
            }
            return "Not found";
        }

        public string GetElements(Units unit)
        {
            var elements = unit.Elements.AsEnumerable();
            var jsonElements = new JavaScriptSerializer().Serialize(elements);
            return jsonElements;
        }

        public string GetLinks(Units unit)
        {
            var links = unit.Links.AsEnumerable();
            var jsonLinks = new JavaScriptSerializer().Serialize(links);
            return jsonLinks;
        }

        public string GetUnit(Units unit)
        {
            var jsonUnit = GetElements(unit) + GetLinks(unit);
            return jsonUnit;
        }

        public string GetClientDataUnits(ClientData clientData)
        {
            var jsonClientDataUnits = GetUnit(clientData.Current) + GetUnit(clientData.Integration) + GetUnit(clientData.Result);
            return jsonClientDataUnits;
        }

        public string FindIndexSourceClientData(List<ClientData> sourceClientData)
        {
            var count = 0;
            foreach (ClientData item in sourceClientData)
            {
                if (_token == item.Token)
                {
                    return count.ToString();
                }
                count++;
            }
            return "Not found";
        }
    }
}
