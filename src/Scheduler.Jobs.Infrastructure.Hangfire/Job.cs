using Hangfire.Server;
using Scheduler.Jobs.Domain;
using Scheduler.Jobs.Domain.Console;

namespace Scheduler.Jobs.Infrastructure.Hangfire
{
    public sealed class Job<T, D> : IJob<D>
        where T : class, IJob<D>
        where D : class
    {
        private readonly T _job;

        public Job(T job)
        {
            _job = job;
        }

        public string Queue => _job.Queue;

        public string JobId { get => _job.JobId; set => _job.JobId = value; }

        public IJobConsole Console { get => _job.Console; set => _job.Console = value; }

        public void Execute(D data, PerformContext performContext)
        {
            _job.JobId = performContext.JobId;
            _job.Console = new JobConsole(performContext);

            Execute(data);
        }

        public void Execute(D data)
        {
            _job.Execute(data);
        }
    }
}
