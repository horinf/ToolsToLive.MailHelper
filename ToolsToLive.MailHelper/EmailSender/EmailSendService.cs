using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Threading.Tasks;
using ToolsToLive.MailHelper.Interfaces;

namespace ToolsToLive.MailHelper.EmailSender
{
    public class EmailSendService : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        private readonly ISmtpClientFactory _smtpClientFactory;

        public EmailSendService(
            IOptions<EmailSettings> emailSettingsOptions,
            ISmtpClientFactory smtpClientFactory)
        {
            _emailSettings = emailSettingsOptions;
            _smtpClientFactory = smtpClientFactory;
        }

        public Task Send(string subject, string body, params string[] destination)
        {
            return SendFrom(_emailSettings.Value.FromEmailAddress, _emailSettings.Value.FromDisplayName, subject, body, destination);
        }

        public async Task SendFrom(string fromEmail, string fromDisplayName, string subject, string body, params string[] destination)
        {
            // TODO: Use factory with smtpClient pool, use MailKit instead of System.Net.Mail.SmtpClient
            // SmtpClient is not thread-safe (both of them -- in dot.net and in MailKit)!
            // https://github.com/jstedfast/MailKit

            SmtpClient client = _smtpClientFactory.CreateSmtpClient();

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromEmail, _emailSettings.Value.Password);
            client.EnableSsl = true;

            var mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, fromDisplayName);
            foreach (string to in destination)
            {
                mail.To.Add(to);
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            await client.SendMailAsync(mail);
            client.Dispose();
        }
    }
}
