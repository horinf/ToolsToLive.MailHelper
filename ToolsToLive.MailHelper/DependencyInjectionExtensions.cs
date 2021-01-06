using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToolsToLive.MailHelper.Interfaces;
using ToolsToLive.MailHelper.MailSender;
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
            services.AddScoped<ISmtpClientFactory, SmtpClientFactory>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
