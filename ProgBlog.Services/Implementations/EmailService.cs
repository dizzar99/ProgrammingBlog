using MailKit.Net.Smtp;
using MimeKit;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;

namespace ProgBlog.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        private const string From = "changing.passchange@yandex.ru";
        private const string SmtpServer = "smtp.yandex.ru";
        private const int Port = 25;
        private const string Password = "DimaZarembo2909";
        public void SendMail(Message message)
        {
            var emailMessage = this.CreateEmailMessage(message);

            this.Send(emailMessage);
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
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
