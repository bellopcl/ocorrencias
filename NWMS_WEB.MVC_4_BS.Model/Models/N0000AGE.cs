using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public partial class N0000AGE
    {
        public N0000AGE()
        {
            this.N0000LOG = new List<N0000LOG>();
        }

        public long CODAGE { get; set; }
        public string DESAGE { get; set; }
        public string TIPAGE { get; set; }
        public string SITAGE { get; set; }
        public string PERAGE { get; set; }
        public System.DateTime DATAGE { get; set; }
        public System.DateTime DATPRX { get; set; }
        public string COMSQL { get; set; }
        public string IDNREL { get; set; }
        public string FILREL { get; set; }
        public string URLREL { get; set; }
        public string EMADES { get; set; }
        public string EMACCC { get; set; }
        public string EMACCU { get; set; }
        public string TITEMA { get; set; }
        public string COREMA { get; set; }
        public string EMAORI { get; set; }
        public string SENORI { get; set; }
        public string SERORI { get; set; }
        public string DIAUTI { get; set; }
        public string SQLVAL { get; set; }
        public string EMAFOR { get; set; }
        public virtual ICollection<N0000LOG> N0000LOG { get; set; }
    }
}
