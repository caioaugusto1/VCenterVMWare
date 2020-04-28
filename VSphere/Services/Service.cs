using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using VSphere.Application.Base;
using VSphere.Models.JsonConvert;
using VSphere.Services.Inteface;

namespace VSphere.Services
{
    public class Service : ServiceBase, IService
    {
        public async Task<DataStoreConvert> GetDataStoreAPI(string url, string username, string password)
        {
            ClientCreate(url);
            GetSession(username, password);

            var _restRequest = new RestRequest(Method.GET);

            AddDefaultHeader(_restRequest, "rest/vcenter/datastore", username, password);
            IRestResponse response = _httpClient.Execute(_restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<DataStoreConvert>(response.Content);
            else
                return null;
        }

        public async Task<HostConvert> GetHostsAPI(string url, string username, string password)
        {
            ClientCreate(url);
            GetSession(username, password);

            var _restRequest = new RestRequest(Method.GET);

            AddDefaultHeader(_restRequest, "rest/vcenter/vm", username, password);

            // In theory this crap should use the cookie container that has the session ID
            //request.AddHeader("Cookie", "vmware-api-session-id=" + session);
            IRestResponse response = _httpClient.Execute(_restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<HostConvert>(response.Content);
            else
                return null;
        }

        public async Task<VMConvert> GetVMsAPI(string url, string username, string password)
        {
            ClientCreate(url);
            GetSession(username, password);

            var _restRequest = new RestRequest(Method.GET);

            AddDefaultHeader(_restRequest, "rest/vcenter/vm", username, password);
            IRestResponse response = _httpClient.Execute(_restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<VMConvert>(response.Content);
            else
                return null;
        }

        public bool SendEmail()
        {
            using (MailMessage message = new MailMessage("From", "To"))
            {
                message.Subject = "Email de teste com anexo";
                message.Body = "Esse e-mail foi enviado diretamente do novo sistema";
                message.IsBodyHtml = false;
                message.Attachments.Add(new Attachment(@"filename.pdf"));

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.live.com";
                    smtp.EnableSsl = true;
                    NetworkCredential cred = new NetworkCredential("email", "password");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = cred;
                    smtp.Port = 587;
                    smtp.Send(message);
                }
            };

            return true;
        }
    }
}