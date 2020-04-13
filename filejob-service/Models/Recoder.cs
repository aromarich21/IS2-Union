using System;
using System.Collections.Generic;

namespace filejob_service.Models
{
    public class Recoder
    {
        public string Token { get; set; }
        public string Url { get; set; }
        public int IndexSourceCurElements { get; set; }
        public int LastIndexCurElement { get; set; }
        public int LastIndexIntElement { get; set; }
        public ClientDataJob Job { get; set; }
        public int _index;

        public Recoder() {}
        public Recoder(string token)
        {
            Token = token;
        }
        public Recoder(string token, List<ClientData> sourceClientData)
        {
            Token = token;
            Job = new ClientDataJob(token, sourceClientData);
            _index = Int32.Parse(Job.IndexClientData);
        }
        public Recoder(string token, List<ClientData> sourceClientData, string id)
        {
            Token = token;
            Job = new ClientDataJob(token, sourceClientData);
            _index = Int32.Parse(Job.IndexClientData);
            Integration(sourceClientData, id);
        }      
        public void RefreshRecoder(List<ClientData> sourceClientData)
        {
            Job.DeleteElements(sourceClientData, Job.Types[2]);
            Job.DeleteLinks(sourceClientData, Job.Types[2]);
        }
        public void FillDiagramm(List<ClientData> sourceClientData) //fill resdiagramm with elements&links of currentdiagramm
        {
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                sourceClientData[_index].Result.Elements.Add(item);
            }
            foreach (Links item in sourceClientData[_index].Current.Links)
            {
                sourceClientData[_index].Result.Links.Add(item);
            }
        }
       public string FindIndexElement(string id, List<ClientData> sourceClientData)
        {
            var count = 0;
            foreach (Elements item in sourceClientData[_index].Result.Elements)
            {
                if (item.Id == id)
                {
                    return count.ToString();
                }
                count++;
            }
            return "null";           
        }
        public void ConnectDiagramm(List<ClientData> sourceClientData, string id)
        {
            var indexElement = Int32.Parse(FindIndexElement(id, sourceClientData));
            
            if (sourceClientData[_index].Result.Elements[indexElement].Level == "1" && sourceClientData[_index].Result.Elements[indexElement].Number == "1")
            {
                Job.DeleteElements(sourceClientData, Job.Types[2]);
                Job.DeleteLinks(sourceClientData, Job.Types[2]);
                sourceClientData[_index].Result.Elements = sourceClientData[_index].Integration.Elements;
                sourceClientData[_index].Result.Links = sourceClientData[_index].Integration.Links;
            }
            else
            {
                List<Links> resLinks = new List<Links>();
                foreach (Links item in sourceClientData[_index].Integration.Links)
                {
                    resLinks.Add(item);
                }
                LastIndexCurElement = 1;
                foreach (Elements item in sourceClientData[_index].Current.Elements)
                {
                    LastIndexCurElement++;
                }
                sourceClientData[_index].Integration.Elements[0].Id = sourceClientData[_index].Result.Elements[indexElement].Id;
                sourceClientData[_index].Integration.Elements[0].Level = sourceClientData[_index].Result.Elements[indexElement].Level;
                sourceClientData[_index].Integration.Elements[0].Number = sourceClientData[_index].Result.Elements[indexElement].Number;
                sourceClientData[_index].Result.Elements[indexElement] = sourceClientData[_index].Integration.Elements[0];
                var count = 0;
                foreach (Elements item in sourceClientData[_index].Integration.Elements)
                {
                    if (count > 0)
                    {   
                        foreach (Links link in resLinks)
                        {
                            if (link.Afe1 == item.Id)
                                link.Afe1 = LastIndexCurElement.ToString();
                            if (link.Afe2 == item.Id)
                                link.Afe2 = LastIndexCurElement.ToString();
                            if (link.Afe3 == item.Id)
                                link.Afe3 = LastIndexCurElement.ToString();
                        }
                        item.Id = LastIndexCurElement.ToString();
                        Job.AddElement(sourceClientData, Job.Types[2], item);
                        LastIndexCurElement++;
                    }
                    count++;
                }
                foreach (Links item in resLinks)
                {
                    if (item.Afe1 == 1.ToString())
                        item.Afe1 = sourceClientData[_index].Result.Elements[indexElement].Id;
                    if (item.Afe2 == 1.ToString())
                        item.Afe2 = sourceClientData[_index].Result.Elements[indexElement].Id;
                    if (item.Afe3 == 1.ToString())
                        item.Afe3 = sourceClientData[_index].Result.Elements[indexElement].Id;
                    Job.AddLink(sourceClientData, Job.Types[2], item);
                }
            }    
        }
        
        public void Integration(List<ClientData> sourceClientData, string id) 
        {
            RefreshRecoder(sourceClientData);
            FillDiagramm(sourceClientData);
            ConnectDiagramm(sourceClientData, id);          
        }
    }
}
