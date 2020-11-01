using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VSphere.Application.Interface;

namespace VSphere.Controllers
{
    [Authorize(Roles = "Admin, Manager, Host")]
    public class HostController : Controller
    {
        private readonly IHostApplication _hostApplication;
        private readonly IServerApplication _serverApplication;

        public HostController(IHostApplication hostApplication, IServerApplication serverApplication)
        {
            _hostApplication = hostApplication;
            _serverApplication = serverApplication;
        }

        //[ClaimAuthorize("Host", "Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Servers = await _serverApplication.GetAll();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAPI(string apiId)
        {
            if (string.IsNullOrWhiteSpace(apiId))
                return Json(HttpStatusCode.Conflict);

            var dataFromAPI = await _hostApplication.GetAllByApi(apiId);

            return PartialView("~/Views/Host/_partial/_ListByAPI.cshtml", dataFromAPI);
        }
    }
}