using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontend_service.Models
{
    public class Service
    {
        public string Name { get; set; }
        public string Host { get; set; }

        public Service() { }

        public Service(string name,string host)
        {
            Name = name;
            Host = host;
        }
    }
}
