using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    public class OCORREABILITADOBusiness
    {
        public List<RelatorioOcorrenciasReabilitadas> pesquisarOcorrenciasHabilitadas(long? Numreg, string dateInicial, string dateFinal, string operacao)
        {
            try
            {
                OCORREABILITADODataAccess OCORREABILITADODataAccess = new OCORREABILITADODataAccess();
                return OCORREABILITADODataAccess.pesquisarOcorrenciasHabilitadas(Numreg, dateInicial, dateFinal, operacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
