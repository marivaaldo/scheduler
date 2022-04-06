using Hangfire.Console;
using Hangfire.Console.Progress;

namespace Scheduler.Jobs.Infrastructure.Hangfire
{
    public sealed class ProgressBar : Domain.Console.IProgressBar, IProgressBar
    {
        private readonly IProgressBar _progressBar;

        public ProgressBar(IProgressBar progressBar)
        {
            _progressBar = progressBar;
        }

        public void SetValue(int value)
        {
            _progressBar.SetValue(value);
        }

        public void SetValue(double value)
        {
            SetValue((int)value);
        }

        public IEnumerable<T> WithProgress<T>(IEnumerable<T> enumerable, int count = -1)
        {
            return enumerable.WithProgress(_progressBar, count);
        }
    }
}
