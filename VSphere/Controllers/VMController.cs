using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models.ViewModels.VM;

namespace VSphere.Controllers
{
    [Authorize(Roles = "Admin, Manager, VM")]
    public class VMController : Controller
    {
        private readonly IVMApplication _vmApplication;
        private readonly IServerApplication _serverApplication;
        private readonly IDataStoreApplication _dataStoreApplication;
        private readonly IFolderApplication _folderApplication;

        public VMController(IVMApplication vmApplication, IServerApplication serverApplication, IDataStoreApplication dataStoreApplication, IFolderApplication folderApplication)
        {
            _vmApplication = vmApplication;
            _serverApplication = serverApplication;
            _dataStoreApplication = dataStoreApplication;
            _folderApplication = folderApplication;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Servers = await _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> IndexCreate()
        {
            ViewBag.Servers = await _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(string apiId)
        {
            ViewBag.Datastores = await _dataStoreApplication.GetAllByApi(apiId);
            ViewBag.Folder = await _folderApplication.GetAllByApi(apiId);
            ViewBag.ResoucerPool = new SelectList("Id", "Name", "1234");

            return PartialView("~/Views/VM/_partial/_Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSave(CreateVMViewModel model)
        {
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByFilterHistory(string apiId, string datetimeFrom, string datetimeTo)
        {
            if (string.IsNullOrWhiteSpace(apiId) || string.IsNullOrWhiteSpace(datetimeFrom) || string.IsNullOrWhiteSpace(datetimeTo))
                return Json(HttpStatusCode.Conflict);

            var vmsViewModel = await _vmApplication.GetAllByDate(apiId, datetimeFrom, datetimeTo);

            return PartialView("~/Views/VM/_partial/_List.cshtml", vmsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AllByAPI()
        {
            ViewBag.Servers = await _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAPI(string apiId)
        {
            if (string.IsNullOrWhiteSpace(apiId))
                return Json(HttpStatusCode.Conflict);

            var dataFromAPI = await _vmApplication.GetAllByApi(apiId);

            return Json(new { data = dataFromAPI, statusCode = HttpStatusCode.OK });
        }

        [HttpPost]
        public async Task<JsonResult> TurnONorTurnOff(string apiId, string name, bool turnOn)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(apiId))
                return Json(new { statusCode = HttpStatusCode.BadRequest });

            var statusCodeResult = await _vmApplication.TurnOnOrTurnOff(apiId, name, turnOn);

            return Json(new { statusCode = statusCodeResult });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(string apiId, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(apiId))
                return Json(new { statusCode = HttpStatusCode.BadRequest });

            var deleteResult = await _vmApplication.Delete(apiId, name);

            return Json(new { statusCode = deleteResult });
        }

        [HttpPost]
        public async Task<IActionResult> PDFGenerator(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return Json(Conflict());

            var file = _vmApplication.PDFGenerator(html);

            return Json(Ok());

            //return File(file, System.Net.Mime.MediaTypeNames.Application.Octet);
        }


    }
}