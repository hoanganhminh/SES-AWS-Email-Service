using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using Microsoft.Extensions.Options;

public interface IMailService
{
    Task<SendEmailResponse> SendEmailAsync(MailRequest mailRequest);
}

public class SESService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly IAmazonSimpleEmailService _mailService;
    public SESService(IOptions<MailSettings> mailSettings,
        IAmazonSimpleEmailService mailService)
    {
        _mailSettings = mailSettings.Value;
        _mailService = mailService;
    }

    public async Task<SendEmailResponse> SendEmailAsync(MailRequest mailRequest)
    {
        var mailBody = new Body(new Content(mailRequest.Body));
        var message = new Message(new Content(mailRequest.Subject), mailBody);
        var destination = new Destination(new List<string> { mailRequest.ToEmail! });
        var request = new SendEmailRequest
        {
            Source = _mailSettings.Mail,
            Destination = destination,
            Message = message
        };

        var sendEmailResponse = await _mailService.SendEmailAsync(request);
        return sendEmailResponse;
    }
}

public class MailRequest
{
    public string? ToEmail { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}

public class MailSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? DisplayName { get; set; }
    public string? Mail { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}