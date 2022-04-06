using Scheduler.Jobs.Infrastructure.Hangfire.Extensions;
using Scheduler.Jobs.Sample.Domain.Enums;

namespace Scheduler.Jobs.Sample.Infrastructure.Jobs
{
    public sealed class DateTimeJob : BaseJob
    {
        public override JobQueue JobQueue => JobQueue.Default;

        public override void Execute()
        {
            Console.WriteLine($"DateTime: {DateTime.UtcNow}");

            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var progressBar = Console.CreateProgressBar();

            foreach (var item in list.WithProgress(progressBar))
            {
                Console.WriteLine($"Item: {item}");
                Thread.Sleep(1000);
            }
        }
    }
}
