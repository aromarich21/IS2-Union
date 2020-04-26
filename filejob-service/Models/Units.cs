using System.Collections.Generic;

namespace filejob_service.Models
{
    public class Units
    {
        public List<Elements> Elements { get; set; }
        public List<Links> Links { get; set; }
        public List<string> DcmpElements { get; set; }

        public Units()
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            DcmpElements = new List<string>();
        }
        public Units(Elements element, Links link)
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            DcmpElements = new List<string>();
            Elements.Add(element);
            Links.Add(link);
        }
        public Units(List<Elements> sourceElements, List<Links> sourceLinks)
        {
            Elements = sourceElements;
            Links = sourceLinks;
            DcmpElements = new List<string>();
        }
        public Units(Units unit)
        {
            Elements = unit.Elements;
            Links = unit.Links;
            DcmpElements = new List<string>();
        }
        public void AddUnits(Elements element, Links link)
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            DcmpElements = new List<string>();
            Elements.Add(element);
            Links.Add(link);
        }
    }
}
