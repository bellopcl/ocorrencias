using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class SalvarTexto
    {
        public static void Salvar(string txt)
        {
            System.IO.File.WriteAllText(@"C:\Projetos_Desenvolvimento\Arquivo.txt", txt);
        }

    }
}
