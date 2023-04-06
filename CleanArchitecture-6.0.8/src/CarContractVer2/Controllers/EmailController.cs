using Microsoft.AspNetCore.Mvc;

namespace CarContractVer2.Controllers;
using System.Net;
using System.Net.Mail;
using System.Threading;
using CleanArchitecture.Application.Repository;
using CleanArchitecture.Domain.Entities_SubModel.Email;
using CleanArchitecture.Domain.Interface;

public class MailController : ControllerBase
{
    private readonly IMailService mailService;
    private readonly IUserRepository _userRepository;
        public MailController(IMailService mailService, IUserRepository userRepository)
    {
        this.mailService = mailService;
        _userRepository = userRepository;   
    }
    [HttpPost("send")]
    public async Task<IActionResult> SendMail(MailRequest request)
    {
        try
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    [HttpPost("encodeId")]
    public async Task<IActionResult> EncodeId(int id)
    {

        var hash = _userRepository.EncodeId(id);
        return Ok(hash);
    }
    [HttpPost("decodeId")]
    public async Task<IActionResult> DecodeId(byte[] encoded)
    {
        if (_userRepository.TryDecodeId(encoded, out int id))
        {
            return Ok(id);
        }
        return BadRequest();
    }
}

//public class EmailController : ControllerBase
//{
//    [HttpPost]
//    [Route("api/sendemail")]
//    public IActionResult SendEmail(string recipient, string subject, string body)
//    {
//        try
//        {
//            // Configure the SMTP client
//            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
//            client.UseDefaultCredentials = false;
//            client.EnableSsl = true;
//            client.Credentials = new NetworkCredential("lauren33@ethereal.email", "1dKUN9FsZwYmGkm3Fa");

//            recipient = "minhtqse130067@fpt.edu.vn";
//            subject = "test mail";
//            // Create the email message
//            MailMessage message = new MailMessage();
//            message.From = new MailAddress("minhtqse130067@fpt.edu.vn");
//            message.To.Add(recipient);
//            message.Subject = subject;
//            message.Body = body;

//            // Send the emails
//            client.Send(message);
//            //this is need to be fix

//            return Ok();
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }
//}
