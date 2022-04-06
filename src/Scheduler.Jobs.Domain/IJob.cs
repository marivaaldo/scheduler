using Scheduler.Jobs.Domain.Console;

namespace Scheduler.Jobs.Domain
{
    public interface IJob<D> where D : class
    {
        string JobId { get; set; }
        IJobConsole Console { get; set; }

        string Queue { get; }
        void Execute(D data);
    }
}
