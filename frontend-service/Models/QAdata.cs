using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontend_service.Models
{
    public class QAdata
    {
       public List<string> DecStrElements { get; set; }
       public string DecStrElementsString { get; set; }

       public QAdata()
        {
            DecStrElements = new List<string>();
        }
    }
}
