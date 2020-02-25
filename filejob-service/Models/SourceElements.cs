using System.Collections.Generic;

namespace filejob_service.Models
{
    public class SourceElements
    {
        public List<Elements> elements = new List<Elements>();
        public string Token { get; set; }
    }
}
