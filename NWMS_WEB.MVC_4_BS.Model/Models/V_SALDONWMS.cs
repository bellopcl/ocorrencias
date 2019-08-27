using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class V_SALDONWMS
    {
        public string Cód_Produto { get; set; }
        public string Cód_Derivação { get; set; }
        public string Cód_Integrado { get; set; }
        public string Desc__Produto { get; set; }
        public string Origem { get; set; }
        public string Família { get; set; }
        public Nullable<long> Saldo_Embalagens { get; set; }
        public Nullable<long> Saldo_Unitário { get; set; }
        public Nullable<long> Qtde_Paletes_Ativos { get; set; }
        public Nullable<long> Capacidade__Paletes_ { get; set; }
        public Nullable<long> Saldo_Estoque_ERP { get; set; }
        public Nullable<long> Estoque_Mínimo { get; set; }
        public Nullable<long> Estoque_Máximo { get; set; }
        public Nullable<long> Estoque_para_Reposição { get; set; }
        public string Situação_Lig_Prod_x_Dep_ { get; set; }
    }
}
