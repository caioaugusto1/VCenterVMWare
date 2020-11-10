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
    public class DataStoreApplication : IDataStoreApplication
    {
        private readonly IMapper _mapper;
        private readonly IService _service;
        private readonly IDataStoreRepository _dataStoreRepository;
        private readonly IServerApplication _serverApplication;

        public DataStoreApplication(IMapper mapper, IService service, IDataStoreRepository dataStoreRepository, IServerApplication serverApplication)
        {
            _mapper = mapper;
            _dataStoreRepository = dataStoreRepository;
            _service = service;
            _serverApplication = serverApplication;
        }

        public async Task<List<DataStoreViewModel>> GetAll(string apiId, string from, string to)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            DateTime dateFrom = Convert.ToDateTime(from);
            DateTime dateTo = Convert.ToDateTime(to);
            dateFrom = dateFrom.AddMinutes(-1);
            dateTo = dateTo.AddMinutes(1);

            return _mapper.Map<List<DataStoreViewModel>>(await _dataStoreRepository.GetAll());

            //return _mapper.Map<List<DataStoreViewModel>>(_dataStoreRepository.GetByOrigem(server.IP, from, to));
        }

        public async Task<List<DataStoreViewModel>> GetAllByApi(string apiId)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            var dataFromAPI = await _service.GetDataStoreAPI("https://" + server.IP, server.UserName, server.Password);

            if (dataFromAPI == null || dataFromAPI.Value == null)
                return null;

            var entityList = new List<DataStoreEntity>();
            dataFromAPI.Value.ForEach(x =>
            {
                entityList.Add(new DataStoreEntity(x.DataStore, x.Name, x.Type, x.FreeSpace, server.IP, x.Capacity));
            });

            var insertManyResult = _dataStoreRepository.InsertMany(entityList);

            var dataStoreVM = new List<DataStoreViewModel>();
            dataFromAPI.Value.ForEach(x =>
            {
                dataStoreVM.Add(new DataStoreViewModel
                {
                    Datastore = x.DataStore,
                    Name = x.Name,
                    Type = x.Type,
                    FreeSpace = x.FreeSpace,
                    Origem = server.IP,
                    Capacity = x.Capacity
                });
            });

            return dataStoreVM;
        }
    }
}
