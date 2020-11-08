using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VSphere.Application.Base;
using VSphere.Models.JsonConvert;
using VSphere.Services.Inteface;
using VSphere.Utils;

namespace VSphere.Services
{
    public class Service : ServiceBase, IService
    {
        private readonly RequestHandler _requestHandler;
        private readonly EmailHelper _emailHelper;

        public Service(IOptions<AppSettings> appSetttings, IConverter converter, RequestHandler requestHandler, EmailHelper emailHelper)
            : base(appSetttings, converter)
        {
            _requestHandler = requestHandler;
            _emailHelper = emailHelper;
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

            AddDefaultHeader(_restRequest, "rest/vcenter/host", username, password);

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

        public async Task<HttpStatusCode> DeleteVMAPI(string url, string username, string password, string name)
        {
            ClientCreate(url);
            GetSession(username, password);

            var _restRequest = new RestRequest(Method.DELETE);

            AddDefaultHeader(_restRequest, string.Format("{0}{1}", "rest/vcenter/vm/", name), username, password);
            IRestResponse response = _httpClient.Execute(_restRequest);

            return response.StatusCode;
        }

        public string PDFGenerator(string html)
        {
            var documentName = String.Format("{0}_{1}.pdf", "PDF_VM_Report", Guid.NewGuid());

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = String.Format(@"{0}\\{1}", _appSetttings.Value.OutPDFSave, documentName)
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

            _converter.Convert(pdf);

            return documentName;
        }

        public byte[] GetFile(string fileName)
        {
            return File.ReadAllBytes(_appSetttings.Value.OutPDFSave + fileName);
        }


    }
}