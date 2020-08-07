using System.Text;
using System.Net.Mail;
using System.Net;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class Email
    {
        public void EnviarEmail(string EmailDestino, string CopiarEmails, string Assunto, string Mensagem)
        {
            MailMessage objEmail = new MailMessage();
            objEmail.From = new MailAddress("nworkflow_web@nutriplan.com.br");
            
            CopiarEmails = CopiarEmails.Replace("diogo.melo@nutriplan.com.br", "sistema02@nutriplan.com.br");
            EmailDestino = EmailDestino.Replace("diogo.melo@nutriplan.com.br", "sistema02@nutriplan.com.br");
            CopiarEmails = CopiarEmails.Replace("nei.junior@nutriplan.com.br", "sistema02@nutriplan.com.br");
            EmailDestino = EmailDestino.Replace("nei.junior@nutriplan.com.br", "sistema02@nutriplan.com.br");
            
            if (!string.IsNullOrEmpty(CopiarEmails))
            {
                var emails = CopiarEmails.Split('&');
                for (int i = 0; i < emails.Length - 1; i++)
                {
                    MailAddress copy = new MailAddress(emails[i]);
                    objEmail.CC.Add(emails[i]);
                }
            }

            objEmail.To.Add(EmailDestino);
            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = Assunto;
            objEmail.Body = Mensagem;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.nutriplan.com.br";
            objSmtp.EnableSsl = false;
            objSmtp.Port = 25;
            // objSmtp.UseDefaultCredentials = true;
            objSmtp.Credentials = new NetworkCredential("nworkflow_web", "WEBnutri2014");
            objSmtp.Send(objEmail);
        }
    }
}
