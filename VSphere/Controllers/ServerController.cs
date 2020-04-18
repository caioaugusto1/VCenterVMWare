using Microsoft.AspNetCore.Mvc;
using VSphere.Application.Interface;
using VSphere.Models;

namespace VSphere.Controllers
{
    public class ServerController : Controller
    {
        private readonly IServerApplication _serverApplication;

        public ServerController(IServerApplication serverApplication)
        {
            _serverApplication = serverApplication;
        }

        public IActionResult Index()
        {
            return View(_serverApplication.GetAll());
        }

        [HttpGet]
        public IActionResult Create(ServerViewModel server)
        {
            if (ModelState.IsValid)
            {
                
            }
            else
            {

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string id)
        {
            return View();
        }
    }
}