

namespace filejob_service.Models
{
    public class Links
    {
        public string Afe1 { get; set; }
        public string Afe2 { get; set; }
        public string Afe3 { get; set; }
        public string Type { get; set; }

        public Links() { }
        public Links(string afe1, string afe2, string afe3, string type)
        {
            Afe1 = afe1;
            Afe2 = afe2;
            Afe3 = afe3;
            Type = type;
        }
    }
}