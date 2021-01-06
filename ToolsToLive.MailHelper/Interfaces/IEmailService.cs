using System.Threading.Tasks;

namespace ToolsToLive.MailHelper.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Send a email message. From parameters will be taken from settings (e.g. from appsettings.json file).
        /// </summary>
        Task Send(string subject, string body, params string[] destination);

        /// <summary>
        /// Send a email from specific address.
        /// </summary>
        Task SendFrom(string fromEmail, string fromDisplayName, string subject, string body, params string[] destination);
    }
}
