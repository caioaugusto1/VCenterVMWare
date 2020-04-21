using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VSphere.Application.Interface;
using VSphere.Models;

namespace VSphere.Controllers
{
    //[Authorize]
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServerViewModel serverVM)
        {
            if (ModelState.IsValid)
            {
                _serverApplication.Insert(serverVM);
                return RedirectToAction("Index", "Server");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return View(_serverApplication.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(string id, ServerViewModel serverViewModel)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (serverViewModel.Id != id)
                return null;



            return View();
        }
    }
}