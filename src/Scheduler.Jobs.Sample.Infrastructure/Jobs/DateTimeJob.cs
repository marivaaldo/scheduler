using Scheduler.Jobs.Sample.Domain.Enums;

namespace Scheduler.Jobs.Sample.Infrastructure.Jobs
{
    public sealed class DateTimeJob : BaseJob
    {
        public override JobQueue JobQueue => JobQueue.Default;

        public override void Execute()
        {
            Console.WriteLine($"DateTime: {DateTime.UtcNow}");
        }
    }
}
