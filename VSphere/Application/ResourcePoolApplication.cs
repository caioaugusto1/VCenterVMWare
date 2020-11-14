using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models.ViewModels.ResourcePool;
using VSphere.Services.Inteface;

namespace VSphere.Application
{
    public class ResourcePoolApplication : IResourcePoolApplication
    {
        private readonly IMapper _mapper;
        private readonly IService _service;
        private readonly IServerApplication _serverApplication;

        public ResourcePoolApplication(IMapper mapper, IService service, IServerApplication serverApplication)
        {
            _mapper = mapper;
            _service = service;
            _serverApplication = serverApplication;
        }

        public async Task<List<ResourcePoolViewModel>> GetAllByApi(string apiId)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            var dataFromAPI = await _service.GetResourcePoolAPI("https://" + server.IP, server.UserName, server.Password);

            var vmResourcePool = new List<ResourcePoolViewModel>();
            dataFromAPI.Value.ForEach(x =>
            {
                vmResourcePool.Add(new ResourcePoolViewModel()
                {
                    Resource_Pool = x.ResourcePool,
                    Name = x.Name,
                });
            });

            return vmResourcePool;
        }
    }
}
