//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.WS_AUTENTICA_USER;
//using NUTRIPLAN_WEB.MVC_4_BS.Model;

//namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
//{
//    public class AutenticaUsuarioSapDataAccess
//    {
//        private sapiens_SyncMCWFUsersClient teste { get; set; }

//        public bool AutenticaUsuarioSapiens(string usuario, string senha)
//        {
//            using (this.teste = new sapiens_SyncMCWFUsersClient())
//            {
//                mcwfUsersAuthenticateJAASIn user = new mcwfUsersAuthenticateJAASIn();

//                user.pmUserName = usuario;
//                user.pmUserPassword = senha;

//                if (teste.AuthenticateJAAS("nworkflow.web", "!nfr@t1n", 0, user).pmLogged == "0")
//                {
//                    return true;
//                }

//                return false;
//            }
//        }
//    }
//}
