using Application;
using Application.Common.Interfaces;
using EssaAPI.QuartzJpbs;
using Infrastructure.Clients;
using Infrastructure.Mail;
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
            //services.AddScoped<IINstaMediaService, InstaMediaService>();


            services.AddQuartz(config =>
            {
                config.UseMicrosoftDependencyInjectionJobFactory();

                config.AddJob<UpdateMediaJob>(options =>
                {
                    options.WithIdentity(AppConstants.UpdateMediaJobKey);
                });
                config.AddTrigger(opt =>
                {
                    opt.ForJob(AppConstants.UpdateMediaJobKey)
                    .WithIdentity(AppConstants.UpdateMediaTriggerKey)
                    .WithDailyTimeIntervalSchedule(x =>
                    {
                        x.OnDaysOfTheWeek(DayOfWeek.Monday, DayOfWeek.Friday);
                        x.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(21, 16)); //10:01
                    });
                });
            });
            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
                opt.AwaitApplicationStarted = false;
            });
        }
    }
}
