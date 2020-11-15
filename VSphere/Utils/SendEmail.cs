using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using VSphere.Services;

namespace VSphere.Utils
{
    public static class SendEmail
    {
        public static bool Send(RequestHandler _requestHandler, EmailHelper _emailHelper, string email, string subject, string emailbody, AttachmentCollection attachments = null)
        {
            Thread sendEmailThread = new Thread(items => _emailHelper.SendEmail(new EmailHelper.EmailModel(email, subject, emailbody, true, attachments)));
            sendEmailThread.Start();

            return true;
        }
    }
}
