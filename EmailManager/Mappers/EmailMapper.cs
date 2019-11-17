using EmailManager.Data;
using EmailManager.Models.EmailViewModel;
using EmailManager.Services.Contracts;
using System.Collections.Generic;

namespace EmailManager.Mappers
{
    public static class EmailMapper
    {

        public static EmailViewModel MapFromEmail(this Email email, IEmailService emailService)
        {
            var emailListing = new EmailViewModel
            {
                Id = email.Id,
                Subject = email.Subject,
                Sender = email.Sender,
                Body = email.EmailBody.Body,
                Attachments = email.Attachments,
                ReceiveDate = email.ReceiveDate,
                EnumStatus = emailService.GetStatus(email.EmailId),
            };

            return emailListing;
        }

        public static EmailIndexViewModel MapFromEmailIndex(this IEnumerable<EmailViewModel> email)
        {
            var model = new EmailIndexViewModel
            {
                Emails = email
            };

            return model;
        }
    }
}