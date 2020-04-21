using System;
using System.Collections.Generic;

namespace filejob_service.Models
{
    public class Recoder
    {
        public string Token { get; set; }
        public string Url { get; set; }
        public ClientDataJob Job { get; set; }
        public int IndexSourceCurElements { get; set; }
        public int LastIndexCurElement { get; set; }
        public int LastIndexIntElement { get; set; }
        public int _index;
        public RecoderInfo RecoderInfo { get; set; }

        public Recoder() 
        {
            RecoderInfo = new RecoderInfo();
        }
        public Recoder(string token)
        {
            Token = token;
            RecoderInfo = new RecoderInfo();
        }
        public Recoder(string token, List<ClientData> sourceClientData)
        {
            Token = token;
            RecoderInfo = new RecoderInfo();
            Job = new ClientDataJob(token, sourceClientData);
            _index = Int32.Parse(Job.IndexClientData);
        }
        public Recoder(string token, List<ClientData> sourceClientData, string id)
        {
            Token = token;
            RecoderInfo = new RecoderInfo();
            Job = new ClientDataJob(token, sourceClientData);
            _index = Int32.Parse(Job.IndexClientData);
            Integration(sourceClientData, id);
        }

        public void Integration(List<ClientData> sourceClientData, string id)
        {
            try
            {
                RefreshRecoder(sourceClientData);
                FillDiagramm(sourceClientData);
                //ConnectDiagramm(sourceClientData, id);
                Migration(sourceClientData, id);
            }
           catch { }
        }

        public void RefreshRecoder(List<ClientData> sourceClientData)
        {
            Job.DeleteElements(sourceClientData, Job.Types[2]);
            Job.DeleteLinks(sourceClientData, Job.Types[2]);
        }

