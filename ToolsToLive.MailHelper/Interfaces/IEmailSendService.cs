namespace ToolsToLive.MailHelper.Interfaces
{
    public interface IEmailSendService
    {
        /// <summary>
        /// Send a email message. From parameters will be taken from settings (e.g. from appsettings.json file).
        /// </summary>
        void Send(string subject, string body, params string[] destination);
    }
}
