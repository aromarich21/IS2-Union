using System.Collections.Generic;

namespace filejob_service.Models
{
    public class SourceUnits
    {
        public List<Elements> Elements { get; set; }
        public List<Links> Links { get; set; }

        public SourceUnits()
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
        }

        public SourceUnits(Elements element, Links link)
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            Elements.Add(element);
            Links.Add(link);
        }
        public SourceUnits(List<Elements> elements, List<Links> links)
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            Elements = elements;
            Links = links;
        }
        public SourceUnits(SourceUnits sourceUnits)
        {
            Elements = new List<Elements>();
            Links = new List<Links>();
            Elements = sourceUnits.Elements;
            Links = sourceUnits.Links;
        }
        public void AddUnits(Elements element, Links link)
        {
            Elements.Add(element);
            Links.Add(link);
        }
    }
}
