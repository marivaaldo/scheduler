using Hangfire.Console;
using Hangfire.Server;
using Scheduler.Jobs.Domain;

namespace Scheduler.Jobs.Infrastructure.Hangfire
{
    public class JobConsole : IJobConsole
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
    }
}
