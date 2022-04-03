using Scheduler.Jobs.Sample.Application.Services;
using Scheduler.Jobs.Sample.Domain;
using Scheduler.Jobs.Sample.Domain.Enums;

namespace Scheduler.Jobs.Sample.Infrastructure.Jobs
{
    public sealed class SendEmailJob : BaseJob<Email>
    {
        private readonly IEmailService _emailService;

        public override JobQueue JobQueue => JobQueue.Emails;

        public SendEmailJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public override void Execute(Email email)
        {
            Console.WriteLine($"Send e-mail (From: {email.From}, To: {email.To})");

            var success = _emailService.ProcessSend(email);

            if (!success)
            {
                Console.WriteLine($"Error while send e-mail.");

                throw new Exception("Error while send e-mail.");
            }
            else
            {
                Console.WriteLine($"Send e-mail successfully.");
            }
        }
    }
}
