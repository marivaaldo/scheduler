using Hangfire.Console;
using Hangfire.Server;
using Scheduler.Jobs.Domain.Console;

namespace Scheduler.Jobs.Infrastructure.Hangfire
{
    public sealed class JobConsole : IJobConsole
    {
        private readonly PerformContext _context;

        public JobConsole(PerformContext context)
        {
            _context = context;
        }

        public void WriteLine(string message)
        {
            _context.WriteLine(message);
        }

        public IProgressBar CreateProgressBar(int value = 0)
        {
            var progressBar = _context.WriteProgressBar(value);

            return new ProgressBar(progressBar);
        }
    }
}
