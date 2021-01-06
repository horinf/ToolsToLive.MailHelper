using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using ToolsToLive.MailHelper.EmailSender;
using ToolsToLive.MailHelper.Interfaces;

namespace ToolsToLive.MailHelper.Pool
{
    public class SmtpClientPool : ISmtpClientPool
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        private readonly ILogger<SmtpClientPool> _logger;

        private readonly ConcurrentBag<SmtpClient> _allClients;
        private readonly ConcurrentBag<SmtpClient> _availableClients;

        private bool _stopped;
        private readonly object _startStopLock = new object();

        public SmtpClientPool(
            IOptions<EmailSettings> emailSettings,
            ILogger<SmtpClientPool> logger)
        {
            _emailSettings = emailSettings;
            _logger = logger;

            _allClients = new ConcurrentBag<SmtpClient>();
            _availableClients = new ConcurrentBag<SmtpClient>();
        }

        public ISmtpClientPoolClient GetPoolClient()
        {
            return new SmtpClientPoolClient(this);
        }

        internal SmtpClient GetClient()
        {
            SmtpClient client;
            if (!_availableClients.TryTake(out client))
            {
                client = CreateClient();
            }

            client.Connect(_emailSettings.Value.SmtpUrl, _emailSettings.Value.SmtpPort, true);
            client.Authenticate(_emailSettings.Value.FromEmailAddress, _emailSettings.Value.Password);

            return client;
        }

        internal void ReleaseClient(SmtpClient client)
        {
            client.Disconnect(true);
            _availableClients.Add(client);
        }

        private SmtpClient CreateClient()
        {
            lock (_startStopLock)
            {
                if (_stopped)
                {
                    throw new Exception("Pool has been stopped");
                }

                var client = new SmtpClient();
                _allClients.Add(client);

                if (_allClients.Count > 100)
                {
                    _logger.LogWarning("Too many clients created. Make sure there is no errors");
                }

                return client;
            }
        }

        public void Dispose()
        {
            lock (_startStopLock)
            {
                if (_stopped)
                {
                    return;
                }
                _stopped = true;
            }


            SmtpClient client;
            while (_availableClients.TryTake(out client))
            {
                // just clean this list
            }

            while (_allClients.TryTake(out client))
            {
                if (client.IsConnected)
                {
                    client.Disconnect(true);
                }
                client.Dispose();
            }
        }
    }
}
