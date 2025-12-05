using Blog.Core.Utilities;
using Blog.Entities.Entities;

namespace Blog.Business.Abstract
{
    public interface IMailService
    {
        void SenConfirmationMail(MailDto mailDto, Guid id);
        void SendMail(MailDto mailDto);

    }
}
