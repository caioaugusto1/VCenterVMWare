using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models;
using VSphere.Repositories.Interfaces;
using VSphere.Services.Inteface;

namespace VSphere.Application
{
    public class HostApplication : IHostApplication
    {
        private readonly IMapper _mapper;
        private readonly IService _service;
        private readonly IHostRepository _hostRepository;
        private readonly IServerApplication _serverApplication;

        public HostApplication(IMapper mapper, IHostRepository hostRepository, IService service, IServerApplication serverApplication)
        {
            _mapper = mapper;
            _service = service;
            _hostRepository = hostRepository;
            _serverApplication = serverApplication;
        }


        public List<HostViewModel> GetAll(string apiId)
        {
            var server = _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            return _mapper.Map<List<HostViewModel>>(_hostRepository.GetAll());
        }

        public async Task<List<HostViewModel>> GetAllByApi(string apiId)
        {
            var server = _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            var dataFromAPI = await _service.GetVMsAPI("https://" + server.IP, server.UserName, server.Password);

            return null;
        }
    }
}
