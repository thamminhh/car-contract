using System;
using System.Net.Mail;
using System.Net.Mime;
using CleanArchitecture.Domain.Entities_SubModel.Email;
using CleanArchitecture.Domain.Interface;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ContentType = MimeKit.ContentType;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CleanArchitecture.Application.Repository
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IUserRepository _userRepository;
        public MailService(IOptions<MailSettings> mailSettings, IUserRepository userRepository)
        {
            _mailSettings = mailSettings.Value;
            _userRepository = userRepository;   
        }


        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            //var rentContract

            var body = mailRequest.Body;
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}

