using Scheduler.Jobs.Domain.Console;

namespace Scheduler.Jobs.Infrastructure.Hangfire.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> WithProgress<T>(this IEnumerable<T> enumerable, IProgressBar progressBar, int count = -1)
        {
            return progressBar.WithProgress(enumerable, count);
        }
    }
}
