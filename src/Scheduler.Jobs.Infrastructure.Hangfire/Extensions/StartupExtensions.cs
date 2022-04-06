using Scheduler.Jobs.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler.Jobs.Infrastructure.Hangfire.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddHangfireJobProvider(this IServiceCollection services)
        {
            services
                .AddSingleton<IJobProvider, JobProvider>()
                .AddSingleton<JobProvider>()
                .AddTransient(typeof(Job<,>));

            return services;
        }
    }
}
