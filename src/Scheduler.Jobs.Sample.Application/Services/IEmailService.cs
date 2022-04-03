using Scheduler.Jobs.Sample.Domain;

namespace Scheduler.Jobs.Sample.Application.Services
{
    public interface IEmailService
    {
        void EnqueueSend(Email email);
        bool ProcessSend(Email email);
    }
}
