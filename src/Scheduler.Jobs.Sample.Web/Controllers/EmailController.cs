using Microsoft.AspNetCore.Mvc;
using Scheduler.Jobs.Sample.Application.Services;
using Scheduler.Jobs.Sample.Domain;

namespace Scheduler.Jobs.Sample.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult> Send(Email email)
        {
            _emailService.EnqueueSend(email);

            return Created("/hangfire", email);
        }
    }
}
