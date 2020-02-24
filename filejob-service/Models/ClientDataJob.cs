using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filejob_service.Models
{
    public class ClientDataJob
    {
        public List<string> Types { get; set; }
        public List<string> Entity { get; set; }
        public string Json { get; set; }
        public ClientDataJob(string token)
        {
            DefaultFunction();
            Json = GetData(token, "null", "null");
        }
        public ClientDataJob(string token,string type)
        {
            DefaultFunction();
            Json = GetData(token, type, "null");
        }
        public ClientDataJob(string token, string type, string entity)
        {
            DefaultFunction();
            Json = GetData(token, type, entity);
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
            foreach (SourceClientData sourceClientData in Startup.sourceClientData)
            {
                if (sourceClientData.Token == token)
                {
                    if (type == Types[0])
                    {
                        if (entity == Entity[0])
                        {
                            return GetElements(sourceClientData.Cur);
                        }
                        if (entity == Entity[1])
                        {
                            return GetLinks(sourceClientData.Cur);
                        }
                        return GetUnit(sourceClientData.Cur);
                    }
                    if (type == Types[1])
                    {
                        if (entity == Entity[0])
                        {
                            return GetElements(sourceClientData.Int);
                        }
                        if (entity == Entity[1])
                        {
                            return GetLinks(sourceClientData.Int);
                        }
                        return GetUnit(sourceClientData.Int);
                    }
                    if (type == Types[2])
                    {
                        if (entity == Entity[0])
                        {
                            return GetElements(sourceClientData.Res);
                        }
                        if (entity == Entity[1])
                        {
                            return GetLinks(sourceClientData.Res);
                        }
                        return GetUnit(sourceClientData.Res);
                    }
                    return GetClientDataUnits(sourceClientData);
                }            
            }
            return "Not found";
        }

        public string GetElements(SourceUnits sourceUnits)
        {
            var elements = sourceUnits.Elements.AsEnumerable();
            var jsonElements = new JavaScriptSerializer().Serialize(elements);
            return jsonElements;
        }

        public string GetLinks(SourceUnits sourceUnits)
        {
            var links = sourceUnits.Links.AsEnumerable();
            var jsonLinks = new JavaScriptSerializer().Serialize(links);
            return jsonLinks;
        }

        public string GetUnit(SourceUnits sourceUnits)
        {
            var jsonUnit = GetElements(sourceUnits) + GetLinks(sourceUnits);
            return jsonUnit;
        }

        public string GetClientDataUnits(SourceClientData sourceClientData)
        {
            var jsonClientDataUnits = GetUnit(sourceClientData.Cur) + GetUnit(sourceClientData.Int) + GetUnit(sourceClientData.Res);
            return jsonClientDataUnits;
        }
    }
}
