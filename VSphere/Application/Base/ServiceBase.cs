using DinkToPdf.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using System.Text;
using VSphere.Utils;

namespace VSphere.Application.Base
{
    public abstract class ServiceBase
    {
        protected readonly RestClient _httpClient;
        protected readonly IConverter _converter;
        protected readonly IOptions<AppSettings> _appSetttings;

        public ServiceBase(IOptions<AppSettings> appSetttings, IConverter converter)
        {
            _httpClient = new RestClient();
            _appSetttings = appSetttings;
            _converter = converter;
        }

        protected string UserStringBase64(string userName, string password)
        {
            string authInfo = userName + ":" + password;
            return Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
        }

        protected string GetSession(string userName, string password)
        {
            var _restRequest = new RestRequest(Method.POST);
            _restRequest.Resource = "rest/com/vmware/cis/session";
            _restRequest.AddHeader("Authorization", "Basic " + UserStringBase64(userName, password));

            IRestResponse response = _httpClient.Execute(_restRequest);

            var temp = JObject.Parse(response.Content);

            return temp["value"].ToString();
        }

        protected void ClientCreate(string url)
        {
            _httpClient.BaseUrl = new Uri(url);
            _httpClient.Timeout = -1;
            _httpClient.CookieContainer = new CookieContainer();
            // Self Signed Cert ???
            _httpClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        protected void AddDefaultHeader(RestRequest restRequest, string resource, string username, string password)
        {
            restRequest.Resource = resource;

            restRequest.AddHeader("Authorization", "Basic " + UserStringBase64(username, password));
            restRequest.AddHeader("Content-Type", "application/json");
        }
    }
}
