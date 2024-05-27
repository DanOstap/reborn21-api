using System.Net.Mail;
using System.Net;
using System.Text;

namespace Reborn.Services
{
    public interface IMailService
    {
        void SendEmail(string toMail, string subject, string body);
    }

    public class MailService : IMailService
    {
        private readonly IConfiguration configuration;
        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(string toMail,string subject, string body) {
            SmtpClient client = new SmtpClient(
                configuration["MailSettings:smtpMail"],
                int.Parse(configuration["MailSettings:port"]) );
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(
                        configuration["MailSettings:email"],
                         configuration["MailSettings:password"]);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(configuration["MailSettings:email"]);
                    mailMessage.To.Add(toMail);
                    mailMessage.Subject = subject;
                    mailMessage.IsBodyHtml = true;
                    StringBuilder mailBody = new StringBuilder();
                    
                    mailBody.AppendFormat(body);
                    mailMessage.Body = mailBody.ToString();
            
            client.Send(mailMessage);
        }
    }
}
