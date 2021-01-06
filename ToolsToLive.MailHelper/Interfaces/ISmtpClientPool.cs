namespace ToolsToLive.MailHelper.Interfaces
{
    public interface ISmtpClientPool
    {
        ISmtpClientPoolClient GetPoolClient();
    }
}
