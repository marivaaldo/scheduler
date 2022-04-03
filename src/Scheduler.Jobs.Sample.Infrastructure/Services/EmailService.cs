using Scheduler.Jobs.Domain;
using Scheduler.Jobs.Sample.Application.Services;
using Scheduler.Jobs.Sample.Domain;
using Scheduler.Jobs.Sample.Infrastructure.Jobs;

namespace Scheduler.Jobs.Sample.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IJobProvider _jobProvider;

        public EmailService(IJobProvider jobProvider)
        {
            _jobProvider = jobProvider;
        }

        public void EnqueueSend(Email email)
        {
            var jobId = _jobProvider.Enqueue<SendEmailJob, Email>(email);

            _jobProvider.After<SendEmailJob, Email>(jobId, new Email
            {
                To = email.From,
                From = "noreply@teste.com",
                Subject = $"[Confirmação] {email.Subject}",
                Message = "E-mail enviado com sucesso."
            });
        }

        public bool ProcessSend(Email email)
        {
            return true;
        }
    }
}
