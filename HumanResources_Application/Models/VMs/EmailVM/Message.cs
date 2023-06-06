using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources_Application.Models.VMs.EmailVM
{
    public class Message
    {
        //public Message(IEnumerable<string> to, string subject, string content)
        //{
        //    To = new List<MailboxAddress>();
        //    To.AddRange(to.Select(x => new MailboxAddress("email", x)));
        //    Subject = subject;
        //    Content = content;

        //}

        //public List<MailboxAddress> To { get; set; }
        //public string Subject { get; set; }

        //public string Content { get; set; }


        public Message(string to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;

        }



        public string To { get; set; }
        public string Subject { get; set; }



        public string Content { get; set; }

    }
}
