using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailsForExam
{
    public static class Utility
    {

        public static void sendMail(string to, string[] filePaths, string subject, string body,string from,string server,int port,string password)
        {
            MimeKit.MimeMessage message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("email_from", from));
            message.To.Add(new MimeKit.MailboxAddress("email_to", to));
            message.Subject = subject;

            var builder = new MimeKit.BodyBuilder();

            builder.TextBody = body;

            if (filePaths != null)
                foreach (string item in filePaths)
                {
                    if (System.IO.File.Exists(item))
                    {
                        builder.Attachments.Add(item);
                    }
                }

            message.Body = builder.ToMessageBody();

            SmtpClient client = new SmtpClient();

            client.Connect(server, port);
            client.Authenticate(from, password);
            client.Send(message);
            client.Disconnect(true);
        }

    }
}
