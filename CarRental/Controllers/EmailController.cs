using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailController> _logger;
    public EmailController(IEmailSender emailSender, ILogger<EmailController> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> SendEmail(string email, string subject, string message)
    {
        await _emailSender.SendEmailAsync(email, subject, message);
        return Ok();
    }
}