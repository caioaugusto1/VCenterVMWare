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

        public List<ServerViewModel> GetAll()
        {
            return _mapper.Map<List<ServerViewModel>>(_serverRepository.GetAll());
        }

        public ServerViewModel GetById(string id)
        {
            return _mapper.Map<ServerViewModel>(_serverRepository.GetById(id));
        }

        public void Insert(ServerViewModel obj)
        {
            var serverEntity = new ServerEntity(obj.IP, obj.UserName, obj.Password, obj.Description);

            _serverRepository.Insert(_mapper.Map<ServerEntity>(serverEntity));
        }
    }
}
