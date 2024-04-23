using MailKit;
using Microsoft.AspNetCore.Mvc;
using Reborn.Services;
using Reborn.Models;

namespace Reborn.Controllers
{
    [ApiController]
    [Route("Mail")]
    public class MailController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public MailController(IConfiguration configuration) {
            this.configuration = configuration;
        }

        [HttpPost]
        public void SendMail(string toMail, string subject, string body) {
            Services.MailService mailService = new Services.MailService(configuration);
            mailService.SendEmail(toMail, subject, body);
        }

    }
}
