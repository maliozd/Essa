using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    public static class DependencyExtensions
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IEssaDbContext, EssaDbContext>(options =>
            {
                //options.UseNpgsql(configuration.GetConnectionString("DbConnectionString"), options =>
                //{
                //    options.EnableRetryOnFailure(5);
                //});
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString"), options =>
                {
                    options.EnableRetryOnFailure(5);
                });

                options.EnableDetailedErrors();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
