using System;
using System.Collections.Generic;

namespace filejob_service.Models
{
    public class RecoderInfo
    {
        public List<SubjectElements> SourceSubjects { get; set; }
        public List<StructDiagramm> LeftStructDiagramm { get; set; }
        public List<StructDiagramm> RightStructDiagramm { get; set; }

        public RecoderInfo() 
        {
            SourceSubjects = new List<SubjectElements>();
            LeftStructDiagramm = new List<StructDiagramm>();
        }

        public void AddSubjectElements(SubjectElements subjectsElement)
        {
            SourceSubjects.Add(subjectsElement);
        }

        public void AddLeftStructDiagramm(StructDiagramm structDiagramm)
        {
            LeftStructDiagramm.Add(structDiagramm);
        }

        public void CreateLeftStructDiagrammInfo(string idChooseElement, List<Elements> sourceElements)
        {
            //var id = Int32.Parse(idChooseElement);
            StructDiagramm str;
            Elements element = sourceElements.Find((x) => x.Id == idChooseElement);
            var level = element.Level;
            var number = (Int32.Parse(element.Number) - 1).ToString();
            /*
            var number = element.Number;
            str = new StructDiagramm(level, number);
            LeftStructDiagramm.Add(str);
            */
            var indexElement = sourceElements.FindIndex((x) => x.Level == level && x.Number == number);
            var indexSubjects = SourceSubjects.FindIndex((x) => x.ParentId == sourceElements[indexElement].Id);
            foreach (string item in SourceSubjects[indexSubjects].SubjectId)
            {
                Elements elementItem = new Elements();
                elementItem = sourceElements.Find((x) => x.Id == item);
                
                if (LeftStructDiagramm.Find((x) => x.Level == elementItem.Level) != null)
                {
                    LeftStructDiagramm[LeftStructDiagramm.FindIndex((x) => x.Level == elementItem.Level)].AddNumber(elementItem.Number);
                }
                else
                {
                    str = new StructDiagramm(elementItem.Level, elementItem.Number);
                    LeftStructDiagramm.Add(str);
                }
            }           
        }

        public void CreateRightStructDiagrammInfo(string idChooseElement, List<Elements> sourceElements)
        {
            StructDiagramm str;
            Elements element = sourceElements.Find((x) => x.Id == idChooseElement);
            var level = element.Level;
            var number = (Int32.Parse(element.Number) + 1).ToString();
            var indexElement = sourceElements.FindIndex((x) => x.Level == level && x.Number == number);
            var indexSubjects = SourceSubjects.FindIndex((x) => x.ParentId == sourceElements[indexElement].Id);
            foreach (string item in SourceSubjects[indexSubjects].SubjectId)
            {
                Elements elementItem = new Elements();
                elementItem = sourceElements.Find((x) => x.Id == item);

                if (LeftStructDiagramm.Find((x) => x.Level == elementItem.Level) != null)
                {
                    LeftStructDiagramm[LeftStructDiagramm.FindIndex((x) => x.Level == elementItem.Level)].AddNumber(elementItem.Number);
                }
                else
                {
                    str = new StructDiagramm(elementItem.Level, elementItem.Number);
                    LeftStructDiagramm.Add(str);
                }
            }
        }
    }
}
