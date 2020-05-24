using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontend_service.Models
{
    public class Environment
    {
        public List<Service> Source { get; set; }

        public Environment()
        {
            Source = new List<Service>();
        }

        public void CreateEnvSource()
        {
            Service frontend_service = new Service("frontend-service", "");
            Source.Add(frontend_service);
            Service filejob_service = new Service("filejob-service", "");
            Source.Add(filejob_service);
            Service filestorage_service = new Service("filestorage-service", "");
            Source.Add(filestorage_service);
        }
    }
}
