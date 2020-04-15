using System;
using System.Collections.Generic;

namespace filejob_service.Models
{
    public class SubjectElements
    {

        public string ParentId { get; set; }
        public List<string> SubjectId { get; set; }
        public List<NumberOfLevel> SubjectsInfo { get; set; }

        public SubjectElements() 
        {
            SubjectId = new List<string>();
        }

        public SubjectElements (string id)
        {
            ParentId = id;
            SubjectId = new List<string>();
            SubjectsInfo = new List<NumberOfLevel>();
        }

        public SubjectElements(string id, List<Elements> sourceElements)
        {
            ParentId = id;
            SubjectId = new List<string>();
            SubjectsInfo = new List<NumberOfLevel>();
            FindSubjects(id, sourceElements);
        }

        public SubjectElements(string id, List<ClientData> sourceClientData, int _index, string type)
        {
            ParentId = id;
            SubjectId = new List<string>();
            SubjectsInfo = new List<NumberOfLevel>();
            FindSubjectInfo(sourceClientData, _index, type);
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
                catch
                {

                }
            }
        }

        public void FindSubjectInfo(List<ClientData> sourceClientData, int _index, string type)
        {
            if (type == "left")
            {
                FindLeftSubjects(ParentId, sourceClientData, _index);
            }
            if (type == "right")
            {
                //правые элементы
            }
        }

        public void FindLeftSubjects(string id, List<ClientData> sourceClientData,int _index)
        {
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                if (item.ParentId == id)
                {
                    SubjectId.Add(item.Id);
                    FindLatestSubjInfo(item, SubjectsInfo);
                }
                foreach (string ParentId in SubjectId)
                {
                    if (item.ParentId == ParentId)
                    {
                        SubjectId.Add(item.Id);
                        FindLatestSubjInfo(item, SubjectsInfo);
                    }
                }
            }
        }

        public void FindLatestSubjInfo(Elements element, List<NumberOfLevel> list)
        {
            bool exist = false;
            foreach (NumberOfLevel item in list)
            {
                if (item.Level == Int32.Parse(element.Level))
                {
                    exist = true;
                    if (item.Number < Int32.Parse(element.Number))
                    {
                        item.Number = Int32.Parse(element.Number);
                    }
                }
            }
            if (!exist)
            {
                NumberOfLevel numberOfLevel = new NumberOfLevel(element.Level, element.Number);
                SubjectsInfo.Add(numberOfLevel);
            }
        }

        public void FindRightSubjects(string id, List<ClientData> sourceClientData, int _index)
        {
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                if (item.ParentId == id)
                {
                    SubjectId.Add(item.Id);
                    FindLatestSubjInfo(item, SubjectsInfo);
                }
                foreach (string ParentId in SubjectId)
                {
                    if (item.ParentId == ParentId)
                    {
                        SubjectId.Add(item.Id);
                        FindLatestSubjInfo(item, SubjectsInfo);
                    }
                }
            }
        }

        public void FindFirstSubjInfo(Elements element, List<NumberOfLevel> list)
        {
            bool exist = false;
            foreach (NumberOfLevel item in list)
            {
                if (item.Level == Int32.Parse(element.Level))
                {
                    exist = true;
                    if (item.Number < Int32.Parse(element.Number))
                    {
                        item.Number = Int32.Parse(element.Number);
                    }
                }
            }
            if (!exist)
            {
                NumberOfLevel numberOfLevel = new NumberOfLevel(element.Level, element.Number);
                SubjectsInfo.Add(numberOfLevel);
            }
        }
    }
}
