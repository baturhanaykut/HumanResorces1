using HumanResources_Application.Models.VMs.EmailVM;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Service.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(Message message);
        
        MailMessage CreateEmailMessage(Message message);
        
        void Send(MailMessage mailMessage);


    }
}
