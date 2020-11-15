using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VSphere.Application.Interface;
using VSphere.Models;

namespace VSphere.Controllers
{
    [Authorize(Roles = "Admin, Manager, Server")]
    public class ServerController : Controller
    {
        private readonly IServerApplication _serverApplication;

        public ServerController(IServerApplication serverApplication)
        {
            _serverApplication = serverApplication;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _serverApplication.GetAll());
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
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var server = await _serverApplication.GetById(id);

            return View(server);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ServerViewModel serverViewModel)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (serverViewModel.Id != id)
                return null;

            var result = await _serverApplication.Update(id, serverViewModel);

            return RedirectToAction("Index", "Server");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var server = await _serverApplication.GetById(id);

            return View(server);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSave(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var result = await _serverApplication.Delete(id);

            return RedirectToAction("Index", "Server");
        }
    }
}