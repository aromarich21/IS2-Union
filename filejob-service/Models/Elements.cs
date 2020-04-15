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
        public SubjectElements Subjects { get; set; }

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