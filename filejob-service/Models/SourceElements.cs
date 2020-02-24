using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filejob_service.Models
{
    public class SourceElements
    {
        public List<Elements> elements = new List<Elements>();
        public string Token { get; set; }
    }
}
