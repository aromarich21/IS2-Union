using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace filejob_service.Models
{
    public class ClientDataJob
    {
        public List<string> Types { get; set; }
        public List<string> Entity { get; set; }
        public string IndexClientData { get; set; }
        public string Json { get; set; }
        public readonly string _token;
     
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
            IndexClientData = FindIndexClientData(sourceClientData);
        }
        public ClientDataJob(string token,string typeUnit)
        {
            _token = token;
            DefaultFunction();
            Json = GetData(_token, typeUnit, "null");
        }
        public ClientDataJob(string token, string typeUnit, string entity)
        {
            _token = token;
            DefaultFunction();
            Json = GetData(_token, typeUnit, entity);
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
            foreach (ClientData clientData in Startup.sourceClientData)
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
        public string FindIndexClientData(List<ClientData> sourceClientData)
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
            return "null";
        }
        public void AddElementWithParametrs(List<ClientData> sourceClientData, string typeUnit, string name, string id, string level, string number, string status, string type, string formalization)
        {
            Elements inputElement = new Elements(name, id, level, number, status, type, formalization);
            if (IndexClientData != "null" && IndexClientData!=null)
            {
                if (typeUnit == Types[0])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Current.Elements.Add(inputElement);
                }
                if (typeUnit == Types[1])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Integration.Elements.Add(inputElement);
                }
                if (typeUnit == Types[2])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Result.Elements.Add(inputElement);
                }
            }
           else
            {
                ClientData _clientData = new ClientData(_token);
                if (typeUnit == Types[0])
                {
                    _clientData.Current.Elements.Add(inputElement);
                }
                if (typeUnit == Types[1])
                {
                    _clientData.Integration.Elements.Add(inputElement);
                }
                if (typeUnit == Types[2])
                {
                    _clientData.Result.Elements.Add(inputElement);
                }
                sourceClientData.Add(_clientData);
                IndexClientData = (sourceClientData.Count - 1).ToString();
            }
        }
        public void AddElement(List<ClientData> sourceClientData, string typeUnit, Elements element)
        {
            if (IndexClientData != "null" && IndexClientData != null)
            {
                if (typeUnit == Types[0])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Current.Elements.Add(element);
                }
                if (typeUnit == Types[1])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Integration.Elements.Add(element);
                }
                if (typeUnit == Types[2])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Result.Elements.Add(element);
                }
            }
            else
            {
                ClientData _clientData = new ClientData(_token);
                if (typeUnit == Types[0])
                {
                    _clientData.Current.Elements.Add(element);
                }
                if (typeUnit == Types[1])
                {
                    _clientData.Integration.Elements.Add(element);
                }
                if (typeUnit == Types[2])
                {
                    _clientData.Result.Elements.Add(element);
                }
                sourceClientData.Add(_clientData);
                IndexClientData = (sourceClientData.Count - 1).ToString();
            }
        }
        public void AddLinkWithParametrs(List<ClientData> sourceClientData, string typeUnit, string afe1, string afe2, string afe3, string type)
        {
            Links inputLink = new Links(afe1, afe2, afe3, type);
            if (IndexClientData != "null" && IndexClientData != null)
            {
                if (typeUnit == Types[0])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Current.Links.Add(inputLink);
                }
                if (typeUnit == Types[1])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Integration.Links.Add(inputLink);
                }
                if (typeUnit == Types[2])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Result.Links.Add(inputLink);
                }
            }
            else
            {
                ClientData _clientData = new ClientData(_token);
                if (typeUnit == Types[0])
                {
                    _clientData.Current.Links.Add(inputLink);
                }
                if (typeUnit == Types[1])
                {
                    _clientData.Integration.Links.Add(inputLink);
                }
                if (typeUnit == Types[2])
                {
                    _clientData.Result.Links.Add(inputLink);
                }
                sourceClientData.Add(_clientData);
                IndexClientData = (sourceClientData.Count - 1).ToString();
            }
        }
        public void AddLink(List<ClientData> sourceClientData, string typeUnit, Links link)
        {
            if (IndexClientData != "null" && IndexClientData != null)
            {
                if (typeUnit == Types[0])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Current.Links.Add(link);
                }
                if (typeUnit == Types[1])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Integration.Links.Add(link);
                }
                if (typeUnit == Types[2])
                {
                    sourceClientData[Int32.Parse(IndexClientData)].Result.Links.Add(link);
                }
            }
            else
            {
                ClientData _clientData = new ClientData(_token);
                if (typeUnit == Types[0])
                {
                    _clientData.Current.Links.Add(link);
                }
                if (typeUnit == Types[1])
                {
                    _clientData.Integration.Links.Add(link);
                }
                if (typeUnit == Types[2])
                {
                    _clientData.Result.Links.Add(link);
                }
                sourceClientData.Add(_clientData);
                IndexClientData = (sourceClientData.Count - 1).ToString();
            }
        }
    }
}
