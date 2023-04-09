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
            //byte[]? hash;
            //byte[]? salt;

            //_userRepository.EncodeId(mailRequest.RentContractId, out hash, out salt, out long timestamp);

            var body = mailRequest.LinkHost + mailRequest.RentContractId;/*mailRequest.LinkHost + Convert.ToBase64String(hash) + "/" + mailRequest.RentContractId + "/" + Convert.ToBase64String(salt) + "/" + timestamp;*/
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public string DecodeLink(string link)
        {
            string result = "";
            var (linkHost, hash, id, salt, timestamp) = ParseLink(link);
            var checkIdandTimeSpan = _userRepository.DecodeId(id, hash, salt, timestamp);
            if (checkIdandTimeSpan)
            {
                result = linkHost + "/" + id;
            }
            else
            {
                result = "This link is time out";
            }
            return result;
        }
        public (string LinkHost, byte[] Hash, int Id, byte[] Salt, long Timestamp) ParseLink(string link)
        {
            // Split the link into its component parts
            var parts = link.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            // Extract the link host
            var linkHost = parts[0];

            // Extract the hash
            byte[] hash = Convert.FromBase64String(parts[1]);

            // Extract the ID
            var id = int.Parse(parts[2]);
            
            // Extract the salt
            byte[] salt = Convert.FromBase64String(parts[3]);

            // Extract the timestamp
            var timestamp = long.Parse(parts[4]);

            return (linkHost, hash, id, salt, timestamp);
        }
    }
}

