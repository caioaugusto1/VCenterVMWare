using Microsoft.AspNetCore.Mvc;
using System.Net;
using VSphere.Application.Interface;

namespace VSphere.Controllers
{
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
        public IActionResult GetAllByFilter(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return Json(HttpStatusCode.Conflict);
 
            return PartialView("~/Views/VM/_partial/_List.cshtml", _vmApplication.GetAll());
        }
    }
}