using AutoMapper;
using Microsoft.AspNet.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using VSphere.Application.Interface;
using VSphere.Entities;
using VSphere.Models;
using VSphere.Models.JsonConvert;
using VSphere.Models.ViewModels.VM;
using VSphere.Repositories.Interfaces;
using VSphere.Services;
using VSphere.Services.Inteface;
using VSphere.Utils;

namespace VSphere.Application
{
    public class VMApplication : IVMApplication
    {
        private readonly IMapper _mapper;
        private readonly IVMRepository _vmRepository;
        private readonly IService _service;
        private readonly IServerApplication _serverApplication;
        private readonly RequestHandler _requestHandler;
        private readonly EmailHelper _emailHelper;

        public VMApplication(IMapper mapper, IVMRepository vmRepository, IService service, IServerApplication serverApplication, RequestHandler requestHandler, EmailHelper emailHelper)
        {
            _mapper = mapper;
            _vmRepository = vmRepository;
            _service = service;
            _serverApplication = serverApplication;
            _requestHandler = requestHandler;
            _emailHelper = emailHelper;
        }

        public async Task<HttpStatusCode> Delete(string apiId, string name)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return HttpStatusCode.NotFound;

            var deleteResult = await _service.DeleteVMAPI("https://" + server.IP, server.UserName, server.Password, name);

            return deleteResult;
        }

        public async Task<List<VMViewModel>> GetAllByApi(string apiId)
        {
            var server = await _serverApplication.GetById(apiId);

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
                    VM = x.VM,
                    GetOnlinesVM = true
                });
            });

            return VMView;
        }

        public async Task<List<VMViewModel>> GetAllByDate(string apiId, string from, string to)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return null;

            var dateFrom = Convert.ToDateTime(DateTime.Parse(from).AddHours(00).AddMinutes(00).AddSeconds(00));
            var dateTo = Convert.ToDateTime(DateTime.Parse(to).AddHours(23).AddMinutes(59).AddSeconds(59));

            var vmsViewModel = await _vmRepository.GetByDate(server.IP, dateFrom, dateTo);

            var vmsVM = _mapper.Map<List<VMViewModel>>(vmsViewModel);

            return vmsVM;
        }

        public async Task<HttpStatusCode> TurnOnOrTurnOff(string apiId, string name, bool turnOn)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return HttpStatusCode.NotFound;

            var deleteResult = await _service.TurnOnOrTurnOffVMAPI("https://" + server.IP, server.UserName, server.Password, name, turnOn);

            return deleteResult;
        }

        public byte[] PDFGenerator(string html, string userEmail)
        {
            var fileName = _service.PDFGenerator(html);

            var body = "<p> Olá, Tudo bem? </p><p> Como solicitado, segue relatório em anexo</p><p> Obrigado, Att </p>";

            userEmail = "caiio_augustto@hotmail.com";

            //List<Attachment> attachment = new List<Attachment>();
            //attachment.Add(new Attachment(fileName, ))

            SendEmail.Send(_requestHandler, _emailHelper, userEmail, "Relatório", body, null);

            return _service.GetFile(fileName);
        }

        public async Task<HttpStatusCode> Create(string apiId, CreateVMViewModel model)
        {
            var server = await _serverApplication.GetById(apiId);

            if (server == null)
                return HttpStatusCode.NotFound;

            var vmPostModel = new VMPost();
            vmPostModel.Spec.Name = model.Name;
            vmPostModel.Spec.Guest_OS = model.OS;
            vmPostModel.Spec.Placement.DataStore = model.DataStore;
            vmPostModel.Spec.Placement.Folder = model.Folder;
            vmPostModel.Spec.Placement.Resource_Pool = model.ResourcePool;
            vmPostModel.Spec.Memory.Size = model.Memory;

            vmPostModel.Spec.CDROMS.Add(new VMPost.SpecConverter.CdRoomsConverter()
            {
                Type = "IDE"
            });

            vmPostModel.Spec.DISKS.Add(new VMPost.SpecConverter.DisksConverter()
            {
                Type = "IDE"
            });

            var createResult = await _service.CreateVM("https://" + server.IP, server.UserName, server.Password, vmPostModel);

            return createResult;
        }
    }
}
