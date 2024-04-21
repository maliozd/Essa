using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblyContaining(typeof(DependencyExtensions));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
