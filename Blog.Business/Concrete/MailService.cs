using Blog.Business.Abstract;
using Blog.Core.Utilities;
using Blog.Entities.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Blog.Business.Concrete
{
    public class MailService(IOptions<MailOptions> options) : IMailService
    {
        private readonly MailOptions _mailSettings = options.Value;

        public void SendMail(MailDto mailDto)
        {
            MailMessage msg = new()
            {
                Subject = mailDto.Subject,
                From = new MailAddress(mailDto.From, mailDto.FromName),
            }; 
            msg.To.Add(new MailAddress(mailDto.To, mailDto.ToName));
            msg.IsBodyHtml = true;
            msg.Body = mailDto.Body;

            msg.Priority = MailPriority.Normal;

            SmtpClient smtp = new(_mailSettings.Host, _mailSettings.Port);
            NetworkCredential AccountInfo = new(_mailSettings.UserName, _mailSettings.Password);
            smtp.UseDefaultCredentials = false; 
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = _mailSettings.EnableSsl;
            smtp.SendMailAsync(msg);
        }
        public void SenConfirmationMail(MailDto mailDto, Guid id)
        {
            string uiUrl = $"{_mailSettings.ApiUrl}Account/{id}";
            string body = "Üyeliğinizi aktif hale getirmek için aşağıdaki Linke tıklayınız.<br>" +
                    "Onay İçin <a href=" + uiUrl + ">TIKLAYINIZ.</a>";
            mailDto.Body = body;
            SendMail(mailDto);
        }
    }

}
