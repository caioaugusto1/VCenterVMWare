using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VCenter.Services.Inteface;
using vmware.samples.common;
using vmware.samples.common.authentication;
using vmware.samples.vcenter.vm.list;
using vmware.vapi.core;
using vmware.vapi.security;
using vmware.vcenter;

namespace VCenter.Services
{
    public class Service<Object> : SamplesBase, IService<Object>
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "192.168.100.102";

        VM vmService;

        public Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
            this.SkipServerVerification = true;
        }

        public Task<Object> CreateAsync(Object task)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Object>> GetAllAsync()
        {
          
            return null;


            ////var vms = new ListVMs();
            ////var testeeee = vms.Get("192.168.100.102", "administrator@vsphere.local", "W@ster123");
            //string username = "administrator@vsphere.local";
            //string password = "W@ster123";
            //ExecutionContext.SecurityContext securityContext =
            //   new UserPassSecurityContext(
            //       username, password.ToCharArray());

            ////_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("administrator@vsphere.local", "W@ster123");

            //var httpResponse = await _httpClient.GetAsync(BaseUrl);        

            //if (!httpResponse.IsSuccessStatusCode)
            //    throw new Exception("Cannot retrieve tasks");

            //var content = await httpResponse.Content.ReadAsStringAsync();

            //var tasks = JsonConvert.DeserializeObject<List<Object>>(content);

            //return tasks;

            //return null;
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

        public override async void Run()
        {
            string username = "administrator@vsphere.local";
            string password = "W@ster123";

            SetupSslTrustForServer();

            this.VapiAuthHelper = new VapiAuthenticationHelper();

            this.SessionStubConfiguration =
                await this.VapiAuthHelper.LoginByUsernameAndPasswordAsync(
                    BaseUrl, username, password);

            this.vmService =
                this.VapiAuthHelper.StubFactory.CreateStub<VM>(this.SessionStubConfiguration);

            List<VMTypes.Summary> vmList = await vmService.ListAsync(new VMTypes.FilterSpec());

        }

        public override void Cleanup()
        {
            throw new NotImplementedException();
        }
    }
}
