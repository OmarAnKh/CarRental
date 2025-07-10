using CarRental.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CarRental.Services;

public class MailKitEmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = Environment.GetEnvironmentVariable("EMAIL");
        var pw = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

        if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pw))
            throw new InvalidOperationException("Email credentials not set in environment variables.");

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Car Rental", mail));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using var smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mail, pw);
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
            throw;
        }
    }
}