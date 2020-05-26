using System;
using System.Collections.Generic;

namespace filejob_service.Models
{
    public class RecoderInfo
    {
        public List<SubjectElements> SourceSubjects { get; set; }
        public List<StructDiagramm> LeftStructDiagramm { get; set; }
        public List<StructDiagramm> ForRightStructDiagramm { get; set; }

        public RecoderInfo() 
        {
            SourceSubjects = new List<SubjectElements>();
            LeftStructDiagramm = new List<StructDiagramm>();
            ForRightStructDiagramm = new List<StructDiagramm>();
        }

        public void AddSubjectElements(SubjectElements subjectsElement)
        {
            SourceSubjects.Add(subjectsElement);
        }

        public void AddLeftStructDiagramm(StructDiagramm structDiagramm)
        {
            LeftStructDiagramm.Add(structDiagramm);
        }
        public void CreateStructDiagrammInfo(Position position, List<Elements> sourceElements)
        {         
            //Position subjPosition = new Position();
            for (; position.Number > 1; position.Number--)
            {
                if (sourceElements.Find((x) => x.Level == position.Level.ToString() && x.Number == (position.Number - 1).ToString())!=null)
                {
                    if (SourceSubjects.Find((x) => x.ParentId == sourceElements.Find((x) => x.Level == position.Level.ToString() && x.Number == (position.Number - 1).ToString()).Id) != null)
                    {
                        if (SourceSubjects.Find((x) => x.ParentId == sourceElements.Find((x) => x.Level == position.Level.ToString() && x.Number == (position.Number - 1).ToString()).Id).SubjectId != null)
                        {
                            foreach (var item in SourceSubjects.Find((x) => x.ParentId == sourceElements.Find((x) => x.Level == position.Level.ToString() && x.Number == (position.Number - 1).ToString()).Id).SubjectId)
                            {
                                Elements element = sourceElements.Find((x) => x.Id == item);
                                if (LeftStructDiagramm.Find((x) => x.Level == element.Level) == null)
                                {
                                    StructDiagramm str = new StructDiagramm(element.Level,element.Number);
                                    AddLeftStructDiagramm(str);
                                }
                                else
                                {
                                    LeftStructDiagramm.Find((x) => x.Level == element.Level).AddNumber(element.Number);
                                }
                            }
                        }
                    }              
                }       
            }      
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

        public void CreateIntegrationStructDiagramm(List<Elements> sourceIntegrElements)
        {
            StructDiagramm str;
            foreach(var item in sourceIntegrElements)
            {
                if (ForRightStructDiagramm.Find((x) => x.Level == item.Level)!=null)
                {
                    ForRightStructDiagramm[ForRightStructDiagramm.FindIndex((x) => x.Level == item.Level)].AddNumber(item.Number);
                }
                else
                {
                    str = new StructDiagramm(item.Level, item.Number);
                    ForRightStructDiagramm.Add(str);
                }
            }
        }
    }
}
