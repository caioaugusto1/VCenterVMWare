using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Entities;
using VSphere.Models;
using VSphere.Models.JsonConvert;
using VSphere.Repositories.Interfaces;
using VSphere.Services.Inteface;

namespace VSphere.Application
{
    public class VMApplication : IVMApplication
    {
        private readonly IMapper _mapper;
        private readonly IVMRepository _vmRepository;
        private readonly IService _service;
        private readonly IServerApplication _serverApplication;

        public VMApplication(IMapper mapper, IVMRepository vmRepository, IService service, IServerApplication serverApplication)
        {
            _mapper = mapper;
            _vmRepository = vmRepository;
            _service = service;
            _serverApplication = serverApplication;
        }

        public async Task<List<VMViewModel>> GetAllByApi(string apiId)
        {
            var server = _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            var dataFromAPI = await _service.GetVMsAPI("https://" + server.IP, server.UserName, server.Password);

            if (dataFromAPI == null || dataFromAPI.Value == null)
                return null;

            var entityList = new List<VMEntity>();
            dataFromAPI.Value.ForEach(x =>
            {
                entityList.Add(new VMEntity(x.Memory, x.VM, x.Name, x.Power, x.CPU, server.IP));
            });

            var insertManyResult = _vmRepository.InsertMany(entityList);

            var VMView = new List<VMViewModel>();
            dataFromAPI.Value.ForEach(x =>
            {
                VMView.Add(new VMViewModel()
                {
                    Memory = x.Memory,
                    CPU = x.CPU,
                    Power = x.Power,
                    Name = x.Name,
                    VM = x.VM
                });
            });

            return VMView;
        }

        public async Task<List<VMViewModel>> GetAllByDate(string apiId, string from, string to)
        {
            var server = _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            DateTime dateFrom = Convert.ToDateTime(from);
            DateTime dateTo = Convert.ToDateTime(to);
            dateFrom = dateFrom.AddMinutes(-1);
            dateTo = dateTo.AddMinutes(1);

            var vmsViewModel = await _vmRepository.GetByDate(server.IP, dateFrom, dateTo);

            return _mapper.Map<List<VMViewModel>>(vmsViewModel);
        }
    }
}
