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

        public Recoder() {}
        public Recoder(string token)
        {
            Token = token;         
        }
        public Recoder(string token, string id, List<SourceElements> sourceCurElements, List<SourceLinks> sourceCurLinks,
            List<SourceElements> sourceIntElements, List<SourceLinks> sourceIntLinks, List<SourceElements> sourceResElements, List<SourceLinks> sourceResLinks)
        {
            Token = token;
            Integration(id, sourceCurElements, sourceCurLinks, sourceIntElements, sourceIntLinks, sourceResElements, sourceResLinks);
        }
        
        public void AddElement(Elements inputElement,List<SourceElements> sourceElements)
        {
            var checkToken = false;
            if (Token != null && Token != "")
            {
                foreach (SourceElements item in sourceElements)
                {
                    if (item.Token == Token)
                    {
                        item.elements.Add(inputElement);
                        checkToken = true;
                    }
                }
                if (checkToken == false)
                {
                    SourceElements s_elements = new SourceElements();
                    s_elements.elements.Add(inputElement);
                    s_elements.Token = Token;
                    sourceElements.Add(s_elements);
                }
            }
        }
        public void AddLink(Links inputLink,List<SourceLinks> sourceLinks)
        {
            var checkToken = false;
            if (Token != null && Token != "")
            {
                foreach (SourceLinks item in sourceLinks)
                {
                    if (item.Token == Token)
                    {
                        item.links.Add(inputLink);
                        checkToken = true;
                    }
                }
                if (checkToken == false)
                {
                    SourceLinks s_links = new SourceLinks();
                    s_links.links.Add(inputLink);
                    s_links.Token = Token;
                    sourceLinks.Add(s_links);
                }
            }
        }
        public void DeleteElements(List<SourceElements> sourceElements)
        {
            if (Token != null && Token != "")
            {
                foreach (SourceElements item in sourceElements)
                {
                    if (item.Token == Token)
                    {
                        item.elements.Clear();
                    }
                }
            }
        }
        public void DeleteLinks(List<SourceLinks> sourceLinks)
        {
            if (Token != null && Token != "")
            {
                foreach (SourceLinks item in sourceLinks)
                {
                    if (item.Token == Token)
                    {
                        item.links.Clear();
                    }
                }
            }
        }
        public string FindIndexElement(string id, List<SourceElements> sourceElementsTo)
        {
            foreach (SourceElements item in sourceElementsTo)
            {
                if (item.Token == Token)
                {
                    var count = 0;
                    foreach (Elements element in item.elements)
                    {
                        if (element.Id == id)
                        {
                            return count.ToString();
                        }
                        count++;
                    }
                }             
            }
            return "null";
        }
        public void RefreshRecoder(List<SourceElements> sourceElementsTo, List<SourceLinks> sourceLinksTo)
        {
            DeleteElements(sourceElementsTo);
            DeleteLinks(sourceLinksTo);
        }
        public void FillDiagramm(List<SourceElements> sourceElementsFrom, List<SourceLinks> sourceLinksFrom, List<SourceElements> sourceElementsTo, List<SourceLinks> sourceLinksTo) //fill resdiagramm with elements&links of currentdiagramm
        {
            foreach (SourceElements item in sourceElementsFrom)
            {
                if (item.Token == Token)
                {
                    foreach (Elements element in item.elements)
                    {
                        AddElement(element, sourceElementsTo);
                    }
                }
            }

            foreach (SourceLinks item in sourceLinksFrom)
            {
                if (item.Token == Token)
                {
                    foreach (Links link in item.links)
                    {
                        AddLink(link, sourceLinksTo);
                    }
                }
            }
        }
        public void ConnectSources(string index, List<SourceElements> sourceIntElements, List<SourceElements> sourceResElements)
        {
            var count = 0;
            foreach (SourceElements item in sourceResElements)
            {
                if (item.Token == Token)
                {
                    foreach (Elements element in item.elements)
                    {
                        AddElement(element, sourceIntElements);
                    }
                }
            }
        }
        public void Integration(string id, List<SourceElements> sourceCurElements, List<SourceLinks> sourceCurLinks, List<SourceElements> sourceIntElements, List<SourceLinks> sourceIntLinks, List<SourceElements> sourceResElements, List<SourceLinks> sourceResLinks) 
        {
            RefreshRecoder(sourceResElements, sourceResLinks);
            FillDiagramm(sourceCurElements, sourceCurLinks, sourceResElements, sourceResLinks);
            var indexElement = FindIndexElement(id, sourceResElements);
        }
    }
}
