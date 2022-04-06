namespace Scheduler.Jobs.Domain.Console
{
    public interface IProgressBar
    {
        void SetValue(int value);
        IEnumerable<T> WithProgress<T>(IEnumerable<T> enumerable, int count = -1);
    }
}
