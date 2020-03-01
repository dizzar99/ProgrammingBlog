using ProgBlog.Services.Models.IdentityManagment;

namespace ProgBlog.Services.Interfaces
{
    public interface IEmailSender
    {
        void SendMail(Message message);
    }
}
