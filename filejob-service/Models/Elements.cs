using System;
using System.Collections.Generic;

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
        public string ParentId { get; set; }
        public string OldId { get; set; }
        public string OldCode { get; set; }
        //public SubjectElements Subjects { get; set; }

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
            OldId = Id;
            if (Int32.Parse(number) > 9)
            {
                OldCode = "z" + level + "." + number;
            }
            else
            {
                OldCode = "z" + level + number;
            }         
        }

        public Elements(Elements element)
        {
            Name = element.Name;
            Id = element.Id;
            Level = element.Level;
            Number = element.Number;
            Status = element.Status;
            Type = element.Type;
            Formalization = element.Formalization;
            Symbol = "122";
            Mark = "";
            OldId = Id;
            if (Int32.Parse(element.Number) > 9)
            {
                OldCode = "z" + element.Level + "." + element.Number;
            }
            else
            {
                OldCode = "z" + element.Level + element.Number;
            }
        }

        public void AddParentId(List<Links> linksList)
        {
            foreach (Links item in linksList)
            {
                if (item.Afe2 == Id && item.Type == "2")
                {
                    ParentId = item.Afe1;
                }
            }
        }
    }
}