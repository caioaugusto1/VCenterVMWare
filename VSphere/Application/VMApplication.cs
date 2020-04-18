using AutoMapper;
using System;
using System.Collections.Generic;
using VSphere.Application.Interface;
using VSphere.Entities;
using VSphere.Models;
using VSphere.Repositories.Interfaces;

namespace VSphere.Application
{
    public class VMApplication : IVMApplication
    {
        private readonly IMapper _mapper;
        private readonly IVMRepository _vmRepository;

        public VMApplication(IMapper mapper, IVMRepository vmRepository)
        {
            _mapper = mapper;
            _vmRepository = vmRepository;
        }

        public List<VMViewModel> GetAll()
        {
            return _mapper.Map<List<VMEntity>, List<VMViewModel>>(_vmRepository.GetAll());
        }

        public List<VMViewModel> GetAllByDate(DateTime from, DateTime to)
        {
            return _mapper.Map<List<VMViewModel>>(_vmRepository.GetAll());
        }
    }
}
