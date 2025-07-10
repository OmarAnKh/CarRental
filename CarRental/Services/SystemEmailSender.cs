using System.Net;
using System.Net.Mail;
using CarRental.Services.Interfaces;

namespace CarRental.Services;

public class SystemEmailSender : IEmailSender
{

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = Environment.GetEnvironmentVariable("EMAIL");
        var pw = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };
        await client.SendMailAsync(new MailMessage(from: mail, to: email, subject, message));
    }
}