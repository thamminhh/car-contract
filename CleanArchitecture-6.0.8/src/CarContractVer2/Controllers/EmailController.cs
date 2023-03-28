using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers;
using System.Net;
using System.Net.Mail;

public class EmailController : ControllerBase
{
    [HttpPost]
    [Route("api/sendemail")]
    public IActionResult SendEmail(string recipient, string subject, string body)
    {
        try
        {
            // Configure the SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("lauren33@ethereal.email", "1dKUN9FsZwYmGkm3Fa");

            recipient = "minhtqse130067@fpt.edu.vn";
            subject = "test mail";
            // Create the email message
            MailMessage message = new MailMessage();
            message.From = new MailAddress("lauren33@ethereal.email");
            message.To.Add(recipient);
            message.Subject = subject;
            message.Body = body;

            // Send the email
            client.Send(message);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
