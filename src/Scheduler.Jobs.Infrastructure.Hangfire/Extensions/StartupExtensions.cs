using Scheduler.Jobs.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler.Jobs.Infrastructure.Hangfire.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddHangfireJobProvider(this IServiceCollection services)
        {
            services.AddSingleton<IJobProvider, JobProvider>();
            services.AddSingleton<JobProvider>();

            services.AddTransient(typeof(Job<,>));

            return services;
        }
    }
}
