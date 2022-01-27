using Blog.Core.Utilities;

namespace Blog.Business.Abstract
{
    public interface IMailService
    {
        void SendMail(MailDto mailDto);
    }
}
