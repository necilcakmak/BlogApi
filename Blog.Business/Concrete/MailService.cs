using Blog.Business.Abstract;
using Blog.Core.Utilities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Blog.Business.Concrete
{
    public class MailService : IMailService
    {
        private readonly MailOptions _mailSettings;
        public MailService(IOptions<MailOptions> options)
        {
            _mailSettings = options.Value;
        }
        public void SendMail(MailDto mailDto, Guid id)
        {
            string uiUrl = $"{ _mailSettings.ApiUrl }auth/accountconfirm/{id}";
            MailMessage msg = new(); //Mesaj gövdesini tanımlıyoruz...
            msg.Subject = "E-Mail Onay";
            msg.From = new MailAddress("necilblog@necil.com", "Necil Blog");
            msg.To.Add(new MailAddress(mailDto.From, mailDto.FromName));

            //Mesaj içeriğinde HTML karakterler yer alıyor ise aşağıdaki alan TRUE olarak gönderilmeli ki HTML olarak yorumlansın. Yoksa düz yazı olarak gönderilir...
            msg.IsBodyHtml = true;
            msg.Body = "Üyeliğinizi aktif hale getirmek için aşağıdaki Linke tıklayınız." +
                    "Onay İçin <a href=" + uiUrl + ">TIKLAYINIZ.</a>";

            //Mesaj önceliği
            msg.Priority = MailPriority.Normal;

            SmtpClient smtp = new(_mailSettings.Host, _mailSettings.Port);
            NetworkCredential AccountInfo = new(_mailSettings.UserName, _mailSettings.Password);
            smtp.UseDefaultCredentials = false; //Standart doğrulama kullanılsın mı? Yalnızca gönderici özellikle istiyor ise TRUE işaretlenir.
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = _mailSettings.EnableSsl; //SSL kullanılarak mı gönderilsin...
            smtp.SendMailAsync(msg);
        }
    }

}
