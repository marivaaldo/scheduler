using Scheduler.Jobs.Domain;
using Scheduler.Jobs.Domain.Console;
using Scheduler.Jobs.Sample.Domain.Enums;
using System.Runtime.ExceptionServices;

namespace Scheduler.Jobs.Sample.Infrastructure.Jobs
{
    public abstract class BaseJob : BaseJob<string>
    {
        public BaseJob() : base()
        {
        }

        public BaseJob(string name) : base(name)
        {
        }

        public override void Execute(string data)
        {
            Execute();
        }

        public abstract void Execute();
    }

    public abstract class BaseJob<D> : IJob<D>
        where D : class
    {
        public BaseJob()
        {
            Name = this.GetType().Name;
        }
        public BaseJob(string name)
        {
            Name = name;
        }

        public string JobId { get; set; }

        public IJobConsole Console { get; set; }

        public string Name { get; }

        public string Queue => JobQueue.ToString();

        public abstract JobQueue JobQueue { get; }

        public void Execute(object data)
        {
            try
            {
                Execute((D)data);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }

        public abstract void Execute(D data);
    }
}
