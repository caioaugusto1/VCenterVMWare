using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VCenter.Services.Inteface;
using vmware.samples.vcenter.vm.list;

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
            var vms = new ListVMs();
            var testeeee = vms.Get("10.100.11.37", "lletnar.adm", "Service@123");

            return null;
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
