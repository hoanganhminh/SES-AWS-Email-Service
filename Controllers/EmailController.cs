using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Mvc;

namespace TestSES.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IMailService mailService;

        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("Mail")]
        public async Task<SendEmailResponse> GetUserProfileByEmail(MailRequest mailRequest)
        {
            var response = await mailService.SendEmailAsync(mailRequest);

            return response;
        }
    }
}