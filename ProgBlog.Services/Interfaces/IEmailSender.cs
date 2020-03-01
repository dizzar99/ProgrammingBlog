using ProgBlog.Services.Models.IdentityManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Interfaces
{
    public interface IEmailSender
    {
        void SendMail(Message message);
    }
}
