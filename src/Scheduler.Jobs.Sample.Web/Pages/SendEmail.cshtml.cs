using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scheduler.Jobs.Sample.Application.Services;
using Scheduler.Jobs.Sample.Domain;

namespace Scheduler.Jobs.Sample.Web.Pages
{
    public class SendEmailModel : PageModel
    {
        private readonly IEmailService _emailService;

        [BindProperty]
        public Email Email { get; set; } = new Email();

        public SendEmailModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void OnPost()
        {
            _emailService.EnqueueSend(Email);
        }
    }
}
