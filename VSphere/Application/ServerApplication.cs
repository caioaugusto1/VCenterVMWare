using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Entities;
using VSphere.Models;
using VSphere.Repositories.Interfaces;

namespace VSphere.Application
{
    public class ServerApplication : IServerApplication
    {
        private readonly IMapper _mapper;
        private readonly IServerRepository _serverRepository;

        public ServerApplication(IMapper mapper, IServerRepository serverRepository)
        {
            _mapper = mapper;
            _serverRepository = serverRepository;
        }

        public async Task<List<ServerViewModel>> GetAll()
        {
            return _mapper.Map<List<ServerViewModel>>(await _serverRepository.GetAll());
        }

        public async Task<ServerViewModel> GetById(string id)
        {
            return _mapper.Map<ServerViewModel>(await _serverRepository.GetById(id));
        }

        public void Insert(ServerViewModel obj)
        {
            var serverEntity = new ServerEntity(obj.IP, obj.UserName, obj.Password, obj.Description);

            _serverRepository.Insert(_mapper.Map<ServerEntity>(serverEntity));
        }
    }
}
