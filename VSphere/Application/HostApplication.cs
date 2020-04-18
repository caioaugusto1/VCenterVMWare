using AutoMapper;
using System;
using System.Collections.Generic;
using VSphere.Application.Interface;
using VSphere.Models;
using VSphere.Repositories.Interfaces;

namespace VSphere.Application
{
    public class HostApplication : IHostApplication
    {
        private readonly IMapper _mapper;
        private readonly IHostRepository _hostRepository;

        public HostApplication(IMapper mapper, IHostRepository hostRepository)
        {
            _mapper = mapper;
            _hostRepository = hostRepository;
        }


        public List<HostViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
