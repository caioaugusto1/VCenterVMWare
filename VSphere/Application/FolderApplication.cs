using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models.ViewModels.Folder;
using VSphere.Services.Inteface;

namespace VSphere.Application
{
    public class FolderApplication : IFolderApplication
    {
        private readonly IMapper _mapper;
        private readonly IService _service;
        private readonly IServerApplication _serverApplication;

        public FolderApplication(IMapper mapper, IService service, IServerApplication serverApplication)
        {
            _mapper = mapper;
            _service = service;
            _serverApplication = serverApplication;
        }

        public async Task<List<FolderViewModel>> GetAllByApi(string apiId)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            var folders = await _service.GetFolderAPI("https://" + server.IP, server.UserName, server.Password);

            if (folders == null || folders.Value == null || !folders.Value.Any())
                return null;


            var foldersViewModel = new List<FolderViewModel>();
            folders.Value.ForEach(x =>
            {
                foldersViewModel.Add(new FolderViewModel()
                {
                    Folder = x.Folder,
                    Name = x.Name,
                    Type = x.Type
                }); ;
            });

            return foldersViewModel;

        }
    }
}
