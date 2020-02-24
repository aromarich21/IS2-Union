using filejob_service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace frontend_service.Models
{
    public class ClientData
    {
        public string Token { get; set; }
        public SelectList Options { get; set; }
        public SelectList ElementsDropdown { get; set; }
        public List<Elements> SourceCurElements { get; set; }

        public ClientData()
        {
            SourceCurElements = new List<Elements>(); //датасурс элементов cur диаграммы
        }

        public ClientData(string token)
        {
            Token = token;
            SourceCurElements = new List<Elements>();
        }
    }
}
