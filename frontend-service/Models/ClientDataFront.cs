﻿using filejob_service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace frontend_service.Models
{
    public class ClientDataFront
    {
        public SelectList Options { get; set; }
        public SelectList ElementsDropdown { get; set; }
        //public List<Elements> SourceCurElements { get; set; }
        public ClientData clientData { get; set; }

        public ClientDataFront()
        {
        }

        public ClientDataFront(string token)
        {
            clientData = new ClientData(token);
        }
    }
}
