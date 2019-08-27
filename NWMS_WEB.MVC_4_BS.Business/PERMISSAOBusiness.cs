using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    public class PERMISSAOBusiness
    {
        public List<RelPermissaoTela> relPermissaoTelas(string usuario, char status)
        {
            PERMISSAODataAccess pERMISSAODataAccess = new PERMISSAODataAccess();
            return pERMISSAODataAccess.relPermissaoTelas(usuario, status);
        }

        public List<RelPermissaoAprovadorOrigem> relPermissaoAprovadorOrigems(string usuario, char status)
        {
            PERMISSAODataAccess pERMISSAODataAccess = new PERMISSAODataAccess();
            return pERMISSAODataAccess.relPermissaoAprovadorOrigems(usuario, status);
        }

        public List<RelPermissaoUsuAproFaturamento> relPermissaoUsuAproFaturamentos(string usuario, char status)
        {
            PERMISSAODataAccess pERMISSAODataAccess = new PERMISSAODataAccess();
            return pERMISSAODataAccess.relPermissaoUsuarioAprovacaoFaturamentos(usuario, status);

        }

        public List<PermissaoDevTrocaUsu> permissaoDevTrocaUsus(string usuario, char status)
        {
            PERMISSAODataAccess pERMISSAODataAccess = new PERMISSAODataAccess();
            return pERMISSAODataAccess.PermissaoTrocaDevolucao(usuario, status);
        }
    }
}
