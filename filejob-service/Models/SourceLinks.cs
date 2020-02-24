using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filejob_service.Models
{
    public class SourceLinks
    {
        public List<Links> links = new List<Links>();
        public string Token { get; set; }
    }
}
