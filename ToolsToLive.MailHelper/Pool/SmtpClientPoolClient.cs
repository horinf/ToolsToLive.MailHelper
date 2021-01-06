using MailKit.Net.Smtp;
using System;
using ToolsToLive.MailHelper.Interfaces;

namespace ToolsToLive.MailHelper.Pool
{
    public class SmtpClientPoolClient : ISmtpClientPoolClient
    {
        private readonly SmtpClientPool _pool;
        private SmtpClient _client;

        internal SmtpClientPoolClient(
            SmtpClientPool pool)
        {
            _pool = pool;
            _client = pool.GetClient();
        }

        public SmtpClient SmtpClient => _client ?? throw new Exception("The client has been released to pool");

        public void Dispose()
        {
            _pool.ReleaseClient(_client);
            _client = null;
        }
    }
}
