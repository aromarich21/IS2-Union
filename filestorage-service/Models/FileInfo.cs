using System.Collections.Generic;

namespace filestorage_service.Models
{
    public class FileInfo
    {
        public string Name { get; set; }
        public string CreateDate { get; set; }
        public string Uuid { get; set; }
        public List<string> Strokes { get; set; }

        public FileInfo()
        {
            Strokes = new List<string>();
        }
        public FileInfo(string name, string token)
        {
            Name = name;
            Uuid = token;
            Strokes = new List<string>();
            //CreateDate
        }
        public FileInfo(string token)
        {
            Name = token;
            Uuid = token;
            Strokes = new List<string>();
        }
    }
}
