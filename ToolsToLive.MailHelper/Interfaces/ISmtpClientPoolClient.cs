using MailKit.Net.Smtp;
using System;

namespace ToolsToLive.MailHelper.Interfaces
{
    public interface ISmtpClientPoolClient : IDisposable
    {
        SmtpClient SmtpClient { get; }
    }
}
