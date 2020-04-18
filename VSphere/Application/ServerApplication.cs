using AutoMapper;
using System.Collections.Generic;
using VSphere.Application.Interface;
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
    }
}
