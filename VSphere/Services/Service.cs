using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VSphere.Application.Base;
using VSphere.Models;
using VSphere.Models.JsonConvert;
using VSphere.Services.Inteface;

namespace VSphere.Services
{
    public class Service : ServiceBase, IService
    {
        public async Task<VMConvert> GetVMsAPI(string url, string username, string password)
        {
            ClientCreate(url);
            GetSession(username, password);

            var _restRequest = new RestRequest(Method.GET);

            _restRequest.Resource = "rest/vcenter/vm";

            _restRequest.AddHeader("Authorization", "Basic " + UserStringBase64(username, password));
            _restRequest.AddHeader("Content-Type", "application/json");

            // In theory this crap should use the cookie container that has the session ID
            //request.AddHeader("Cookie", "vmware-api-session-id=" + session);
            IRestResponse response = _httpClient.Execute(_restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<VMConvert>(response.Content);
            else
                return null;
        }
    }
}
