using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filejob_service.Models
{
    public class SourceClientData
    {
        public SourceUnits Cur { get; set; }
        public SourceUnits Int { get; set; }
        public SourceUnits Res { get; set; }
        public string Token { get; set; }

        public SourceClientData(string token)
        {
            Token = token;
            Cur = new SourceUnits();
            Int = new SourceUnits();
            Res = new SourceUnits();
        }
        public SourceClientData(string token, SourceUnits sourceCurUnits, SourceUnits sourceIntUnits, SourceUnits sourceResUnits)
        {
            Token = token;
            Cur = new SourceUnits();
            Int = new SourceUnits();
            Res = new SourceUnits();
            Cur = sourceCurUnits;
            Int = sourceIntUnits;
            Res = sourceResUnits;
        }

        public SourceClientData(string token, SourceClientData sourceClientData)
        {
            Token = token;
            Cur = new SourceUnits();
            Int = new SourceUnits();
            Res = new SourceUnits();
            Cur = sourceClientData.Cur;
            Int = sourceClientData.Int;
            Res = sourceClientData.Res;
        }
    }
}
