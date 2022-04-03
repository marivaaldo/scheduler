namespace Scheduler.Jobs.Domain
{
    public interface IJobProvider
    {
        string Enqueue<T, D>(D data = null)
            where T : class, IJob<D>
            where D : class;

        string Schedule<T, D>(TimeSpan delay, D data = null)
            where T : class, IJob<D>
            where D : class;

        string Schedule<T, D>(DateTime dateTime, D data = null)
            where T : class, IJob<D>
            where D : class;

        string After<T, D>(string parentJobId, D data = null)
            where T : class, IJob<D>
            where D : class;

        void Recurring<T, D>(string cronExpression, D data = null, bool trigger = false, TimeZoneInfo timeZone = null, string name = null)
            where T : class, IJob<D>
            where D : class;

        string Enqueue<T>()
            where T : class, IJob<string>;

        string Schedule<T>(TimeSpan delay)
            where T : class, IJob<string>;

        string Schedule<T>(DateTime dateTime)
            where T : class, IJob<string>;

        string After<T>(string parentJobId)
            where T : class, IJob<string>;

        void Recurring<T>(string cronExpression, bool trigger = false, TimeZoneInfo timeZone = null, string name = null)
            where T : class, IJob<string>;
    }
}
