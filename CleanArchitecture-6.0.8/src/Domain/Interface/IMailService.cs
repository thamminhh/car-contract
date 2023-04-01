using System;
using CleanArchitecture.Domain.Entities_SubModel.Email;

namespace CleanArchitecture.Domain.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}

