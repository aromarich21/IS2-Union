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
        }
        public Units(Elements element, Links link)
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            Elements.Add(element);
            Links.Add(link);
        }
        public Units(List<Elements> sourceElements, List<Links> sourceLinks)
        {
            Elements = sourceElements;
            Links = sourceLinks;
        }
        public Units(Units unit)
        {
            Elements = unit.Elements;
            Links = unit.Links;
        }
        public void AddUnits(Elements element, Links link)
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            Elements.Add(element);
            Links.Add(link);
        }
    }
}
