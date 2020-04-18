using System.Collections.Generic;

namespace filejob_service.Models
{
    public class SubjectElements
    {
        public string ParentId { get; set; }
        public List<string> SubjectId { get; set; }

        public SubjectElements() 
        {
            SubjectId = new List<string>();
        }

        public SubjectElements (string id)
        {
            ParentId = id;
            SubjectId = new List<string>();
        }

        public SubjectElements(string id, List<Elements> sourceElements)
        {
            ParentId = id;
            SubjectId = new List<string>();
            FindSubjects(id, sourceElements);
        }

        public void FindSubjects(string id, List<Elements> sourceElements)
        {
            foreach (Elements item in sourceElements)
            {
                if (item.ParentId == id)
                {
                    SubjectId.Add(item.Id);
                }
                try
                {
                    foreach (string ParentId in SubjectId)
                    {
                        if (item.ParentId == ParentId)
                        {
                            SubjectId.Add(item.Id);
                        }
                    }
                }
                catch { }
            }
        }
    }
}
