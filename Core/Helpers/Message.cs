using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public TextFormat EmailFormat { get; set; }


        public Message(IEnumerable<string> to, string subject, string content ,TextFormat emailFormat = TextFormat.Html)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(_ => new MailboxAddress(_)));

            Subject = subject;
            Content = content;
            EmailFormat = emailFormat;            
        }
    }
}
