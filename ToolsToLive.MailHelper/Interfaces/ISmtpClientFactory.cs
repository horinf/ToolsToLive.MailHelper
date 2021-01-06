using System.Net.Mail;

namespace ToolsToLive.MailHelper.Interfaces
{
    public interface ISmtpClientFactory
    {
        SmtpClient CreateSmtpClient();
    }
}
