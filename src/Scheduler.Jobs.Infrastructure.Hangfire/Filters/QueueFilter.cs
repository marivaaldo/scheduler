using Scheduler.Jobs.Infrastructure.Hangfire.States;
using Hangfire.Client;
using Hangfire.States;

namespace Scheduler.Jobs.Infrastructure.Hangfire.Filters
{
    internal sealed class QueueFilter : IClientFilter, IElectStateFilter
    {
        private const string QueueParameterName = "Queue";

        public void OnCreated(CreatedContext filterContext)
        {
        }

        public void OnCreating(CreatingContext filterContext)
        {
            var queue = filterContext.InitialState switch
            {
                EnqueuedState es => es.Queue,
                ScheduledQueueState sqs => sqs.Queue,
                _ => null
            };

            if (!string.IsNullOrWhiteSpace(queue))
            {
                filterContext.SetJobParameter(QueueParameterName, queue);
            }
        }

        public void OnStateElection(ElectStateContext context)
        {
            if (context.CandidateState.Name == EnqueuedState.StateName)
            {
                string queue = context.GetJobParameter<string>(QueueParameterName)?.Trim();

                if (string.IsNullOrWhiteSpace(queue))
                {
                    queue = EnqueuedState.DefaultQueue;
                }

                context.CandidateState = new EnqueuedState(queue);
            }
        }
    }
}
