using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VCenter.Services.Inteface;

namespace VCenter.Services
{
    public class Service<Object> : IService<Object>
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";

        public Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<Object> CreateAsync(Object task)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Object>> GetAllAsync()
        {
            //VimClientImpl client = new VimClientImpl();

            //client.Connect("https://10.100.11.37/rest/vcenter/vm");
            //var userSession = client.Login("lletnar.adm", "Service@123");
            //List<VMware.Vim.EntityViewBase> vms = client.FindEntityViews(typeof(VMware.Vim.VirtualMachine),
            //    null, null, null);

            //var teste = vms;
            //return null;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var httpResponse = await _httpClient.GetAsync("https://10.100.11.37api/rest/vcenter/vm/");

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception("Cannot retrieve tasks");

            var content = await httpResponse.Content.ReadAsStringAsync();

            var tasks = JsonConvert.DeserializeObject<List<Object>>(content);

            return null;



            //var httpResponse = await _httpClient.GetAsync(BaseUrl);

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic ", "codigo");

            //if (!httpResponse.IsSuccessStatusCode)
            //    throw new Exception("Cannot retrieve tasks");

            //var content = await httpResponse.Content.ReadAsStringAsync();

            //var tasks = JsonConvert.DeserializeObject<List<Object>>(content);

            //return tasks;
        }

        public Task<Object> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Object> UpdateAsync(Object task)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
