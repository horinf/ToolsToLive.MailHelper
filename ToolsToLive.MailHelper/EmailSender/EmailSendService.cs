using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using ToolsToLive.MailHelper.Interfaces;

namespace ToolsToLive.MailHelper.EmailSender
{
    public class EmailSendService : IEmailSendService
    {
        private readonly ISmtpClientPool _smtpClientPool;
        private readonly IOptions<EmailSettings> _emailSettings;

        public EmailSendService(
            ISmtpClientPool smtpClientPool,
            IOptions<EmailSettings> emailSettingsOptions)
        {
            _smtpClientPool = smtpClientPool;
            _emailSettings = emailSettingsOptions;
        }

        public void Send(string subject, string body, params string[] destination)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.Value.FromDisplayName, _emailSettings.Value.FromEmailAddress));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body,
            };

            foreach (var dest in destination)
            {
                message.To.Add(new MailboxAddress(dest, dest));

            }

            using (var client = _smtpClientPool.GetPoolClient())
            {
                client.SmtpClient.Send(message);
            }
        }
    }
}
