using Microsoft.Extensions.Configuration;
using System;
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
                using (SmtpClient client = new SmtpClient(_host))
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_from, _alias);
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.To.Add(emailModel.To);
                    mailMessage.Body = emailModel.Message;
                    mailMessage.Subject = emailModel.Subject;
                    mailMessage.IsBodyHtml = emailModel.IsBodyHtml;

                    client.Port = 587;
                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_userName, _password);
                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        public class EmailModel
        {
            public EmailModel(string to, string subject, string message, bool isBodyHtml)
            {
                To = to;
                Subject = subject;
                Message = message;
                IsBodyHtml = isBodyHtml;
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
        }
    }
}
