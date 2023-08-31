using System.Net;
using System.Net.Mail;

namespace ZvadoHacks.Infrastructure.Email
{
    public static class EmailClient
    {
        const string Email = "scan-results@zvado.net";
        const string DisplayName = "Zvado";
        const string Password = "Viktor11@";
        const string HostUrl = "smtp.hostinger.com";

        public static async Task Send(string emailAddress, string subject, string body)
        {
            var fromAddress = new MailAddress(Email, DisplayName);
            var toAddress = new MailAddress(emailAddress, emailAddress);
            const string fromPassword = Password;
            var message = GetMessage(body, fromAddress, toAddress, subject);

            //message.Headers.Add("Message-ID", $"<{Guid.NewGuid()}@zvado.online>");

            await GetSmtp(fromAddress, fromPassword).SendMailAsync(message, CancellationToken.None);
        }

        private static MailMessage GetMessage(string body, MailAddress fromAddress, MailAddress toAddress, string subject)
        {
            return new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
            };
        }

        private static SmtpClient GetSmtp(MailAddress mailAddress, string password)
        {
            return new SmtpClient
            {
                Host = HostUrl,
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(mailAddress.Address, password)
            };
        }
    }
}
