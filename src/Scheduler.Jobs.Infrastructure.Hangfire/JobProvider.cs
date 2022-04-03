using Hangfire;
using Hangfire.States;
using Microsoft.Extensions.Logging;
using Scheduler.Jobs.Domain;
using Scheduler.Jobs.Infrastructure.Hangfire.States;
using System.Collections.Concurrent;

namespace Scheduler.Jobs.Infrastructure.Hangfire
{
    public class JobProvider : IJobProvider
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly JobActivator _jobActivator;
        private readonly ILoggerProvider _loggerProvider;
        private readonly ILogger _logger;

        private readonly ConcurrentDictionary<Type, string> _jobsQueues = new();

        public JobProvider(
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            JobActivator jobActivator,
            ILoggerProvider loggerProvider)
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _jobActivator = jobActivator;
            _loggerProvider = loggerProvider;

            _logger = _loggerProvider.CreateLogger(typeof(JobProvider).FullName);
        }

        string IJobProvider.Enqueue<T, D>(D data)
        {
            return Enqueue<T, D>(data);
        }

        string IJobProvider.Schedule<T, D>(TimeSpan delay, D data)
        {
            return Schedule<T, D>(delay, data);
        }

        string IJobProvider.Schedule<T, D>(DateTime dateTime, D data)
        {
            return Schedule<T, D>(dateTime, data);
        }

        string IJobProvider.After<T, D>(string parentJobId, D data)
        {
            return After<T, D>(parentJobId, data);
        }

        void IJobProvider.Recurring<T, D>(string cronExpression, D data, bool trigger, TimeZoneInfo timeZone, string name)
        {
            Recurring<T, D>(cronExpression, data, trigger, timeZone, name);
        }

        string IJobProvider.Enqueue<T>()
        {
            return Enqueue<T, string>(null);
        }

        string IJobProvider.Schedule<T>(TimeSpan delay)
        {
            return Schedule<T, string>(delay, null);
        }

        string IJobProvider.Schedule<T>(DateTime dateTime)
        {
            return Schedule<T, string>(dateTime, null);
        }

        string IJobProvider.After<T>(string parentJobId)
        {
            return After<T, string>(parentJobId, null);
        }

        void IJobProvider.Recurring<T>(string cronExpression, bool trigger, TimeZoneInfo timeZone, string name)
        {
            Recurring<T, string>(cronExpression, null, trigger, timeZone, name);
        }

        private string GetQueue<T, D>()
            where T : class, IJob<D>
            where D : class
        {
            return _jobsQueues.GetOrAdd(typeof(T), type =>
            {
                var job = (T)_jobActivator.ActivateJob(type);

                return job.Queue.ToLower();
            });
        }

        private string Enqueue<T, D>(D data)
            where T : class, IJob<D>
            where D : class
        {
            return _backgroundJobClient.Create<Job<T, D>>(o => o.Execute(data, null), new EnqueuedState(GetQueue<T, D>()));
        }

        private string Schedule<T, D>(TimeSpan delay, D data)
            where T : class, IJob<D>
            where D : class
        {
            return _backgroundJobClient.Create<Job<T, D>>(o => o.Execute(data, null), new ScheduledQueueState(delay, GetQueue<T, D>()));
        }

        private string Schedule<T, D>(DateTime dateTime, D data)
            where T : class, IJob<D>
            where D : class
        {
            return _backgroundJobClient.Create<Job<T, D>>(o => o.Execute(data, null), new ScheduledQueueState(dateTime, GetQueue<T, D>()));
        }

        private string After<T, D>(string parentJobId, D data)
            where T : class, IJob<D>
            where D : class
        {
            return _backgroundJobClient.Create<Job<T, D>>(o => o.Execute(data, null), new EnqueuedState(GetQueue<T, D>()));
        }

        private void Recurring<T, D>(string cronExpression, D data, bool trigger, TimeZoneInfo timeZone, string name)
            where T : class, IJob<D>
            where D : class
        {
            if (string.IsNullOrWhiteSpace(name))
                name = nameof(T);

            _recurringJobManager.AddOrUpdate<Job<T, D>>(name, o => o.Execute(data, null), cronExpression, timeZone, queue: GetQueue<T, D>());

            if (trigger)
                _recurringJobManager.Trigger(name);
        }
    }
}