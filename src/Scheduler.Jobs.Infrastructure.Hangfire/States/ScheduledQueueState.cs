using Hangfire.States;
using Newtonsoft.Json;

namespace Scheduler.Jobs.Infrastructure.Hangfire.States
{
    public sealed class ScheduledQueueState : ScheduledState
    {
        public string Queue { get; }

        public ScheduledQueueState(TimeSpan enqueueIn)
            : this(enqueueIn, null)
        {
        }

        public ScheduledQueueState(DateTime enqueueAt)
            : this(enqueueAt, null)
        {
        }

        [JsonConstructor]
        public ScheduledQueueState(TimeSpan enqueueIn, string queue)
            : base(enqueueIn)
        {
            Queue = queue?.Trim();
        }

        [JsonConstructor]
        public ScheduledQueueState(DateTime enqueueAt, string queue)
            : base(enqueueAt)
        {
            Queue = queue?.Trim();
        }
    }
}
