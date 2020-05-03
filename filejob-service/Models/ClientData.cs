using System;
using System.Collections.Generic;

namespace filejob_service.Models
{
    public class ClientData
    {
        public Units Current { get; set; }
        public Units Integration { get; set; }
        public Units Result { get; set; }
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public Recoder ClientRecoder { get; set; }
        public List<string> ResultFile { get; set; }

        public ClientData(string token)
        {
            Token = token;
            Current = new Units();
            Integration = new Units();
            Result = new Units();
            CreateDate = DateTime.Now;
            ResultFile = new List<string>();
        }

        public ClientData(string token, Units curUnit, Units intUnit, Units resUnit)
        {
            Token = token;
            Current = curUnit;
            Integration = intUnit;
            Result = resUnit;
            CreateDate = DateTime.Now;
            ResultFile = new List<string>();
        }

        public ClientData(string token, ClientData clientData)
        {
            Token = token;
            Current = clientData.Current;
            Integration = clientData.Integration;
            Result = clientData.Result;
            CreateDate = DateTime.Now;
            ResultFile = new List<string>();
        }

        public void GenerateTotalResult()
        {
            if (ResultFile.Count > 0)
                ResultFile.Clear();
            try
            {
                ResultFile.Add("<project info=" + '\u0022' + '\u0022' + " name=" + '\u0022' + "IntegrationIS2" + '\u0022' + ">");
                ResultFile.Add("<user info=" + '\u0022' + '\u0022' + " name=" + '\u0022' + "by_moduleIntegration" + '\u0022' + "/>");
                ResultFile.Add("<models>");
                ResultFile.Add("<primaryModel>");
                ResultFile.Add("<params LastID=" + '\u0022' + "0" + '\u0022' + "/>");
                ResultFile.Add("<objects LastID=" + '\u0022' + "0" + '\u0022' + "/>");
                ResultFile.Add("<spd>");
                ResultFile.Add("<actions lastID=" + '\u0022' + "0" + '\u0022' + ">");
                foreach (Elements item in Result.Elements)
                {
                    var result = "<pd ";
                    string id = "id=" + '\u0022' + item.Id + '\u0022';
                    string name = "name=" + '\u0022' + item.Name + '\u0022';
                    string level = "level=" + '\u0022' + item.Level + '\u0022';
                    string number = "number=" + '\u0022' + item.Number + '\u0022';
                    string status = "status=" + '\u0022' + item.Status + '\u0022';
                    string type = "type=" + '\u0022' + item.Type + '\u0022';
                    string form = "formalization=" + '\u0022' + item.Formalization + '\u0022';
                    string symbol = "symbol=" + '\u0022' + item.Symbol + '\u0022';
                    string mark = "mark=" + '\u0022' + item.Mark + '\u0022';
                    result += id + " " + name + " " + level + " " + number + " " + status + " " + type + " " + form + " " + symbol + " " + mark + " " + "/>";
                    ResultFile.Add(result);
                }
                ResultFile.Add("</actions>");
                ResultFile.Add("<links>");
                foreach (Links item in Result.Links)
                {
                    var result = "<link ";
                    string afe1 = "afe1=" + '\u0022' + item.Afe1 + '\u0022';
                    string afe2 = "afe2=" + '\u0022' + item.Afe2 + '\u0022';
                    string afe3 = "afe3=" + '\u0022' + item.Afe3 + '\u0022';
                    string type = "type=" + '\u0022' + item.Type + '\u0022';
                    result += afe1 + " " + afe2 + " " + afe3 + " " + type + "/>";
                    ResultFile.Add(result);
                }
                ResultFile.Add("</links>");
                ResultFile.Add("</spd>");
                ResultFile.Add("<matrix/>");
                ResultFile.Add("</primaryModel>");
                ResultFile.Add("</models>");
                ResultFile.Add("<ModuleParams>");
                ResultFile.Add("<Module name=" + '\u0022' + "PrimaryModelAuto" + '\u0022' + ">");
                var result3 = "<param decStr=" + '\u0022';
                foreach (var item in Result.DcmpElements)
                {
                    result3 += item + ";";
                }
                result3 += '\u0022' + "/>";
                ResultFile.Add(result3);
                ResultFile.Add("</Module>");
                ResultFile.Add("</ModuleParams>");
                ResultFile.Add("</project>");
            }
            catch
            {
            }
        }
    }
}
