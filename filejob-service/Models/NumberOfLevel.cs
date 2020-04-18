using System;

namespace filejob_service.Models
{
    public class NumberOfLevel
    {
        public int Level { get; set; }
        public int Number { get; set; }

        public NumberOfLevel() { }

        public NumberOfLevel(string level, string number)
        {
            Level = Int32.Parse(level);
            Number = Int32.Parse(number);
        }

        public NumberOfLevel(int level, int number)
        {
            Level = level;
            Number = number;
        }
    }
}
