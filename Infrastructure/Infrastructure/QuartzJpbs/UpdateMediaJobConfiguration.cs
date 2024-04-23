using Application;
using EssaAPI.QuartzJpbs;
using Microsoft.Extensions.Options;
using Quartz;

namespace Infrastructure.QuartzJpbs
{
    public class UpdateMediaJobConfiguration : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            JobKey jobKey = new JobKey(AppConstants.UpdateMediaJobKey);

            options.AddJob<UpdateMediaJob>(options =>
            {
                options.WithIdentity(jobKey);
            });

            options.AddTrigger(opt =>
            {
                opt.ForJob(jobKey)
                   .WithIdentity(AppConstants.UpdateMediaTriggerKey)
                   .WithDailyTimeIntervalSchedule(x =>
                   {
                       x.OnDaysOfTheWeek(DayOfWeek.Monday, DayOfWeek.Tuesday) // Pazartesi ve Salı günleri
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(14, 03)) // Saat 14:00'da başlayacak
                        .EndingDailyAfterCount(1); // Tekrar sayısı: 1
                   });
            });

        }
    }
}
