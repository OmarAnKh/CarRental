using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

/// <summary>
/// Email sending controller 
/// </summary>
[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailController> _logger;
    /// <summary>
    /// the Email controller constructor 
    /// </summary>
    /// <param name="emailSender">the Email services you want to use </param>
    /// <param name="logger">a logger to log the info</param>
    public EmailController(IEmailSender emailSender, ILogger<EmailController> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }
    /// <summary>
    /// use it to send emails
    /// </summary>
    /// <param name="email">the receiver email</param>
    /// <param name="subject">the email subject</param>
    /// <param name="message">the email message</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> SendEmail(string email, string subject, string message)
    {
        await _emailSender.SendEmailAsync(email, subject, message);
        return Ok();
    }
}