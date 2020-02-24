
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
        /*
        public List<Elements> ResElements { get; set; }
        public List<Links> ResLinks { get; set; }
        public List<Elements> IntElements { get; set; }
        public List<Links> IntLinks { get; set; }
        public string CountResElements { get; set; }
        public string CountResLinks { get; set; }
        public string CountIntElements { get; set; }
        public string CountIntLinks { get; set; }
        public string LevelChoiceElement { get; set; }
        public string NumberChoiceElement { get; set; }
        public static Elements choiceElement { get; set; }
        public static Elements XElement { get; set; }
        public string Type { get; set; }

        public Recoder(List<Elements> curElements, List<Elements> intElements,
                          List<Links> curLinks, List<Links> intLinks,
                          string curCountElements, string intCountElements,
                          string curCountLinks, string intCountLinks,
                          string levelElement, string numberElement)
        {
            Type = "emptyElement";
            ResElements = new List<Elements>();
            ResLinks = new List<Links>();
            ResElements = curElements;
            ResLinks = curLinks;
            IntElements = intElements;
            IntLinks = intLinks;
            CountResElements = curCountElements;
            CountIntElements = intCountElements;
            CountResLinks = curCountLinks;
            CountIntLinks = intCountLinks;
            LevelChoiceElement = levelElement;
            NumberChoiceElement = numberElement;
            var index = SearchIndexElement(LevelChoiceElement, NumberChoiceElement, ResElements);
            SearchElementWithIndexChoiceElement(index);
            ConnectInEmptyElement(index);
            RecodingElements(Int32.Parse(index));
            //ConsoleLog(choiceElement.Name);
        }
        public Recoder(List<Elements> curElements, List<Elements> intElements,
                          List<Links> curLinks, List<Links> intLinks,
                          string curCountElements, string intCountElements,
                          string curCountLinks, string intCountLinks)
        {
            Type = "newElement";
            ResElements = new List<Elements>();
            ResLinks = new List<Links>();
            ResElements = curElements;
            ResLinks = curLinks;
            IntElements = intElements;
            IntLinks = intLinks;
            CountResElements = curCountElements;
            CountIntElements = intCountElements;
            CountResLinks = curCountLinks;
            CountIntLinks = intCountLinks;
        }
        public Recoder()
        {

        }
        public string SearchIndexElement(string level, string number, List<Elements> elementsForSearch)
        {
            var count = 0;
            string index = "";
            foreach (Elements elements in elementsForSearch)
            {
                if (elements.Level == level && elements.Number == number)
                {
                    index = count.ToString();
                    break;
                }
                count++;
            }
            return index;
        }
        public void SearchElementWithIndexChoiceElement(string index)
        {
            choiceElement = new Elements();
            var i = Int32.Parse(index);
            choiceElement = ResElements[i];
        }
        public Elements SearchElementWithIndex(int index)
        {
            return ResElements[index];
        }
        public string SearchNumberLastElement(string id, List<Links> linksCurrent)
        {
            foreach (Links elements in linksCurrent)
            {
                if (elements.Afe1 == id && elements.Type == "3" && elements.Afe3 == "0")
                {
                    return elements.Afe2;
                }
            }
            return null;
        }
        public void ConnectInEmptyElement(string index)
        {
            //var countResultDiagramm = Int32.Parse(CountIntElements) + Int32.Parse(CountResElements) - 1;
            var count = Int32.Parse(CountResElements);
            var indexChange = Int32.Parse(index);
            var iteration = 1;
            foreach (Elements elements in IntElements)
            {
                if (iteration == 1)
                {
                    ResElements[indexChange] = elements;
                    ResElements[indexChange].Level = choiceElement.Level;
                    ResElements[indexChange].Number = choiceElement.Number;
                    XElement = ResElements[indexChange];
                }
                else
                {
                    ResElements.Add(elements);
                    count++;
                }
                iteration++;
            }
            CountResElements = count.ToString();
            //ConsoleLogElements(ResElements); //test
            count = Int32.Parse(CountResLinks);
            iteration = 1;
            foreach (Links links in IntLinks)
            {
                if (iteration != 1)
                    ResLinks.Add(links);

                count++;
                iteration++;
            }
            CountResLinks = count.ToString();
            //ConsoleLogLinks(ResLinks); //test
        }
        public void RecodingElements(int indexChange)
        {
            if (Type == "emptyElement")
            {
                var number = Int32.Parse(ResElements[indexChange].Number);
                var level = Int32.Parse(ResElements[indexChange].Level);
                number--;
                var index = SearchIndexElement(level.ToString(), number.ToString(), ResElements);
                var workElement = SearchElementWithIndex(Int32.Parse(index));
                var workLastNumber = SearchNumberLastElement(workElement.Id, ResLinks);
                //ConsoleLog(workElement.Name);
                //ConsoleLog(workLastNumber);
            }

            if (Type == "newElement")
            {

            }
        }
    }
    */
