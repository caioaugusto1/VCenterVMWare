using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace VSphere.Services
{
    public class EmailHelper
    {
        private string _host;
        private string _userName;
        private string _password;
        private string _alias;
        private string _from;

        public EmailHelper(IConfiguration iConfiguration)
        {
            var smtpSection = iConfiguration.GetSection("SMTP");
            if (smtpSection != null)
            {
                _host = smtpSection.GetSection("Host").Value;
                _from = smtpSection.GetSection("From").Value;
                _alias = smtpSection.GetSection("Alias").Value;
                _userName = smtpSection.GetSection("UserName").Value;
                _password = smtpSection.GetSection("Password").Value;
            }
        }

        public void SendEmail(EmailModel emailModel)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient(_host))
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_from, _alias);
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.To.Add(emailModel.To);
                    mailMessage.Body = emailModel.Message;
                    mailMessage.Subject = emailModel.Subject;
                    mailMessage.IsBodyHtml = emailModel.IsBodyHtml;

                    if (emailModel.Attachments != null)
                    {
                        foreach (var attach in emailModel.Attachments)
                        {
                            mailMessage.Attachments.Add(attach);
                        }
                    }

                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(_userName, _password);
                    smtp.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        public class EmailModel
        {
            public EmailModel(string to, string subject, string message, bool isBodyHtml, List<Attachment> attachments)
            {
                To = to;
                Subject = subject;
                Message = message;
                IsBodyHtml = isBodyHtml;
                Attachments = attachments;
            }

            public string To
            {
                get;
            }
            public string Subject
            {
                get;
            }
            public string Message
            {
                get;
            }
            public bool IsBodyHtml
            {
                get;
            }

            public List<Attachment> Attachments
            {
                get;
            }
        }
    }
}
