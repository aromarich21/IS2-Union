

namespace filejob_service.Models
{
    public class ClientData
    {
        public Units Current { get; set; }
        public Units Integration { get; set; }
        public Units Result { get; set; }
        public string Token { get; set; }

        public ClientData(string token)
        {
            Token = token;
            Current = new Units();
            Integration = new Units();
            Result = new Units();
        }
        public ClientData(string token, Units curUnit, Units intUnit, Units resUnit)
        {
            Token = token;
            Current = curUnit;
            Integration = intUnit;
            Result = resUnit;
        }

        public ClientData(string token, ClientData clientData)
        {
            Token = token;
            Current = clientData.Current;
            Integration = clientData.Integration;
            Result = clientData.Result;
        }
    }
}
