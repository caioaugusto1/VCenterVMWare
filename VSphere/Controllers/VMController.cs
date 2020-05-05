using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using VSphere.Application.Interface;

namespace VSphere.Controllers
{
    [Authorize(Roles = "Admin, Manager, VM")]
    public class VMController : Controller
    {
        private readonly IVMApplication _vmApplication;
        private readonly IServerApplication _serverApplication;

        public VMController(IVMApplication vmApplication, IServerApplication serverApplication)
        {
            _vmApplication = vmApplication;
            _serverApplication = serverApplication;
        }

        public IActionResult Index()
        {
            ViewBag.Servers = _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByFilterHistory(string apiId, string datetimeFrom, string datetimeTo)
        {
            if (string.IsNullOrWhiteSpace(apiId) || string.IsNullOrWhiteSpace(datetimeFrom) || string.IsNullOrWhiteSpace(datetimeTo))
                return Json(HttpStatusCode.Conflict);

            return PartialView("~/Views/VM/_partial/_List.cshtml", await _vmApplication.GetAllByDate(apiId, datetimeFrom, datetimeTo));
        }

        [HttpGet]
        public IActionResult AllByAPI()
        {
            ViewBag.Servers = _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAPI(string apiId)
        {
            if (string.IsNullOrWhiteSpace(apiId))
                return Json(HttpStatusCode.Conflict);

            var dataFromAPI = await _vmApplication.GetAllByApi(apiId);

            return Json(new { data = dataFromAPI, statusCode = HttpStatusCode.OK });

            //return PartialView("~/Views/VM/_partial/_ListByAPI.cshtml", dataFromAPI);
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