using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace filejob_service.Models
{
    public class Elements
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Formalization { get; set; }
        public string Symbol { get; set; }
        public string Mark { get; set; }

        public Elements()
        {
            Symbol = "122";
            Mark = "";
        }

        public Elements(string name, string id, string level, string number, string status, string type, string formalization)
        {
            Name = name;
            Id = id;
            Level = level;
            Number = number;
            Status = status;
            Type = type;
            Formalization = formalization;
            Symbol = "122";
            Mark = "";
        }
    }
}