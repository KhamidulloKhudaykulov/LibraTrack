using Microsoft.Extensions.Configuration;
using RentalService.Application.Services;
using System.Net;
using System.Net.Mail;

namespace RentalService.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly string _from;
    public EmailService(IConfiguration configuration)
    {
        var smtpSection = configuration.GetSection("EmailSettings");

        _from = smtpSection["From"]!;
        _smtpClient = new SmtpClient(smtpSection["Host"])
        {
            Port = int.Parse(smtpSection["Port"]!),
            Credentials = new NetworkCredential(smtpSection["Username"], smtpSection["Password"]),
            EnableSsl = true
        };
    }
    public async Task SendAsync(string to, string subject, string body)
    {
        var mail = new MailMessage(_from, to, subject, body);
        await _smtpClient.SendMailAsync(mail);
    }
}
