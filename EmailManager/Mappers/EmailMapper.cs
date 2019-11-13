using EmailManager.Data;
using EmailManager.Data.Contracts;
using EmailManager.Models.EmailViewModel;
using EmailManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace EmailManager.Mappers
//{
//    public static class EmailMapper
//    {
//        public static EmailViewModel MapFromEmail(this IEmail email, IEmailBody body)
//        {
//            var emailListing = new EmailViewModel
//            {
//                Subject = email.Subject,
//                Sender = email.Sender,
//                Body = body.EmailBody.Body,
//                Attachments = email.Attachments,
//                ReceiveDate = email.ReceiveDate,
//                EnumStatus = email.GetStatus(e.EmailId),
//            };


//        }
//    }
//}

        //public static EmailIndexViewModel MapFromEmailIndex(this IEmail email)
        //{
        //    var model = new EmailIndexViewModel
        //    {
        //        Emails = email
        //    };

        //    return model;
        //}