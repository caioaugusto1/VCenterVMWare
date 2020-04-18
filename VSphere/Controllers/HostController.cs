using Microsoft.AspNetCore.Mvc;
using VSphere.Application.Interface;

namespace VSphere.Controllers
{
    public class HostController : Controller
    {
        private readonly IHostApplication _hostApplication;

        public HostController(IHostApplication hostApplication)
        {
            _hostApplication = hostApplication;
        }

        public IActionResult Index()
        {
            return View(_hostApplication.GetAll());
        }
    }
}