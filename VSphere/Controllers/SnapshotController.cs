using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models;

namespace VSphere.Controllers
{
    public class SnapshotController : Controller
    {
        private readonly IServerApplication _serverApplication;

        public SnapshotController(IServerApplication serverApplication)
        {
            _serverApplication = serverApplication;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Servers = await _serverApplication.GetAll();


            return View();
        }

        public async Task<IActionResult> List(string apiId)
        {
            var all = new SnapshotViewModel().GetAll();
            
            return PartialView("~/Views/Snapshot/_partial/_List.cshtml", all);
        }
    }
}
