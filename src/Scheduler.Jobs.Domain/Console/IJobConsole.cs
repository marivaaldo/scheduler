namespace Scheduler.Jobs.Domain.Console
{
    public interface IJobConsole
    {
        void WriteLine(string message);
        IProgressBar CreateProgressBar(int value = 0);
    }
}
