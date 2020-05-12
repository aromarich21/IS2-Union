using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                sourceClientData.Find((x) => x.Token == Token).GenerateTotalResult();
                DeleteFile(Token);
                //Thread.Sleep(10);
                FileCreate(sourceClientData);
            }
            catch { }
        }

        public void RefreshRecoder(List<ClientData> sourceClientData)
        {
            Job.DeleteElements(sourceClientData, Job.Types[2]);
            Job.DeleteLinks(sourceClientData, Job.Types[2]);
            Job.DeleteDcmp(sourceClientData, Job.Types[2]);
        }

        public void FillDiagramm(List<ClientData> sourceClientData) //fill resdiagramm with elements&links of currentdiagramm
        {
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                item.AddParentId(sourceClientData[_index].Current.Links);
                Elements element = new Elements(item);            
                //element.AddParentId(sourceClientData[_index].Current.Links);
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
                Startup.subjTest.Add(subjects); //test
            }
        }
        
        public void Migration(List<ClientData> sourceClientData, string id)
        {
            //var indexElement = Int32.Parse(FindIndexElement(id, sourceClientData));
            var indexElement = sourceClientData[_index].Result.Elements.FindIndex((x) => x.Id == id);

            if (sourceClientData[_index].Result.Elements[indexElement].Level == "1" && sourceClientData[_index].Result.Elements[indexElement].Number == "1")
            {
                MigrationFirstElement(sourceClientData, id);
            }
            if (sourceClientData[_index].Result.Elements[indexElement].Level != "1" && sourceClientData[_index].Result.Elements[indexElement].Number == "1")
            {
                MigrationFirstNumberSubjectNull(sourceClientData, id, indexElement);
            }
        }

        public void MigrationFirstElement(List<ClientData> sourceClientData, string id)
        {
            Job.DeleteElements(sourceClientData, Job.Types[2]);
            Job.DeleteLinks(sourceClientData, Job.Types[2]);
            Job.DeleteDcmp(sourceClientData, Job.Types[2]);
            sourceClientData[_index].Result.Elements = sourceClientData[_index].Integration.Elements;
            sourceClientData[_index].Result.Links = sourceClientData[_index].Integration.Links;
            sourceClientData[_index].Result.DcmpElements = sourceClientData[_index].Integration.DcmpElements;
        }

        public void MigrationFirstNumberSubjectNull(List<ClientData> sourceClientData, string id, int indexElement)
        {
            sourceClientData[_index].Result.Elements[indexElement].Name = sourceClientData[_index].Integration.Elements[0].Name;
            sourceClientData[_index].Result.Elements[indexElement].OldId = sourceClientData[_index].Integration.Elements[0].Id;
            sourceClientData[_index].Result.Elements[indexElement].OldCode = sourceClientData[_index].Integration.Elements[0].OldCode;
            List<Elements> sourceMigrationElements = new List<Elements>();
            List<Links> sourceMigrationLinks = new List<Links>();
            var diff = Int32.Parse(sourceClientData[_index].Result.Elements[indexElement].Level) - 1;
            var indexMigrationElement = 0;
            var lastIdElements = 0;
            
            //clear subjects of choose elements
            if (RecoderInfo.SourceSubjects.Find((x) => x.ParentId == sourceClientData[_index].Result.Elements[indexElement].Id)!=null)
            {
                foreach (var item in RecoderInfo.SourceSubjects.Find((x) => x.ParentId == sourceClientData[_index].Result.Elements[indexElement].Id).SubjectId)
                {
                    Elements elementToRemove = sourceClientData[_index].Result.Elements.Find((x) => x.Id == item);
                    sourceClientData[_index].Result.Elements.Remove(elementToRemove);
                    List<int> indexLinkToDel = new List<int>();
                    var countLinks = 0;
                    foreach (Links link in sourceClientData[_index].Result.Links)
                    {
                        if ((link.Afe2 == elementToRemove.Id && link.Afe1 == elementToRemove.ParentId) || (link.Afe3 == elementToRemove.Id && link.Afe1 == elementToRemove.ParentId))
                        {
                            //Links linkToRemove = link;
                            //sourceClientData[_index].Result.Links.Remove(linkToRemove);
                            indexLinkToDel.Add(countLinks);
                        }
                        countLinks++;
                    }
                    foreach (var index in indexLinkToDel)
                    {
                        //Links removeLink = sourceClientData[_index].Result.Links[index];
                        //sourceClientData[_index].Result.Links.Remove(removeLink);
                        //sourceClientData[_index].Result.Links.Remove(sourceClientData[_index].Result.Links[index]);
                        sourceClientData[_index].Result.Links[index].Afe1 = "0";
                        sourceClientData[_index].Result.Links[index].Afe2 = "0";
                        sourceClientData[_index].Result.Links[index].Afe3 = "0";
                    }
                }
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
                    element.Level = (Int32.Parse(element.Level) + diff).ToString();
                    element.Id = (++lastIdElements).ToString();
                    sourceMigrationElements.Add(element);
                    indexMigrationElement++;
                }
                count++;
            }
            foreach (Elements item in sourceMigrationElements)
            {
                Job.AddElement(sourceClientData, Job.Types[2], item);
            }
            foreach (Links item in sourceClientData[_index].Integration.Links)
            {
                Links link = new Links(item);

                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe1 && x.OldId != x.Id) != null)
                {
                    link.Afe1 = sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe1 && x.OldId != x.Id).Id;
                }
                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe2 && x.OldId != x.Id) != null)
                {
                    link.Afe2 = sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe2 && x.OldId != x.Id).Id;
                }
                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe3 && x.OldId != x.Id) != null)
                {
                    link.Afe3 = sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe3 && x.OldId != x.Id).Id;
                }
                sourceClientData[_index].Result.Links.Add(link);
            }
            foreach (string item in sourceClientData[_index].Integration.DcmpElements)
            {
                var value = item;
                sourceClientData[_index].Result.DcmpElements.Add(value);
            }
            for (int i = 0; i < sourceClientData[_index].Result.DcmpElements.Count; i++)
            {
                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldCode == sourceClientData[_index].Result.DcmpElements[i] && (x.OldCode != ("z" + x.Level + x.Number) && x.OldCode != ("z" + x.Level + "." + x.Number))) != null)
                {
                    Elements element = new Elements(sourceClientData[_index].Result.Elements.Find((x) => x.OldCode == sourceClientData[_index].Result.DcmpElements[i] && (x.OldCode != ("z" + x.Level + x.Number) && x.OldCode != ("z" + x.Level + "." + x.Number))));
                    if (Int32.Parse(element.Number) > 9)
                    {
                        sourceClientData[_index].Result.DcmpElements[i] = "z" + element.Level + "." + element.Number;
                    }
                    else
                    {
                        sourceClientData[_index].Result.DcmpElements[i] = "z" + element.Level + element.Number;
                    }
                }
            }
            var _dcmp = "z" + sourceClientData[_index].Result.Elements[indexElement].Level + sourceClientData[_index].Result.Elements[indexElement].Number;
            sourceClientData[_index].Result.DcmpElements.Add(_dcmp);
            foreach (string item in sourceClientData[_index].Current.DcmpElements)
            {
                var value = item;
                sourceClientData[_index].Result.DcmpElements.Add(value);
            }
            List<SubjectElements> sourceSubjects = new List<SubjectElements>();
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                if (item.Level == sourceClientData[_index].Result.Elements[indexElement].Level && Int32.Parse(item.Number) > Int32.Parse(sourceClientData[_index].Result.Elements[indexElement].Number))
                {
                    SubjectElements subjects = new SubjectElements(item.Id, sourceClientData[_index].Current.Elements);
                    if (subjects.SubjectId.Count > 0)
                    {
                        sourceSubjects.Add(subjects);
                        //Startup.subjTest.Add(subjects);
                    }
                }
            }
            count = 0;
            List<int> indexLinkToDelete = new List<int>();
            foreach (var item in sourceClientData[_index].Result.Links)
            {
                var countCopy = 0;
                foreach (var linkCopyCheck in sourceClientData[_index].Result.Links)
                {
                    if (item.Afe1 == linkCopyCheck.Afe1 && item.Afe2 == linkCopyCheck.Afe2 && item.Afe3 == linkCopyCheck.Afe3 && item.Type == linkCopyCheck.Type && count != countCopy)
                    {
                        indexLinkToDelete.Add(countCopy);
                    }
                    countCopy++;
                }
                count++;
            }
            /*
            foreach (var index in indexLinkToDelete)
            {
                //sourceClientData[_index].Result.Links.Remove(sourceClientData[_index].Result.Links[index]);
                //sourceClientData[_index].Result.Links[index].Afe1 = "0";
                //sourceClientData[_index].Result.Links[index].Afe2 = "0";
                sourceClientData[_index].Result.Links[index].Afe3 = "0";
            }
            
            count = 0;
            indexLinkToDelete.Clear();           
            foreach (var item in sourceClientData[_index].Result.Links)
            {
                if (item.Afe1 == "0" && item.Afe2 == "0")
                {
                    indexLinkToDelete.Add(count);
                }               
                count++;
            }
            foreach (var item in indexLinkToDelete)
            {
                Links linkToDelete = sourceClientData[_index].Result.Links[item];
                sourceClientData[_index].Result.Links.Remove(linkToDelete);
            }*/
        }

        public void MigrationFull(List<ClientData> sourceClientData, string id, int indexElement)
        {
            sourceClientData[_index].Result.Elements[indexElement].Name = sourceClientData[_index].Integration.Elements[0].Name;
            sourceClientData[_index].Result.Elements[indexElement].OldId = sourceClientData[_index].Integration.Elements[0].Id;
            sourceClientData[_index].Result.Elements[indexElement].OldCode = sourceClientData[_index].Integration.Elements[0].OldCode;
            List<Elements> sourceMigrationElements = new List<Elements>();
            List<Links> sourceMigrationLinks = new List<Links>();
            var diff = Int32.Parse(sourceClientData[_index].Result.Elements[indexElement].Level) - 1;
            var indexMigrationElement = 0;
            var lastIdElements = 0;
            Position position = new Position(sourceClientData[_index].Result.Elements[indexElement].Level, sourceClientData[_index].Result.Elements[indexElement].Number);

            //clear subjects of choose elements
            if (RecoderInfo.SourceSubjects.Find((x) => x.ParentId == sourceClientData[_index].Result.Elements[indexElement].Id) != null)
            {
                foreach (var item in RecoderInfo.SourceSubjects.Find((x) => x.ParentId == sourceClientData[_index].Result.Elements[indexElement].Id).SubjectId)
                {
                    Elements elementToRemove = sourceClientData[_index].Result.Elements.Find((x) => x.Id == item);
                    sourceClientData[_index].Result.Elements.Remove(elementToRemove);
                    List<int> indexLinkToDel = new List<int>();
                    var countLinks = 0;
                    foreach (Links link in sourceClientData[_index].Result.Links)
                    {
                        if (link.Afe2 == elementToRemove.Id || link.Afe1 == elementToRemove.ParentId)
                        {
                            //Links linkToRemove = link;
                            //sourceClientData[_index].Result.Links.Remove(linkToRemove);
                            indexLinkToDel.Add(countLinks);
                        }
                        countLinks++;
                    }
                    foreach (var index in indexLinkToDel)
                    {
                        //Links removeLink = sourceClientData[_index].Result.Links[index];
                        //sourceClientData[_index].Result.Links.Remove(removeLink);
                        //sourceClientData[_index].Result.Links.Remove(sourceClientData[_index].Result.Links[index]);
                        sourceClientData[_index].Result.Links[index].Afe1 = "0";
                        sourceClientData[_index].Result.Links[index].Afe2 = "0";
                        sourceClientData[_index].Result.Links[index].Afe3 = "0";
                    }
                }
            }
            //find lastId
            foreach (Elements item in sourceClientData[_index].Result.Elements)
            {
                if (Int32.Parse(item.Id) > lastIdElements)
                    lastIdElements = Int32.Parse(item.Id);
            }
            //check left elements
            if (position.Number - 1 > 0)
            {
                RecoderInfo.CreateStructDiagrammInfo(position, sourceClientData[_index].Result.Elements); //create info left struct
                //migration
            }
            else
            {
                MigrationFirstNumberSubjectNull(sourceClientData, id, indexElement);
            }

            var count = 0;
            foreach (Elements item in sourceClientData[_index].Integration.Elements)
            {
                if (count > 0)
                {
                    Elements element = new Elements(item);
                    element.Level = (Int32.Parse(element.Level) + diff).ToString();
                    element.Id = (++lastIdElements).ToString();
                    sourceMigrationElements.Add(element);
                    indexMigrationElement++;
                }
                count++;
            }
            foreach (Elements item in sourceMigrationElements)
            {
                Job.AddElement(sourceClientData, Job.Types[2], item);
            }
            foreach (Links item in sourceClientData[_index].Integration.Links)
            {
                Links link = new Links(item);

                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe1 && x.OldId != x.Id) != null)
                {
                    link.Afe1 = sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe1 && x.OldId != x.Id).Id;
                }
                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe2 && x.OldId != x.Id) != null)
                {
                    link.Afe2 = sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe2 && x.OldId != x.Id).Id;
                }
                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe3 && x.OldId != x.Id) != null)
                {
                    link.Afe3 = sourceClientData[_index].Result.Elements.Find((x) => x.OldId == link.Afe3 && x.OldId != x.Id).Id;
                }
                sourceClientData[_index].Result.Links.Add(link);
            }
            foreach (string item in sourceClientData[_index].Integration.DcmpElements)
            {
                var value = item;
                sourceClientData[_index].Result.DcmpElements.Add(value);
            }
            for (int i = 0; i < sourceClientData[_index].Result.DcmpElements.Count; i++)
            {
                if (sourceClientData[_index].Result.Elements.Find((x) => x.OldCode == sourceClientData[_index].Result.DcmpElements[i] && (x.OldCode != ("z" + x.Level + x.Number) && x.OldCode != ("z" + x.Level + "." + x.Number))) != null)
                {
                    Elements element = new Elements(sourceClientData[_index].Result.Elements.Find((x) => x.OldCode == sourceClientData[_index].Result.DcmpElements[i] && (x.OldCode != ("z" + x.Level + x.Number) && x.OldCode != ("z" + x.Level + "." + x.Number))));
                    if (Int32.Parse(element.Number) > 9)
                    {
                        sourceClientData[_index].Result.DcmpElements[i] = "z" + element.Level + "." + element.Number;
                    }
                    else
                    {
                        sourceClientData[_index].Result.DcmpElements[i] = "z" + element.Level + element.Number;
                    }
                }
            }
            var _dcmp = "z" + sourceClientData[_index].Result.Elements[indexElement].Level + sourceClientData[_index].Result.Elements[indexElement].Number;
            sourceClientData[_index].Result.DcmpElements.Add(_dcmp);
            foreach (string item in sourceClientData[_index].Current.DcmpElements)
            {
                var value = item;
                sourceClientData[_index].Result.DcmpElements.Add(value);
            }
            List<SubjectElements> sourceSubjects = new List<SubjectElements>();
            foreach (Elements item in sourceClientData[_index].Current.Elements)
            {
                if (item.Level == sourceClientData[_index].Result.Elements[indexElement].Level && Int32.Parse(item.Number) > Int32.Parse(sourceClientData[_index].Result.Elements[indexElement].Number))
                {
                    SubjectElements subjects = new SubjectElements(item.Id, sourceClientData[_index].Current.Elements);
                    if (subjects.SubjectId.Count > 0)
                    {
                        sourceSubjects.Add(subjects);
                        //Startup.subjTest.Add(subjects);
                    }
                }
            }
            count = 0;
            List<int> indexLinkToDelete = new List<int>();
            foreach (var item in sourceClientData[_index].Result.Links)
            {
                var countCopy = 0;
                foreach (var linkCopyCheck in sourceClientData[_index].Result.Links)
                {
                    if (item.Afe1 == linkCopyCheck.Afe1 && item.Afe2 == linkCopyCheck.Afe2 && item.Afe3 == linkCopyCheck.Afe3 && item.Type == linkCopyCheck.Type && count != countCopy)
                    {
                        indexLinkToDelete.Add(countCopy);
                    }
                    countCopy++;
                }
                count++;
            }
        }

        public void Recode(ClientData sourceClientData, string id)
        {
            RecoderInfo.CreateLeftStructDiagrammInfo(id, sourceClientData.Current.Elements);
        }
        
        public void FileCreate(List<ClientData> sourceClientData)
        {
            List<Stroke> sourceStroke = new List<Stroke>();
            int count = 0;
            foreach (var item in sourceClientData.Find((x) => x.Token == Token).ResultFile)
            {
                Stroke stroke = new Stroke(count.ToString(), item);
                sourceStroke.Add(stroke);
            }
            foreach (var item in sourceStroke)
            {
                Thread.Sleep(20);
                PatchFileStrokes(Token, item);
            }
            CreateFile(Token);
        }
        
        static async Task PatchFileStrokes(string token, Stroke stroke)
        {
            var json = JsonConvert.SerializeObject(stroke);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://localhost:44352/api/filestorage-service/filesis2?token=" + token; //поменять хост

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Patch;
            request.Content = data;
            HttpResponseMessage response = await client.SendAsync(request);
        }

        public WebResponse DeleteFile(string token) //
        {
            var delUrl = "https://localhost:44352/api/filestorage-service/filesis2?token=" + token; //поменять хост
            WebRequest request = WebRequest.Create(delUrl);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }

        public WebResponse CreateFile(string token) //post req link to fj-service
        {
            var postUrl = "https://localhost:44352/api/filestorage-service/filesis2?token=" + token; //поменять хост
            WebRequest reqPOST = WebRequest.Create(postUrl);
            reqPOST.Method = "POST"; // Устанавливаем метод передачи данных в POST
            reqPOST.Timeout = 120000; // Устанавливаем таймаут соединения
            reqPOST.ContentType = "application/x-www-form-urlencoded"; // указываем тип контента
            Stream sendStream = reqPOST.GetRequestStream();
            sendStream.Close();
            WebResponse result = reqPOST.GetResponse();
            return result;
        }
    }
}