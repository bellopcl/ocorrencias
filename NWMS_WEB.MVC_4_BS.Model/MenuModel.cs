using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class MenuModel
    {
        public long CODMEN { get; set; }
        public string DESMEN { get; set; }
        public long CODSIS { get; set; }
        public Nullable<long> MENPAI { get; set; }
        public Nullable<long> ORDMEN { get; set; }
        public string ENDPAG { get; set; }
        public string PERMEN { get; set; }
        public string INSMEN { get; set; }
        public string ALTMEN { get; set; }
        public string EXCMEN { get; set; }
        public string ICOMEN { get; set; }
        public long CODUSU;
    }
}
