using System;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Domain.Entities_SubModel.Email
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public int RentContractId { get; set; }

        public string LinkHost { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}

