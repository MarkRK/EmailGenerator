using EmailGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailGenerator.Controller
{
    public class SendMails
    {
        private EmailModel _emailModel;
        public SendMails(EmailModel emailModel)
        {
            _emailModel = emailModel;
        }

        public void Send()
        {
            var smtpClient = new SmtpClient(_emailModel.SMTPServer);
            for (int i = 0; i < _emailModel.NumberOfMails; i++)
            {

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailModel.EmailFrom),
                    Subject = string.Format(_emailModel.Subject,(i+1)),
                    Body = _emailModel.Message
                };
                mailMessage.To.Add(_emailModel.EmailTo);
                foreach (string filename in _emailModel.AttachmentFiles)
                {
                    mailMessage.Attachments.Add(new Attachment(filename));
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
