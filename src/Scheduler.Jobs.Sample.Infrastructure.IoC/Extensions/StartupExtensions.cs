using Hangfire;
using Hangfire.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Jobs.Domain;
using Scheduler.Jobs.Infrastructure.Hangfire.Extensions;
using Scheduler.Jobs.Sample.Application.Services;
using Scheduler.Jobs.Sample.Domain.Enums;
using Scheduler.Jobs.Sample.Infrastructure.Jobs;
using Scheduler.Jobs.Sample.Infrastructure.Services;
using Cron = Scheduler.Jobs.Domain.Cron;

namespace Scheduler.Jobs.Sample.Infrastructure.IoC.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //
            // Register framework services

            services.AddScoped<IEmailService, EmailService>();

            services.AddTransient<SendEmailJob>();
            services.AddTransient<DateTimeJob>();

            //
            // Register Hangfire framework

            services.AddHangfire(conf => conf
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage()
                .UseConsole());

            services.AddHangfireJobProvider();

            //
            // Run Hangfire server

            services.AddHangfireServer(conf =>
            {
                conf.WorkerCount = 5; // Max Parallels Jobs
                conf.Queues = Enum.GetValues<JobQueue>()
                    .Select(o => o.ToString().ToLower())
                    .ToArray();
            });

            return services;
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
         
            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(app.ApplicationServices));

            var jobProvider = app.ApplicationServices.GetService<IJobProvider>();

            jobProvider.Recurring<DateTimeJob>(Cron.Minutely());

            return app;
        }
    }
}
