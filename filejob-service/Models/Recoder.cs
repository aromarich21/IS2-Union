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
                sourceClientData[_index].Result.Elements[indexElement] = sourceClientData[_index].Integration.Elements[0];
                var count = 0;
                foreach (Elements item in sourceClientData[_index].Integration.Elements)
                {
                    if (count > 0)
                    {
                        sourceClientData[_index].Result.Elements.Add(item);
                    }
                    count++;
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
