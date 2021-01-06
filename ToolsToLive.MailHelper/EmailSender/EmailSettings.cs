namespace ToolsToLive.MailHelper.EmailSender
{
    public class EmailSettings
    {
        public string FromEmailAddress { get; set; }
        public string FromDisplayName { get; set; }
        public string Password { get; set; }
        public string SmtpUrl { get; set; }
        public int SmtpPort { get; set; }
    }
}
