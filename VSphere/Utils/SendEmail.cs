using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VSphere.Services;

namespace VSphere.Utils
{
    public static class SendEmail
    {
        public static bool Send(RequestHandler _requestHandler, EmailHelper _emailHelper, string email, string subject, string emailbody)
        {
            var host = _requestHandler._httpContextAccessor.HttpContext.Request.Host;
            var scheme = _requestHandler._httpContextAccessor.HttpContext.Request.Scheme;

            Thread sendEmailThread = new Thread(items => _emailHelper.SendEmail(new EmailHelper.EmailModel(email, subject, emailbody, true)));
            sendEmailThread.Start();

            return true;
        }
    }
}
