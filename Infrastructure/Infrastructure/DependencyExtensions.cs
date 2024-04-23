using Application.Common.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Mail;
using Infrastructure.QuartzJpbs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure
{
    public static class DependencyExtensions
    {

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IInstaApiClient, InstaApiClient>();
            services.AddScoped<IMailSender, MailSender>();
            services.ConfigureOptions<UpdateMediaJobConfiguration>();

            services.AddQuartz(config =>
            {
                config.UseMicrosoftDependencyInjectionJobFactory();


            });
            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
                opt.AwaitApplicationStarted = true;
            });
        }
    }
}
