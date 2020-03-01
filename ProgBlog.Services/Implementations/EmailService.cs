using MailKit.Net.Smtp;
using MimeKit;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        private const string From = "dizarr99@yandex.ru";
        private const string SmtpServer = "smtp.yandex.ru";
        private const int Port = 25;
        private const string Password = "DimaZarembo2909";
        public void SendMail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("From", From));
            emailMessage.To.Add(new MailboxAddress("To", message.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(SmtpServer, Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(From, Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
