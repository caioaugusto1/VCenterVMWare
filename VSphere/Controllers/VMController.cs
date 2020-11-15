using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models.Identity;
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
        private readonly IResourcePoolApplication _resourcePoolApplication;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public VMController(IVMApplication vmApplication, IServerApplication serverApplication, IDataStoreApplication dataStoreApplication, IFolderApplication folderApplication, IResourcePoolApplication resourcePoolApplication, UserManager<ApplicationIdentityUser> userManager)
        {
            _vmApplication = vmApplication;
            _serverApplication = serverApplication;
            _dataStoreApplication = dataStoreApplication;
            _folderApplication = folderApplication;
            _resourcePoolApplication = resourcePoolApplication;
            _userManager = userManager;
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
            ViewBag.ResoucerPool = await _resourcePoolApplication.GetAllByApi(apiId);
            ViewBag.OS = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text= "Windows Server 2012 R2",
                    Value = "RHEL_7_64"
                },
                new SelectListItem
                {
                    Text= "Linux - FEDORA 7",
                    Value = "RHEL_7_64"
                }
            });

            ViewBag.Memory = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text= "4 GB",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text= "8 GB",
                    Value = "8"
                },
                new SelectListItem
                {
                    Text= "16 GB",
                    Value = "16"
                }
            });

            ViewBag.CPU = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text= "1 - CPU",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text= "2 - CPU",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text= "4 - CPU",
                    Value = "4"
                }
            });

            var networkingSelectList = new List<SelectListItem>();

            networkingSelectList.Add(new SelectListItem()
            {
                Text = "VM - Networking",
                Value = "1"
            });

            ViewBag.Networking = new SelectList(networkingSelectList, "Value", "Text");

            CreateVMViewModel model = new CreateVMViewModel();
            model.ApiId = apiId;

            return PartialView("~/Views/VM/_partial/_Create.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSave(CreateVMViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var createResult = await _vmApplication.Create(model.ApiId, model);

            if (createResult == HttpStatusCode.OK)
            {
                return RedirectToAction("AllByAPI", "VM");
            }

            return View();
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

            var user = await _userManager.GetUserAsync(User);

            var file = _vmApplication.PDFGenerator(html, user.Email);

            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet);
        }
    }
}