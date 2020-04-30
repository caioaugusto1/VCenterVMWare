using DinkToPdf;
using DinkToPdf.Contracts;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VSphere.Application.Base;
using VSphere.Models.JsonConvert;
using VSphere.Services.Inteface;

namespace VSphere.Services
{
    public class Service : ServiceBase, IService
    {
        private IConverter _converter;

        public Service(IConverter converter)
        {
            _converter = converter;
        }

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

        public byte[] PDFGenerator(string html)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF VM Report",
                Out = @"C:\Users\caiio\Desktop\PDFCreator\Employee_Report.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                //WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "adminlte.css") },
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return file;
        }

        public void SendEmail(string to, string filename = "", string extension = "")
        {
            using (MailMessage message = new MailMessage("", to))
            {
                message.Subject = "Email de teste com anexo";
                message.Body = "Esse e-mail foi enviado diretamente do novo sistema";
                message.IsBodyHtml = false;

                if (!string.IsNullOrWhiteSpace(filename) && !string.IsNullOrWhiteSpace(extension))
                {
                    message.Attachments.Add(new Attachment(@"Employee_Report.pdf"));
                    message.Attachments.Add(new Attachment(@"image.jpg"));
                }

                //message.Attachments.Add(new Attachment(String.Format("{0}.{1}", filename, extension)));

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.live.com";
                    smtp.EnableSsl = true;
                    NetworkCredential cred = new NetworkCredential("", "");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = cred;
                    smtp.Port = 587;
                    smtp.Send(message);
                }
            };

        }
    }
}