        public void FillDiagramm(List<ClientData> sourceClientData) //fill resdiagramm with elements&links of currentdiagramm
        {         
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                Elements element = new Elements(item);
                element.AddParentId(sourceClientData[_index].Current.Links);
                sourceClientData[_index].Result.Elements.Add(element); //переделать через джоб
            }
            foreach (Links item in sourceClientData[_index].Current.Links)
            {
                Links link = new Links(item);
                sourceClientData[_index].Result.Links.Add(link); //переделать через джоб
            }          
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                SubjectElements subjects = new SubjectElements(item.Id, sourceClientData[_index].Current.Elements);
                RecoderInfo.AddSubjectElements(subjects);
            }
        }
        public void Migration(List<ClientData> sourceClientData, string id)
        {
            //var indexElement = Int32.Parse(FindIndexElement(id, sourceClientData));
            var indexElement = sourceClientData[_index].Result.Elements.FindIndex((x) => x.Id == id);

            if (sourceClientData[_index].Result.Elements[indexElement].Level == "1" && sourceClientData[_index].Result.Elements[indexElement].Number == "1")
            {
                Job.DeleteElements(sourceClientData, Job.Types[2]);
                Job.DeleteLinks(sourceClientData, Job.Types[2]);
                //sourceClientData[_index].Result.Elements.Clear();
                //sourceClientData[_index].Result.Links.Clear();
                sourceClientData[_index].Result.Elements = sourceClientData[_index].Integration.Elements;
                sourceClientData[_index].Result.Links = sourceClientData[_index].Integration.Links;
            }
            if (sourceClientData[_index].Result.Elements[indexElement].Level != "1" && sourceClientData[_index].Result.Elements[indexElement].Number == "1")
            {
                sourceClientData[_index].Result.Elements[indexElement].Name = sourceClientData[_index].Integration.Elements[0].Name;
                sourceClientData[_index].Result.Elements[indexElement].OldId = sourceClientData[_index].Integration.Elements[0].Id;
                //sourceClientData[_index].Result.Elements[indexElement].Name = "hdshjdfshjdsfhjdfshjfdshjdsfjkhl";
                List<Elements> sourceMigrationElements = new List<Elements>();
                List<Links> sourceMigrationLinks = new List<Links>();
                var diff = Int32.Parse(sourceClientData[_index].Result.Elements[indexElement].Level) - 1;
                var indexMigrationElement = 0;
                var lastIdElements = 0;
                foreach (Links item in sourceClientData[_index].Integration.Links)
                {
                    Links link = new Links(item);
                    sourceMigrationLinks.Add(link);
                }
                foreach (Elements item in sourceClientData[_index].Result.Elements)
                {
                    if (Int32.Parse(item.Id) > lastIdElements)
                        lastIdElements = Int32.Parse(item.Id);
                }
                var count = 0;
                foreach (Elements item in sourceClientData[_index].Integration.Elements)
                {
                    if (count > 0)
                    {
                        Elements element = new Elements(item);
                        //element.Level = (Int32.Parse(element.Level) + diff).ToString();
                        //element.Id = (++lastIdElements).ToString();
                        //element.ParentId = element.OldId;
                        sourceMigrationElements.Add(element);
                        sourceMigrationElements[indexMigrationElement].Level = (Int32.Parse(sourceMigrationElements[indexMigrationElement].Level) + diff).ToString();
                        sourceMigrationElements[indexMigrationElement].Id = (++lastIdElements).ToString();
                        //sourceMigrationElements[indexMigrationElement].ParentId = item.Id;
                        indexMigrationElement++;
                    }
                    count++;
                }
                diff = Int32.Parse(sourceClientData[_index].Result.Elements[indexElement].Id) - Int32.Parse(sourceClientData[_index].Result.Elements[indexElement].OldId);
                foreach (Elements item in sourceMigrationElements)
                {
                    //Elements element = new Elements(item);
                    //sourceClientData[_index].Result.Elements.Add(element);
                    Job.AddElement(sourceClientData, Job.Types[2], item);
                    foreach (Links link in sourceMigrationLinks)
                    {
                        if (link.Afe1 == item.OldId)
                            //link.Afe1 = item.Id;
                            link.Afe1 = (Int32.Parse(link.Afe1) + diff).ToString();
                        if (link.Afe2 == item.OldId)
                            link.Afe2 = (Int32.Parse(link.Afe2) + diff).ToString();
                        if (link.Afe3 == item.OldId)
                            link.Afe3 = (Int32.Parse(link.Afe3) + diff).ToString();
                        if (link.Afe1 == sourceClientData[_index].Result.Elements[indexElement].OldId)
                            link.Afe1 = sourceClientData[_index].Result.Elements[indexElement].Id;
                        if (link.Afe2 == sourceClientData[_index].Result.Elements[indexElement].OldId)
                            link.Afe2 = sourceClientData[_index].Result.Elements[indexElement].Id;
                        if (link.Afe3 == sourceClientData[_index].Result.Elements[indexElement].OldId)
                            link.Afe3 = sourceClientData[_index].Result.Elements[indexElement].Id;
                    }
                }
                foreach (Links link in sourceMigrationLinks)
                {
                    sourceClientData[_index].Result.Links.Add(link);
                    //Job.AddLink(sourceClientData, Job.Types[2], link);
                }
            }
        }
        /*
        public void ConnectDiagramm(List<ClientData> sourceClientData, string id)
        {
            var indexElement = Int32.Parse(FindIndexElement(id, sourceClientData));
            var count = 0;

            if (sourceClientData[_index].Result.Elements[indexElement].Level == "1" && sourceClientData[_index].Result.Elements[indexElement].Number == "1")
            {
                Job.DeleteElements(sourceClientData, Job.Types[2]);
                Job.DeleteLinks(sourceClientData, Job.Types[2]);
                sourceClientData[_index].Result.Elements = sourceClientData[_index].Integration.Elements;
                sourceClientData[_index].Result.Links = sourceClientData[_index].Integration.Links;
            }
            else
            {
                List<Links> resLinks = new List<Links>();

                foreach (Links item in sourceClientData[_index].Integration.Links)
                {
                    resLinks.Add(item);
                }
                LastIndexCurElement = 1;
                foreach (Elements item in sourceClientData[_index].Current.Elements)
                {
                    LastIndexCurElement++;
                }
                sourceClientData[_index].Integration.Elements[0].Id = sourceClientData[_index].Result.Elements[indexElement].Id;
                sourceClientData[_index].Integration.Elements[0].Level = sourceClientData[_index].Result.Elements[indexElement].Level;
                sourceClientData[_index].Integration.Elements[0].Number = sourceClientData[_index].Result.Elements[indexElement].Number;
                sourceClientData[_index].Integration.Elements[0].ParentId = sourceClientData[_index].Result.Elements[indexElement].ParentId;
                sourceClientData[_index].Result.Elements[indexElement] = sourceClientData[_index].Integration.Elements[0];
                Recode(sourceClientData[_index], id);
                foreach (Elements item in sourceClientData[_index].Integration.Elements)
                {
                    if (count > 0)
                    {   
                        foreach (Links link in resLinks)
                        {
                            if (link.Afe1 == item.Id)
                                link.Afe1 = LastIndexCurElement.ToString();
                            if (link.Afe2 == item.Id)
                                link.Afe2 = LastIndexCurElement.ToString();
                            if (link.Afe3 == item.Id)
                                link.Afe3 = LastIndexCurElement.ToString();
                        }
                        item.Id = LastIndexCurElement.ToString();
                        Job.AddElement(sourceClientData, Job.Types[2], item);
                        LastIndexCurElement++;
                    }
                    count++;
                }
                foreach (Links item in resLinks)
                {
                    if (item.Afe1 == 1.ToString())
                        item.Afe1 = sourceClientData[_index].Result.Elements[indexElement].Id;
                    if (item.Afe2 == 1.ToString())
                        item.Afe2 = sourceClientData[_index].Result.Elements[indexElement].Id;
                    if (item.Afe3 == 1.ToString())
                        item.Afe3 = sourceClientData[_index].Result.Elements[indexElement].Id;
                    Job.AddLink(sourceClientData, Job.Types[2], item);
                }
            }    
        }
        */
        public void Recode(ClientData sourceClientData, string id)
        {
            RecoderInfo.CreateLeftStructDiagrammInfo(id, sourceClientData.Current.Elements);
            var diff = Int32.Parse(sourceClientData.Integration.Elements[0].Level) - 1;
            
            foreach (Elements item in sourceClientData.Integration.Elements)
            {
                if (RecoderInfo.LeftStructDiagramm.Count > 0)
                {
                    item.Level = (Int32.Parse(item.Level) + diff).ToString();
                    item.Number = (Int32.Parse(item.Number) + RecoderInfo.LeftStructDiagramm[RecoderInfo.LeftStructDiagramm.FindIndex((x) => x.Level == item.Level)].LastNumber).ToString();
                }
            }
        }
    }
}
