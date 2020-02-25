using System.Collections.Generic;

namespace filejob_service.Models
{
    public class SourceLinks
    {
        public List<Links> links = new List<Links>();
        public string Token { get; set; }
    }
}
