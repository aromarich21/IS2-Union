using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filejob_service.Models
{
    public class Stroke
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public Stroke()
        {

        }

        public Stroke(string id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
