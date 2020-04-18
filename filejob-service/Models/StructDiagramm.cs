using System;
using System.Collections.Generic;

namespace filejob_service.Models
{
    public class DiagrammInfo
    {

        public string Level { get; set; }
        public List<string> Numbers { get; set; }
        public int FirstNumber { get; set; }
        public int LastNumber { get; set; }

        public DiagrammInfo() {}

        public DiagrammInfo(string level)
        {
            Level = level;
            Numbers = new List<string>();
            FirstNumber = 0;
            LastNumber = 0;
        }

        public DiagrammInfo(string level,string number)
        {
            Level = level;
            Numbers = new List<string>();
            FirstNumber = Int32.Parse(number);
            LastNumber = Int32.Parse(number);
            AddNumber(number);
        }

        public void AddNumber(string number)
        {
            Numbers.Add(number);
            var _number = Int32.Parse(number);
            if (_number < FirstNumber)
                FirstNumber = _number;
            if (_number > LastNumber)
                LastNumber = _number;
        }
    }
}
