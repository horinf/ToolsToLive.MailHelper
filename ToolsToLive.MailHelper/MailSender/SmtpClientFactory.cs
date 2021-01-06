using Microsoft.Extensions.Options;
using System.Net.Mail;
using ToolsToLive.MailHelper.Interfaces;

namespace ToolsToLive.MailHelper.MailSender
{
    // SmtpClient doesn't support many modern protocols. It is compat-only. It's great for one off emails from tools, but doesn't scale to modern requirements of the protocol.
    // Use MailKit or other libraries.
    // https://github.com/dotnet/platform-compat/blob/master/docs/DE0005.md
    // https://github.com/jstedfast/MailKit

    public class SmtpClientFactory : ISmtpClientFactory
    {
        private readonly IOptions<EmailSettings> _emailSettings;

        public SmtpClientFactory(IOptions<EmailSettings> emailSettingsOptions)
        {
            _emailSettings = emailSettingsOptions;
        }

        public SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(_emailSettings.Value.SmtpUrl, _emailSettings.Value.SmtpPort));
        }
    }
}
