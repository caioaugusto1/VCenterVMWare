using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VSphere.Services.Inteface;

namespace VSphere.Services
{
    public class Service : IService
    {
        RestClient _httpClient;

        string BaseUrl = "https://10.100.11.37";

        string session;
        string username = "lletnar.adm";
        string password = "Service@123";

        public Service()
        {

        }

        public string UserStringBase64()
        {
            //Encode user and pwd base64
            string authInfo = username + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            return authInfo;
        }

        public void CreateClient()
        {
            _httpClient = new RestClient();

            _httpClient.BaseUrl = new Uri(BaseUrl);
            _httpClient.Timeout = -1;
            _httpClient.CookieContainer = new CookieContainer();
            // Self Signed Cert ???
            _httpClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public object GetAllAsync()
        {
            var _restRequest = new RestRequest(Method.GET);
            
            _restRequest.Resource = "rest/vcenter/vm";

            _restRequest.AddHeader("Authorization", "Basic " + UserStringBase64());
            _restRequest.AddHeader("Content-Type", "application/json");

            // In theory this crap should use the cookie container that has the session ID
            //request.AddHeader("Cookie", "vmware-api-session-id=" + session);
            IRestResponse response = _httpClient.Execute(_restRequest);

            return null;
        }

        public string GetSession()
        {
            var _restRequest = new RestRequest(Method.POST);
            _restRequest.Resource = "rest/com/vmware/cis/session";
            _restRequest.AddHeader("Authorization", "Basic " + UserStringBase64());

            IRestResponse response = _httpClient.Execute(_restRequest);

            var temp = JObject.Parse(response.Content);

            session = temp["value"].ToString();

            return session;
        }
    }
}
