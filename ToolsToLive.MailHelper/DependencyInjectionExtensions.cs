using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToolsToLive.MailHelper.EmailSender;
using ToolsToLive.MailHelper.Interfaces;
using ToolsToLive.MailHelper.Renderer;

namespace ToolsToLive.MailHelper
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Sets up dependency for using mail helper
        /// </summary>
        public static IServiceCollection AddEmailHelper(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            services.Configure<EmailSettings>(configurationSection);

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddSingleton<ISmtpClientFactory, SmtpClientFactory>();
            services.AddSingleton<IEmailService, EmailSendService>();

            return services;
        }
    }
}
