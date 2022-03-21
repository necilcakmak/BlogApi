using Blog.Core.Utilities;
using Blog.Entities.Entities;

namespace Blog.Business.Abstract
{
    public interface IMailService
    {
        void SendMail(MailDto mailDto, Guid id);
    }
}
