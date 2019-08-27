using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class DebugEmail
    {
        public void Email(string Assunto, string comando)
        {
            Email email = new Email();
            string EmailDestino = "sistema02@nutriplan.com.br";
            string CopiarEmail = "sistema02nutriplan.com.br";
            email.EnviarEmail(EmailDestino, CopiarEmail, Assunto, comando);
        }
    }
}
