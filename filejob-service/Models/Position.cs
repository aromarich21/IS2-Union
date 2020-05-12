using System;

namespace filejob_service.Models
{
    public class Position
    {
        public int Level { get; set; }
        public int Number { get; set; }

        public Position() { }
        public Position(string level, string number)
        {
            try
            {
                Level = Int32.Parse(level);
                Number = Int32.Parse(number);
            }
            catch
            {

            }
        }
        public Position(int level, int number)
        {
            Level = level;
            Number = number;
        }

        public void ChangeNumber(string number)
        {
            Number = Int32.Parse(number);
        }
        public void ChangeNumber(int number)
        {
            Number = number;
        }
        public void ChangeLevel (string level)
        {
            Level = Int32.Parse(level);
        }
        public void ChangeLevel(int level)
        {
            Level = level;
        }
    }
}
